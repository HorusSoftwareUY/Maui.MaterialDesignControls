﻿using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class TextFieldPage : BaseContentPage<TextFieldViewModel>
    {
        public TextFieldPage(TextFieldViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void MaterialTextField_Focused(object sender, FocusEventArgs e)
        {
            Labelfocused.Text = e.IsFocused ? "Focused" : "Unfocused";
        }
    }
}