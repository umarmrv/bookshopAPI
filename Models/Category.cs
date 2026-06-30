namespace BookSHopCsharp.Models;


public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Навигационное свойство: у одной категории МНОГО книг
    public List<Book> Books { get; set; } = new();
}