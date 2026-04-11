using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace z1.Models
{
    public class AssignmentModel : INotifyPropertyChanged
    {
        private int _id;
        private string _subject = string.Empty;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime _dueDate;
        private int _createdByUserId;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        public int CreatedByUserId
        {
            get { return _createdByUserId; }
            set
            {
                _createdByUserId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}