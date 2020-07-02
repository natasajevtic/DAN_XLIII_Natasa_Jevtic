using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EditReportView.xaml
    /// </summary>
    public partial class EditReportView : Window
    {
        public EditReportView(vwReport reportToDelete)
        {
            InitializeComponent();
            this.DataContext = new EditReportViewModel(this, reportToDelete);
        }
    }
}
