﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonents_Book
{
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _phone;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }
        public event Action DataModified;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            DataModified?.Invoke();
        }
    }

    public class ContactManager : INotifyPropertyChanged
    {
        public bool IsDataChanged { get; set; } = false;
        public ContactManager()
        {
            _persons.CollectionChanged += (s, e) =>
            {
                IsDataChanged = true;
                OnPropertyChanged(nameof(IsDataChanged));
            };
        }

        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();
        private Person _selectedPerson = new Person();

        public ObservableCollection<Person> Persons => _persons;

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged(nameof(SelectedPerson));
                }
            }
        }
        public void AddPerson(Person person)
        {
            person.DataModified += () =>
            {
                IsDataChanged = true;
                OnPropertyChanged(nameof(IsDataChanged));
            };
            _persons.Add(person);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
