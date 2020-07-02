using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddReportView.xaml
    /// </summary>
    public partial class AddReportView : Window
    {
        public AddReportView(vwEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new AddReportViewModel(this, employee);
        }
    }
}
