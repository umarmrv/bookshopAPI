using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSHopCsharp.Dtos;
using BookSHopCsharp.Models;
using BookSHopCsharp;


[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;  // connection into the DBase 
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var categories = await _context.Category
            .Select(b => new CategoryReadDto()
            {
                Id = b.Id,
                Name = b.Name

            }).ToListAsync();

        return Ok(categories);


    }


    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateUpdateDto categoryCreateUpdateDto)
    {
        var newCategory = new Category()
        {
            Name = categoryCreateUpdateDto.Name
        };
        
        _context.Category.Add(newCategory);
        
        await _context.SaveChangesAsync();
        return Ok(newCategory);

    }    
    
}