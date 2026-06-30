using System.ComponentModel.DataAnnotations;

namespace BookSHopCsharp.Dtos;

// 1. Этот класс — аналог сериализатора на создание (BookCreateSerializer)
// Здесь мы используем Data Annotations для валидации (как в Django поля базы/сериализатора)
public class BookCreateDto
{
    [Required(ErrorMessage = "Название книги обязательно")]
    [MaxLength(100, ErrorMessage = "Название не может быть длиннее 100 символов")]
    public string Title { get; set; }

    [Range(1, 10000, ErrorMessage = "Цена должна быть от 1 до 10000")]
    public decimal Price { get; set; }

    [Required]
    public int AuthorId { get; set; } // К какому автору привязать книгу

    public List<int> CategoryIds { get; set; } = new();
}

// 2. Этот класс — аналог сериализатора на чтение (BookReadSerializer)
// Мы отдаем клиенту красивый плоский объект, где вместо ID автора будет сразу его Имя!
public class BookReadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string AuthorName { get; set; } // Избавились от лишних вложенных объектов!

    public List<string> Categories { get; set; } = new();
}