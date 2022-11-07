using System;
using System.Collections.Generic;

namespace HomeControl.Source.Helpers; 

public static class ListHelpers {
    public static List<string> RandomizeList(List<string> list) {
        Random rand = new();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rand.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}