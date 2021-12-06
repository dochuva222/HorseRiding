namespace HorseRiding.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HorseQuantity { get; set; }
        public System.DateTime RentDate { get; set; }
        public System.TimeSpan RentTime { get; set; }
        public int TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual User User { get; set; }
    }
}
