using AutoMapper;
using carcompanion.Contract.V1.Responses.Expense;
using carcompanion.Contract.V1.Responses.Car;
using carcompanion.Models;

namespace carcompanion.Mapping
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<Car, GetExpensesByCarIdResponse>();
            
            CreateMap<Car, CreateCarResponse>();
            CreateMap<Car, GetCarByIdResponse>();
            CreateMap<Car, UpdateCarResponse>();
        }
    }
}