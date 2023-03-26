using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;


namespace MvcMovie.Web.Pages.Movies;

public class CreateModalModel : MvcMoviePageModel
{
    [BindProperty]
    public CreateMovieViewModel Movie { get; set; }

    public List<SelectListItem> Genres { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IMovieAppService _movieAppService;

    public CreateModalModel(
        IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync()
    {

        Movie = new CreateMovieViewModel();

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
    //    Movie = new CreateMovieViewModel();
    //}

    public async Task<IActionResult> OnPostAsync()
    {
       // var dto = ObjectMapper.Map<CreateMovieViewModel, CreateMovieDto>(Movie);
      // await _movieAppService.CreateAsync(dto);

        await _movieAppService.CreateAsync(
            ObjectMapper.Map<CreateMovieViewModel, CreateUpdateMovieDto>(Movie));
        
        return NoContent();
    }

    public class CreateMovieViewModel 
    {

        [Required]
        public string Title { get; set; }

        [Required]
        [SelectItems(nameof(Genres))]
        [DisplayName("Genre")]
        public Guid Genreid { get; set; }

        [Required]
        [SelectItems(nameof(Authors))]
        [DisplayName("Author")]
        public Guid AuthorId { get; set; }


        [Required]
        //[DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


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



        //public List<Genre>? Genres { get; set; }

        // public SelectList? Genres { get; set; }

    }




}
