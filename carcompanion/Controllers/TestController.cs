using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Works";
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Test>>> GetTests()
        {
            return await _context.Tests.ToListAsync();
        }

        [HttpGet("add/{content}")]
        public async Task<ActionResult<string>> AddToDb(string content)
        {
            await _context.Tests.AddAsync(new Test {Content = content});
            await _context.SaveChangesAsync();
            return "Added to db";
        }
    }
}