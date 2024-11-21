using System.Collections.Specialized;
using System.Text.Json;
using System.Windows.Controls;
using HomeControl.Source.Helpers;
using HomeControl.Source.Json;

namespace HomeControl.Source.Control;

public class CustomListView : ListView {
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs? e) {
        try {
            // Only proceed if there are new items
            if (e.NewItems != null && e.NewItems.Count > 0) {
                // Scroll to the newly added item
                ScrollIntoView(e.NewItems[e.NewItems.Count - 1]);
            }

            // Call the base implementation after processing
            base.OnItemsChanged(e);
        } catch (NullReferenceException) {
            // Handling specific expected exception
            // Consider logging if necessary
        } catch (Exception ex) {
            // Improve logging with detailed message
            DebugTextBlock? debugBlock = new() {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "GridViewScroll",
                Description = $"An error occurred while handling items change: {ex.Message} | StackTrace: {ex.StackTrace}"
            };
            ReferenceValues.JsonDebugMaster.DebugBlockList.Add(debugBlock);
            FileHelpers.SaveFileText("debug", JsonSerializer.Serialize(ReferenceValues.JsonDebugMaster), true);
        }
    }
}