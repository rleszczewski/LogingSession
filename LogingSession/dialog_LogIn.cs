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

namespace LogingSession
{
    public class OnLogInEventArgs : EventArgs
    {
        private string mLogin;
        private string mPassword;

        public string Login
        {
            get { return mLogin; }
            set { mLogin = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public OnLogInEventArgs( string login, string password) :base()
        {
            Login = login;
            Password = password;
        }
    }
        class dialog_LogIn : DialogFragment
    {
        private EditText mLoginText;
        private EditText mPasswordText;
        private Button mLogginButton;

        public event EventHandler<OnLogInEventArgs> mOnLogInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //taking the layout and puting it in view
            var view = inflater.Inflate(Resource.Layout.Logger, container, false);
            ////ataching it to container
           
            mLoginText = view.FindViewById<EditText>(Resource.Id.logintext);
            mPasswordText = view.FindViewById<EditText>(Resource.Id.passwordtext);
            mLogginButton = view.FindViewById<Button>(Resource.Id.loginbutton);

            mLogginButton.Click += mLogginButton_Click;

            return view;
        }

        void mLogginButton_Click(object sender, EventArgs e)
        {
            mOnLogInComplete.Invoke(this, new OnLogInEventArgs( mPasswordText.Text, mLoginText.Text));
            this.Dismiss();
        }
    }

   
}