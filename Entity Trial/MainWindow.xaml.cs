using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

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
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    using var ctx = new AppDbContext();
                    // Creates the DB + tables if they don't exist
                    ctx.Database.EnsureCreated();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB setup failed: " + ex.Message);
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var ctx = new AppDbContext();
                var list = await ctx.People
                                     .OrderBy(p => p.Id)
                                     .ToListAsync();

                PeopleListBox.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load failed: " + ex.Message);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text)
                || BirthdayPicker.SelectedDate is null)
            {
                MessageBox.Show("Name and Birthday are required.");
                return;
            }

            var person = new Person
            {
                Name = NameTextBox.Text.Trim(),
                Birthday = BirthdayPicker.SelectedDate.Value,
                Occupation = OccupationTextBox.Text.Trim()
            };

            try
            {
                using var ctx = new AppDbContext();
                ctx.People.Add(person);
                await ctx.SaveChangesAsync();

                MessageBox.Show("Saved via EF Core!");
                // Clear inputs
                NameTextBox.Clear();
                BirthdayPicker.SelectedDate = null;
                OccupationTextBox.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
            }
        }
    }
}