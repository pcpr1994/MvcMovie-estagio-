using Microsoft.AspNetCore.Mvc;
using MvcMovie.Localization;
using MvcMovie.Movies;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MvcMovie.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class MvcMoviePageModel : AbpPageModel
{
    protected MvcMoviePageModel()
    {
        LocalizationResourceType = typeof(MvcMovieResource);
    }

    //public void OnGet()
    //{

    //}


}
