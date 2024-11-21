using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeControl.Source.Control;

public class GridViewSort {
    // Dependency Property for Command
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridViewSort), new UIPropertyMetadata(null, OnCommandChanged));

    // Dependency Property for AutoSort
    public static readonly DependencyProperty AutoSortProperty =
        DependencyProperty.RegisterAttached("AutoSort", typeof(bool), typeof(GridViewSort), new UIPropertyMetadata(false, OnAutoSortChanged));

    // Dependency Property for PropertyName
    public static readonly DependencyProperty PropertyNameProperty =
        DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(GridViewSort), new UIPropertyMetadata(null));

    // Command Dependency Property Getter and Setter
    public static ICommand GetCommand(DependencyObject obj) {
        return (ICommand)obj.GetValue(CommandProperty);
    }

    public static void SetCommand(DependencyObject obj, ICommand value) {
        obj.SetValue(CommandProperty, value);
    }

    // AutoSort Dependency Property Getter and Setter
    public static bool GetAutoSort(DependencyObject obj) {
        return (bool)obj.GetValue(AutoSortProperty);
    }

    public static void SetAutoSort(DependencyObject obj, bool value) {
        obj.SetValue(AutoSortProperty, value);
    }

    // PropertyName Dependency Property Getter and Setter
    public static string GetPropertyName(DependencyObject obj) {
        return (string)obj.GetValue(PropertyNameProperty);
    }

    public static void SetPropertyName(DependencyObject obj, string value) {
        obj.SetValue(PropertyNameProperty, value);
    }

    // Event handler for Command Dependency Property changes
    private static void OnCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
        if (o is ItemsControl listView && GetAutoSort(listView) == false) // Don't change click handler if AutoSort enabled
        {
            if (e.OldValue != null && e.NewValue == null) {
                listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }

            if (e.OldValue == null && e.NewValue != null) {
                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
        }
    }

    // Event handler for AutoSort Dependency Property changes
    private static void OnAutoSortChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
        if (o is ListView listView && GetCommand(listView) == null) // Don't change click handler if a command is set
        {
            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            if (oldValue && !newValue) {
                listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }

            if (!oldValue && newValue) {
                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
        }
    }

    // Column header click handler to initiate sorting
    private static void ColumnHeader_Click(object sender, RoutedEventArgs e) {
        try {
            if (e.OriginalSource is GridViewColumnHeader headerClicked) {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName)) {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null) {
                        ICommand command = GetCommand(listView);
                        if (command != null && command.CanExecute(propertyName)) {
                            command.Execute(propertyName);
                        } else if (GetAutoSort(listView)) {
                            ApplySort(listView.Items, propertyName);
                        }
                    }
                }
            }
        } catch (Exception ex) {
            // Log exception with more context
            LogError("Error in ColumnHeader_Click", ex);
        }
    }

    // Helper method to find the ancestor of a specific type in the visual tree
    private static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject {
        DependencyObject parent = VisualTreeHelper.GetParent(reference);
        while (parent != null && !(parent is T)) {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return (T)parent;
    }

    // Helper method to apply sorting to a collection view
    private static void ApplySort(ICollectionView view, string propertyName) {
        ListSortDirection direction = ListSortDirection.Ascending;
        if (view.SortDescriptions.Count > 0) {
            SortDescription currentSort = view.SortDescriptions[0];
            direction = currentSort.PropertyName == propertyName && currentSort.Direction == ListSortDirection.Ascending
                ? ListSortDirection.Descending
                : ListSortDirection.Ascending;

            view.SortDescriptions.Clear();
        }

        if (!string.IsNullOrEmpty(propertyName)) {
            view.SortDescriptions.Add(new SortDescription(propertyName, direction));
        }
    }

    // Helper method to log errors with additional context
    private static void LogError(string message, Exception ex) {
        // Implement proper logging mechanism here
        // Example: log to a file, or an external logging service
        Debug.WriteLine($"{message}: {ex.Message}");
    }
}