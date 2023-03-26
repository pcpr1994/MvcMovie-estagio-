using Microsoft.AspNetCore.Authorization;
using MvcMovie.Authors;
using MvcMovie.Genres;
using MvcMovie.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MvcMovie.Movies;

[Authorize(MvcMoviePermissions.Movies.Default)]
public class MovieAppService : CrudAppService<
    Movie,
    MovieDto,
    Guid,
    GetMovieListDto,
    CreateUpdateMovieDto>, 
    IMovieAppService
{

    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly MovieManager _movieManager;

    public MovieAppService(
        IRepository<Movie, Guid> repository,
        IGenreRepository genreRepository,
        IMovieRepository movieRepository,
        IAuthorRepository authorRepository,
        MovieManager movieManager) 
        : base(repository)
    {
        _genreRepository = genreRepository;
        _movieRepository = movieRepository;
        _authorRepository = authorRepository;
        _movieManager = movieManager;
        GetPolicyName = MvcMoviePermissions.Movies.Detail;
        GetListPolicyName= MvcMoviePermissions.Movies.Default;
        CreatePolicyName = MvcMoviePermissions.Movies.Create;
        UpdatePolicyName = MvcMoviePermissions.Movies.Edit;
        DeletePolicyName= MvcMoviePermissions.Movies.Delete;
    }


    public override async Task<MovieDto> GetAsync(Guid id)
    {

        var movie = await _movieRepository.GetAsync(id);


        if (movie == null)
        {
            throw new EntityNotFoundException(typeof(Movie), id);
        }

        var movieDto = ObjectMapper.Map<Movie, MovieDto>(movie);

        return movieDto;
    }


    public override async Task<PagedResultDto<MovieDto>> GetListAsync(GetMovieListDto input)
    {
        if(input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Movie.Title);
        }

        /*if(!input.Filter.IsNullOrWhiteSpace()) {
        //    var x = input.Filter;
        }*/

        var movies = await _movieRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,     
            input.MovieAuthor
           );

        /*
        //var queryResult = await AsyncExecuter.ToListAsync(query);
        //
        //var movieDtos = queryResult.Select(x =>
        //{
        //    var movieDto = ObjectMapper.Map<Movie, MovieDto>(x.movie);
        //    movieDto.Description = x.genre.Description;
        //    movieDto.AuthorName = x.author.Name;
        //    return movieDto;
        //}).ToList();
        */

        var total = await _movieRepository.GetCountAsync(input.Filter, input.MovieAuthor);
        
        /*
        //PagedResultDto<MovieDto> pagedResultDto = new PagedResultDto<MovieDto>(
        //            total,
        //            movieDtos);
        //
        //return pagedResultDto;
        */


        return new PagedResultDto<MovieDto>(
            total,
            ObjectMapper.Map<List<Movie>, List<MovieDto>>(movies));

      
        /*
        ////Get the IQueryable<Movie> from the repository
        //var queryable = await Repository.GetQueryableAsync();
        //
        ////Prepare a query to join movie and genre
        //var query = from movie in queryable
        //            join genre in await _genreRepository.GetQueryableAsync() on movie.Genreid equals genre.Id
        //            join author in await _authorRepository.GetQueryableAsync() on movie.Authorid equals author.Id
        //            select new { movie, genre, author };
        //
        ////Paging
        //query = query
        //    .OrderBy(NormalizeSorting(input.Sorting))
        //    .Skip(input.SkipCount)
        //    .Take(input.MaxResultCount);
        //
        ////Execute the query and get a list
        //var queryResult = await AsyncExecuter.ToListAsync(query);
        //
        ////Convert the query result to a list of MovieDto objects
        //var movieDtos = queryResult.Select(x =>
        //{
        //    var movieDto = ObjectMapper.Map<Movie, MovieDto>(x.movie);
        //    movieDto.Description = x.genre.Description;
        //    movieDto.AuthorName = x.author.Name;
        //    return movieDto;
        //}).ToList();
        //
        ////Get the total count with another query
        //var totalCount = await Repository.GetCountAsync();
        //
        //return new PagedResultDto<MovieDto>(
        //    totalCount,
        //    movieDtos
        //);*/
    }

    public async Task<List<MovieDto>> GetAllMovies()
    {

        var movies = await _movieRepository.GetAllMovies();

        return ObjectMapper.Map<List<Movie>, List<MovieDto>>(movies);
    }


    public async Task<ListResultDto<GenreLookupDto>> GetGenreLookupAsync()
    {
        var genres = await _genreRepository.GetListAsync();

        return new ListResultDto<GenreLookupDto>(
            ObjectMapper.Map<List<Genre>, List<GenreLookupDto>>(genres)
            );
    }

    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var author = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(author)
        );
    }


    /*
    //public async Task<MovieAuthorDto> GetMAListAsyncMovieAuthor(Guid movieAuthor)
    //{
    //    var author = await _authorRepository.GetListAsync();
    //    if (author == null)
    //    {
    //        throw new ArgumentNullException();
    //    }

    //    IEnumerable<Movie> authorQuery = await _movieRepository.GetListAsyncMovieAuthor(movieAuthor);

    //    var movieAut = new MovieAuthorDto
    //    {
    //        Authors = new SelectList(authorQuery, nameof(AuthorDto.Id), nameof(AuthorDto.Name))

    //        //Authors = new SelectList(authorQuery, nameof(_authorRepository.GetListAsync), nameof(_authorRepository.GetListAsync))
    //    };


    //    return (MovieAuthorDto)movieAut;

    //}
    */

    //private static string NormalizeSorting(string sorting)
    //{
    //    if(sorting.IsNullOrEmpty())
    //    {
    //        return $"movie.{nameof(Movie.Title)}";
    //    }
    //    if(sorting.Contains("Description", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return sorting.Replace(
    //            "Description",
    //            "genre.description",
    //            StringComparison.OrdinalIgnoreCase
    //            );
    //    }
    //    return $"movie.{sorting}";
    //}


/*
//    [Authorize(MvcMoviePermissions.Movies.Create)]
//    public async Task<MovieDto> CreateAsyncMovie(CreateMovieDto input)
//    {
//        //// Creating DateTime object
//        //DateTime testDateTime = input.ReleaseDate;
//        //// Creating TimeOnly object from DateTime.
//        //DateOnly testTimeOnly = DateOnly.FromDateTime(testDateTime);


//        var movie = await _movieManager.CreateAsync(
//            input.Title,
//            input.Genreid,
//            input.ReleaseDate,
//            input.Price,
//            input.Rating
//         );
////.ForMember(dest => dest.ReleaseDate, src => src.MapFrom(x => x.ReleaseDate.ToDateTime(TimeOnly.MinValue)));
//        await _movieRepository.InsertAsync(movie);

//        return ObjectMapper.Map<Movie, MovieDto>(movie);

//    }

    */

}
