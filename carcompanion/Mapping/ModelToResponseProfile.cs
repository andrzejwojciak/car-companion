using AutoMapper;
using carcompanion.Contract.V1.Responses.Expense;
using carcompanion.Contract.V1.Responses.Car;
using carcompanion.Models;
using System;

namespace carcompanion.Mapping
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Expense, ExpenseResponse>()
                .ForMember(x => x.Date,
                    opt => opt.MapFrom(src => src.Date == null ? null : ((DateTime) src.Date).ToString("yyyy-MM-dd")))
                .ForMember(x => x.EndOfDateInterval,
                    opt => opt.MapFrom(src =>
                        src.EndOfDateInterval == null
                            ? null
                            : ((DateTime) src.EndOfDateInterval).ToString("yyyy-MM-dd")));

            CreateMap<Car, GetExpensesByCarIdResponse>();

            CreateMap<Car, CreateCarResponse>()
                .ForMember(x => x.Mileage, opt => opt.MapFrom(src => src.Mileage == null ? null : src.Mileage));

            CreateMap<Car, GetCarByIdResponse>();
            CreateMap<Car, UpdateCarResponse>();
            CreateMap<UserCar, GetCarByIdResponse>();

            CreateMap<ExpenseCategory, ExpenseCategoryResponse>();
        }
    }
}