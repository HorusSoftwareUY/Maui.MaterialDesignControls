using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ControlPageWithTopAppBar : ContentView
{
    public static readonly BindableProperty PageContentProperty = BindableProperty.Create(nameof(PageContent), typeof(View), typeof(ControlPageWithTopAppBar), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is ControlPageWithTopAppBar self && newValue is View view)
        {
            self.contentPresenter.Content = view;
        }
    });
    
    public static readonly BindableProperty AnimateScrollViewProperty = BindableProperty.Create(nameof(AnimateScrollView), typeof(bool), typeof(ControlPageWithTopAppBar), false, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is ControlPageWithTopAppBar self && newValue is bool animateScrollView)
        {
            self.topAppBar.ScrollViewName = animateScrollView ? "MdcControlPageScroll" : null;
        }
    });
    
    public View? PageContent
    {
        get => (View?)GetValue(PageContentProperty);
        set => SetValue(PageContentProperty, value);
    }
    
    public bool AnimateScrollView
    {
        get => (bool)GetValue(AnimateScrollViewProperty);
        set => SetValue(AnimateScrollViewProperty, value);
    }
    
    public ControlPageWithTopAppBar()
    {
        InitializeComponent();
    }
}