using System;
using System.Collections.Generic;

namespace HomeControl.Source.Reference {
    public static class ReferenceValues {
        public const string COPYRIGHT = "Copyright © 2022  Robert Higgins";
        public const string VERSION = "1.0.0";

        public static readonly List<string> CategorySpendingList = new List<string> {
            "Billing",
            "Grocery",
            "Petrol",
            "Takeout",
            "Shopping",
            "Health",
            "Travel",
            "Entertainment",
            "Services",
            "Personal Care",
            "Home Improvement",
            "Alcohol",
            "Firearms",
            "Stupid/Dumb",
            "Interest",
            "Other"
        };

        public static readonly List<string> CategoryProfitList = new List<string> {
            "Paycheck",
            "Gift",
            "Government",
            "Child Support"
        };

        public static readonly string FILE_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TheOnlyRupert/HomeControl/";
    }
}