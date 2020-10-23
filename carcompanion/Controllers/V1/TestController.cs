using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Contract.V1.Responses;
using carcompanion.Data;
using carcompanion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static carcompanion.Contract.V1.ApiRoutes.Tests;

namespace carcompanion.Controllers.V1
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TestController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(GetAll)]
        public async Task<ActionResult> GetTests()
        {
            var tests = await _context.Tests.ToListAsync();
            
            if(tests != null) 
                return Ok(tests);

            return NotFound("There is no tests");
        }

        [HttpPost(Create)]
        public async Task<ActionResult<string>> CreateTest([FromBody] CreateTestRequest request)
        {
            var newTest = _mapper.Map<Test>(request);
            await _context.Tests.AddAsync(newTest);

            if(await _context.SaveChangesAsync() > 0)                 
                return Ok(_mapper.Map<CreateTestResponse>(newTest));

            return BadRequest();
        }

        [HttpGet(GetTestById)]
        public async Task<ActionResult> GetTest([FromRoute] int id)
        {
            var test = await GetTestByIAsync(id);
            
            if(test != null)
                return Ok(test);

            return NotFound("There is no test like you are looking for");
        }

        [HttpDelete(Delete)]
        public async Task<ActionResult> DeleteTest([FromRoute] int id)
        {
            var test = await GetTestByIAsync(id);
            
            if(test == null)
                return NotFound("There is no test like you are looking for");

            _context.Tests.Remove(test);

            if(await _context.SaveChangesAsync() > 0)
                return(Ok(new { Message = "Test deleted"}));

            return BadRequest(new { Error = "Something went wrong" });
        }

        [HttpPut(Update)]
        public async Task<ActionResult> UpdateTest([FromRoute] int id, [FromBody] UpdateTestRequest request)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(x => x.Id == id);
            
            if(test == null)
                return NotFound("There is no test like you are looking for");

            _mapper.Map(request, test);

            if(await _context.SaveChangesAsync() > 0)
                return(Ok(new { Message = "Test updated"}));
                
            return BadRequest(new { error = "Something went wrong"});
        }

        private async Task<Test> GetTestByIAsync(int id)
        {
            return await _context.Tests.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}