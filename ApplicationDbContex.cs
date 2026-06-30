// this is registration of models in Db like manager for Db 
using Microsoft.EntityFrameworkCore;
using BookSHopCsharp.Models;


namespace BookSHopCsharp;

public class ApplicationDbContext : DbContext
{
// Конструктор принимает настройки (строку подключения) из Program.cs

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Category> Category { get; set; }

    // Конфигурируем сущности при создании модели базы данных
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Заполнение авторов (оставляем как было)
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Стивен Кинг" },
            new Author { Id = 2, Name = "Джоан Роулинг" }
        );

        // НАПРАВЛЯЕМ КУРСОР ЕF СORE РУКАМИ:
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(b => b.Author) // У книги один автор
                .WithMany(a => a.Books) // У автора много книг
                .HasForeignKey(b => b.AuthorID) // Внешний ключ в C# классе
                .HasConstraintName("FK_Books_Authors"); // Имя связи в БД
        });
    }
}














//
//
//
//
// public class ApplicationDbContext : DbContext
// {
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
//         : base(options)
//     {
//     }
//
//     // registration of model like in Django 
//     public DbSet<Book> Books { get; set; }
//     public DbSet<Author> Authors { get; set; }
//     
//     
//     
//     
// }





