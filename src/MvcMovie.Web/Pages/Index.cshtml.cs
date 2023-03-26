using Abp.Collections;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.ObjectMapping;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using PagedList;
using PagedList.Mvc;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcMovie.Web.Pages;

public class IndexModel : MvcMoviePageModel
{

    //[BindProperty]
    //public ViewMoviesViewModal ViewMovie { get; set; }

    [BindProperty]
    public PagedResultDto<MovieDto> MoviesIndex { get; set; }

    //[BindProperty]
    //public MoviePagination PageMovie { get; set; }

    //[BindProperty]
    //public GetMovieListDto Input { get; set; }

    //[BindProperty(Name = "currentPage")]
    //public int CurrentPage { get; set; }

    [BindProperty]
    public PagerModel PagerModel { get; set; }

    public int PageSize { get; set; }


    //[BindProperty]
    //public int Page { get; set; }

    private readonly IMovieAppService _movieAppService;

    public IndexModel(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }



    //public async Task IndexAsync(GetMovieListDto input, string sortOrder, string currentFilter, string searchString, int? page)
    //{

    //    if (CurrentUser.IsAuthenticated)
    //    {

    //        input.MaxResultCount = 3;
    //        input.SkipCount = 0;



    //        ViewComponen.CurrentSort = sortOrder;

    //        var listMovie = await _movieAppService.GetAllMovies();

    //        if(!String.IsNullOrEmpty(searchString))
    //        {
    //            listMovie = (List<MovieDto>)listMovie.Where(s =>  s.Title.Contains(searchString));
    //        }

    //        // get the data to be paginated
    //        MoviesIndex = await _movieAppService.GetListAsync(input);



    //        // create a paginated list
    //        //var pagedList = new PagedList<MovieDto>(input);

    //        // pass the paginated list to the view
    //    }

    //    }



    public async Task OnGetAsync(GetMovieListDto input, int currentPage, string sort, int? page, int PageSize)
    {
        if (CurrentUser.IsAuthenticated)
        {

            var listMovie = await _movieAppService.GetAllMovies();
           
            if(PageSize == null || PageSize == 0)
            {
                PageSize = 5;

            }
          
            //define quantos elementos aparece por pagina
             int elementPage = PageSize;
            input.MaxResultCount = elementPage;
            //faz o calculo de quanto elemento aparecem na pagina
            input.SkipCount = (currentPage > 0 ? currentPage - 1 : 0) * elementPage;


            MoviesIndex = await _movieAppService.GetListAsync(input);

            long totalCount = MoviesIndex.TotalCount;
            int totalmovie = (int)MoviesIndex.TotalCount;


         
            
            //funciona
            PagerModel = new PagerModel(totalCount, input.MaxResultCount, currentPage, elementPage, "/", sort);


            /*MoviesIndex = await _movieAppService.GetListAsync(new GetMovieListDto { MaxResultCount = 2, SkipCount = 0 , Filter = filter});
            
            //var pagedList = new PagedList<MovieDto>(MoviesIndex, pageNumber, pageSize);


            //MoviesIndex = movieDto;

            //MoviesIndex = ObjectMapper.Map<PagedResultDto< MovieDto>, PagedResultDto< ViewMoviesViewModal>>(movieget);

            //ViewMovie = ObjectMapper.Map<List<MovieDto>, List<ViewMoviesViewModal>>(listMovie);*/

        }

        /*fazer um mettodo para ir buscar todos os movies a tabela
        //var movieDto = await _movieAppService.GetListAsync(new GetMovieListDto { MaxResultCount = 50, SkipCount = 0 });

        //ViewMovie2 = ObjectMapper.Map<MovieDto, ViewMoviesViewModal>(movieDto);*/
    }

//    public class GetMoviesInputDto : PagedAndSortedResultRequestDto
//    {
//        public string Filter { get; set; }
    
//}

    public class MoviePagination : PagerModel
    {
        public MoviePagination(long totalCount, int shownItemsCount, int currentPage, int pageSize, string pageUrl, List<PageItem> items, string sort = null)
             : base(totalCount, shownItemsCount, currentPage, pageSize, pageUrl, sort)
        {
            Pages = items;
        }
    }


    public class ViewMoviesViewModal
    {
        [HiddenInput]
        public Guid Id { get; set; }

        public Guid MovieAuthor { get; set; }
        public string Title { get; set; }

        public string GenreDescription { get; set; }

        public Guid Genreid { get; set; }

        public Guid Authorid { get; set; }

        public DateTime ReleaseDate { get; set; }

        //public string Description { get; set; }

        public string AuthorName { get; set; }

        public float Price { get; set; }

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


}
