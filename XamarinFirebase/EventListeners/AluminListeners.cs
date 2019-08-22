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
using Firebase.Database;
using XamarinFirebase.Data_Models;
using XamarinFirebase.Helpers;

namespace XamarinFirebase.EventListeners
{
    public class AluminListeners : Java.Lang.Object, IValueEventListener
    {
        List<Alumini> aluminiList = new List<Alumini>();

        public event EventHandler<AluminDataEventArgs> AluminRetrived;

        public class AluminDataEventArgs : EventArgs
        {
            public List<Alumini> Alumini { get; set; }
        }

        public void OnCancelled(DatabaseError error)
        {
           
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
           if(snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                aluminiList.Clear();
               foreach (DataSnapshot aluminiData in child)
                {
                    Alumini alumini = new Alumini();
                    alumini.ID = aluminiData.Key;
                    alumini.FullName = aluminiData.Child("fullname").Value.ToString();
                    alumini.Department = aluminiData.Child("department").Value.ToString();
                    alumini.Status = aluminiData.Child("status").Value.ToString();
                    alumini.Set = aluminiData.Child("set").Value.ToString();
                    aluminiList.Add(alumini);
                }
                AluminRetrived.Invoke(this, new AluminDataEventArgs { Alumini = aluminiList });
            }
        }

        public void Create()
        {
            DatabaseReference alumiRef = AppDataHelper.GetDatabase().GetReference("alumini");
            alumiRef.AddValueEventListener(this);
        }

        public void DeleteAlumini(string key)
        {
            DatabaseReference reference = AppDataHelper.GetDatabase().GetReference("alumini/" + key);
            reference.RemoveValue();
        }
    }
}