using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System;
using XamarinFirebase.Adapter;
using System.Collections.Generic;
using XamarinFirebase.Data_Models;
using XamarinFirebase.Fragments;
using XamarinFirebase.EventListeners;
using System.Linq;

namespace XamarinFirebase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView addButton;
        ImageView searchButton;
        EditText searchText;
        RecyclerView myRecyclerView;
        List<Alumini> AluminiList;

        AddAluminFragment addAluminFragment;
        AluminListeners aluminListener;

        AluminiAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            myRecyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);
            searchButton = (ImageView)FindViewById(Resource.Id.searchButton);
            addButton = (ImageView)FindViewById(Resource.Id.addNewButton);
            searchText = (EditText)FindViewById(Resource.Id.searchText);

            searchText.TextChanged += SearchText_TextChanged;
            searchButton.Click += SearchButton_Click;
            addButton.Click += AddButton_Click;

            RetriveData();
          
        }

        private void SearchText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Alumini> SearchResult =
                (from alumini in AluminiList
                 where alumini.FullName.ToLower().Contains(searchText.Text.ToLower()) || alumini.Set.ToLower().Contains(searchText.Text.ToLower()) ||
                 alumini.Department.ToLower().Contains(searchText.Text.ToLower()) || alumini.Status.ToLower().Contains(searchText.Text.ToLower())
                 select alumini).ToList();

            adapter = new AluminiAdapter(SearchResult);
            myRecyclerView.SetAdapter(adapter);
                 
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            addAluminFragment = new AddAluminFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            addAluminFragment.Show(trans, "new alumini");
        }

        private void SetupRecyClerView()
        {
            myRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(myRecyclerView.Context));
            adapter = new AluminiAdapter(AluminiList);
            adapter.DeleteItemClick += Adapter_DeleteItemClick;
            adapter.ItemLongClick += Adapter_ItemLongClick;
            myRecyclerView.SetAdapter(adapter);
            
        }

        private void Adapter_ItemLongClick(object sender, AluminiAdapterClickEventArgs e)
        {
            Alumini thisAlumini = AluminiList[e.Position];
            EditAluminiFragment editAluminiFragment = new EditAluminiFragment(thisAlumini);
            var trans = SupportFragmentManager.BeginTransaction();
            editAluminiFragment.Show(trans, "edit");
        }

        private void Adapter_DeleteItemClick(object sender, AluminiAdapterClickEventArgs e)
        {
            string key = AluminiList[e.Position].ID;
            Android.Support.V7.App.AlertDialog.Builder deleteAlumini = new Android.Support.V7.App.AlertDialog.Builder(this);
            deleteAlumini.SetTitle("Delete Alumini");
            deleteAlumini.SetMessage("Are you sure?");
            deleteAlumini.SetPositiveButton("Continue", (deleteAlert, args) =>
            {
                // Deletes Alumini From the Database
                aluminListener.DeleteAlumini(key);
            });

             deleteAlumini.SetNegativeButton("Cancel", (deleteAlert, args) =>
             {
                 deleteAlumini.Dispose();
             });

            deleteAlumini.Show();
        }

        public void CreateData()
        {
            AluminiList = new List<Alumini>();
            AluminiList.Add(new Alumini { Department = "Department of Computer Science", FullName = "Mark Edwardds", ID = "1", Set = "2011", Status = "Graduated" });
            AluminiList.Add(new Alumini { Department = "Department of Civil Engineering", FullName = "Jonh Doe", ID = "2", Set = "2014", Status = "Graduated" });

            AluminiList.Add(new Alumini { Department = "Department of King Studies", FullName = "John Snow", ID = "3", Set = "2012", Status = "Drop Out" });
            AluminiList.Add(new Alumini { Department = "Department of History", FullName = "Uchenna Nnodim", ID = "4", Set = "2016", Status = "Graduated" });
            AluminiList.Add(new Alumini { Department = "Department of Computer Science", FullName = "Gregg Williams", ID = "5", Set = "2014", Status = "Failed" });

        }

        public void RetriveData()
        {
            aluminListener = new AluminListeners();
            aluminListener.Create();
            aluminListener.AluminRetrived += AluminListener_AluminRetrived;
        }

        private void AluminListener_AluminRetrived(object sender, AluminListeners.AluminDataEventArgs e)
        {
            AluminiList = e.Alumini;
            SetupRecyClerView();
        }

        private void SearchButton_Click(object sender, System.EventArgs e)
        {
            if(searchText.Visibility == Android.Views.ViewStates.Gone)
            {
                searchText.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                searchText.ClearFocus();
                searchText.Visibility = Android.Views.ViewStates.Gone;
            }
        }
    }
}