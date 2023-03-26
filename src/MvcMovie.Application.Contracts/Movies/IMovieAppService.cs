using Microsoft.AspNetCore.Mvc;
using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace MvcMovie.Movies;

public interface IMovieAppService : ICrudAppService<MovieDto,
    Guid,
    GetMovieListDto,
    CreateUpdateMovieDto>
{
    //Task<MovieDto> CreateAsyncMovie(CreateMovieDto input);
    //Task CreateAsync(CreateMovieDto dto);

    Task<List<MovieDto>> GetAllMovies();


    Task<ListResultDto<GenreLookupDto>> GetGenreLookupAsync();

    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();


    //Task<MovieAuthorDto> GetMAListAsyncMovieAuthor(Guid movieAuthor);
}
