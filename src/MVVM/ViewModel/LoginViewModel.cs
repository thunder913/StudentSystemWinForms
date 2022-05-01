using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystemWinForms.Models
{
    public sealed class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        public AutoCompleteStringCollection AutoCompleteCollection { get; set; }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Username)));
            }

        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Password)));
            }
        }
        public LoginViewModel()
        {
            AutoCompleteCollection = new AutoCompleteStringCollection();
            AutoCompleteCollection.Add("ivan");
            AutoCompleteCollection.Add("petur");
            AutoCompleteCollection.Add("goergi");
            AutoCompleteCollection.Add("dragan");
            AutoCompleteCollection.Add("petkan");
        }

        internal void Login()
        {
            var test = Username + Password;
        }

        internal void Register()
        {
            throw new NotImplementedException();
        }
    }
}
