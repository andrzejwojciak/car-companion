using AutoMapper;
using carcompanion.Contract.Security.Requests;
using carcompanion.Contract.V1.Requests;
using carcompanion.Models;

namespace carcompanion.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<CreateCarRequest, Car>();
            CreateMap<CreateExpenseRequest, Expense>();
    
            //security
            CreateMap<RegisterRequest, User>();
        }
    }
}