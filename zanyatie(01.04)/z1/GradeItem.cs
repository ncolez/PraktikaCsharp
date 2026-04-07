using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace z1
{
    public class GradeItem : INotifyPropertyChanged
    {
        private string _studentName;
        private string _subject;
        private int _grade;
        private string _date;

        public string StudentName
        {
            get { return _studentName; }
            set
            {
                _studentName = value;
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

        public int Grade
        {
            get { return _grade; }
            set
            {
                _grade = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}