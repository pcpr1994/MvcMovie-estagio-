using Microsoft.AspNetCore.Mvc.RazorPages;
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
using MvcMovie.MovieAPIs;

namespace MvcMovie.Web.Pages.Movies;

public class CreateIMDBModalModel : MvcMoviePageModel
{
    [BindProperty]
    public CreateIdImdbViewModel MovieIdImdb { get; set; }


    private readonly IMovieApiImdbAppService _movieApiImdbAppService;

    public CreateIMDBModalModel(
        IMovieApiImdbAppService movieApiImdbAppService)
    {
        _movieApiImdbAppService= movieApiImdbAppService;
    }


    public void OnGet()
    {
        MovieIdImdb = new CreateIdImdbViewModel();
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string id = MovieIdImdb.Id.ToString();

        //string x = "tt1630029";

        await _movieApiImdbAppService.ImportMovieToIMDBAsync(id);

        return NoContent();

    }



    public class CreateIdImdbViewModel
    {
        [Required]
        public string Id { get; set; }
    }

   
}
