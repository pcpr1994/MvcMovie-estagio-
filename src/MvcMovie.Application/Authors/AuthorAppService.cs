using Microsoft.AspNetCore.Authorization;
using MvcMovie.Genres;
using MvcMovie.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static MvcMovie.Permissions.MvcMoviePermissions;

namespace MvcMovie.Authors;

[Authorize(MvcMoviePermissions.Authors.Default)]
public class AuthorAppService : CrudAppService<
    Author,
    AuthorDto,
    Guid,
    GetAuthorListDto,
    CreateUpdateAuthorDto>,
    IAuthorAppService
{

    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;

    public AuthorAppService(
        IRepository<Author, Guid> repository,
        IAuthorRepository authorRepository,
        AuthorManager authorManager )
        : base( repository )
    {
        _authorRepository = authorRepository;
        _authorManager = authorManager;
        GetPolicyName = MvcMoviePermissions.Authors.Default;
        GetListPolicyName = MvcMoviePermissions.Authors.Default;
        CreatePolicyName = MvcMoviePermissions.Authors.Create;
        UpdatePolicyName = MvcMoviePermissions.Authors.Edit;
        DeletePolicyName = MvcMoviePermissions.Authors.Delete;
    }



    public override async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
    {
        if(input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Author.Name);
        }

        if(input.Filter !=null)
        {
            string x = input.Filter.ToLower();

        }


        var authors = await _authorRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
            ) ;


        var total = await _authorRepository.GetCountAsync(input.Filter);

        return new PagedResultDto<AuthorDto>(
            total,
            ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors));

        /*
       //Get the IQueryable<Author> from the repository
       var queryable = await Repository.GetQueryableAsync();

       //Prepare a query to author
       var query = from author in queryable
                   select new { author };

       //Execute the query and get a list
       var queryResult = await AsyncExecuter.ToListAsync(query);

       var authorDtos = queryResult.Select(x =>
       {
           var authoDto = ObjectMapper.Map<Author, AuthorDto>(x.author);
           return authoDto;
       }).ToList();

       var totalcount = await Repository.GetCountAsync();

       return new PagedResultDto<AuthorDto>(
           totalcount,
           authorDtos
           );
       */

    }


    [Authorize(MvcMoviePermissions.Authors.Create)]
    public async Task<AuthorDto> CreateAsyncAuthor(CreateAuthorDto input)
    {
        var author = await _authorManager.CreateAsync(
            input.Name,
            input.Birthday,
            input.ImdbId
        );

        await _authorRepository.InsertAsync(author);

        return ObjectMapper.Map<Author, AuthorDto>(author);

    }

}
