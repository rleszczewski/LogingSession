using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LogingSession
{
    class PersonView : BaseAdapter<Person>
    {
        private List<Person> mItems;
        private Context mContext;

        public PersonView(Context context, List<Person> items)
        {
            mItems = items;
            mContext = context;
        }
        public override int Count
        {
            get { return mItems.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Person this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtname);
            txtName.Text = mItems[position].FirstName;

            TextView txtLastName = row.FindViewById<TextView>(Resource.Id.txtlastname);
            txtLastName.Text = mItems[position].LastName;

            TextView txtAge = row.FindViewById<TextView>(Resource.Id.txtage);
            txtAge.Text = mItems[position].Age;

            TextView txtGender = row.FindViewById<TextView>(Resource.Id.txtgender);
            txtGender.Text = mItems[position].Gender;

            return row;
        }
    }
}