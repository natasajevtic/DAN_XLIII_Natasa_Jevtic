using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView(vwEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, employee);
        }
    }
}
