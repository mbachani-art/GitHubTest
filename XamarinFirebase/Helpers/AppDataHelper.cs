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
using Firebase;
using Firebase.Database;

namespace XamarinFirebase.Helpers
{
   public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase()
        {

            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if(app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("xamarinfirebase-12912")
                    .SetApiKey("AIzaSyDH0Fs3pF14HQL_ibQ6CAx_3nKHGDZ2348")
                    .SetDatabaseUrl("https://xamarinfirebase-12912.firebaseio.com")
                    .SetStorageBucket("xamarinfirebase-12912.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }

            return database;
        }
    }
}