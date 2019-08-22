using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using XamarinFirebase.Data_Models;
using Android.Graphics;

namespace XamarinFirebase.Adapter
{
    class AluminiAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AluminiAdapterClickEventArgs> ItemClick;
        public event EventHandler<AluminiAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<AluminiAdapterClickEventArgs> DeleteItemClick; 
        List<Alumini> Items;

        public AluminiAdapter(List<Alumini>Data)
        {
            Items = Data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.aluminirow, parent, false);
           
            var vh = new AluminiAdapterViewHolder(itemView, OnClick, OnLongClick, OnDeleteClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as AluminiAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.nameText.Text = Items[position].FullName;
            holder.departmentText.Text = "Department of " + Items[position].Department;
            holder.statusText.Text = Items[position].Status;
            holder.setText.Text = "SET " + Items[position].Set;
            if(Items[position].Status == "Graduated")
            {
                holder.statusText.SetTextColor(Color.Rgb(9, 155, 11));
            }
            else if (Items[position].Status == "Undergraduate")
            {
                holder.statusText.SetTextColor(Color.Rgb(238, 134, 31));
            }
            else if (Items[position].Status == "Failed")
            {
                holder.statusText.SetTextColor(Color.Red);
            }
            else if (Items[position].Status == "Dropped Out")
            {
                holder.statusText.SetTextColor(Color.Maroon);
            }

        }

        public override int ItemCount => Items.Count;

        void OnClick(AluminiAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(AluminiAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnDeleteClick(AluminiAdapterClickEventArgs args) => DeleteItemClick(this, args);
    }

    public class AluminiAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView nameText { get; set; }
        public TextView statusText { get; set; }
        public TextView setText { get; set; }
        public TextView departmentText { get; set; }
        public ImageView deleteButton { get; set; }

        public AluminiAdapterViewHolder(View itemView, Action<AluminiAdapterClickEventArgs> clickListener,
                            Action<AluminiAdapterClickEventArgs> longClickListener, Action<AluminiAdapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            //TextView = v;
            nameText = (TextView)itemView.FindViewById(Resource.Id.nameText);
            departmentText = (TextView)itemView.FindViewById(Resource.Id.departmentText);
            statusText = (TextView)itemView.FindViewById(Resource.Id.statusText);
            setText = (TextView)itemView.FindViewById(Resource.Id.setText);
            deleteButton = (ImageView)itemView.FindViewById(Resource.Id.deleteButton);

                itemView.Click += (sender, e) => clickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteButton.Click += (sender, e) => deleteClickListener(new AluminiAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class AluminiAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}