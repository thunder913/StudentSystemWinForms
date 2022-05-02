using StudentSystemWinForms.DAL;
using StudentSystemWinForms.Models;
using StudentSystemWinForms.MVVM.Model;
using StudentSystemWinForms.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystemWinForms.MVVM.ViewModel
{
    public class SearchStudentViewModel : ViewModelBase
    {
        private string _searchWord;
        private StudentService _studentService;
        private SuggestionFileManager _suggestionFileManager;        
        private ListView listView { get; set; }
        public string SearchWord
        {
            get => _searchWord;
            set
            {
                _searchWord = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SearchWord)));
            }
        }

        public void RemoveText(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Търси...")
            {
                SearchWord = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace((sender as TextBox).Text))
                SearchWord = "Търси...";
        }

        public SearchStudentViewModel(ListView listView)
        {
            this.listView = listView;
            SearchWord = "Търси...";
            _studentService = new StudentService(new StudentContext());
            _suggestionFileManager = new SuggestionFileManager();
        }

        public void Search()
        {
            var students = this._studentService.SearchStudentsByFacultyNumber(SearchWord);
            this.listView.Items.Clear();
            foreach (var item in students)
            {
                var listViewItem = new ListViewItem(item.FacultyNumber, 0);
                listViewItem.SubItems.Add(item.FirstName + " " + item.LastName);
                this.listView.Items.Add(listViewItem);
            }
            if (students.Count > 0)
            {
                _suggestionFileManager.AddStudentSearchSuggestion(new StudentSearchSuggestion() { FacultyNumber = SearchWord });
            }
        }
    }
}
