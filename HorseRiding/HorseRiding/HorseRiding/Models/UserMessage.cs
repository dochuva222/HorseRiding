using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRiding.Models
{
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public System.DateTime Date { get; set; }
    }
}
