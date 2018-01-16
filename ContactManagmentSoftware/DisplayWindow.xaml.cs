using MySql.Data.MySqlClient;
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

namespace ContactManagmentSoftware
{
    /// <summary>
    /// Interaction logic for DisplayWindow.xaml
    /// </summary>
    public partial class DisplayWindow : Window
    {
        public MainWindow mainWinDow;
        public int id;
        private Dictionary<int, string> myDico = new Dictionary<int, string>();
        private string housenb = "";
        private string streetname1 = "";
        private string streetname2 = "";
        private string postcode = "";
        private string city = "";
        private string county = "";
        private string country = "";

        public DisplayWindow(MainWindow mainWin, int idc)
        {
            InitializeComponent();

            mainWinDow = mainWin;
            id = idc;
            isReadOnlyTxtBox();
            loadContact();

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

        private void loadContact()
        {
            MySqlDataReader reader = null;

            myDico.Clear();

            clearTxtBox();

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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mainWinDow.Show();
            mainWinDow.ContactIDTxtBox.Text = "";
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainWinDow.DeleteContactButton_Click(sender, e);
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mainWinDow.EditContactButton_Click(sender, e);
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            mainWinDow.SearchContactButton_Click(sender, e);
            this.Close();
        }
    }
}
