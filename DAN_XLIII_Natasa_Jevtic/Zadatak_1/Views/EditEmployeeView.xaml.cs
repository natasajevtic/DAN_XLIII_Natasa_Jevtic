using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EditEmployeeView.xaml
    /// </summary>
    public partial class EditEmployeeView : Window
    {
        public EditEmployeeView(vwEmployee employeeToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditEmployeeViewModel(this, employeeToEdit);
        }
    }
}

