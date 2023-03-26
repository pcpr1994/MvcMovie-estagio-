using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using MvcMovie.Permissions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using MvcMovie.Authors;


namespace MvcMovie.Genres;

[Authorize(MvcMoviePermissions.Genres.Default)]
public class GenreAppService : CrudAppService<
    Genre,
    GenreDto,
    Guid,
    GetGenreListDto,
    CreateUpdateGenreDto>,
    IGenreAppService
{
    private readonly IGenreRepository _genreRepository;
    private readonly GenreManager _genreManager;

    public GenreAppService(
        IRepository<Genre, Guid> repository,
        IGenreRepository genreRepository, 
        GenreManager genreManager)
        : base(repository)
    {
        _genreRepository = genreRepository;
        _genreManager = genreManager;
        GetPolicyName = MvcMoviePermissions.Genres.Default;
        GetListPolicyName = MvcMoviePermissions.Genres.Default;
        CreatePolicyName = MvcMoviePermissions.Genres.Create;
        UpdatePolicyName = MvcMoviePermissions.Genres.Edit;
        DeletePolicyName = MvcMoviePermissions.Genres.Delete;
    }


    //public override async Task<GenreDto> GetAsync(Guid id)
    //{
    //    var genre = await _genreRepository.GetAsync(id);
    //    return ObjectMapper.Map<Genre, GenreDto>(genre);
    //}

    public override async Task<PagedResultDto<GenreDto>> GetListAsync(GetGenreListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Genre.Description);
        }

        var genres = await _genreRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
            );

      

        var total = await _genreRepository.GetCountAsync(input.Filter);

        return new PagedResultDto<GenreDto>(
            total,
            ObjectMapper.Map<List<Genre>, List<GenreDto>>(genres));

        /*
        //var genres = await _genreRepository.GetListAsync(
        //    skipCount,
        //    maxResultCount,
        //    sorting,
        //    filter
        //    );
        //
        //? se for nulo  
        //: se for nao nulo
        //var totalCount = input.Filter == null
        //    ? await _genreRepository.CountAsync()
        //    : await _genreRepository.CountAsync(
             genre => genre.Description.Contains(input.Filter)); 
        

        //Get the IQueryable<Genre> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to  genre
        var query = from genre in queryable
                    select new { genre };

        ////Paging
        //query = query
        //    .OrderBy(NormalizeSorting(input.Sorting))
        //    .Skip(input.SkipCount)
        //    .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        var genreDtos = queryResult.Select(x =>
        {
            var genreDto = ObjectMapper.Map<Genre, GenreDto>(x.genre);
            return genreDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<GenreDto>(
            totalCount,
            genreDtos
            );
        */
    }

    //private static string NormalizeSorting(string sorting)
    //{
    //    if (sorting.IsNullOrEmpty())
    //    {
    //        return $"genre.{nameof(Genre.Description)}";
    //    }
    //    if (sorting.Contains("Description", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return sorting.Replace(
    //            "Description",
    //            "genre.description",
    //            StringComparison.OrdinalIgnoreCase
    //            );
    //    }
    //    return $"genre.{sorting}";
    //}




    [Authorize(MvcMoviePermissions.Genres.Create)]
    public async Task<GenreDto> CreateAsyncGenre(CreateGenreDto input)
    {
        var genre = await _genreManager.CreateAsync(
            input.Description
         );

        await _genreRepository.InsertAsync(genre);

        return ObjectMapper.Map<Genre, GenreDto>(genre);

    }

   
    //[Authorize(MvcMoviePermissions.Genres.Edit)]
    //public async Task UpdateAsync(Guid id, CreateUpdateGenreDto input)
    //{
    //    var genre = await _genreRepository.GetAsync(id);

    //    if (genre.Description != input.Description)
    //    {
    //        await _genreManager.ChangeNameAsync(genre, input.Description);

    //    }

    //    await _genreRepository.UpdateAsync(genre);
    //}

    //[Authorize(MvcMoviePermissions.Genres.Delete)]
    //public async Task DeleteAsync(Guid id)
    //{
    //    await _genreRepository.DeleteAsync(id,true);
    //}

}
