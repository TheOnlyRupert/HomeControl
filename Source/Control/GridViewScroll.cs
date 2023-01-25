using System;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace HomeControl.Source.Control;

public class CustomListView : ListView {
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e) {
        try {
            int newItemCount = e.NewItems.Count;

            if (newItemCount > 0) {
                ScrollIntoView(e.NewItems[newItemCount - 1]);
            }

            base.OnItemsChanged(e);
        } catch (Exception) { }
    }
}