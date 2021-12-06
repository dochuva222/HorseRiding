using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRiding.Models
{
    public partial class Trainer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public byte[] Image { get; set; }
        public int TrainingTypeId { get; set; }
        public System.DateTime ExperienceDate { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual TrainingType TrainingType { get; set; }
    }
}
