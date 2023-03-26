using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Entities.Auditing;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace MvcMovie.Movies;

public class MovieManager : DomainService
{
    private readonly IMovieRepository _movieRepository;


    public MovieManager(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> CreateAsync(
        [NotNull]  string title, Guid genreid, Guid authorid, DateTime relaseTime, 
        float price, float rating, string RuntimeMins, string Plot, string Writers,
        string Stars, string Countries, string Languages, string IdImdb,
        string Awards, string LinkEmbed, string VideoDescription,
        string LinkPoster1, string LinkPoster2,
        string Image1, string Image2)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));

        var existingTitle = await _movieRepository.FindByTitleAsync(title);

        if (existingTitle != null)
        {
            throw new MovieAlreadyExistsException(title);
        }

        return new Movie(
            GuidGenerator.Create(),
            title,
            genreid,
            authorid,
            relaseTime,
            price,
            rating,
             RuntimeMins,  Plot,  
             Writers,
             Stars,  Countries,  
             Languages,  IdImdb,
              Awards, LinkEmbed, VideoDescription,
        LinkPoster1, LinkPoster2, Image1, Image2

            );
    }

    public async Task ChangeNameAsync(
        [NotNull] Movie movie,
        [NotNull] string title,
        [NotNull] Guid genreid,
        [NotNull] Guid authorid,
        [NotNull] DateTime relaseTime,
       [NotNull] float price,
        [NotNull] float rating,
        string RuntimeMins, 
        string Plot, string Writers,
        string Stars, string Countries, 
        string Languages, string IdImdb,
        string Awards, string LinkEmbed, string VideoDescription,
        string LinkPoster1, string LinkPoster2, 
        string Image1, string Image2

        )
    {
        Check.NotNull(movie, nameof(movie));
        Check.NotNullOrWhiteSpace(title, nameof(title));

        var existingMovie = await _movieRepository.FindByTitleAsync(title);
        if (existingMovie != null)
        {
            throw new MovieAlreadyExistsException(title);
        }
        movie.ChangeTitle(title, genreid, authorid, relaseTime, price, rating,
        RuntimeMins, Plot, Writers,
        Stars, Countries,  Languages,  IdImdb,  Awards,  LinkEmbed,  VideoDescription,  
        LinkPoster1,  LinkPoster2,  Image1,  Image2);
    }

}
