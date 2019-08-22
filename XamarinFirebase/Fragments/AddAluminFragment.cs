using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using FR.Ganfra.Materialspinner;
using Java.Util;
using XamarinFirebase.Helpers;
using SupportV7 = Android.Support.V7.App;

namespace XamarinFirebase.Fragments
{
    public class AddAluminFragment : Android.Support.V4.App.DialogFragment
    { 
        TextInputLayout fullnameText;
        TextInputLayout departmentText;
        TextInputLayout setText;
        MaterialSpinner statusSpinner;
        Button submitButton;

        List<string> statusList;
        ArrayAdapter<string> adapter;
        string status;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = inflater.Inflate(Resource.Layout.newalumini, container, false);

            fullnameText = (TextInputLayout)view.FindViewById(Resource.Id.fullnameText);
            departmentText = (TextInputLayout)view.FindViewById(Resource.Id.departmentText);
            setText = (TextInputLayout)view.FindViewById(Resource.Id.setText);
            statusSpinner = (MaterialSpinner)view.FindViewById(Resource.Id.statusSpinner);
            submitButton = (Button)view.FindViewById(Resource.Id.submitButton);

            submitButton.Click += SubmitButton_Click;
            SetupStatusPinner();

            return view;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string fullname = fullnameText.EditText.Text;
            string department = departmentText.EditText.Text;
            string set = setText.EditText.Text;

            HashMap aluminiInfo = new HashMap();
            aluminiInfo.Put("fullname", fullname);
            aluminiInfo.Put("department", department);
            aluminiInfo.Put("set", set);
            aluminiInfo.Put("status", status);

            SupportV7.AlertDialog.Builder saveDataAlert = new SupportV7.AlertDialog.Builder(Activity);
            saveDataAlert.SetTitle("SAVE ALUMINI INFORMATION");
            saveDataAlert.SetMessage("Are you sure?");
            saveDataAlert.SetPositiveButton("Continue", (senderAlert, args) =>
            {
                DatabaseReference newAluminRef = AppDataHelper.GetDatabase().GetReference("alumini").Push();
                newAluminRef.SetValue(aluminiInfo);
                this.Dismiss();
            });
            saveDataAlert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                saveDataAlert.Dispose();
            });

            saveDataAlert.Show();

        }

        public void SetupStatusPinner()
        {
            statusList = new List<string>();
            statusList.Add("Graduated");
            statusList.Add("Undergraduate");
            statusList.Add("Dropped Out");
            statusList.Add("Failed");

            adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, statusList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            statusSpinner.Adapter = adapter;
            statusSpinner.ItemSelected += StatusSpinner_ItemSelected;
        }

        private void StatusSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position != -1)
            {
                status = statusList[e.Position];
            }
        }
    }
}