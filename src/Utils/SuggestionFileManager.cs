﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StudentSystemWinForms.DAL;
using StudentSystemWinForms.MVVM.Model;

namespace StudentSystemWinForms.Utils
{
    public class SuggestionFileManager
    {
        private const string LoginFileName = "logindata.dat";
        private const string AddStudentFileName = "addstudent.dat";
        private const string SearchStudentFileName = "searchstudent.dat";
        private StudentService studentService { get; set; }
        public List<StudentAddSuggestion> GetStudentAddSuggestions()
        {
            var data = Cryptography.Decrypt(File.ReadAllText(AddStudentFileName), "yolo123");
            var lines = data.Split('\n');
            List<string> facultyNumbers = new List<string>();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    facultyNumbers.Add(line);
                }
            }

            return studentService
                .GetStudentsByFacNumber(facultyNumbers)
                .Select(x => new StudentAddSuggestion()
                {
                    Course = x.Course.ToString(),
                    Specialty = x.Specialty,
                    Email = x.Email,
                    Stream = x.Stream.ToString(),
                    Faculty = x.Faculty,
                    FacultyNumber = x.FacultyNumber,
                    FirstName = x.FirstName,
                    Group = x.Group.ToString(),
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    PhoneNumber = x.PhoneNumber,
                    SuggestedFacultyNumber = x.FacultyNumber,
                }).ToList();
        }

        public List<StudentSearchSuggestion> GetStudentSearchSuggestion()
        {
            var data = Cryptography.Decrypt(File.ReadAllText(SearchStudentFileName), "yolo123");
            var lines = data.Split('\n');
            List<string> facultyNumbers = new List<string>();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    facultyNumbers.Add(line);
                }
            }

            return facultyNumbers.Select(x => new StudentSearchSuggestion()
            {
                FacultyNumber = x,
            }).ToList();
        }
        public List<UserLoginSuggestion> GetLoginSuggestions()
        {
            var data = Cryptography.Decrypt(File.ReadAllText(LoginFileName), "yolo123");
            var lines = data.Split('\n');
            List<UserLoginSuggestion> suggestions = new List<UserLoginSuggestion>();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var index = line.IndexOf('+');
                    if (index < 0) continue;
                    var uls = new UserLoginSuggestion(line.Substring(0, index).Trim(), line.Substring(index + 1).Trim());
                    suggestions.Add(uls);
                }
            }

            return suggestions;
        }

        public void SetAddStudentSuggetions(List<StudentAddSuggestion> suggestions)
        {
            StringBuilder sb = new StringBuilder();
            if (suggestions.Count >= 5)
            {
                suggestions = suggestions.Take(5).ToList();
            }
            foreach (var suggestion in suggestions)
            {
                sb.Append(suggestion.FacultyNumber);
                sb.Append("\n");
            }
            File.WriteAllText(AddStudentFileName, Cryptography.Encrypt(sb.ToString(), "yolo123"));
        }
        public void SetSearchStudentSuggestions(List<StudentSearchSuggestion> suggestions)
        {
            StringBuilder sb = new StringBuilder();
            if (suggestions.Count >= 5)
            {
                suggestions = suggestions.Take(5).ToList();
            }
            foreach (var suggestion in suggestions)
            {
                sb.Append(suggestion.FacultyNumber);
                sb.Append("\n");
            }
            File.WriteAllText(SearchStudentFileName, Cryptography.Encrypt(sb.ToString(), "yolo123"));
        }

        public void SetLoginSuggestions(List<UserLoginSuggestion> suggestions)
        {
            StringBuilder sb = new StringBuilder();
            if (suggestions.Count >= 5)
            {
                suggestions = suggestions.Take(5).ToList();
            }
            foreach (var suggestion in suggestions)
            {
                sb.Append(suggestion.Username);
                sb.Append("+");
                sb.Append(suggestion.Password);
                sb.Append("\n");
            }
            File.WriteAllText(LoginFileName, Cryptography.Encrypt(sb.ToString(), "yolo123"));
        }

        public void AddLoginSuggestion(UserLoginSuggestion suggestion)
        {
            var list = GetLoginSuggestions() ?? new List<UserLoginSuggestion>();
            if (list.Any(x => x.Equals(suggestion)))
            {
                var index = list.FindIndex(x => x.Equals(suggestion));
                list.RemoveAt(index);
            }
            if (!list.Contains(suggestion))
                list.Insert(0, suggestion);
            SetLoginSuggestions(list);
        }

        public void AddStudentAddSuggestion(StudentAddSuggestion suggestion)
        {
            var list = GetStudentAddSuggestions() ?? new List<StudentAddSuggestion>();
            if (list.Any(x => x.FacultyNumber == suggestion.FacultyNumber))
            {
                var index = list.FindIndex(x => x.FacultyNumber == suggestion.FacultyNumber);
                list.RemoveAt(index);
            }
            if (!list.Contains(suggestion))
                list.Insert(0, suggestion);
            SetAddStudentSuggetions(list);
        }
        public void AddStudentSearchSuggestion(StudentSearchSuggestion suggestion)
        {
            var list = GetStudentSearchSuggestion() ?? new List<StudentSearchSuggestion>();
            if (list.Any(x => x.FacultyNumber == suggestion.FacultyNumber))
            {
                var index = list.FindIndex(x => x.FacultyNumber == suggestion.FacultyNumber);
                list.RemoveAt(index);
            }
            if (!list.Any(x => x.FacultyNumber == suggestion.FacultyNumber))
                list.Insert(0, suggestion);
            SetSearchStudentSuggestions(list);
        }

        public SuggestionFileManager()
        {
            studentService = new StudentService(new StudentContext());
        }
    }
}
