using MvcMovie.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MvcMovie.Permissions;

public class MvcMoviePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MvcMoviePermissions.GroupName, L("Permission:MvcMovie"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MvcMoviePermissions.MyPermission1, L("Permission:MyPermission1"));
        var moviePermission = myGroup.AddPermission(MvcMoviePermissions.Movies.Default, L("Permission:Movie"));
        moviePermission.AddChild(MvcMoviePermissions.Movies.Create, L("Permission:Movie.Create"));
        moviePermission.AddChild(MvcMoviePermissions.Movies.Edit, L("Permission:Movie.Edit"));
        moviePermission.AddChild(MvcMoviePermissions.Movies.Detail, L("Permission:Movie.Detail"));
        moviePermission.AddChild(MvcMoviePermissions.Movies.Delete, L("Permission:Movie.Delete"));


        var GenresPermission = myGroup.AddPermission(MvcMoviePermissions.Genres.Default, L("Permission:Genre"));
        GenresPermission.AddChild(MvcMoviePermissions.Genres.Create, L("Permission:Genre.Create"));
        GenresPermission.AddChild(MvcMoviePermissions.Genres.Edit, L("Permission:Genre.Edit"));
        GenresPermission.AddChild(MvcMoviePermissions.Genres.Delete, L("Permission:Genre.Delete"));


        var authorsPermission = myGroup.AddPermission(MvcMoviePermissions.Authors.Default, L("Permission:Author"));
        authorsPermission.AddChild(MvcMoviePermissions.Authors.Create, L("Permission:Author.Create"));
        authorsPermission.AddChild(MvcMoviePermissions.Authors.Edit, L("Permission:Author.Edit"));
        authorsPermission.AddChild(MvcMoviePermissions.Authors.Delete, L("Permission:Author.Delete"));



    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MvcMovieResource>(name);
    }
}
