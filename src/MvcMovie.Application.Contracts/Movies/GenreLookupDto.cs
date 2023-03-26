using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Movies;

public class GenreLookupDto : EntityDto<Guid>
{
    public string Description { get; set; }
}
