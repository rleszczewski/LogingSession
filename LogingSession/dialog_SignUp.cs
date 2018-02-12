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
using LogingSession;

namespace LoginSystem
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mLastName;
        private string mPassword;
        private string mAge;
        private string mGender;
        private string mLogin;

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }

        }
        public string Age
        {
            get { return mAge; }
            set { mAge = value; }
        }
        public string Gender
        {
            get { return mGender; }
            set { mGender = value; }
        }
        public string Login
        {
            get { return mLogin; }
            set { mLogin = value; }
        }

        public OnSignUpEventArgs(string firstName, string lastName, string password, string age, string gender, string login) : base()
        {
            FirstName = firstName;
            LastName = LastName;
            Password = password;
            Age = age;
            Gender = gender;
            Login = login;
        }
    }

    class dialog_SignUp : DialogFragment
    {
        private EditText mFnText;
        private EditText mLnText;
        private EditText mAgeText;
        private EditText mLoginText;
        private EditText mGenderText;
        private EditText mPasswordText;
        private Button mCreateDialogButton;


        //event after click
        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //taking the layout and puting it in view
            var view = inflater.Inflate(Resource.Layout.DialogSignUp, container, false);
            ////ataching it to container
            mFnText = view.FindViewById<EditText>(Resource.Id.fntext);
            mLnText = view.FindViewById<EditText>(Resource.Id.lnttext);
            mAgeText = view.FindViewById<EditText>(Resource.Id.agetext);
            mGenderText = view.FindViewById<EditText>(Resource.Id.gendertext);
            mLoginText = view.FindViewById<EditText>(Resource.Id.logintext);
            mPasswordText = view.FindViewById<EditText>(Resource.Id.passwordtext);
            mCreateDialogButton = view.FindViewById<Button>(Resource.Id.createdialogbutton);

            mCreateDialogButton.Click += mCreateDialogButton_Click;

            return view;
        }

        //ivoking event after click
        void mCreateDialogButton_Click(object sender, EventArgs e)
        {
            mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mFnText.Text, mLnText.Text, mPasswordText.Text, mAgeText.Text, mGenderText.Text, mLoginText.Text));
            this.Dismiss();
        }


    }
}