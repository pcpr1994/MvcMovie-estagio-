using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Genres;
using MvcMovie.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace MvcMovie.Web.Pages.Movies;

public class IndexModel : MvcMoviePageModel
{
    private readonly IMovieAppService _movieAppService;

    [BindProperty]
    public MovieAuthorViewModel MovieAuthor { get; set; }

    public IndexModel(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    public List<SelectListItem> Authors { get; set; }


    //public void OnGet()
    //{
    //}

    public async Task OnGetAsync()
    {

        var authorLookup = await _movieAppService.GetAuthorLookupAsync();
        Authors = authorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();


    }

    //public async Task<IActionResult> OnPostAsync(Guid movieAuthor)
    //{

    //    await _movieAppService.GetMAListAsyncMovieAuthor(movieAuthor);
     
    //    return NoContent();

    //}

    public class MovieAuthorViewModel
    {
        //public List<Movie>? Movies { get; set; }

        public SelectList Authors { get; set; }
        public int? MovieAuthor { get; set; }
        //public string? SearchString { get; set; }
    }
}
