using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using MvcMovie.Movies;
using Volo.Abp.EntityFrameworkCore.Modeling;
using MvcMovie.Genres;
using MvcMovie.Authors;

namespace MvcMovie.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MvcMovieDbContext :
    AbpDbContext<MvcMovieDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public DbSet<Author> Authors { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MvcMovieDbContext(DbContextOptions<MvcMovieDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(MvcMovieConsts.DbTablePrefix + "YourEntities", MvcMovieConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Movie>(b =>
        {
            
            b.ToTable(MvcMovieConsts.DbTablePrefix + "Movies", MvcMovieConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired();
            b.Property(x => x.Genreid).IsRequired();
            b.Property(x => x.Authorid).IsRequired();

            //RELACAO COM GENREID e AUTHORID
            b.HasOne(x=>x.Genre).WithMany().HasForeignKey(x => x.Genreid);
            b.HasOne(x=>x.Author).WithMany().HasForeignKey(x =>x.Authorid);

        });

        //GENRE

        builder.Entity<Genre>(b =>
        {
            b.ToTable(MvcMovieConsts.DbTablePrefix + "Genre",
                MvcMovieConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(GenreConsts.MaxNameLength);

            b.HasIndex(x => x.Description);
        });

        //Authors
        builder.Entity<Author>(b =>
        {
            b.ToTable(MvcMovieConsts.DbTablePrefix + "Author",
                MvcMovieConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired();

            b.HasIndex(x => x.Name);
        });
    }
}
