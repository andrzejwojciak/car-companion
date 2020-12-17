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
            CreateMap<CreateCarRequest, Car>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateExpenseRequest, Expense>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<PatchCarRequest, Car>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => (srcMember != null) && !srcMember.Equals(0)));
                
            CreateMap<PutCarRequest, Car>();
                
            //security
            CreateMap<RegisterRequest, User>();
        }
    }
}