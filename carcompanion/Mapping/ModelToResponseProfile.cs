using AutoMapper;
using carcompanion.Contract.V1.Responses;
using carcompanion.Models;

namespace carcompanion.Mapping
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Car, CreateCarResponse>();
            CreateMap<Car, GetCarByIdResponse>();

            CreateMap<Expense, GetCarExpensesResponse>();
            CreateMap<Expense, CreateExpenseResponse>();
        }
    }
}