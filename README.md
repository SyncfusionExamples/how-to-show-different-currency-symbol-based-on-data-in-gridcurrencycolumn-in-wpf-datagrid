# How to show different currency symbol based on data in GridCurrencyColumn in WPF DataGrid(SfDataGrid)?

## About the sample

This sample show cases how to show different currency symbol based on data in GridCurrencyColumn in [WPF DataGrid](https://www.syncfusion.com/wpf-ui-controls/datagrid) (SfDataGrid)?

[WPF DataGrid](https://www.syncfusion.com/wpf-ui-controls/datagrid) (SfDataGrid) does not provide the support to show the different currency symbol based on the data in [GridCurrencyColumn](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfGrid.WPF~Syncfusion.UI.Xaml.Grid.GridCurrencyColumn.html). You can achieve this by adding converter for currency symbol and assigned it for edit element in [OnInitializeEditElement](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfGrid.WPF~Syncfusion.UI.Xaml.TreeGrid.Cells.TreeGridVirtualizingCellRendererBase%602~OnInitializeEditElement.html) method of [GridCellCurrencyRenderer](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfGrid.WPF~Syncfusion.UI.Xaml.Grid.Cells.GridCellCurrencyRenderer.html?_ga=2.191477162.1055857806.1595159344-393461464.1573033696).

```c#
public class CurrencyConverter : IValueConverter
{
public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
{
var val = value as Employee;
if (val == null)
return "C";
var cultureInfo = new CultureInfo("fr-FR");
if (val.EmployeeID > 1005)
return "F";                
return cultureInfo.NumberFormat.CurrencySymbol;
}

public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
{
throw new NotImplementedException();
}
}
```

```c#
public class GridCellCurrencyRendererExt : GridCellCurrencyRenderer
{
public override void OnInitializeDisplayElement(DataColumnBase dataColumn, TextBlock uiElement, object dataContext)
{
base.OnInitializeDisplayElement(dataColumn, uiElement, dataContext);
}
public override void OnInitializeEditElement(DataColumnBase dataColumn, CurrencyTextBox uiElement, object dataContext)
{
base.OnInitializeEditElement(dataColumn, uiElement, dataContext);
var binding = new Binding {Converter=new CurrencyConverter() };
uiElement.SetBinding(CurrencyTextBox.CurrencySymbolProperty, binding);
}
}
```

```c#
public class SfDataGridBehavior : Behavior<SfDataGrid>
{
protected override void OnAttached()
{
base.OnAttached();
this.AssociatedObject.CellRenderers.Remove("Currency");
this.AssociatedObject.CellRenderers.Add("Currency", new GridCellCurrencyRendererExt());
}  
}
```

## Requirements to run the demo

Visual Studio 2015 and above versions
