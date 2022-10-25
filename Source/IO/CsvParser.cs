using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using HomeControl.Source.Reference;

namespace HomeControl.Source.IO {
    public static class CsvParser {
        private static readonly ObservableCollection<FinanceBlock> _list = new ObservableCollection<FinanceBlock>();

        static CsvParser() {
            try {
                Directory.CreateDirectory(ReferenceValues.FILE_DIRECTORY);

                if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "settings.csv")) {
                    Console.WriteLine("settings.csv does not exist. Restoring default settings");
                    StreamWriter file = new StreamWriter(ReferenceValues.FILE_DIRECTORY + "settings.csv", true);
                    file.WriteLine("!KEY,VALUE");
                    file.Close();
                }

                if (!File.Exists(ReferenceValues.FILE_DIRECTORY + "finances2022.csv")) {
                    Console.WriteLine("finances2022.csv does not exist. Restoring default settings");
                    StreamWriter file = new StreamWriter(ReferenceValues.FILE_DIRECTORY + "finances2022.csv", true);
                    file.WriteLine("!DATE,ITEM,AMOUNT,CATEGORY,PERSON");
                    file.Close();
                }
            } catch (Exception) {
                MessageBox.Show("Error! Unable to create program files in directory.\n" +
                                "Using default settings with saving disabled.", "Error");
            }
        }

        public static ObservableCollection<FinanceBlock> GetFinanceList() {
            try {
                StreamReader streamReader = new StreamReader(ReferenceValues.FILE_DIRECTORY + "finances2022.csv");
                while (!streamReader.EndOfStream) {
                    string str = streamReader.ReadLine();

                    if (str != null && str[0] != '!') {
                        string[] strArray = str.Split(',');

                        _list.Add(new FinanceBlock {
                            Date = strArray[0],
                            Item = strArray[1],
                            Cost = strArray[2],
                            Category = strArray[3],
                            Person = strArray[4]
                        });
                    }
                }

                streamReader.Close();
            } catch (Exception) {
                MessageBox.Show("Error! finances2022.csv file error.", "Error");
            }

            return _list;
        }

        public static void AddFiance(string line) {
            try {
                StreamWriter file = new StreamWriter(ReferenceValues.FILE_DIRECTORY + "finances2022.csv", true);
                file.WriteLine(line);
                file.Close();
                //return true;
            } catch (Exception) {
                MessageBox.Show("Error! Unable to save finances2022.csv file.", "Error");
                //return false;
            }
        }

        public static int[] GetSettings() {
            StreamReader streamReader = new StreamReader(ReferenceValues.FILE_DIRECTORY + "settings.csv");
            int[] values = { 0, 1 };

            while (!streamReader.EndOfStream) {
                string str = streamReader.ReadLine();

                if (str != null && str[0] != '!') {
                    string[] strArray = str.Split(',');
                    switch (strArray[0]) { }
                }
            }

            return values;
        }
    }
}