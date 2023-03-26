using MvcMovie.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MvcMovie.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MvcMovieController : AbpControllerBase
{
    protected MvcMovieController()
    {
        LocalizationResource = typeof(MvcMovieResource);
    }
}
