using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Contract.V1.Responses;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.V1.ApiRoutes;

namespace carcompanion.Controllers.V1
{
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
        public async Task<ActionResult> CreateCarExpenses([FromRoute] Guid carId, [FromBody] CreateExpenseRequest request)
        {
            var car = await _carService.GetCarByIdAsync(carId);

            if(car == null)
                return NotFound($"Car {carId} doesn't exist");
            
            var newExpense = _mapper.Map<Expense>(request);
            
            var added = await _expenseService.AddExpenseAsync(car, newExpense);

            if(added == false)
                return BadRequest();

            return Ok(_mapper.Map<CreateExpenseResponse>(newExpense));
        }
    }
}