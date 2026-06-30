namespace BookSHopCsharp.Models;


public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    
    public int AuthorID  { get; set; }
    //setting up models.ForeignKey from author ?? 
    public Author Author { get; set; }
    
    
    // making many to many set 
    
    public List<Category> Categories { get; set; } = new();
    
}