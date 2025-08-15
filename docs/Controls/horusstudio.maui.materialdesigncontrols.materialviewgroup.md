# MaterialViewGroup

View groups are logical groups of IGroupableView views. 
 This class provides the core logic for handling views that share a common parent layout and are grouped together.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialViewGroup → [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)

<br>

Remarks: We reuse some code from MAUI official repository: [See here.](https://github.com/dotnet/maui/blob/10.0.0-preview.5.25306.5/src/Controls/src/Core/RadioButton/RadioButtonGroup.cs)

### Chip XAML samples

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<FlexLayout Wrap="Wrap" Direction="Row" JustifyContent="Start"
		material:MaterialViewGroup.GroupName="TestingChipsGroup"
		material:MaterialViewGroup.SelectionType="Single"
		material:MaterialViewGroup.SelectedValue="{Binding SelectedChipItem}"
		BindableLayout.ItemsSource="{Binding Chips}">
		<BindableLayout.ItemTemplate>
			<DataTemplate x:DataType="x:String">
				<material:MaterialChip
					Type="Filter"
					Text="{Binding .}" />
			</DataTemplate>
		</BindableLayout.ItemTemplate>
	</FlexLayout>
```

[See more chip examples](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)

### Radio button XAML samples

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<VerticalStackLayout 
		material:MaterialViewGroup.GroupName="SingleGroupSample" 
		material:MaterialViewGroup.SelectionType="Single"
		material:MaterialViewGroup.SelectedValue="{Binding CheckedColor}"
		material:MaterialViewGroup.SelectedValueChangedCommand="{Binding CheckChangedCommand}"
		BindableLayout.ItemsSource="{Binding ItemsSourceColors}">
		<BindableLayout.ItemTemplate>
			<DataTemplate x:DataType="vm:CustomColor">
				<material:MaterialRadioButton 
					Text="{Binding Color}"
					Value="{Binding .}" />
			</DataTemplate>
		</BindableLayout.ItemTemplate>
	</VerticalStackLayout>
```

[See more radio button examples](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/RadioButtonPage.xaml)

## Known issues and pending features

* The SelectedValues property only supports binding to properties of type IList{object} or to classes that inherit from it.
