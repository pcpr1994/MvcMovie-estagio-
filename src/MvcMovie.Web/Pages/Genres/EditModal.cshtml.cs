using MvcMovie.Genres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;


namespace MvcMovie.Web.Pages.Genres;

public class EditModalModel : MvcMoviePageModel
{
    [BindProperty]
    public EditGenreViewModel Genre { get; set; }

    private readonly IGenreAppService _genreAppService;

    public EditModalModel(IGenreAppService genreAppService)
    {
        _genreAppService = genreAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var genreDto = await _genreAppService.GetAsync(id);
        Genre = ObjectMapper.Map<GenreDto, EditGenreViewModel>(genreDto);
       
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _genreAppService.UpdateAsync(
            Genre.Id,
            ObjectMapper.Map<EditGenreViewModel, CreateUpdateGenreDto>(Genre)
            );

        return NoContent();
    }


    public class EditGenreViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
