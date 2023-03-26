using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MvcMovie.Authors;

public class CreateUpdateAuthorDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
}
