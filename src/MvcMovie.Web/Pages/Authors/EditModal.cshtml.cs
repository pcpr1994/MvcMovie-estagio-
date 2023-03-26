using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using MvcMovie.Authors;
using System;
using System.Threading.Tasks;

namespace MvcMovie.Web.Pages.Authors;

public class EditModalModel : MvcMoviePageModel
{
    [BindProperty] 
    public EditAuthorViewModal Author { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public EditModalModel(IAuthorAppService authorAppService)
    {
        _authorAppService= authorAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var authorDto = await _authorAppService.GetAsync(id);

        Author = ObjectMapper.Map<AuthorDto, EditAuthorViewModal>(authorDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _authorAppService.UpdateAsync(
            Author.Id,
            ObjectMapper.Map<EditAuthorViewModal, CreateUpdateAuthorDto>(Author)
            );

        return NoContent();
    }

    public class EditAuthorViewModal
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
