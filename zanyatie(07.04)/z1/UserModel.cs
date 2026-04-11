using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace z1
{
    public class UserModel : INotifyPropertyChanged
    {
        private int _id;
        private string _username = string.Empty;
        private string _passwordHash = string.Empty;
        private string _role = string.Empty;
        private string _fullName = string.Empty;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set
            {
                _passwordHash = value;
                OnPropertyChanged();
            }
        }

        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
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