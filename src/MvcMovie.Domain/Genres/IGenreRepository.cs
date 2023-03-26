using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MvcMovie.Genres;

public interface IGenreRepository : IRepository<Genre, Guid>
{
    Task<Genre> FindByDescriptionAsync(string description);
    Task<Genre> FindByIdAsync(Guid id);

    Task<Guid> FindByIdAsync(string name);


    Task<List<Genre>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
       );

    Task<int> GetCountAsync(string filter = null);
}
