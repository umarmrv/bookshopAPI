namespace BookSHopCsharp.Models;
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    //one author keep many books here is relation one to many 
    public  List<Book> Books { get; set; }
}