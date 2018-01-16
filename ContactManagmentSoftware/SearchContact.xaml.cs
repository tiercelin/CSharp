using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ContactManagmentSoftware
{
    /// <summary>
    /// Interaction logic for SearchContact.xaml
    /// </summary>
    public partial class SearchContact : Window
    {
        public MainWindow mainWinDow;
        private int search;
        private Dictionary<int, string> myDico = new Dictionary<int, string>();

        public SearchContact(MainWindow Window)
        {
            InitializeComponent();
            mainWinDow = Window;
            dataGrid.IsReadOnly = true;
            search = 6;
            clearTxtBox();
            isReadOnlyTxtBox();
            textBox.Text = "";
        }

        private void NameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CompanyTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PhoneTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BirthDayTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EmailTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GenderTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PositionTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NotesTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddressTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void YearsTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PhoneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhoneTxtBox.Text = "";

            switch (PhoneComboBox.SelectedIndex)
            {
                case 0:
                    PhoneTxtBox.Text = "Choose a phone to display";
                    break;
                case 1:
                    if (myDico.ContainsKey(1))
                    {
                        PhoneTxtBox.Text = myDico[1];
                    }
                    else PhoneTxtBox.Text = "No data stored";
                    break;
                case 2:
                    if (myDico.ContainsKey(2))
                    {
                        PhoneTxtBox.Text = myDico[2];
                    }
                    else PhoneTxtBox.Text = "No data stored";
                    break;
                case 3:
                    if (myDico.ContainsKey(3))
                    {
                        PhoneTxtBox.Text = myDico[3];
                    }
                    else PhoneTxtBox.Text = "No data stored";
                    break;
                case 4:
                    if (myDico.ContainsKey(4))
                    {
                        PhoneTxtBox.Text = myDico[4];
                    }
                    else PhoneTxtBox.Text = "No data stored";
                    break;
                default:
                    break;
            }


        }
        private void PhoneComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> comboBoxList = new List<string>();
            comboBoxList.Add("Choose wich phone you want to display");
            comboBoxList.Add("Telephone");
            comboBoxList.Add("Mobile");
            comboBoxList.Add("Work");
            comboBoxList.Add("Fax");

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = comboBoxList;
            comboBox.SelectedIndex = 0;
        }


        private void NameRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 0;
        }

        private void CompanyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 1;
        }

        private void PositionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 2;
        }

        private void EmailRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 3;
        }

        private void PhoneRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 4;
        }

        private void AddressRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            search = 5;
        }

        private void isReadOnlyTxtBox()
        {
            NameTxtBox.IsReadOnly = true;
            CompanyTxtBox.IsReadOnly = true;
            PhoneTxtBox.IsReadOnly = true;
            EmailTxtBox.IsReadOnly = true;
            AddressTxtBox.IsReadOnly = true;
            YearsTxtBox.IsReadOnly = true;
            NotesTxtBox.IsReadOnly = true;
            GenderTxtBox.IsReadOnly = true;
            BirthDayTxtBox.IsReadOnly = true;
            PositionTxtBox.IsReadOnly = true;
        }
        private void clearTxtBox()
        {
            NameTxtBox.Clear();
            CompanyTxtBox.Clear();
            AddressTxtBox.Clear();
            EmailTxtBox.Clear();
            PositionTxtBox.Clear();
            BirthDayTxtBox.Clear();
            PhoneTxtBox.Clear();
            GenderTxtBox.Clear();
            YearsTxtBox.Clear();
            NotesTxtBox.Clear();
        }

        private void loadContact(int id)
        {
            MySqlDataReader reader = null;

            string housenb = "";
            string streetname1 = "";
            string streetname2 = "";
            string postcode = "";
            string city = "";
            string county = "";
            string country = "";

            myDico.Clear();

            reader = mainWinDow.mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE id='" + id + "'");
            if (reader.Read())
            {
                NameTxtBox.Text = reader[1] + " " + reader[2];
                CompanyTxtBox.Text = reader[3] + "";
            }

            reader.Close();

            reader = mainWinDow.mySimpleDataSource.Query("SELECT iddictionary, data FROM ContactDetails WHERE idcontact='" + id + "'");

            while (reader.Read())
            {
                int read = reader.GetInt32(0);
                switch (read)
                {
                    case 0:
                        GenderTxtBox.Text = reader.GetString(1);
                        break;
                    case 1:
                        PositionTxtBox.Text = reader.GetString(1);
                        break;
                    case 2:
                        EmailTxtBox.Text = reader.GetString(1);
                        break;
                    case 3:
                        myDico.Add(1, reader.GetString(1));
                        break;
                    case 4:
                        myDico.Add(2, reader.GetString(1));
                        break;
                    case 5:
                        myDico.Add(3, reader.GetString(1));
                        break;
                    case 6:
                        myDico.Add(4, reader.GetString(1));
                        break;
                    case 7:
                        BirthDayTxtBox.Text = reader.GetString(1);
                        break;
                    case 8:
                        YearsTxtBox.Text = reader.GetString(1);
                        break;
                    case 9:
                        housenb = reader.GetString(1);
                        break;
                    case 10:
                        streetname1 = reader.GetString(1);
                        break;
                    case 11:
                        streetname2 = reader.GetString(1);
                        break;
                    case 12:
                        postcode = reader.GetString(1);
                        break;
                    case 13:
                        city = reader.GetString(1);
                        break;
                    case 14:
                        county = reader.GetString(1);
                        break;
                    case 15:
                        country = reader.GetString(1);
                        break;
                    case 16:
                        NotesTxtBox.Text = reader.GetString(1);
                        break;
                    default:
                        break;

                }
            }
            reader.Close();
            AddressTxtBox.Text = housenb + " " + streetname1 + "\n" + streetname2 + "\n" + postcode + "\n" + city + "\n" + county + "\n" + country;
            PhoneTxtBox.Text = "";

        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void QuitSearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            clearTxtBox();
            switch (search)
            {
                case 0:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE FirstName LIKE '%" + textBox.Text + "%' OR LastName LIKE '%" + textBox.Text + "%' ");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                case 1:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE Company LIKE '%" + textBox.Text + "%'");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                case 2:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE id IN (SELECT idcontact FROM ContactDetails WHERE data LIKE '%" + textBox.Text + "%' AND iddictionary = 1)");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                case 3:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE id IN (SELECT idcontact FROM ContactDetails WHERE data LIKE '%" + textBox.Text + "%' AND iddictionary = 2)");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                case 4:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE id IN (SELECT idcontact FROM ContactDetails WHERE data LIKE '%" + textBox.Text + "%' AND (iddictionary = 3 OR iddictionary = 4 OR iddictionary = 5 OR iddictionary = 6))");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                case 5:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE id IN (SELECT idcontact FROM ContactDetails WHERE data LIKE '%" + textBox.Text + "%' AND (iddictionary = 9 OR iddictionary = 10 OR iddictionary = 11 OR iddictionary = 12 OR iddictionary = 13 OR iddictionary = 14 OR iddictionary = 15))");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;
                default:
                    if (mainWinDow.mySimpleDataSource != null)
                    {
                        MessageBox.Show("You did not select a way to search, so your data has been search in every contact fields");
                        DataTable dt = mainWinDow.mySimpleDataSource.DataTableQuery("SELECT DISTINCT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory WHERE id IN (SELECT DISTINCT idcontact FROM ContactDetails WHERE data LIKE '%" + textBox.Text + "%') OR FirstName LIKE '%" + textBox.Text + "%' OR LastName LIKE '%" + textBox.Text + "%'  OR Company LIKE '%" + textBox.Text + "%' ");
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    break;


            }
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            int b = 0;
            if (dataGrid.CurrentItem != null)
            {
                object[] a = ((DataRowView)dataGrid.CurrentItem).Row.ItemArray;
                b = (int)a[0];
            }
            loadContact(b);
        }
    }
}
