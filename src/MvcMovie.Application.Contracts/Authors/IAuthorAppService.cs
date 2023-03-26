using MvcMovie.Genres;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MvcMovie.Authors;

public interface IAuthorAppService : ICrudAppService<AuthorDto,
    Guid,
    GetAuthorListDto,
    CreateUpdateAuthorDto>
{

    Task<AuthorDto> CreateAsyncAuthor(CreateAuthorDto input);


    //Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);
}
