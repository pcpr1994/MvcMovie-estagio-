using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MvcMovie.Genres;

public class GetGenreListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
