using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Log
{
    public class GetAllLogsResponse : IResponseData
    {  
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<AdminLogResponse> Logs { get; set; }        
    }
}