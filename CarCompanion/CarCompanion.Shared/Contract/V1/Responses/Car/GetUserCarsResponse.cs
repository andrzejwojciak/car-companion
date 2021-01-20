using System.Collections;
using System.Collections.Generic;

namespace CarCompanion.Shared.Contract.V1.Responses.Car
{
    public class GetUserCarsResponse
    {
        public string UserId { get; set; }
        public IEnumerable<GetCarByIdResponse> Cars { get; set; }        
    }
}