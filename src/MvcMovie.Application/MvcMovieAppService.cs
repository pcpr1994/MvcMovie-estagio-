using System;
using System.Collections.Generic;
using System.Text;
using MvcMovie.Localization;
using Volo.Abp.Application.Services;

namespace MvcMovie;

/* Inherit your application services from this class.
 */
public abstract class MvcMovieAppService : ApplicationService
{
    protected MvcMovieAppService()
    {
        LocalizationResource = typeof(MvcMovieResource);
    }
}
