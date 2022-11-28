using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PostGressGrid.Annotations;
using PostGressGrid.Models;

namespace PostGressGrid.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<User> ListUser { get; set; } = new ObservableCollection<User>();
        private User _selectedUser = null;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    //SelectedMarkerChanged();
                    RaisePropertyChanged("SelectedUser");
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private RelayCommand _addUser;
        public RelayCommand AddUser
        {
            get
            {
                return _addUser ??
                       (_addUser = new RelayCommand(() =>
                       {

                           loadUsers();

                       }));
            }
        }


        public void loadUsers()
        {
            ListUser.Clear();
            for (int i = 0; i < 10; i++)
            {
                User u = new User();
                u.id = i;
                u.name = "dfgd " + i;
                u.age = i * 4;
                ListUser.Add(u);
                RaisePropertyChanged("dataGridView");


            }
        }


    }
}
