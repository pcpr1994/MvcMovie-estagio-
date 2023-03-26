
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcMovie.Movies;

public class CreateMovieDto 
{
    [Required]
    public string Title { get; set; }

    //[SelectItems(nameof(Genres))]
    [DisplayName("Genre")]
    public Guid Genreid { get; set; }

    [DisplayName("Genre")]
    public Guid AuthorId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }


  
    public float Price { get; set; }


    [Required]
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
