// this is registration of models in Db like manager for Db 
using Microsoft.EntityFrameworkCore;
using BookShopCsharp.Models;


namespace BookSHopCsharp;




public class ApplicationDbContext : DbContext
{
    // Конструктор принимает настройки (строку подключения) из Program.cs
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    // registration of model like in Django 
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
}

