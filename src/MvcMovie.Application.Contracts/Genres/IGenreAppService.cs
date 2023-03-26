using MvcMovie.Movies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MvcMovie.Genres;

public interface IGenreAppService : ICrudAppService<GenreDto,
    Guid,
    GetGenreListDto,
    CreateUpdateGenreDto>
{
    //Task<GenreDto> GetAsync(Guid id);

    //Task<PagedResultDto<GenreDto>> GetListAsync(GetGenreListDto input);

  
    Task<GenreDto> CreateAsyncGenre(CreateGenreDto input);

    //Task<PagedResultDto<GenreDto>> GetListAsync1(GetGenreListDto input);

    // Task UpdateAsync(Guid id, CreateUpdateGenreDto createUpdateGenreDto);

    //Task UpdateAsync(Guid id, UpdateGenreDto input);

    //Task DeleteAsync(Guid id);
}
