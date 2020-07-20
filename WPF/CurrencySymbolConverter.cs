using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace LocalizationDemo
{
 
    public class SfDataGridBehavior : Behavior<SfDataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.CellRenderers.Remove("Currency");
            this.AssociatedObject.CellRenderers.Add("Currency", new GridCellCurrencyRendererExt());
        }  
    }
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
    public class CurrencySymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as Employee;
            if (val == null)
                return val;
            var cultureInfo = new CultureInfo("fr-FR");
            if (val.EmployeeID > 1005)
            {
                cultureInfo.NumberFormat.CurrencySymbol = "F";
                return val.Salary.ToString("C", cultureInfo);
            }
                return val.Salary.ToString("C",cultureInfo);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    } 
}
