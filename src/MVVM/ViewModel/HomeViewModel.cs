using StudentSystemWinForms.Core;
using StudentSystemWinForms.DAL;
using StudentSystemWinForms.Models;
using StudentSystemWinForms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystemWinForms.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private int _suggestionCount;
        private int _inputLengthSuggestions;
        private readonly UserService _userService;
        public int SuggestionCount
        {
            get => _suggestionCount;
            set
            {
                _suggestionCount = value;
                OnPropertyChanged();
            }
        }
        public int InputLengthSuggestions
        {
            get => _inputLengthSuggestions;
            set
            {
                _inputLengthSuggestions = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            _userService = new UserService(new StudentContext());
            SuggestionCount = UserInfo.CurrentUser.Settings.SuggestionsCount;
            InputLengthSuggestions = UserInfo.CurrentUser.Settings.InputLengthThreshold;
        }

        public void ButtonClicked()
        {
            var user = UserInfo.CurrentUser;
            var settings = user.Settings;
            settings.InputLengthThreshold = _inputLengthSuggestions;
            settings.SuggestionsCount = _suggestionCount;
            if(_userService.UpdateUserSettings(user.UserId, settings))
            UserInfo.CurrentUser.Settings = settings;
        }
    }
}
