using System.Collections;
using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Models;

namespace carcompanion.Contract.V1.Responses.Car
{
    public class GetUserCarsResponse : IResponseData
    {
        public string UserId { get; set; }
        public IEnumerable<GetCarByIdResponse> Cars { get; set; }        
    }
}