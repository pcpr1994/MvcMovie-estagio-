using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Movies;

public class AuthorLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
