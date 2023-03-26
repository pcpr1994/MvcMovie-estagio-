using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using MvcMovie.Web.Pages.Movies;

namespace MvcMovie.Web.Pages.Movies;

public class EditModalModel : MvcMoviePageModel
{
    [BindProperty]
    public EditMovieViewModel Movie { get; set; }

    public List<SelectListItem> Genres { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IMovieAppService _movieAppService;

    public EditModalModel(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var movieDto = await _movieAppService.GetAsync(id);
        Movie = ObjectMapper.Map<MovieDto, EditMovieViewModel>(movieDto);

        var genreLookup = await _movieAppService.GetGenreLookupAsync();
        Genres = genreLookup.Items
            .Select(x => new SelectListItem(x.Description, x.Id.ToString()))
            .ToList();

        var authorLookup = await _movieAppService.GetAuthorLookupAsync();
        Authors = authorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _movieAppService.UpdateAsync(
            Movie.Id,
            ObjectMapper.Map<EditMovieViewModel, CreateUpdateMovieDto>(Movie)
            );

        return NoContent();
    }


    public class EditMovieViewModel 
    {
        [HiddenInput] 
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [SelectItems(nameof(Genres))]
        [DisplayName("Genre")]
        public Guid GenreId { get; set; }

        [SelectItems(nameof(Authors))]
        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

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
