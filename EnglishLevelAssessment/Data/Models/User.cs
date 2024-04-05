using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishLevelAssessment.Data.Models;

public partial class User
{
    public int Id { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
