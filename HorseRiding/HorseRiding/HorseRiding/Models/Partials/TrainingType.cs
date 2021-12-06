using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace HorseRiding.Models
{
    public partial class TrainingType
    {
        [JsonIgnore]
        public ImageSource TrainingTypeImage
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(this.Image));
            }
        }
    }
}
