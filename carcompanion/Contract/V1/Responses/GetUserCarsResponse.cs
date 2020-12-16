using System.Collections;
using System.Collections.Generic;
using carcompanion.Models;

namespace carcompanion.Contract.V1.Responses
{
    public class GetUserCarsResponse
    {
        public string UserId { get; set; }
        public IEnumerable<GetCarByIdResponse> Cars { get; set; }        
    }
}