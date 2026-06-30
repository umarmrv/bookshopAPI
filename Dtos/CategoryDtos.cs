using System.ComponentModel.DataAnnotations;

namespace BookSHopCsharp.Dtos;


    
public class CategoryCreateUpdateDto
{
    public string Name { get; set; } = string.Empty;
}

public class CategoryReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}