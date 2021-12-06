using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace HorseRiding.Models
{
    public partial class Horse
    {
        [JsonIgnore]
        public ImageSource HorseImage
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(this.Image));
            }
        }
    }
}
