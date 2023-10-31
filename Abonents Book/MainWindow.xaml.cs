using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Abonents_Book
{
    public partial class MainWindow : Window
    {
        private bool isChanged = false;
        public MainWindow()
        {
            CommandBindings.Add(new CommandBinding(DataCommands.Add, AddCommand, AddCommand_CanExecute));
            CommandBindings.Add(new CommandBinding(DataCommands.Delete, DeleteCommand, DeleteCommand_CanExecute));
            CommandBindings.Add(new CommandBinding(DataCommands.Save, SaveCommand, SaveCommand_CanExecute));
            CommandBindings.Add(new CommandBinding(DataCommands.Load, LoadCommand, LoadCommand_CanExecute));

            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged = true;
            CommandManager.InvalidateRequerySuggested();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;

            personName.Text = contactManager.SelectedPerson.Name;
            personAddress.Text = contactManager.SelectedPerson.Address;
            personPhone.Text = contactManager.SelectedPerson.Phone;
        }

        private void AddCommand(object sender, ExecutedRoutedEventArgs e) => AddButton_Click(sender, e);
        private void AddCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = !string.IsNullOrWhiteSpace(personName.Text) && !string.IsNullOrWhiteSpace(personAddress.Text) && !string.IsNullOrWhiteSpace(personPhone.Text);
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;

            Abonent newPerson = new Abonent
            {
                Name = personName.Text,
                Address = personAddress.Text,
                Phone = personPhone.Text
            };

            contactManager.Persons.Add(newPerson);

            personName.Clear();
            personAddress.Clear();
            personPhone.Clear();
        }



        private void DeleteCommand(object sender, ExecutedRoutedEventArgs e) => DeleteButton_Click(sender, e);
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;
            e.CanExecute = contactManager.Persons.Count > 0 && contactManager.SelectedPerson != null;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;

            if (contactManager.SelectedPerson != null)
            {
                contactManager.Persons.Remove(contactManager.SelectedPerson);
            }
        }


        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) => SaveButton_Click(sender, e);

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;
            e.CanExecute = contactManager != null && contactManager.IsDataChanged;
        }

        private void LoadCommand(object sender, ExecutedRoutedEventArgs e) => LoadButton_Click(sender, e);
        private void LoadCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;

            String json = JsonConvert.SerializeObject(contactManager.Persons);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                FileName = "contacts.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            ContactManager contactManager = Resources["contactManager"] as ContactManager;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string jsonText = File.ReadAllText(openFileDialog.FileName);
                try
                {
                    ObservableCollection<Abonent> loadedPersons = JsonConvert.DeserializeObject<ObservableCollection<Abonent>>(jsonText);

                    if (loadedPersons != null)
                    {
                        contactManager.Persons.Clear();

                        foreach (var person in loadedPersons)
                        {
                            contactManager.Persons.Add(person);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
    }


}

