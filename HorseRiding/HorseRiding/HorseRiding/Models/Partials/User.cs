using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace HorseRiding.Models
{
    public partial class User
    {
        [JsonIgnore]
        public ImageSource UserImage
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(this.Image));
            }
        }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{this.Lastname} {this.Firstname}";
            }
        }
    }
}
