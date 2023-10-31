using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonents_Book
{
    public class Abonent : INotifyPropertyChanged
    {
        private string name;
        private string address;
        private string phone;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                if (phone != value)
                {
                    phone = value;
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

        private ObservableCollection<Abonent> _persons = new ObservableCollection<Abonent>();
        private Abonent _selectedPerson = new Abonent();

        public ObservableCollection<Abonent> Persons => _persons;

        public Abonent SelectedPerson
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
        public void AddPerson(Abonent person)
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
