using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MvcMovie.MovieAPIs;
using MvcMovie.Movies;
using static MvcMovie.Web.Pages.Movies.EditModalModel;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Genres;
using MvcMovie.Authors;

namespace MvcMovie.Web.Pages.Movies;

public class DetailModalModel : MvcMoviePageModel
{
    [BindProperty]
    public DetailMovieViewModel DetailMovie { get; set; }

    public List<SelectListItem> Genres { get; set; }



    public List<SelectListItem> Authors { get; set; }
    //public EditMovieViewModel MovieEdit { get; private set; }

    public readonly IMovieApiImdbAppService _movieApiImdbAppService;

    private readonly IMovieAppService _movieAppService;

    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;

    public DetailModalModel(IMovieApiImdbAppService movieApiImdbAppService,
        IMovieAppService movieAppService,
        IGenreRepository genreRepository,
        IAuthorRepository authorRepository)
    {
        _movieApiImdbAppService= movieApiImdbAppService;
        _movieAppService = movieAppService;
        _genreRepository= genreRepository;
        _authorRepository= authorRepository;
    }

    public async Task OnGetAsync(Guid id)
    {
        //id = new Guid("3a0a0e77-4f82-3bdc-70b0-7b00ee4f7833");

        var movieDto = await _movieAppService.GetAsync(id);
        //MovieEdit = ObjectMapper.Map<MovieDto, EditMovieViewModel>(movieDto);

        //PARA IR BUSCAR A DESCRIPTION
        var genre = await _genreRepository.FindByIdAsync(movieDto.Genreid);
        string des = genre.Description.ToString();

        movieDto.GenreDescription = des;

        //PARA IR BUSCAR AUTHORNAME 
        var author = await _authorRepository.FindByIdAsync(movieDto.Authorid);

        movieDto.AuthorName = author.Name.ToString();

        //Mapear
        DetailMovie = ObjectMapper.Map<MovieDto, DetailMovieViewModel>(movieDto);


        var genreLookup = await _movieAppService.GetGenreLookupAsync();
        Genres = genreLookup.Items
            .Select(x => new SelectListItem(x.Description, x.Id.ToString()))
            .ToList();

        

        

        var authorLookup = await _movieAppService.GetAuthorLookupAsync();
        Authors = authorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();

    }

    //public void OnGet()
    //{
    //}

    public class DetailMovieViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [SelectItems(nameof(Genres))]
        [DisplayName("Genre")]
        public Guid GenreId { get; set; }

        public string GenreDescription { get; set; }

        [SelectItems(nameof(Authors))]
        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Rating { get; set; }

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


}
