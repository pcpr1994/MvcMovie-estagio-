using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace MvcMovie.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public DateTime Birthday { get; set; }

    public string ImdbId { get; set; }

    public Author()
    {

    }

    internal Author(
        Guid id,
        [NotNull] string Name,
        [NotNull] DateTime Birthday,
        string imdbId)
        : base(id)
    {
        SetName(Name, imdbId);
        SetName(Birthday);
    }

    private void SetName(DateTime birthday)
    {
        Birthday = birthday;
    }

    private void SetName(string name, string imdbId)
    {
        Name= name;
        ImdbId = imdbId;
    }

    internal Author ChangeName([NotNull] string name, string imdbId, DateTime birthday) 
    { 
        SetName(name,imdbId);
        SetName(birthday); 
        return this;
    
    }
}
