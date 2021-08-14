using System;
namespace PropertyManager.DTOs
{
    public class CommandReadDTO
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }
        //Implementation detail that is not useful for our client is removed
        //public string Platform { get; set; }
    }
}
