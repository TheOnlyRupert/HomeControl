using System.Collections.Specialized;
using System.Windows.Controls;
using HomeControl.Source.Helpers;

namespace HomeControl.Source.Control;

public class CustomListView : ListView {
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs? e) {
        try {
            // Only proceed if there are new items
            if (e?.NewItems is { Count: > 0 }) {
                // Scroll to the newly added item
                ScrollIntoView(e.NewItems[e.NewItems.Count - 1]);
            }

            // Call the base implementation after processing
            base.OnItemsChanged(e);
        } catch (Exception ex) {
            FileHelpers.LogDebugMessage("WARN", "CustomListView.OnItemsChanged", $"An error occurred while handling items change:\n{ex.Message}");
        }
    }
}