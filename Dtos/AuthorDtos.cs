using System.ComponentModel.DataAnnotations;

namespace BookSHopCsharp.Dtos;



public class AuthorCreateDto
{
    
    [Required(ErrorMessage = "The name of Author must have ! ")]
    [MaxLength(50, ErrorMessage = "The name of Author cannot exceed 50 characters")]
    public string Name { get; set; }
    
}



public class AuthorGetDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}