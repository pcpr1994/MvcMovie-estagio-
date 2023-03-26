using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Movies;

public class GetMovieListDto : PagedAndSortedResultRequestDto
{
    public Guid? MovieAuthor { get; set; }
    public string Filter { get; set; }
}
