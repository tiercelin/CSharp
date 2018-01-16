using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContactManagmentSoftware
{
    /// <summary>
    /// Interaction logic for EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {
        public MainWindow mainWinDow;
        public int id;
        private int numLenght;
        private List<string> OriginalItemsComboBox = new List<string>();
        private Dictionary<int, string> myDico = new Dictionary<int, string>();
        //USE A DICO!!!
        private string housenb = "";
        private string streetname1 = "";
        private string streetname2 = "";
        private string postcode = "";
        private string city = "";
        private string county = "";
        private string country = "";

        public EditContact(MainWindow mainWin, int idc)
        {
            InitializeComponent();

            mainWinDow = mainWin;
            id = idc;
            numLenght = 15;

            isReadOnlyTxtBox();
            loadContact();

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

        private void loadContact()///USE A DICO
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

        private void ChangeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<string> filteredCollection = new List<string>();
            MySqlDataReader reader = null;

            bool addcheck = (bool)AddCheckBox.IsChecked;
            bool delcheck = (bool)DeleteCheckBox.IsChecked;
            if (addcheck || delcheck)
            {
                ChangeCheckBox.IsChecked = false;
            }
            //don't use 4 selected variables
            var selected = OriginalItemsComboBox.Where(item => item.Equals("First Name")).ToList();
            filteredCollection.AddRange(selected);
            var selected1 = OriginalItemsComboBox.Where(item => item.Equals("Last Name")).ToList();
            filteredCollection.AddRange(selected1);
            var selected2 = OriginalItemsComboBox.Where(item => item.Equals("Company Name")).ToList();
            filteredCollection.AddRange(selected2);

            reader = mainWinDow.mySimpleDataSource.Query("SELECT DataStored FROM ContactDictionary INNER JOIN ContactDetails  WHERE ContactDictionary.type = ContactDetails.iddictionary AND idcontact = '" + id + "'");

            while (reader.Read())
            {

                var selected3 = OriginalItemsComboBox.Where(item => item.Equals(reader.GetString(0))).ToList();

                filteredCollection.AddRange(selected3);
            }

            comboBox.ItemsSource = filteredCollection;

            reader.Close();
        }
        //RadioButton!!
        private void AddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<string> filteredCollection = new List<string>();
            MySqlDataReader reader = null;
            bool changecheck = (bool)ChangeCheckBox.IsChecked;
            bool delcheck = (bool)DeleteCheckBox.IsChecked;
            if (changecheck || delcheck)
            {
                AddCheckBox.IsChecked = false;
            }

            reader = mainWinDow.mySimpleDataSource.Query("SELECT DataStored FROM ContactDictionary WHERE type NOT IN (SELECT iddictionary FROM ContactDetails WHERE idcontact= '" + id + "')");

            while (reader.Read())
            {
                var selected3 = OriginalItemsComboBox.Where(item => item.Equals(reader.GetString(0))).ToList();

                filteredCollection.AddRange(selected3);
            }

            comboBox.ItemsSource = filteredCollection;

            reader.Close();
        }

        private void DeleteCeckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<string> filteredCollection = new List<string>();
            MySqlDataReader reader = null;
            bool addcheck = (bool)AddCheckBox.IsChecked;
            bool changecheck = (bool)ChangeCheckBox.IsChecked;
            if (addcheck || changecheck)
            {
                DeleteCheckBox.IsChecked = false;
            }

            reader = mainWinDow.mySimpleDataSource.Query("SELECT DataStored FROM ContactDictionary INNER JOIN ContactDetails  WHERE ContactDictionary.type = ContactDetails.iddictionary AND idcontact = '" + id + "'");

            while (reader.Read())
            {

                var selected3 = OriginalItemsComboBox.Where(item => item.Equals(reader.GetString(0))).ToList();

                filteredCollection.AddRange(selected3);
            }

            comboBox.ItemsSource = filteredCollection;

            reader.Close();
        }
        //Buttons
        private void ActionButton_Click(object sender, RoutedEventArgs e) ///Can be optimized
        {
            string Changechoice = comboBox.Text;
            string UpdateString = "";
            bool isChangechecked = false;
            bool isAddchecked = false;
            bool isDeletechecked = false;
            bool address = false;
            isChangechecked = (bool)ChangeCheckBox.IsChecked;
            isAddchecked = (bool)AddCheckBox.IsChecked;
            isDeletechecked = (bool)DeleteCheckBox.IsChecked;
            int iddico = -1;

            switch (Changechoice)
            {
                case "First Name":
                    if (MainWindow.isLettersAndSpaces(textBox.Text))
                    {
                        if (!changeAddNameCompany(0))
                        {
                            mainWinDow.mySimpleDataSource.Update("UPDATE ContactDirectory SET FirstName='" + textBox.Text + "' WHERE id='" + id + "'");
                            MessageBox.Show("Contact modified with success");
                        }
                        else MessageBox.Show("That contact already exist in the database. You can not have duplicates. The data has not been modified.");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Last Name":
                    if (MainWindow.isLettersAndSpaces(textBox.Text))
                    {
                        if (!changeAddNameCompany(1))
                        {
                            mainWinDow.mySimpleDataSource.Update("UPDATE ContactDirectory SET LastName='" + textBox.Text + "' WHERE id='" + id + "'");
                            MessageBox.Show("Contact modified with success");
                        }
                        else MessageBox.Show("That contact already exist in the database. You can not have duplicates. The data has not been modified.");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Position":
                    if (MainWindow.isLettersAndSpaces(textBox.Text) || isDeletechecked)
                    {
                        iddico = 1;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Street Name1":
                    if (MainWindow.isLettersAndSpaces(textBox.Text) || isDeletechecked)
                    {
                        iddico = 10;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "Street Name2":
                    if (MainWindow.isLettersAndSpaces(textBox.Text)/* && mainWinDow.alreadyExistInDetails(textBox.Text)*/ || isDeletechecked)
                    {
                        iddico = 11;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "City":
                    if ((MainWindow.isLettersAndSpaces(textBox.Text) /*&& mainWinDow.alreadyExistInDetails(textBox.Text)*/ ) || isDeletechecked)
                    {
                        iddico = 13;

                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "County":
                    if ((MainWindow.isLettersAndSpaces(textBox.Text)/* && mainWinDow.alreadyExistInDetails(textBox.Text)*/ ) || isDeletechecked)
                    {
                        iddico = 14;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "Country":
                    if ((MainWindow.isLettersAndSpaces(textBox.Text) /*&& mainWinDow.alreadyExistInDetails(textBox.Text)*/) || isDeletechecked)
                    {
                        iddico = 15;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "Company Name":
                    if (MainWindow.isLettersDigitsAndSpaces(textBox.Text))
                    {
                        if (!changeAddNameCompany(2))
                        {
                            mainWinDow.mySimpleDataSource.Update("UPDATE ContactDirectory SET Company='" + textBox.Text + "' WHERE id='" + id + "'");
                            MessageBox.Show("Contact modified with success");
                        }
                        else MessageBox.Show("That contact already exist in the database. You can not have duplicates. The data has not been modified.");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the company name");
                    break;
                case "Postal Code":
                    if ((MainWindow.isLettersDigitsAndSpaces(textBox.Text)/* && mainWinDow.alreadyExistInDetails(textBox.Text)*/ ) || isDeletechecked)
                    {
                        iddico = 12;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "Gender":
                    if (textBox.Text == "Mr" || textBox.Text == "Mrs" || isDeletechecked)
                    {
                        iddico = 0;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Email":
                    if (MainWindow.isAnEmail(textBox.Text) || isDeletechecked)
                    {
                        if (!mainWinDow.alreadyExistInDetails(textBox.Text) || isDeletechecked)
                        {
                            iddico = 2;
                        }
                        else
                        {

                            MessageBox.Show("This data already exist! You can not store a data that already exist in your database!");

                        }
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");

                    break;
                case "House Nb":
                    if (MainWindow.isOnlyNumbers(textBox.Text) || isDeletechecked)
                    {
                        iddico = 9;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    address = true;
                    break;
                case "Telephone":
                    if ((MainWindow.isOnlyNumbers(textBox.Text) && textBox.Text.Length <= numLenght) || isDeletechecked)
                    {
                        if (!mainWinDow.alreadyExistInDetails(textBox.Text) || isDeletechecked)
                        {
                            iddico = 3;
                        }
                        else MessageBox.Show("This data already exist! You can not store a data that already exist in your database!");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Mobile":
                    if ((MainWindow.isOnlyNumbers(textBox.Text) && textBox.Text.Length <= numLenght) || isDeletechecked)
                    {
                        if (!mainWinDow.alreadyExistInDetails(textBox.Text) || isDeletechecked)
                        {
                            iddico = 4;
                        }
                        else MessageBox.Show("This data already exist! You can not store a data that already exist in your database!");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Work":
                    if ((MainWindow.isOnlyNumbers(textBox.Text) && textBox.Text.Length <= numLenght) || isDeletechecked)
                    {
                        if (!mainWinDow.alreadyExistInDetails(textBox.Text) || isDeletechecked)
                        {
                            iddico = 5;
                        }
                        else MessageBox.Show("This data already exist! You can not store a data that already exist in your database!");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Fax":
                    if ((MainWindow.isOnlyNumbers(textBox.Text) && textBox.Text.Length <= numLenght) || isDeletechecked)
                    {
                        if (!mainWinDow.alreadyExistInDetails(textBox.Text) || isDeletechecked)
                        {
                            iddico = 6;
                        }
                        else MessageBox.Show("This data already exist! You can not store a data that already exist in your database!");
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Birthday":
                    if (MainWindow.isADate(textBox.Text) || isDeletechecked)
                    {
                        iddico = 7;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Years in the company":
                    if ((MainWindow.isOnlyNumbers(textBox.Text) && textBox.Text.Length == 4) || isDeletechecked)
                    {
                        iddico = 8;
                    }
                    else MessageBox.Show("You did not enter a correct value => it did not change the data");
                    break;
                case "Notes":
                    iddico = 16;
                    break;
                default:
                    break;
            }

            if (iddico != -1 && Changechoice != "First Name" && Changechoice != "Last Name" && Changechoice != "Company Name")
            {

                if (isChangechecked)
                {
                    if (address)
                    {
                        AddAddressWindow addWind = new AddAddressWindow(this);
                        addWind.ShowDialog();
                        UpdateString = "";
                    }
                    else UpdateString = "UPDATE ContactDetails SET data='" + textBox.Text + "' WHERE idcontact='" + id + "' AND iddictionary ='" + iddico + "'";
                }
                if (isAddchecked)
                {
                    if (address)
                    {
                        AddAddressWindow addWind = new AddAddressWindow(this);
                        addWind.ShowDialog();
                        UpdateString = "";
                    }
                    else UpdateString = "INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + id + "', '" + iddico + "', '" + textBox.Text + "')";
                }
                if (isDeletechecked)
                {
                    if (iddico == 9 || iddico == 10 || iddico == 11 || iddico == 12 || iddico == 13 || iddico == 14 || iddico == 15)
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the whole address ? ", "Delete Contact", System.Windows.MessageBoxButton.YesNoCancel);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                for (int i = 9; i < 16; i++)
                                {
                                    mainWinDow.mySimpleDataSource.Update("DELETE FROM ContactDetails WHERE iddictionary = '" + i + "' AND idcontact='" + id + "'");
                                }
                                break;
                            case MessageBoxResult.No:
                            case MessageBoxResult.Cancel:
                                MessageBox.Show("The contact has been kept");
                                break;
                        }
                    }
                    UpdateString = "DELETE FROM ContactDetails WHERE idcontact='" + id + "' AND iddictionary ='" + iddico + "'";
                }
                mainWinDow.mySimpleDataSource.Update(UpdateString);
                MessageBox.Show("Contact modified with success");
                textBox.Text = "";
            }
            resetEdit();
            loadContact();
        }

        private void AllDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            mainWinDow.ContactIDTxtBox.Text = id + "";
            mainWinDow.DeleteContactButton_Click(sender, e);
            mainWinDow.refreshDataGrid();
            mainWinDow.ContactIDTxtBox.Text = "";
            this.Close();
        }

        private void QuitEditionButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void comboBox_Loaded(object sender, RoutedEventArgs e)
        {
            OriginalItemsComboBox.Add("First Name");
            OriginalItemsComboBox.Add("Last Name");
            OriginalItemsComboBox.Add("Company Name");
            OriginalItemsComboBox.Add("Position");
            OriginalItemsComboBox.Add("House Nb");
            OriginalItemsComboBox.Add("Street Name1");
            OriginalItemsComboBox.Add("Street Name2");
            OriginalItemsComboBox.Add("Postal Code");
            OriginalItemsComboBox.Add("City");
            OriginalItemsComboBox.Add("County");
            OriginalItemsComboBox.Add("Country");
            OriginalItemsComboBox.Add("Gender");
            OriginalItemsComboBox.Add("Email");
            OriginalItemsComboBox.Add("Telephone");
            OriginalItemsComboBox.Add("Mobile");
            OriginalItemsComboBox.Add("Work");
            OriginalItemsComboBox.Add("Fax");
            OriginalItemsComboBox.Add("Notes");
            OriginalItemsComboBox.Add("Birthday");
            OriginalItemsComboBox.Add("Years in the company");

            var comboBox = sender as ComboBox;

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
        private bool changeAddNameCompany(int col)
        {
            MySqlDataReader reader = null;
            string fname = "", lname = "", comp = "";
            reader = mainWinDow.mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE id = '" + id + "'");
            if (reader.Read())
            {
                fname = reader.GetString(1);
                lname = reader.GetString(2);
                comp = reader.GetString(3);
            }
            reader.Close();

            switch (col)
            {
                case 0:
                    reader = mainWinDow.mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE FirstName= '" + textBox.Text + "' AND LastName='" + lname + "' AND Company='" + comp + "'");
                    break;
                case 1:
                    reader = mainWinDow.mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE FirstName= '" + fname + "' AND LastName='" + textBox.Text + "' AND Company='" + comp + "'");
                    break;
                case 2:
                    reader = mainWinDow.mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE FirstName= '" + fname + "' AND LastName='" + lname + "' AND Company='" + textBox.Text + "'");
                    break;
            }
            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }

        private void resetEdit()
        {
            List<string> comboBoxList = new List<string>();
            bool isChangechecked = false;
            bool isAddchecked = false;
            bool isDeletechecked = false;

            isChangechecked = (bool)ChangeCheckBox.IsChecked;
            isAddchecked = (bool)AddCheckBox.IsChecked;
            isDeletechecked = (bool)DeleteCheckBox.IsChecked;

            if (isChangechecked)
            {
                ChangeCheckBox.IsChecked = false;
            }
            if (isAddchecked)
            {
                AddCheckBox.IsChecked = false;
            }
            if (isDeletechecked)
            {
                DeleteCheckBox.IsChecked = false;
            }
            textBox.Text = "";

            comboBox.ItemsSource = comboBoxList;
        }
    }
}
