using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Authors;

public class AuthorDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public string ImdbId { get; set; }

}
