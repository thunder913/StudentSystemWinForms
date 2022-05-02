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
    public partial class SearchStudentView : ViewBase
    {
        private SearchStudentViewModel _model;
        public SearchStudentView(SearchStudentViewModel model = null)
        {
            if (model != null)
            {
                _model = model;
            }
            else
            {
                _model = new SearchStudentViewModel();
            }
            InitializeComponent();
        }

        public override void PerformBinding()
        {
        }
    }
}
