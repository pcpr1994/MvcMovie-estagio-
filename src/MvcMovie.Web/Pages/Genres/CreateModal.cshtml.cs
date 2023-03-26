using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcMovie.Genres;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MvcMovie.Web.Pages.Genres;

public class CreateModalModel : MvcMoviePageModel
{
    [BindProperty]
    public CreateGenreViewModel Genre { get; set; }

    private readonly IGenreAppService _genreAppService;

    public CreateModalModel(IGenreAppService genreAppService)
    {
        _genreAppService = genreAppService;
    }

    public void OnGet()
    {
        Genre = new CreateGenreViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateGenreViewModel, CreateGenreDto>(Genre);
        await _genreAppService.CreateAsyncGenre(dto);
        return NoContent();
    }


    public class CreateGenreViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
