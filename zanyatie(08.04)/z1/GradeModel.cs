using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace z1
{
    public class GradeModel : INotifyPropertyChanged
    {
        private int _id;
        private int _studentId;
        private string _subject = string.Empty;
        private int _gradeValue;
        private DateTime _date;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public int StudentId
        {
            get { return _studentId; }
            set
            {
                _studentId = value;
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

        public int GradeValue
        {
            get { return _gradeValue; }
            set
            {
                _gradeValue = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
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