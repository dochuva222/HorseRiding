using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRiding.Models
{
    public partial class UserTraining
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingTypeId { get; set; }
        public int TrainerId { get; set; }
        public int HorseId { get; set; }
        public System.DateTime TrainingDate { get; set; }
        public System.TimeSpan TrainingTime { get; set; }

        public virtual Horse Horse { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual TrainingType TrainingType { get; set; }
        public virtual User User { get; set; }
    }
}
