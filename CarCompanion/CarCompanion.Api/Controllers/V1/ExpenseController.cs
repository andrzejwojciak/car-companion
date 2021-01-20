using System;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Extensions;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using carcompanion.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.V1.ApiRoutes;
using carcompanion.Contract.V1.Requests.Expense;

namespace carcompanion.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost(Expenses.CreateCarExpense)]
        public async Task<ActionResult> CreateCarExpense([FromRoute] Guid carId,
            [FromBody] CreateExpenseRequest request)
        {
            var result =
                await _expenseService.CreateExpenseAsync(carId, HttpContext.GetUserId(), _mapper.Map<Expense>(request));
            return GenerateResponse(result);
        }

        [HttpGet(Expenses.GetCarExpense)]
        public async Task<ActionResult> GetCarExpense([FromRoute] Guid carId, Guid expenseId)
        {
            var result = await _expenseService.GetExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        [HttpGet(Expenses.GetCarExpesnes)]
        public async Task<ActionResult> GetCarExpenses([FromRoute] Guid carId)
        {
            var result = await _expenseService.GetExpensesByCarIdAsync(carId, HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        [HttpGet(Expenses.GetCategories)]
        public async Task<IActionResult> GetExpenseCategories()
        {
            var result = await _expenseService.GetExpenseCategoriesAsync();
            return GenerateResponse(result);
        }

        [HttpPut(Expenses.PutCarExpense)]
        public async Task<IActionResult> PutExpense([FromRoute] Guid carId, Guid expenseId,
            [FromBody] PutExpenseRequest request)
        {
            var result =
                await _expenseService.UpdateExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId(), request);
            return GenerateResponse(result);
        }

        [HttpPatch(Expenses.PatchCarExpense)]
        public async Task<IActionResult> PatchExpense([FromRoute] Guid carId, Guid expenseId,
            [FromBody] PatchExpenseRequest request)
        {
            var result =
                await _expenseService.UpdateExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId(), request);
            return GenerateResponse(result);
        }

        [HttpDelete(Expenses.DeleteCarExpense)]
        public async Task<IActionResult> DeleteExpense([FromRoute] Guid carId, Guid expenseId)
        {
            var result = await _expenseService.DeleteExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        private ActionResult GenerateResponse(ServiceResult result)
            => !result.Success
                ? StatusCode(result.Status, result.ErrorMessage)
                : StatusCode(result.Status, result.ResponseData);
    }
}