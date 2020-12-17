using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Contract.V1.Responses;
using carcompanion.Extensions;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using carcompanion.Results;
using carcompanion.Results.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.V1.ApiRoutes;

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
            var expense = await _expenseService.GetExpenseById(expenseId);
            
            if(expense == null)
                return NotFound( new { errorMessage = "Expense doesn't exist" });

            if(expense.Car.CarId != carId)
                return BadRequest( new { errorMessage = "Car doesn't have that expense" });

            var response = _mapper.Map<GetCarExpensesResponse>(expense);

            return Ok(_mapper.Map<GetCarExpensesResponse>(expense));
        }
        
        //TODO: Need to be fixed. Every one can get car expenses!
        [HttpGet(Expenses.GetCarExpesnes)]
        public async Task<ActionResult> GetCarExpenses([FromRoute] Guid carId)
        {
            var car = await _carService.GetCarWithExpesnesByIdAsync(carId);

            if(car == null)
                return NotFound($"Car {carId} doesn't exists");
            
            if(!car.Expenses.Any())
                return Ok("Car doesn't have any expenses");

            var response = _mapper.Map<ICollection<GetCarExpensesResponse>>(car.Expenses);
            return Ok(response);
        }

        [HttpPost(Expenses.CreateCarExpense)]
        public async Task<ActionResult> CreateCarExpense([FromRoute] Guid carId, [FromBody] CreateExpenseRequest request)
        {
            var userCar = await _carService.GetUserCarByIdsAsync(HttpContext.GetUserId(), carId);

            if(userCar.Car == null)
                return NotFound( new { errorMessage = $"Car {carId} doesn't exist or user doesn't have that car" });
            
            var newExpense = _mapper.Map<Expense>(request);

            var added = await _expenseService.AddExpenseAsync(userCar.User, userCar.Car, newExpense);

            if(added == false)
                return BadRequest();

            var response = _mapper.Map<CreateExpenseResponse>(newExpense);

            return Ok(response);
        }

        [HttpDelete(Expenses.DeleteCarExpenseById)]
        public async Task<IActionResult> DeleteExpenseById([FromRoute] Guid carId, Guid expenseId)
        {
            var result = await _expenseService.DeleteExpenseByIdAsync(carId, expenseId, HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        private IActionResult GenerateResponse(IServiceResult result)
        {
            if(!result.Success)
            {
                if(result.ErrorMessage == null)
                    result.ErrorMessage = "Something went wrong";

                return StatusCode(result.StatusCode, result.ErrorMessage);
            }
            
            return Ok();
        }        
        
    }
}