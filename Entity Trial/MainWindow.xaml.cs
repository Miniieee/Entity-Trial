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
        private readonly ObservableCollection<Person> _people = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            PeopleListBox.ItemsSource = _people;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    using var ctx = new AppDbContext();
                    ctx.Database.EnsureCreated();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB setup failed: " + ex.Message);
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
            => await ReloadPeopleAsync();

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || BirthdayPicker.SelectedDate is null)
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
                await ReloadPeopleAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = NameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a name to search for.");
                return;
            }

            try
            {
                using var ctx = new AppDbContext();
                var results = await ctx.People
                                       .Where(p => p.Name.Contains(searchTerm))
                                       .OrderBy(p => p.Id)
                                       .ToListAsync();

                _people.Clear();
                foreach (var p in results)
                    _people.Add(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search failed: " + ex.Message);
            }
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PeopleListBox.SelectedItem is Person selected)
            {
                NameTextBox.Text = selected.Name;
                BirthdayPicker.SelectedDate = selected.Birthday;
                OccupationTextBox.Text = selected.Occupation;
                ModifyButton.IsEnabled = true;
            }
            else
            {
                ClearInputs();
                ModifyButton.IsEnabled = false;
            }
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleListBox.SelectedItem is not Person selected)
            {
                MessageBox.Show("Please select a person to modify.");
                return;
            }
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || BirthdayPicker.SelectedDate is null)
            {
                MessageBox.Show("Name and Birthday are required.");
                return;
            }

            try
            {
                using var ctx = new AppDbContext();
                var person = await ctx.People.FindAsync(selected.Id);
                if (person is null)
                {
                    MessageBox.Show("That record no longer exists.");
                    return;
                }

                person.Name = NameTextBox.Text.Trim();
                person.Birthday = BirthdayPicker.SelectedDate.Value;
                person.Occupation = OccupationTextBox.Text.Trim();

                await ctx.SaveChangesAsync();
                MessageBox.Show("Record updated!");

                await ReloadPeopleAsync();
                ClearInputs();
                PeopleListBox.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Modify failed: " + ex.Message);
            }
        }

        private async Task ReloadPeopleAsync()
        {
            try
            {
                using var ctx = new AppDbContext();
                var list = await ctx.People.OrderBy(p => p.Id).ToListAsync();
                _people.Clear();
                foreach (var p in list)
                    _people.Add(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load failed: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            NameTextBox.Clear();
            BirthdayPicker.SelectedDate = null;
            OccupationTextBox.Clear();
        }
    }
}