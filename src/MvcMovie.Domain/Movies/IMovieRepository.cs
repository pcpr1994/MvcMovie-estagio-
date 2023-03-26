using MvcMovie.Authors;
using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MvcMovie.Movies;

public interface IMovieRepository : IRepository<Movie, Guid>
{
    Task<Movie> FindByTitleAsync(string title);

    //Task<IEnumerable<Movie>> GetListAsyncMovieAuthor(Guid movieAuthor);
    Task<List<Movie>> GetMovie(IGenreRepository genreRepository, IAuthorRepository authorRepository);

    Task<int> GetCountAsync(string filter = null,
                            Guid? movieAuthor = null);
    Task<List<Movie>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null,
        Guid? movieAuthor = null);
    Task<List<Movie>> GetAllMovies();



}
