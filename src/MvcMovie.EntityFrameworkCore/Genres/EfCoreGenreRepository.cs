using Microsoft.EntityFrameworkCore;
using MvcMovie.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MvcMovie.Genres;

public class EfCoreGenreRepository
    :EfCoreRepository<MvcMovieDbContext, Genre, Guid>,
    IGenreRepository
{
    public EfCoreGenreRepository(
        IDbContextProvider<MvcMovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<Genre> FindByDescriptionAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(genre => genre.Description == name);

    }

    public async Task<Genre> FindByIdAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(genre => genre.Id == id);

    }


    public async Task<Guid> FindByIdAsync(string name)
    {
        var dbSet = await GetDbSetAsync();

        var existingGenre = await (from genre in dbSet
                                   where genre.Description == name
                                   select genre.Id).SingleOrDefaultAsync();

        return existingGenre;
    }

    public async Task<List<Genre>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        //var dbSet = await GetDbSetAsync();
        var dbSet = await WithDetailsAsync();

        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                genre => genre.Description.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();

    }

    public async Task<int> GetCountAsync(
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();

        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                genre => genre.Description.Contains(filter)
            )
            .CountAsync();
    }


    //public async Task ExistingGenre(string genreDes)
    //{

    //    var dbSet = await GetQueryableAsync();

    //    var existingGenre = from genre in dbSet
    //                        where genre.Description == genreDes
    //                        select genre;

    //    if (existingGenre == null)
    //    {
    //        dbSet.()
    //    }
    //}

    


}
