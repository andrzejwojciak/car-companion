using System;

namespace carcompanion.Contract.V1.Responses.Log
{
    public class UserLogResponse
    {        
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string ClientIp { get; set; }
    }
}