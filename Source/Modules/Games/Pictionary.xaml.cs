﻿using HomeControl.Source.ViewModel.Games;

namespace HomeControl.Source.Modules.Games;

public partial class Pictionary {
    public Pictionary() {
        InitializeComponent();
        DataContext = new PictionaryVM();
    }
}