using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Genres;

public class GenreDto : AuditedEntityDto<Guid>
{
    public string Description { get; set; }

    
}
