using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentSystemWinForms.Models;

namespace StudentSystemWinForms.Views
{
    public sealed partial class LoginView : ViewBase
    {
        private LoginViewModel _model;
        public LoginView(LoginViewModel model = null)
        {
            if (model != null)
            {
                _model = model;
            }
            else
            {
                _model = new LoginViewModel();
            }

            InitializeComponent();
            PerformBinding();
        }
        public sealed override void PerformBinding()
        {
            usernameBox.DataBindings.Add("Text", _model, nameof(_model.Username), false, DataSourceUpdateMode.OnPropertyChanged);
            usernameBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            usernameBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            usernameBox.AutoCompleteCustomSource = _model.AutoCompleteCollection;
            passwordBox.DataBindings.Add("Text", _model, nameof(_model.Password), false, DataSourceUpdateMode.OnPropertyChanged);
            
            usernameBox.TextChanged += (sender, e) => _model.HandleSuggestionClicked(sender);
            loginButton.Click += (sender, e) => _model.Login(() => (this.Parent as Main).SwapView(new OtherView()));
            registerButton.Click += (sender, e) => _model.Register();
        }
    }
}
