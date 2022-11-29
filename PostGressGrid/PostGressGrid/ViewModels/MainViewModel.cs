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
using npgsqlProject;
using PostGressGrid.Annotations;
using PostGressGrid.Models;
using System.Text.RegularExpressions;


namespace PostGressGrid.ViewModel
{
    class MainViewModel : ViewModelBase
    {

        public ObservableCollection<User> ListUser { get; set; } = new ObservableCollection<User>();
        private User _selectedUser = null;
        public Connector connector;
        public bool shouldShowImage = true;

        public Visibility ShowImage
        {
            get { return shouldShowImage ? Visibility.Visible : Visibility.Hidden; }
        }

        private int? _id;
        public int? Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

 

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }



        private int? _age;
        public int? Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }




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



        private RelayCommand _addUser;
        public RelayCommand AddUser
        {
            get
            {
                return _addUser ??
                       (_addUser = new RelayCommand(() =>
                       {
                           bool val = validate();
                           if (val)
                           {
                               User u = new User();
                               u.id = (int)Id;
                               u.age = (int)Age;
                               u.name = Name;
                               connector.Insert(u);
                               loadUsers();
                               Id = null;
                               Name = "";
                               Age = null;
                               RaisePropertyChanged("Id");
                               RaisePropertyChanged("Name");
                               RaisePropertyChanged("Age");
                           }

                       }));
            }
        }

        public bool validate()
        {
            if (Id == null || Id < 0)
            {
                MessageBox.Show("¬ведите корректный id");
                return false;
            }

            if (Age == null || Age < 0)
            {
                MessageBox.Show("¬ведите корректный Age");
                return false;
            }

            if (String.IsNullOrEmpty(Name))
            {
                MessageBox.Show("¬ведите не пустой Name");
                return false;
            }
            return true;
        }


        private RelayCommand _myICommandThatShouldHandleLoaded;
        public RelayCommand MyICommandThatShouldHandleLoaded
        {
            get
            {
                return _myICommandThatShouldHandleLoaded ??
                       (_myICommandThatShouldHandleLoaded = new RelayCommand(() =>
                       {

                           loadUsers();

                       }));
            }
        }




        private RelayCommand<User> _DeleteUserCommand;

        public RelayCommand<User> DeleteUserCommand
        {
            get
            {
                return _DeleteUserCommand ??
                       (_DeleteUserCommand = new RelayCommand<User>((User e) =>
                       {
                           User u = e;
                           connector.remove(u);
                           loadUsers();

                       }));
            }
        }







        public void loadUsers()
        {
            if (connector == null)
            {
                connector = new Connector();
            }
            ListUser.Clear();
            List<User> users = connector.Select();
            ListUser = new ObservableCollection<User>(users);
            RaisePropertyChanged("ListUser");

        }


    }
}
