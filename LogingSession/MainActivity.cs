using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using LoginSystem;
using SQLite;
using System.IO;
using System.Collections.Generic;
using Android.Database.Sqlite;
using Android.Database;
using System.Linq;

namespace LogingSession
{
    [Activity(Label = "LogingSession", MainLauncher = true)]
    public class MainActivity : Activity
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user1.db3");
        

       
        private Button LoginButton;
        private Button CreateButton;
        private ProgressBar mProgressBar;
        private ListView listView;
        protected override void OnCreate(Bundle bundle) 
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Login);

            CreateButton = FindViewById<Button>(Resource.Id.createbutton);

            LoginButton = FindViewById<Button>(Resource.Id.loginbutton);

            listView = FindViewById<ListView>(Resource.Id.myListView);

            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            CreateDB();

          

            LoginButton.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_LogIn logInDialog = new dialog_LogIn();
                logInDialog.Show(transaction, "dialog fragment");

                logInDialog.mOnLogInComplete += logInDialog_mOnLogInComplete;
                
            };
          
            //Method with transaction poping out dialog 



            CreateButton.Click += (object sender, EventArgs args) =>
            {
                
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");


                //subscribe to event
               signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
            };
        }
        public List<Person> GetData()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath
               (System.Environment.SpecialFolder.Personal), "user.db3");
            var db = new SQLiteConnection(dbPath);
            List<Person> people = db.Table<Person>().ToList();
            return people;
        }
        public  void logInDialog_mOnLogInComplete(object sender, OnLogInEventArgs e)
        {
            
            try
            {

                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user1.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                
                var data = db.Table<Person>(); //Call Table  
                var data1 = data.Where(x => x.Login == e.Login && x.Password == e.Password).FirstOrDefault();
                var g1 = e.Login;
                var g2 = e.Password;

                //Linq Query  
                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    var users = GetData();
                    //var users = new List<Person>();
                    //users.Add(new Person() { Id = 1, FirstName = "Joe", LastName = "Smith", Age = "22", Gender = "Male", Login="1" ,Password="1"});

                    //users.Add(new Person() { Id = 2, FirstName = "Joe1", LastName = "Smith", Age = "22", Gender = "Male", Login="2", Password = "2" });
                    
                    //users.Add(new Person() {Id=3, FirstName = "Joe2", LastName = "Smith", Age = "22", Gender = "Male", Login="3", Password = "3" });
                    PersonView adapter = new PersonView(this, users);
                    listView.Adapter = adapter;
                    SetContentView(Resource.Layout.listview);

                   
                    
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
                db.Close();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        Random rnd = new Random();
        void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user1.db3");
                var db2 = new SQLiteConnection(dpPath);
                db2.CreateTable<Person>();
                Person tbl = new Person
                {   
                    FirstName = e.FirstName,

                    LastName = e.LastName,
                    Age = e.Age,
                    Gender = e.Gender,
                    Login = e.Login,
                    Password = e.Password
                };

                db2.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                db2.Close();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            

            mProgressBar.Visibility = ViewStates.Visible;

            Thread thread = new Thread(ActLikeARequest);
            thread.Start();


        }
        
        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user1.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
        private void ActLikeARequest()
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
            
        }
    }
}

