using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppContacts
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Window
    {
        ContactsEntities db = new ContactsEntities();
        int Id;
        public static DataGrid datagrid;



        public EditPage(int ContactId)
        {
            InitializeComponent();
            Id = ContactId;

            Contact user = new Contact();
            user = (from c in db.Contacts
                    where c.Id == Id
                    select c).Single();

            txtName.Text = user.Name;
            txtLastName.Text = user.Last_Name;
            txtNumber.Text = user.Number;
            txtAddress.Text = user.Address;
            txtEmail.Text = user.Email;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Contact editContact = (from c in db.Contacts
                                   where c.Id == Id
                                   select c).Single();

            editContact.Name = txtName.Text;
            editContact.Last_Name = txtLastName.Text;
            editContact.Number = txtNumber.Text;
            editContact.Address = txtAddress.Text;
            editContact.Email = txtEmail.Text;

            db.SaveChanges();
            MainWindow.datagrid.ItemsSource = db.Contacts.ToList();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void BtnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics | *.jpg; *.jpeg; *.png | " +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if(op.ShowDialog() == true)
            {
                ImgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
