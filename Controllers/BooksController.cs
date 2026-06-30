using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSHopCsharp.Dtos;
using BookSHopCsharp.Models;
using BookSHopCsharp;



// 1. Указываем атрибуты контроллера (Аналог @api_view или APIView в DRF)
[ApiController]
[Route("api/[controller]")] // Автоматически превратит маршрут в api/books
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    // 2. Через DI просим "кухню" выдать нам пульт управления базой данных
    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // === ЭНДПОИНТ 1: ПОЛУЧИТЬ ВСЕ КНИГИ (GET api/books) ===
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Используем ПРОЕКЦИЮ через .Select(). 
        // Здесь мы руками связываем данные из таблицы с нашим легковесным DTO!
        var booksDto = await _context.Books
            .Select(b => new BookReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                AuthorName = b.Author.Name // EF Core сам сделает JOIN под капотом!
            })
            .ToListAsync(); // Только здесь запрос улетает в базу (IQueryable -> List)

        return Ok(booksDto); // Возвращаем статус 200 и наш красивый список JSON
    }

    // === ЭНДПОИНТ 3: ПОЛУЧИТЬ ОДНУ КНИГУ ПО ID (GET api/books/1) ===

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var bookDto = await _context.Books
            .Where(b => b.Id == id)
            .Select(b => new BookReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                AuthorName = b.Author.Name
            }).FirstOrDefaultAsync();


        if (bookDto == null)
        {
            return NotFound($"The book with id {id} was not found.");

        }

        return Ok(bookDto);
    }





    // === ЭНДПОИНТ 2: СОЗДАТЬ КНИГУ (POST api/books) ===
    [HttpPost]
    public async Task<IActionResult> Create(BookCreateDto dto)
    {
        // 3. ПРОВЕРКА СВЯЗИ: База данных требует модель AuthorId, но существует ли такой автор?
        var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
        var categoryExists = await _context.Category.AnyAsync(c => c.Id == dto.CategoryIds.FirstOrDefault());
        if (!authorExists && !categoryExists)
        {
            return BadRequest("Такого автора не существует в базе данных!");
        }

        // 4. РУЧНОЙ МАППИНГ: Перекладываем данные из DTO в реальную модель базы данных Book
        var newBook = new Book
        {
            Title = dto.Title,
            Price = dto.Price,
            AuthorID = dto.AuthorId,

        };

        // 3. СВЯЗЫВАНИЕ КАТЕГОРИЙ: Если клиент передал ID категорий, ищем их в базе
        if (dto.CategoryIds != null && dto.CategoryIds.Any())
        {
            // Достаем из базы только те категории, чьи ID пришли в DTO
            var categories = await _context.Category
                .Where(c => dto.CategoryIds.Contains(c.Id))
                .ToListAsync();

            // Добавляем найденные категории в навигационное свойство книги
            newBook.Categories.AddRange(categories);
        }

        // 5. Говорим EF Core: "Подготовь эту книгу к добавлению"
        _context.Books.Add(newBook);

        // 6. Накатываем изменения на реальную базу данных
        await _context.SaveChangesAsync();

        // СЮДА: Перекладываем данные в чистый плоский объект без циклов
        var response = new 
        {
            Id = newBook.Id,
            Title = newBook.Title,
            Price = newBook.Price,
            AuthorId = newBook.AuthorID,
            // Вытаскиваем только имена добавленных категорий, чтобы не было бесконечной вложенности
            Categories = newBook.Categories.Select(c => c.Name).ToList()
        };

        // Возвращаем статус 201 (Created) и безопасный объект response
        return StatusCode(201, response);
    }
}

 




