namespace HomeControl.Source.Helpers;

public static class ListHelpers {
    private static readonly Random Rand = new();

    public static List<string>? RandomizeList(List<string>? list) {
        if (list == null || list.Count < 2) {
            return list;
        }

        int n = list.Count;

        // Fisher-Yates shuffle (Durstenfeld version)
        for (int i = n - 1; i > 0; i--) {
            // Randomly pick an index from 0 to i
            int j = Rand.Next(i + 1);

            // Swap elements at i and j
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list;
    }
}