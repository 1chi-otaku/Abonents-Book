using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Abonents_Book
{
    public class Abonent : INotifyPropertyChanged
    {
        private string name;
        private string address;
        private string phone;

        public event Action DataModified;
        public event PropertyChangedEventHandler PropertyChanged;

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
        

        protected virtual void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            DataModified?.Invoke();
        }
    }

    public class ContactManager : INotifyPropertyChanged
    {
        public bool IsDataChanged { get; set; } = false;
        public ContactManager()
        {
            abonents.CollectionChanged += (s, e) =>
            {
                IsDataChanged = true;
                OnPropertyChanged(nameof(IsDataChanged));
            };
        }

        private ObservableCollection<Abonent> abonents = new ObservableCollection<Abonent>();
        private Abonent currentAbonent = new Abonent();

        public ObservableCollection<Abonent> Persons => abonents;

        public Abonent SelectedPerson
        {
            get => currentAbonent;
            set
            {
                if (currentAbonent != value)
                {
                    currentAbonent = value;
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
            abonents.Add(person);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
