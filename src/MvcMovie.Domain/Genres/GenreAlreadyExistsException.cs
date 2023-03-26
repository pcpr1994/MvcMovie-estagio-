using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace MvcMovie.Genres;

public class GenreAlreadyExistsException : BusinessException
{
    public GenreAlreadyExistsException(string description)
        : base(MvcMovieDomainErrorCodes.GenreAlreadyExists)
    {
        WithData("description", description);
    }
}
