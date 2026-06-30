
//dotnet add package Swashbuckle.AspNetCore  --  have to download this for swagger 

using BookSHopCsharp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === 1. РЕГИСТРАЦИЯ СЕРВИСОВ (DI КОНТЕЙНЕР) ===
// Добавляем поддержку контроллеров (аналог Views в Django)
builder.Services.AddControllers();


// РЕГИСТРИРУЕМ НАШУ БАЗУ ДАННЫХ (Кладем пульт на кухню как Scoped)
// Говорим: "Используй SQLite, а файл базы назови bookshop.db"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=bookshop.db"));




// Добавляем генератор Swagger (генерация документации API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// === 2. НАСТРОЙКА MIDDLEWARE PIPELINE (КОНВЕЙЕР) ===
// Если проект запущен в режиме разработки, включаем Swagger интерфейс
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Это создаст красивую страничку по адресу /swagger
}


// Мидлварь для перенаправления с http на https
app.UseHttpsRedirection();

// Мидлварь маршрутизации (определяет, какой контроллер вызвать)
app.UseRouting();

// Мидлварь авторизации
app.UseAuthorization();

// Конец конвейера — маппинг контроллеров к маршрутам
app.MapControllers();

// Запуск Kestrel сервера
app.Run();