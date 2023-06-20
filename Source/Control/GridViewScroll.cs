using System;
using System.Collections.Specialized;
using System.Windows.Controls;
using HomeControl.Source.IO;
using HomeControl.Source.Reference;

namespace HomeControl.Source.Control;

public class CustomListView : ListView {
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e) {
        try {
            int newItemCount = e.NewItems.Count;

            if (newItemCount > 0) {
                ScrollIntoView(e.NewItems[newItemCount - 1]);
            }

            base.OnItemsChanged(e);
        } catch (NullReferenceException) {
            // NORMAL
        } catch (Exception e2) {
            ReferenceValues.DebugTextBlockOutput.Add(new DebugTextBlock {
                Date = DateTime.Now,
                Level = "WARN",
                Module = "GridViewScroll",
                Description = e2.ToString()
            });
            SaveDebugFile.Save();
        }
    }
}