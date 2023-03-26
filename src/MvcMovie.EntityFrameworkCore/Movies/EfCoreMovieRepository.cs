using Microsoft.EntityFrameworkCore;
using MvcMovie.Authors;
using MvcMovie.EntityFrameworkCore;
using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;


namespace MvcMovie.Movies;

public class EfCoreMovieRepository
    : EfCoreRepository<MvcMovieDbContext, Movie, Guid>,

    IMovieRepository
{
    public EfCoreMovieRepository(
        IDbContextProvider<MvcMovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {


    }
    public async Task<Movie> FindByTitleAsync(string title)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(movie => movie.Title == title);

    }

    public async Task<List<Movie>> GetMovie(IGenreRepository genreRepository, IAuthorRepository authorRepository)
    {
        var dbSet = await GetQueryableAsync();

        var query = from movie in dbSet
                    join genre in await genreRepository.GetQueryableAsync() on movie.Genreid equals genre.Id
                    join author in await authorRepository.GetQueryableAsync() on movie.Authorid equals author.Id
                    select new { movie, genre, author };

        return (List<Movie>)query;
    }

    ////para converter movieAuthor de int para guid
    //byte[] bytes = new byte[16];
    //BitConverter.GetBytes((Half)movieAuthor).CopyTo(bytes, 0);
    //Guid a = new Guid(bytes);
    //
    //
    //public async Task<IEnumerable<Movie>> GetListAsyncMovieAuthor(Guid movieAuthor)
    //{
    //
    //    var dbSet = await GetDbSetAsync();
    //    return await dbSet
    //                              .Include(g => g.Author)
    //                              .Where(x => x.Authorid == movieAuthor)
    //                              .OrderBy(x => x.Title)
    //
    //                              .ToListAsync();
    //
    //}


    public async Task<List<Movie>> GetAllMovies()
    {
        var dbSet = await WithDetailsAsync(x => x.Author, x => x.Genre);

        return await dbSet
            .ToListAsync();
    }


    public async Task<List<Movie>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null,
        Guid? movieAuthor = null)
    {
        var dbSet = await WithDetailsAsync(x => x.Author, x => x.Genre);

        return await dbSet
            
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                movie => movie.Title.ToUpper().Contains(filter.ToUpper())
                )
            .WhereIf(movieAuthor.HasValue, movie => movie.Authorid == movieAuthor)

            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync(
        string filter = null, Guid? movieAuthor = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(!filter.IsNullOrWhiteSpace(), movie => movie.Title.ToUpper().Contains(filter.ToUpper())
                )
            .WhereIf(movieAuthor.HasValue, movie => movie.Authorid == movieAuthor)

            .CountAsync();
    }



}

