using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeControl.Source.Control;

public class GridViewSort {
    // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(GridViewSort),
            new UIPropertyMetadata(null,
                (o, e) => {
                    if (o is ItemsControl listView) {
                        if (!GetAutoSort(listView)) // Don't change click handler if AutoSort enabled
                        {
                            if (e is { OldValue: not null, NewValue: null }) {
                                listView.RemoveHandler(ButtonBase.ClickEvent,
                                    new RoutedEventHandler(ColumnHeader_Click));
                            }

                            if (e.OldValue == null && e.NewValue != null) {
                                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                            }
                        }
                    }
                }));

    // Using a DependencyProperty as the backing store for AutoSort.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AutoSortProperty =
        DependencyProperty.RegisterAttached("AutoSort",
            typeof(bool),
            typeof(GridViewSort),
            new UIPropertyMetadata(false,
                (o, e) => {
                    if (o is ListView listView) {
                        if (GetCommand(listView) == null) // Don't change click handler if a command is set
                        {
                            bool oldValue = (bool)e.OldValue;
                            bool newValue = (bool)e.NewValue;
                            if (oldValue && !newValue) {
                                listView.RemoveHandler(ButtonBase.ClickEvent,
                                    new RoutedEventHandler(ColumnHeader_Click));
                            }

                            if (!oldValue && newValue) {
                                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                            }
                        }
                    }
                }));

    // Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PropertyNameProperty =
        DependencyProperty.RegisterAttached("PropertyName",
            typeof(string),
            typeof(GridViewSort),
            new UIPropertyMetadata(null));

    private static ICommand GetCommand(DependencyObject obj) {
        return (ICommand)obj.GetValue(CommandProperty);
    }

    public static void SetCommand(DependencyObject obj, ICommand value) {
        obj.SetValue(CommandProperty, value);
    }

    public static bool GetAutoSort(DependencyObject obj) {
        return (bool)obj.GetValue(AutoSortProperty);
    }

    public static void SetAutoSort(DependencyObject obj, bool value) {
        obj.SetValue(AutoSortProperty, value);
    }

    public static string GetPropertyName(DependencyObject obj) {
        return (string)obj.GetValue(PropertyNameProperty);
    }

    public static void SetPropertyName(DependencyObject obj, string value) {
        obj.SetValue(PropertyNameProperty, value);
    }

    private static void ColumnHeader_Click(object sender, RoutedEventArgs e) {
        try {
            if (e.OriginalSource is GridViewColumnHeader headerClicked) {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (!string.IsNullOrEmpty(propertyName)) {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null) {
                        ICommand command = GetCommand(listView);
                        if (command != null) {
                            if (command.CanExecute(propertyName)) {
                                command.Execute(propertyName);
                            }
                        } else if (GetAutoSort(listView)) {
                            ApplySort(listView.Items, propertyName);
                        }
                    }
                }
            }
        } catch (Exception) {
            //ignore
        }
    }

    private static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject {
        DependencyObject parent = VisualTreeHelper.GetParent(reference);
        while (!(parent is T)) {
            if (parent != null) {
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        return (T)parent;
    }

    private static void ApplySort(ICollectionView view, string propertyName) {
        ListSortDirection direction = ListSortDirection.Ascending;
        if (view.SortDescriptions.Count > 0) {
            SortDescription currentSort = view.SortDescriptions[0];
            if (currentSort.PropertyName == propertyName) {
                direction = currentSort.Direction == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }

            view.SortDescriptions.Clear();
        }

        if (!string.IsNullOrEmpty(propertyName)) {
            view.SortDescriptions.Add(new SortDescription(propertyName, direction));
        }
    }
}