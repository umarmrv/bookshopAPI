using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSHopCsharp.Dtos;
using BookSHopCsharp.Models;
using BookSHopCsharp;



//giving a atribute of controller 

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    private readonly ApplicationDbContext _context; // connting to the Db from AppContext.cs
    
    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _context.Authors.Select(a => new AuthorGetDto { Id = a.Id, Name = a.Name }).ToListAsync();
            
        return Ok(authors); 
    }



    [HttpPost]
    public async Task<IActionResult> Create(AuthorCreateDto authorCreateDto)
    {
        var newauthor = new Author()
        {
            Name = authorCreateDto.Name
        };
        
        _context.Authors.Add(newauthor);
        await _context.SaveChangesAsync();
        return Ok(newauthor);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var author  = await _context.Authors.FindAsync(id);

         if (author == null)
         {
             return NotFound($"We don't have such author  with ID : {id} ");
         }
         
         _context.Authors.Remove(author);
         
         await _context.SaveChangesAsync();
         return NoContent(); // статус 204 No Content (Стандарт для успешного удаления в REST API)
         
    }
    
    
}