using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HorseRiding.Models
{
    public partial class Trainer
    {
        [JsonIgnore]
        public ImageSource TrainerImage
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(this.Image));
            }
        }

        [JsonIgnore]
        public string Experience
        {
            get
            {
                var exp = DateTime.Now.Year - this.ExperienceDate.Year;
                if (exp.ToString().Length < 2)
                {
                    if (exp.ToString().LastOrDefault() == '1')
                        return $"Стаж: {exp} год";
                    if (exp.ToString().LastOrDefault() == '2' || exp.ToString().LastOrDefault() == '3' || exp.ToString().LastOrDefault() == '4')
                        return $"Стаж: {exp} года";
                    return $"Стаж: {exp} лет";
                }
                else
                    return $"Стаж: {exp} лет";

            }
        }
    }
}
