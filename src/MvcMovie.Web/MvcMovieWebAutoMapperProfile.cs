using Abp.Application.Services.Dto;
using AutoMapper;
using MvcMovie.Authors;
using MvcMovie.Genres;
using MvcMovie.Movies;
using static MvcMovie.Web.Pages.IndexModel;

namespace MvcMovie.Web;

public class MvcMovieWebAutoMapperProfile : Profile
{
    public MvcMovieWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.

        CreateMap<MovieDto, CreateUpdateMovieDto>();

        CreateMap<Pages.Genres.CreateModalModel.CreateGenreViewModel,
            CreateGenreDto>();

        CreateMap<GenreDto, Pages.Genres.EditModalModel.EditGenreViewModel>();
        CreateMap<Pages.Genres.EditModalModel.EditGenreViewModel, CreateUpdateGenreDto>();



        CreateMap<Pages.Movies.CreateModalModel.CreateMovieViewModel, CreateUpdateMovieDto>();
        CreateMap<MovieDto, Pages.Movies.EditModalModel.EditMovieViewModel>();
        CreateMap<MovieDto, Pages.Movies.DetailModalModel.DetailMovieViewModel>();



      //  CreateMap<MovieDto, ViewMoviesViewModal>();
        CreateMap<PagedResultDto< MovieDto>, PagedResultDto< ViewMoviesViewModal>>();


        CreateMap<Pages.Movies.EditModalModel.EditMovieViewModel, CreateUpdateMovieDto>();


        //Author
        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel,
            CreateAuthorDto>();

        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModal> ();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModal, CreateUpdateAuthorDto>();



        //CreateMap<Pages.Movies.IndexModel.MovieAuthorViewModel,
        //    MovieAuthorDto>();

    }
}
