using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class PageLayout : ContentView
{
    public static readonly BindableProperty PageContentProperty = BindableProperty.Create(nameof(PageContent), typeof(View), typeof(PageLayout), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is PageLayout self && newValue is View view)
        {
            self.contentPresenter.Content = view;
        }
    });
    
    public View? PageContent
    {
        get => (View?)GetValue(PageContentProperty);
        set => SetValue(PageContentProperty, value);
    }
    
    public PageLayout()
    {
        InitializeComponent();
    }
}