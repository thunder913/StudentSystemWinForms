using StudentSystemWinForms.MVVM.ViewModel;
using StudentSystemWinForms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystemWinForms.MVVM.View
{
    public partial class AddStudentView : ViewBase
    {
        private AddStudentViewModel _model;
        public AddStudentView(AddStudentViewModel model = null)
        {
            if (model != null)
            {
                _model = model;
            }
            else
            {
                _model = new AddStudentViewModel();
            }
            InitializeComponent();
        }

        public override void PerformBinding()
        {
        }
    }
}
