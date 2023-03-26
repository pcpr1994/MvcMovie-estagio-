using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MvcMovie.Authors;

public interface IAuthorRepository : IRepository<Author, Guid>
{
    Task<Author> FindByNameAsync(string name);

    Task<Author> FindByIdAsync(Guid id);

    Task<Guid> FindByIdAsync(string name);

    Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter 
       );

    Task<int> GetCountAsync(string filter = null);
}
