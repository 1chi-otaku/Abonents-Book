using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Abonents_Book
{
    public class DataCommands
    {
        public static RoutedUICommand Add { get; } = CreateCommand("Add", "Add", new KeyGesture(Key.A, ModifierKeys.Control));
        public static RoutedUICommand Delete { get; } = CreateCommand("Delete", "Delete", new KeyGesture(Key.D, ModifierKeys.Control));
        public static RoutedUICommand Save { get; } = CreateCommand("Save", "Save", new KeyGesture(Key.S, ModifierKeys.Control));
        public static RoutedUICommand Load { get; } = CreateCommand("Load", "Load", new KeyGesture(Key.L, ModifierKeys.Control));

        private static RoutedUICommand CreateCommand(string text, string name, KeyGesture keyGesture)
        {
            RoutedUICommand command = new RoutedUICommand(text, name, typeof(DataCommands));
            return command;
        }
    }
}
