using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinFirebase.Data_Models
{
   public class Alumini
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Set { get; set; }
        public string ID { get; set; }
    }
}