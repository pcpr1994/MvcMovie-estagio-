using Microsoft.EntityFrameworkCore;
using MvcMovie.EntityFrameworkCore;
using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MvcMovie.Authors;

public class EfCoreAuthorRepository
    : EfCoreRepository<MvcMovieDbContext, Author, Guid>,
    IAuthorRepository
{
    public EfCoreAuthorRepository(
        IDbContextProvider<MvcMovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<Author> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(author => author.Name == name);

    }

    public async Task<Author> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(author => author.Id == id);

    }

    public async Task<Guid> FindByIdAsync(string name)
    {
        var dbSet = await GetDbSetAsync();

        var existingGenre = await (from author in dbSet
                                   where author.Name == name
                                   select author.Id).SingleOrDefaultAsync();

        return existingGenre;
    }

    public async Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter)
    {
        var dbSet = await WithDetailsAsync();

        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                author => author.Name.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<int>GetCountAsync(
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();

        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                author => author.Name.Contains(filter)
            )
            .CountAsync();
    }


}
