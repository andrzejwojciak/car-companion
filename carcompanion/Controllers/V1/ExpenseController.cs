using System;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Contract.V1.Responses;
using carcompanion.Extensions;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using carcompanion.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.V1.ApiRoutes;
using carcompanion.Contract.V1.Responses.Expense;

namespace carcompanion.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        private readonly IExpenseService _expenseService;

        public ExpenseController(IMapper mapper, ICarService carService, IExpenseService expenseService)
        {
            _carService = carService;
            _expenseService = expenseService;
            _mapper = mapper;            
        }

        [HttpGet(Expenses.GetCarExpenseById)]
        public async Task<ActionResult> GetCarExpenseById([FromRoute] Guid carId, Guid expenseId)
        {
            var result = await _expenseService.GetExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        [HttpPost(Expenses.CreateCarExpense)]
        public async Task<ActionResult> CreateCarExpense([FromRoute] Guid carId, [FromBody] CreateExpenseRequest request)
        {
            var result = await _expenseService.CreateExpenseAsync(carId, HttpContext.GetUserId(), _mapper.Map<Expense>(request));
            return GenerateResponse(result);
        }
        
        [HttpGet(Expenses.GetCarExpesnes)]
        public async Task<ActionResult> GetCarExpenses([FromRoute] Guid carId)
        {
            var result = await _expenseService.GetExpensesByCarIdAsync(carId, HttpContext.GetUserId());        
            return GenerateResponse(result);    
        }

        [HttpDelete(Expenses.DeleteCarExpenseById)]
        public async Task<IActionResult> DeleteExpenseById([FromRoute] Guid carId, Guid expenseId)
        {
            var result = await _expenseService.DeleteExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId());      
            return GenerateResponse(result);    
        }

        private ActionResult GenerateResponse(ServiceResult result)
        {
            if(!result.Success)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }
                        
            return StatusCode(result.StatusCode, result.ResponseData);
        }
    }
}