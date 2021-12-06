//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HorseRidingAPI.Models
{
    using System;
    using System.Collections.Generic;
    
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
