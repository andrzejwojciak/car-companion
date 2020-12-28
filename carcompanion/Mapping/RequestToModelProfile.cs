using AutoMapper;
using carcompanion.Contract.Security.Requests;
using carcompanion.Contract.V1.Requests.Car;
using carcompanion.Contract.V1.Requests.Expense;
using carcompanion.Models;

namespace carcompanion.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<CreateCarRequest, Car>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PatchCarRequest, Car>()
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom((src, dest) => (src.ProductionYear == null ? dest.ProductionYear : src.ProductionYear)))                
                .ForMember(dest => dest.Mileage, opt => opt.MapFrom((src, dest) => (src.Mileage == null ? dest.Mileage : src.Mileage)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => srcMember != null));
                
            CreateMap<PutCarRequest, Car>();


            CreateMap<CreateExpenseRequest, Expense>();       

            CreateMap<PutExpenseRequest, Expense>();

            //TODO: I lost a huge part of the day and still I don't know why 
            //      automapper is ignoring condition and map int?: null to int?: 0
            CreateMap<PatchExpenseRequest, Expense>()
                .ForMember(dest => dest.MileageInterval, opt => opt.MapFrom((src, dest) => (src.MileageInterval == null ? dest.MileageInterval : src.MileageInterval)))                
                .ForMember(dest => dest.Amount, opt => opt.MapFrom((src, dest) => (src.Amount == null ? dest.Amount : src.Amount)))
                .ForMember(dest => dest.EndOfDateInterval, opt => opt.MapFrom((src, dest) => (src.EndOfDateInterval == null ? dest.EndOfDateInterval : src.EndOfDateInterval)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => srcMember != null));
            

            CreateMap<RegisterRequest, User>();
        }
    }
}