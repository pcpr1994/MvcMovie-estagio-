using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMovie.EntityFrameworkCore;
using MvcMovie.Localization;
using MvcMovie.MultiTenancy;
using MvcMovie.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcMovie.Permissions;
using Volo.Abp.Data;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Web;

[DependsOn(
    typeof(MvcMovieHttpApiModule),
    typeof(MvcMovieApplicationModule),
    typeof(MvcMovieEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpDataModule)

    )]
public class MvcMovieWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MvcMovieResource),
                typeof(MvcMovieDomainModule).Assembly,
                typeof(MvcMovieDomainSharedModule).Assembly,
                typeof(MvcMovieApplicationModule).Assembly,
                typeof(MvcMovieApplicationContractsModule).Assembly,
                typeof(MvcMovieWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("MvcMovie");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context/*, IServiceCollection services*/)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();


        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/Movies/Index", MvcMoviePermissions.Movies.Default);
            options.Conventions.AuthorizePage("/Movies/CreateModal", MvcMoviePermissions.Movies.Create);
            options.Conventions.AuthorizePage("/Movies/CreateIMDBModal", MvcMoviePermissions.Movies.Create);
            options.Conventions.AuthorizePage("/Movies/DetailModal", MvcMoviePermissions.Movies.Detail);
            options.Conventions.AuthorizePage("/Movies/EditModal", MvcMoviePermissions.Movies.Edit);

            options.Conventions.AuthorizePage("/Genres/Index", MvcMoviePermissions.Genres.Default);
            options.Conventions.AuthorizePage("/Genres/CreateModal", MvcMoviePermissions.Genres.Create);
            options.Conventions.AuthorizePage("/Genres/EditModal", MvcMoviePermissions.Genres.Edit);

            options.Conventions.AuthorizePage("/Authors/Index", MvcMoviePermissions.Authors.Default);
            options.Conventions.AuthorizePage("/Authors/CreateModal", MvcMoviePermissions.Authors.Create);
            options.Conventions.AuthorizePage("/Authors/EditModal", MvcMoviePermissions.Authors.Edit);
        });

        //services.AddControllersWithViews();

        //services.AddDbContext<ApplicationDbContext>(options =>
        //{
        //    //Configurar o Entity Framework Core para utilizar o Microsoft SQL Server.
        //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

        //    // Registar os conjuntos de entidades necessários ao OpenIddict.
        //    // Nota: utilize a sobrecarga genérica se precisar de substituir as entidades OpenIddict por defeito.
        //    options.UseOpenIddict();
        //});

        //services.AddOpenIddict()
        //    // Registar os componentes do núcleo OpenIddict.
        //    .AddCore(options =>
        //    {
        //        // Configurar o OpenIddict para utilizar as lojas e modelos do Entity Framework Core.
        //        // Nota: chamar ReplaceDefaultEntities() para substituir as entidades por defeito.
        //        options.UseEntityFrameworkCore()
        //            .UseDbContext<ApplicationDbContext>();
        //    })


        //// Registar os componentes do servidor OpenIddict.
        //.AddServer(options =>
        //{
        //    // Habilitar o ponto final simbólico.
        //    options.SetTokenEndPointUris("connect/token");

        //    // Activar o fluxo de credenciais do cliente.
        //    options.AllowClientCredentialsFlow();


        //    options.AddDevelopmentEncryptionCertificate()
        //            .AddDecelopmentSigningCertificate();

        //    options.UseAspNetCore()
        //            .EnableTokenEndpointPassthrough();
        //})

        //.AddValidation(options =>
        //{
        //    options.UseLocalServer();

        //    options.UseAspNetCOre();
        //});

        //services.AddHostedService<Worker>();


    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MvcMovieWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MvcMovieDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MvcMovie.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<MvcMovieDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MvcMovie.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<MvcMovieApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MvcMovie.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<MvcMovieApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}MvcMovie.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<MvcMovieWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MvcMovieMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MvcMovieApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MvcMovie API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MvcMovie API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
