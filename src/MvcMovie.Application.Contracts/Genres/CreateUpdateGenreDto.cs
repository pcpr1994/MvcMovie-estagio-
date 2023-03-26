using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcMovie.Genres;

public class CreateUpdateGenreDto
{
    [Required]
    public string Description { get; set; }
}
