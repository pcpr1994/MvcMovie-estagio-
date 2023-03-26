using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Movies;

public class MovieDto : AuditedEntityDto<Guid>
{
 
    public Guid MovieAuthor { get; set; }
    public string Title { get; set; }

    public string GenreDescription { get; set; }

    public Guid Genreid { get; set; }

    public Guid Authorid { get; set; }

    public DateTime ReleaseDate { get; set; }    

    //public string Description { get; set; }

    public string AuthorName { get; set; }

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



}
