using AutoMapper;
using MvcMovie.Authors;
using MvcMovie.Genres;
using MvcMovie.Movies;
using System;

namespace MvcMovie;

public class MvcMovieApplicationAutoMapperProfile : Profile
{
    public MvcMovieApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Movie, MovieDto>();
            //.ForMember(dest => dest.ReleaseDate, src => src.MapFrom(x => x.ReleaseDate.ToDateTime(TimeOnly.MinValue)));

        CreateMap<CreateUpdateMovieDto, Movie>();

        CreateMap<Genre, GenreDto>();
        CreateMap<Genre, GenreLookupDto>();
        CreateMap<CreateUpdateGenreDto, Genre>();


        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<CreateUpdateAuthorDto, Author>();
    }
}
