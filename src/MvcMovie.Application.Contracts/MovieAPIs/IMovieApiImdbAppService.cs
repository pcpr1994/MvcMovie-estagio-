using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.MovieAPIs;

public interface IMovieApiImdbAppService
{
    Task ImportMovieToIMDBAsync(string idImdb);
}
