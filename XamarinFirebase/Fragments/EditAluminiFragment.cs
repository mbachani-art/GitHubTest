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
using XamarinFirebase.Data_Models;
using XamarinFirebase.Helpers;

namespace XamarinFirebase.Fragments
{
    public class EditAluminiFragment : Android.Support.V4.App.DialogFragment
    {
        TextInputLayout fullnameText, departmentText, setText;
        Button savechangesButton;

        Alumini thisAlunini;
        public EditAluminiFragment (Alumini alumini)
        {
            thisAlunini = alumini;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);          
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            View view = inflater.Inflate(Resource.Layout.editalumini, container, false);

            fullnameText = (TextInputLayout)view.FindViewById(Resource.Id.fullnameText);
            departmentText = (TextInputLayout)view.FindViewById(Resource.Id.departmentText);
            setText = (TextInputLayout)view.FindViewById(Resource.Id.setText);
            savechangesButton = (Button)view.FindViewById(Resource.Id.submitButton);
            savechangesButton.Click += SavechangesButton_Click;

            fullnameText.EditText.Text = thisAlunini.FullName;
            departmentText.EditText.Text = thisAlunini.Department;
            setText.EditText.Text = thisAlunini.Set;

            return view;
        }

        private void SavechangesButton_Click(object sender, EventArgs e)
        {
            string fullname = fullnameText.EditText.Text;
            string department = departmentText.EditText.Text;
            string set = setText.EditText.Text;

            AppDataHelper.GetDatabase().GetReference("alumini/" + thisAlunini.ID + "/fullname").SetValue(fullname);
            AppDataHelper.GetDatabase().GetReference("alumini/" + thisAlunini.ID + "/department").SetValue(department);
            AppDataHelper.GetDatabase().GetReference("alumini/" + thisAlunini.ID + "/set").SetValue(set);

            this.Dismiss();
        }
    }
}