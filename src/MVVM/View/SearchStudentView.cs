using StudentSystemWinForms.MVVM.Model;
using StudentSystemWinForms.MVVM.ViewModel;
using StudentSystemWinForms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitializeComponent();
            if (model != null)
            {
                _model = model;
            }
            else
            {
                _model = new SearchStudentViewModel(searchResult);
            }
            PerformBinding();
        }

        public override void PerformBinding()
        {
            searchBox.DataBindings.Add("Text", _model, nameof(_model.SearchWord), false, DataSourceUpdateMode.OnPropertyChanged);

            searchResult.Columns.Add("Faculty Number", 88,HorizontalAlignment.Left);
            searchResult.Columns.Add("Name", 88, HorizontalAlignment.Right);

            searchBox.GotFocus += (sender, e) => _model.RemoveText(sender, e);
            searchBox.LostFocus += (sender, e) => _model.AddText(sender, e);
            searchButton.Click += (sender, e) => _model.Search();
        }
    }
}
