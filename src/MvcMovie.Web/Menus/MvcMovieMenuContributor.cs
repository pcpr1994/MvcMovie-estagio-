using System.Threading.Tasks;
using MvcMovie.Localization;
using MvcMovie.MultiTenancy;
using MvcMovie.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace MvcMovie.Web.Menus;

public class MvcMovieMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<MvcMovieResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                MvcMovieMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //criar um menu e um separador na pagina
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "MvcMovie",
                l["Menu:MvcMovies"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "MvcMovies.Movies",
                    l["Movies"],
                    url: "/Movies" 
                ).RequirePermissions(MvcMoviePermissions.Movies.Default)
             ).AddItem( // ADDED THE NEW "Genre" MENU ITEM UNDER THE "Movie" MENU
                new ApplicationMenuItem(
                    "MvcMovie.Genres",
                    l["Menu:Genres"],
                    url: "/Genres"
                ).RequirePermissions(MvcMoviePermissions.Genres.Default)
            ).AddItem( // ADDED THE NEW "Author" MENU ITEM UNDER THE "Movie" MENU
                new ApplicationMenuItem(
                    "MvcMovie.Authors",
                    l["Menu:Authors"],
                    url: "/Authors"
                ).RequirePermissions(MvcMoviePermissions.Authors.Default)
            )
          );
    
    }
}
