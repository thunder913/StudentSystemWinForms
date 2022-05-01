using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentSystemWinForms.Models;
using StudentSystemWinForms.Views;

namespace StudentSystemWinForms
{
    public partial class Main : Form
    {
        private ViewBase _view;      
        private ViewModelBase _model;
        private void SwapView(ViewBase view = null)
        {
            if (view != null)
            {
                this.Controls.Remove(_view);

                if (view is LoginView)
                {
                    _model = new LoginViewModel();
                    _view = new LoginView(_model as LoginViewModel);
                }

                if (view is OtherView)
                {
                    _model = new OtherViewModel();
                    _view = new OtherView(_model as OtherViewModel);
                }
            }
            else
            {
                _model = new LoginViewModel();
                _view = new LoginView(_model as LoginViewModel);
            }

            this.Controls.Add(_view);

        }

        public Main(ViewBase view = null)
        {
            SwapView(new LoginView());

            InitializeComponent();
        }
        
        private void btnLoadExampleView_Click(object sender, EventArgs e)
        {
            SwapView(new LoginView());
        }

        private void btnLoadOtherView_Click(object sender, EventArgs e)
        {
            SwapView(new OtherView());
        }
    }
}
