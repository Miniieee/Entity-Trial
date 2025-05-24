using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Entity_Trial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCalibratedBy_Click(object sender, RoutedEventArgs e)
        {
            // TODO: open search dialog for “Calibrated By”
        }
        private void AddType_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddManufacturer_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddUnitMeasure_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddOwner_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddArea_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddAssignee_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddEnvironment_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void AddReferenceItem_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void UploadAttachment_Click(object sender, RoutedEventArgs e) { /* … */ }
        private void SaveButton_Click(object sender, RoutedEventArgs e) { /* … */ }
    }
}