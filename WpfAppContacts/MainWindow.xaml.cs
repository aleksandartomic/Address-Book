using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ContactsEntities db = new ContactsEntities();
        public static DataGrid datagrid;
        public string Photo { get; set; } = "Image/images1.jpg";

        public MainWindow()
        {
            InitializeComponent();
            Load();
            this.DataContext = this;
        }

        private void Load()
        {
            dataGrid.ItemsSource = db.Contacts.ToList();
            datagrid = dataGrid;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Contact newContact = new Contact()
            {
                Name = txtName.Text,
                Last_Name = txtLastName.Text,
                Number = txtNumber.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                PhotoUrl = ImgPhoto.Source.ToString()
            };

            txtName.Clear();
            txtLastName.Clear();
            txtNumber.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            Uri imageUri = new Uri(Photo, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            ImgPhoto.Source = imageBitmap;

            db.Contacts.Add(newContact);
            db.SaveChanges();
            MainWindow.datagrid.ItemsSource = db.Contacts.ToList();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem != null)
            {
                Contact contactTest = (datagrid.SelectedItem as Contact);

                db.Contacts.Remove(contactTest);
                db.SaveChanges();
                datagrid.ItemsSource = db.Contacts.ToList();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int Id = (datagrid.SelectedItem as Contact).Id;
                EditPage Epage = new EditPage(Id);
                Epage.ShowDialog();
            }
        }

        private void BtnPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Contact> nameList = db.Contacts.ToList();
            var filteredList = from nl in nameList
                               where nl.Name.ToLower().StartsWith(txtSearch.Text.ToLower())
                               select nl;
            dataGrid.ItemsSource = filteredList;
            
            if(txtSearch.Text == String.Empty)
            {
                dataGrid.ItemsSource = db.Contacts.ToList();
            }
        }

        private void TxtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
