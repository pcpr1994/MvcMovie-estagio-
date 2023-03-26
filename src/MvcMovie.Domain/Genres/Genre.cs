using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MvcMovie.Genres;

public class Genre : FullAuditedAggregateRoot<Guid>
{
    public string Description { get; set; }

    public Genre()
    {

    }

    internal Genre(
        Guid id,
        [NotNull] string Description)
        : base(id)
    {
        SetName(Description);
    }

    
    private void SetName([NotNull] string name)
    {
        Description = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: GenreConsts.MaxNameLength
        );
    }

    internal Genre ChangeDescription([NotNull] string Description)
    {
        SetName(Description);
        return this;
    }
}
