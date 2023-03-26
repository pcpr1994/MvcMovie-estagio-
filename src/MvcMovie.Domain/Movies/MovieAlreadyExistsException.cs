using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace MvcMovie.Movies;

public class MovieAlreadyExistsException : BusinessException
{
    public MovieAlreadyExistsException(string title)
        : base(MvcMovieDomainErrorCodes.MovieAlreadyExists)
    {
        WithData("title", title);
    }
}
