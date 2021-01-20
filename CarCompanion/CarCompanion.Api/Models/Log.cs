using System;

namespace carcompanion.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        public string UserId { get; set; }
        public string ClientIp { get; set; }
    }
}