using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcMovie.Authors;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MvcMovie.Web.Pages.Authors;

public class CreateModalModel : MvcMoviePageModel
{
    [BindProperty]
    public CreateAuthorViewModel Author { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public CreateModalModel( IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }



    public void OnGet()
    {
        Author = new CreateAuthorViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateAuthorViewModel, CreateAuthorDto>(Author);
        await _authorAppService.CreateAsyncAuthor(dto);
        return NoContent();
    }



    public class CreateAuthorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

    }
}
