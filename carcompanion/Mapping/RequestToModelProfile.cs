using AutoMapper;
using carcompanion.Contract.V1.Requests;
using carcompanion.Models;

namespace carcompanion.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<CreateCarRequest, Car>();
        }
    }
}