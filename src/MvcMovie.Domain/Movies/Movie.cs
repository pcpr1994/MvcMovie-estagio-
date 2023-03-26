using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using MvcMovie.Genres;
using Volo.Abp;
using System.Diagnostics;
using System.Data;
using MvcMovie.Authors;

namespace MvcMovie.Movies;

public class Movie : AuditedAggregateRoot<Guid>
{
    //private DateOnly relaseTime;
    public string Title { get; set; }

    public Guid Genreid { get; set; }

    public virtual Genre Genre { get; set; }

    public Guid Authorid { get; set; }
    public virtual Author Author { get; set; }


    //[DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public float Price { get; set; }

    public float Rating { get; set; }

    public string RuntimeMins { set; get; }

    public string Plot { set; get; }

    public string Writers { set; get; }

    public string Stars { set; get; }

    public string Countries { set; get; }

    public string Languages { set; get; }

    public string IdImdb { get; set; }

    public string Awards { set; get; }

    //Trailer
    public string LinkEmbed { set; get; }

    public string VideoDescription { set; get; }

    //Poster
    public string LinkPoster1 { set; get; }

    public string LinkPoster2 { set; get; }

    // Images

    public string Image1 { set; get; }
    public string Image2 { set; get; }
    



    public Movie()
    {
        /* This constructor is for deserialization / ORM purpose */

    }

    //private Movie()
    //{

    //}
    internal Movie(
    Guid Id,
        [NotNull] string Title, 
        [NotNull] Guid Genreid,
        [NotNull] Guid Authorid,
        [NotNull] DateTime ReleaseDate, 
        [NotNull] float Price, 
        [NotNull] float Rating,
        string RuntimeMins, string Plot, 
        string Writers,
        string Stars, string Countries, 
        string Languages, string IdImdb,
        string Awards, string LinkEmbed, string VideoDescription, 
        string LinkPoster1, string LinkPoster2,
        string Image1, string Image2)
        : base(Id)
    {
        SetName(Title, RuntimeMins, Plot, Writers,
        Stars, Countries, Languages, IdImdb,  Awards,  LinkEmbed,  VideoDescription, 
         LinkPoster1,  LinkPoster2,  Image1,  Image2);
        SetName(Genreid, Authorid);
        SetName(ReleaseDate);
        SetName(Price, Rating);
    }

    
    internal Movie ChangeTitle([NotNull] string Title, Guid genreid, Guid authorid, DateTime releaseDate,  
        float price,
         float Rating, string RuntimeMins, 
         string Plot, string Writers,
        string Stars, string Countries, 
        string Languages, string IdImdb,
        string Awards, string LinkEmbed, 
        string VideoDescription, 
        string LinkPoster1, string LinkPoster2, 
        string Image1, string Image2)
    {
        SetName(Title, RuntimeMins, Plot, Writers,
        Stars, Countries, Languages, IdImdb,  Awards,  LinkEmbed,  VideoDescription, 
         LinkPoster1,  LinkPoster2,  Image1,  Image2);
        SetName(genreid, authorid);
        SetName(releaseDate);
        SetName(price, Rating);


        return this;
    }



    private void SetName(Guid genreid, Guid authorid)
    {
        Genreid= genreid;
        Authorid = authorid;
    }

   

    private void SetName(float price, float rating)
    {
        Price= price;
        Rating = rating;
    }
    private void SetName(DateTime releaseDate)
    {
        ReleaseDate = releaseDate;
    }



    //private void SetName(int genreid)
    //{
    //    throw new NotImplementedException();
    //}

    private void SetName([NotNull]string title, string runtimeMins, 
        string plot, string writers,
        string stars, string countries, string languages, string idImdb,
        string awards, string linkEmbed, string videoDescription,
        string linkPoster1, string linkPoster2, 
        string image1, string image2)
    {
        Title = Check.NotNullOrWhiteSpace(
            title,
            nameof(title),
            maxLength: GenreConsts.MaxNameLength
        );

        RuntimeMins = runtimeMins;

        Plot= plot;
        Writers = writers;  
        Stars= stars;
        Countries = countries;
        Languages = languages;
        IdImdb = idImdb;
        Awards = awards;
        LinkEmbed = linkEmbed;
        VideoDescription = videoDescription;
        LinkPoster1 = linkPoster1;
        LinkPoster2 = linkPoster2;
        Image1 = image1;
        Image2 = image2;


    }


}
