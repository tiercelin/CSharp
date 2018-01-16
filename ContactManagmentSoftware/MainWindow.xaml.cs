using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ContactManagmentSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SimpleDataSource mySimpleDataSource = new SimpleDataSource("mysql.cs.bangor.ac.uk", "eeu8a9", 3306, "eeu8a9", "aMuSic97");
        private string defaultText = "Hello";
        private HashSet<string> myset;
        private Dictionary<int, string> addDico;

        public MainWindow()
        {
            InitializeComponent();

            addDico = new Dictionary<int, string>();

            initialiseSet();

            //Prevent the user to change the datagrid 
            dataGrid.IsReadOnly = true;

            ContactIDTxtBox.IsReadOnly = true;

            refreshDataGrid();
        }


        //Buttons
        public void SearchContactButton_Click(object sender, RoutedEventArgs e)
        {
            SearchContact mySearchWindow = new SearchContact(this);
            mySearchWindow.Show();

        }

        public void DisplayContactButton_Click(object sender, RoutedEventArgs e)
        {
            int j;
            if (Int32.TryParse(ContactIDTxtBox.Text, out j))
            {
                DisplayWindow window3 = new DisplayWindow(this, j);
                window3.Show();
            }
            else MessageBox.Show("Please enter a select a contact first! ");

        }

        public void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            int j;
            if (Int32.TryParse(ContactIDTxtBox.Text, out j))
            {
                EditContact window2 = new EditContact(this, j);
                window2.Show();
            }
            else MessageBox.Show("Please enter a select a contact first! ");

        }

        public void DeleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable myDataTable = null;

            //If the ContactIDTxtBox does not contain a number
            if (isOnlyNumbers(ContactIDTxtBox.Text) == false || myset.Contains(ContactIDTxtBox.Text) || ContactIDTxtBox.Text == string.Empty)
            {
                MessageBox.Show("You need to enter the Id value of the contact that you want to delete! ");
            }

            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this contact", "Delete Contact", System.Windows.MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        myDataTable = mySimpleDataSource.DataTableQuery("DELETE FROM ContactDirectory WHERE id='" + ContactIDTxtBox.Text + "'");
                        myDataTable = mySimpleDataSource.DataTableQuery("DELETE FROM ContactDetails WHERE idcontact='" + ContactIDTxtBox.Text + "'");
                        break;
                    case MessageBoxResult.No:
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("The contact has been kept");
                        break;
                }

            }
            refreshDataGrid();
            ContactIDTxtBox.Text = "";
        }

        private void AddNewContactButton_Click(object sender, RoutedEventArgs e)
        {


            //if the name and the company is not entered by the user, the contact is not created !
            if ((NewContactFirstNameTxtBox.Text.Equals("First Name") || NewContactFirstNameTxtBox.Text.Length == 0) || (NewContactLastNameTxtBox.Text.Equals("Last Name") || NewContactLastNameTxtBox.Text.Length == 0) || (NewContactCompanyNameTxtBox.Text.Equals("Company Name") || NewContactCompanyNameTxtBox.Text.Length == 0))
            {
                MessageBox.Show("You need to enter the First Name and Last Name, as well as the Company Name to be able to add a new contact! ");
            }
            else
            {
                add();
            }
            refreshDataGrid();

        }
        //TextBox

        //Delete the text inside the box if it is the help text to complete the text box
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            defaultText = tb.Text;
            if (myset.Contains(defaultText))
            {
                tb.Text = string.Empty;
            }

            //tb.GotFocus -= TextBox_GotFocus;
        }

        private void switchsender(RoutedEventArgs e)
        {
            if (e.Source == NewContactFirstNameTxtBox)
            {
                defaultText = "First Name";
            }
            if (e.Source == NewContactLastNameTxtBox)
            {
                defaultText = "Last Name";
            }
            if (e.Source == NewContactCompanyNameTxtBox)
            {
                defaultText = "Company Name";
            }
            if (e.Source == NewContactBirthDayTxtBox)
            {
                defaultText = "dd/mm/yyyy";
            }
            if (e.Source == NewContactCityTxtBox)
            {
                defaultText = "City";
            }
            if (e.Source == NewContactCountryTxtBox)
            {
                defaultText = "Country";
            }
            if (e.Source == NewContactCountyTxtBox)
            {
                defaultText = "County";
            }
            if (e.Source == NewContactEmailTxtBox)
            {
                defaultText = "Email address";
            }
            if (e.Source == NewContactHouseNbTxtBox)
            {
                defaultText = "House Number";
            }
            if (e.Source == NewContactNotesTxtBox)
            {
                defaultText = "Notes";
            }
            if (e.Source == NewContactPhoneNbTxtBox)
            {
                defaultText = "No spaces or +";
            }
            if (e.Source == NewContactPositionTxtBox)
            {
                defaultText = "Position";
            }
            if (e.Source == NewContactPostalCodeTxtBox)
            {
                defaultText = "Postal Code";
            }
            if (e.Source == NewContactStreetName1TxtBox)
            {
                defaultText = "Street Name";
            }
            if (e.Source == NewContactStreetName2TxtBox)
            {
                defaultText = "Street Name2";
            }
            if (e.Source == NewContactYearInTheCompanyTxtBox)
            {
                defaultText = "Year (yyyy)";
            }

        }
        //Put the text back
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == string.Empty)
            {
                switchsender(e);
                tb.Text = defaultText;
            }

            // tb.LostFocus -= TextBox_LostFocus;
        }
        

        //Combo Boxes
        private void NewContactPhoneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myComboBox = sender as ComboBox;
        }

        private void NewContactGenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myComboBox = sender as ComboBox;
        }

        private void NewContactPhoneComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> comboBoxList = new List<string>();
            comboBoxList.Add("Telephone");
            comboBoxList.Add("Mobile");
            comboBoxList.Add("Work");
            comboBoxList.Add("Fax");

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = comboBoxList;

            comboBox.SelectedIndex = 0;
        }

        private void NewContactGenderComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> comboBoxList = new List<string>();
            comboBoxList.Add("Select a gender");
            comboBoxList.Add("Mr");
            comboBoxList.Add("Mrs");

            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = comboBoxList;

            comboBox.SelectedIndex = 0;
        }


        public void refreshDataGrid()
        {
            if (mySimpleDataSource != null)
            {
                DataTable dt = mySimpleDataSource.DataTableQuery("SELECT id, CONCAT(FirstName,' ', LastName) AS 'Name', Company FROM ContactDirectory");
                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        //Check methods for TxtBox Inputs
        public static bool isOnlyNumbers(string s)//HouseNb, PhoneNb and Year
        {
            string sPattern = "^[0-9]*$";

            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isLettersAndSpaces(string s) // Check for FirstName, LastName, Position, StreetName1, StreetName2, City, County, Country
        {
            string sPattern = "^[a-zA-Z ]*$";
            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isLettersDigitsAndSpaces(string s)//Company, PostalCode
        {
            string sPattern = "^[a-zA-Z0-9 ]*$";
            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isAnEmail(string s)//Email
        {
            //Pattern found on the internet to try to have the best email address validation as possible. 
            // Check that there is text before and after the @, that there is only one @ in the address, and makes sure that there is a domain name
            string sPattern = "^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))$";
            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isADate(string s)//BirthDate
        {
            DateTime date;
            if (DateTime.TryParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return true;
            }
            else return false;
        }

        //Methods for adding data
        public void add()
        {
            bool boolemail = true;
            bool boolposition = true;
            bool boolpostaladdress = true;
            bool boolphonenumber = true;
            bool boolyear = true;
            bool boolbirthday = true;

            MySqlDataReader reader = null;

            addDico.Clear();

            //if the Name and Company fields are filled and checked correctly
            if (isLettersAndSpaces(NewContactFirstNameTxtBox.Text) && isLettersAndSpaces(NewContactFirstNameTxtBox.Text) && isLettersDigitsAndSpaces(NewContactCompanyNameTxtBox.Text))
            {
                reader = mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE FirstName='" + NewContactFirstNameTxtBox.Text + "' AND LastName='" + NewContactLastNameTxtBox.Text + "'  AND Company='" + NewContactCompanyNameTxtBox.Text + "'");
                if (reader.Read())
                {
                    reader.Close();
                    MessageBox.Show("The Contact already exist! ");
                }
                else
                {
                    reader.Close();

                    //I add the other information
                    addgender();
                    boolemail = addemail();
                    boolposition = addposition();
                    boolpostaladdress = addpostaladdress();
                    boolphonenumber = addphonenumber();
                    boolyear = addyear();
                    boolbirthday = addbirthday();
                    addnotes();

                    if (boolemail && boolbirthday && boolphonenumber && boolposition && boolpostaladdress && boolyear)
                    {
                        mySimpleDataSource.Update("INSERT INTO ContactDirectory(id, FirstName, LastName, Company) VALUES(null, '" + NewContactFirstNameTxtBox.Text + "', '" + NewContactLastNameTxtBox.Text + "', '" + NewContactCompanyNameTxtBox.Text + "')");


                        //I get the Contact ID back in idnew
                        int idnew = 0;

                        reader = mySimpleDataSource.Query("SELECT * FROM ContactDirectory WHERE FirstName='" + NewContactFirstNameTxtBox.Text + "' AND LastName= '" + NewContactLastNameTxtBox.Text + "' AND Company='" + NewContactCompanyNameTxtBox.Text + "'");
                        if (reader.Read())
                        { idnew = reader.GetInt32(0); }
                        reader.Close();


                        mySimpleDataSource.NonQueryPreparedStatement("INSERT INTO ContactDetails (idkey, idcontact, iddictionary, data) VALUES(null," + idnew + ", @iddictionary, @data)", addDico);

                        MessageBox.Show("The contact has been successfully added to your database! ");
                        resetTxtBox();
                    }

                }

            }
            else
            {
                MessageBox.Show("You did not enter good values for the names and company! \n Be carefull to only use letters and spaces for the names. You can also have digits in the company name.");
            }

        }

        public void addgender()
        {
            if (!addDico.ContainsValue(NewContactGenderComboBox.Text) && NewContactGenderComboBox.Text != "Select a gender")
            {
                addDico.Add(0, NewContactGenderComboBox.Text);
            }

            //mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', 0, '" + NewContactGenderComboBox.Text + "')");
        }

        public bool addbirthday()
        {
            if (isADate(NewContactBirthDayTxtBox.Text) && !myset.Contains(NewContactBirthDayTxtBox.Text))
            {
                if (!addDico.ContainsValue(NewContactBirthDayTxtBox.Text))
                {
                    addDico.Add(7, NewContactBirthDayTxtBox.Text);
                }
                //mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', 7, '" + NewContactBirthDayTxtBox.Text + "')");
            }
            else
            {
                if (!myset.Contains(NewContactBirthDayTxtBox.Text))
                {
                    MessageBox.Show("Careful! The birthday is incorrect. You need to enter in the format dd/mm/yyyy");
                    return false;
                }
            }
            return true;
        }

        public bool addyear()
        {
            if (isOnlyNumbers(NewContactYearInTheCompanyTxtBox.Text) && !myset.Contains(NewContactYearInTheCompanyTxtBox.Text))
            {
                if (!addDico.ContainsValue(NewContactYearInTheCompanyTxtBox.Text))
                {
                    addDico.Add(8, NewContactYearInTheCompanyTxtBox.Text);
                }
                //mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', 8, '" + NewContactYearInTheCompanyTxtBox.Text + "')");
            }
            else
            {
                if (!myset.Contains(NewContactYearInTheCompanyTxtBox.Text))
                {
                    MessageBox.Show("Careful! The year is incorrect. You need to enter it in the format yyyy");
                    return false;
                }
            }
            return true;
        }

        public bool addemail()
        {
            if (!myset.Contains(NewContactEmailTxtBox.Text))
            {
                // and if this email is checked correctly and does not exist in the database already
                if (isAnEmail(NewContactEmailTxtBox.Text) && !alreadyExistInDetails(NewContactEmailTxtBox.Text))
                {
                    if (!addDico.ContainsValue(NewContactEmailTxtBox.Text))
                    {
                        addDico.Add(2, NewContactEmailTxtBox.Text);
                    }
                    //i'll add it to my contact details table
                    //mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey, idcontact, iddictionary, data) VALUES(null, '" + idnew + "', 2, '" + NewContactEmailTxtBox.Text + "')");
                }
                else
                {
                    if (!isAnEmail(NewContactEmailTxtBox.Text))
                    {
                        MessageBox.Show("The email address is wrong. Please type in a valid email address!");
                    }
                    if (alreadyExistInDetails(NewContactEmailTxtBox.Text))
                    {
                        MessageBox.Show("You can not enter an email address that is already related to a contact that is stored. \n Please enter another email address!");
                    }
                    return false;
                }
            }
            return true;
        }

        public bool addposition()
        {
            if (!myset.Contains(NewContactPositionTxtBox.Text))
            {
                // and if this position is checked correctly
                if (isLettersAndSpaces(NewContactPositionTxtBox.Text))
                {
                    if (!addDico.ContainsValue(NewContactPositionTxtBox.Text))
                    {
                        addDico.Add(1, NewContactPositionTxtBox.Text);
                    }

                    //i'll add it to my contact details table
                    // mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', 1, '" + NewContactPositionTxtBox.Text + "')");
                }
                else
                {
                    MessageBox.Show("Careful! You can only enter letter and spaces in the position");
                    return false;
                }
            }
            return true;
        }

        public bool addpostaladdress()
        {
            if (!(myset.Contains(NewContactHouseNbTxtBox.Text) && myset.Contains(NewContactStreetName1TxtBox.Text) && myset.Contains(NewContactPostalCodeTxtBox.Text) && myset.Contains(NewContactCityTxtBox.Text) && myset.Contains(NewContactCountyTxtBox.Text) && myset.Contains(NewContactCountryTxtBox.Text)))
            {

                //If the address is incomplete
                if (myset.Contains(NewContactHouseNbTxtBox.Text) || myset.Contains(NewContactStreetName1TxtBox.Text) || myset.Contains(NewContactPostalCodeTxtBox.Text) || myset.Contains(NewContactCityTxtBox.Text) || myset.Contains(NewContactCountyTxtBox.Text) || myset.Contains(NewContactCountryTxtBox.Text))
                {
                    //We tell the user
                    MessageBox.Show("Carefull ! You need to fill in all the fields for the postal address (except street name2). Otherwise the address is incorrect and you can't add it to your contact manager! ");
                    return false;
                }
                else //Otherwise, if the address is complete
                {
                    //And the validation worked
                    if ((isOnlyNumbers(NewContactHouseNbTxtBox.Text) && isLettersAndSpaces(NewContactStreetName1TxtBox.Text) && isLettersDigitsAndSpaces(NewContactPostalCodeTxtBox.Text) && isLettersAndSpaces(NewContactCityTxtBox.Text) && isLettersAndSpaces(NewContactCountyTxtBox.Text) && isLettersAndSpaces(NewContactCountryTxtBox.Text)))
                    {
                        if (alreadyExistInDetails(NewContactHouseNbTxtBox.Text) && alreadyExistInDetails(NewContactStreetName1TxtBox.Text) && alreadyExistInDetails(NewContactPostalCodeTxtBox.Text) && alreadyExistInDetails(NewContactCityTxtBox.Text) && alreadyExistInDetails(NewContactCountyTxtBox.Text) && alreadyExistInDetails(NewContactCountryTxtBox.Text))
                        {
                            MessageBox.Show("This address is already stored! \n Enter another address please!");
                            return false;
                        }
                        else
                        {//And it is not already stored in the database
                            //I'll add all my data (housenb, streetname, postcode,city,county,country
                            if (!addDico.ContainsKey(9))
                            {
                                addDico.Add(9, NewContactHouseNbTxtBox.Text);
                            }
                            if (!addDico.ContainsKey(10))
                            {
                                addDico.Add(10, NewContactStreetName1TxtBox.Text);
                            }
                            //As StreetName 2 is an option, i'll check first that the string is not "Street Name2" or Empty before adding it
                            if (!myset.Contains(NewContactStreetName2TxtBox.Text) && NewContactStreetName2TxtBox.Text != string.Empty && isLettersAndSpaces(NewContactStreetName2TxtBox.Text))
                            {
                                if (!addDico.ContainsKey(11))
                                {
                                    addDico.Add(11, NewContactStreetName2TxtBox.Text);
                                }
                            }
                            if (!addDico.ContainsKey(12))
                            {
                                addDico.Add(12, NewContactPostalCodeTxtBox.Text);
                            }
                            if (!addDico.ContainsKey(13))
                            {
                                addDico.Add(13, NewContactCityTxtBox.Text);
                            }
                            if (!addDico.ContainsKey(14))
                            {
                                addDico.Add(14, NewContactCountyTxtBox.Text);
                            }
                            if (!addDico.ContainsKey(15))
                            {
                                addDico.Add(15, NewContactCountryTxtBox.Text);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Carefull! You have an Error in one of the address field. \n Make sure to only use English letters, spaces (and digits for the House Nb and the Postal Code).\n The address has not been stored!");
                        return false;
                    }

                }
            }
            return true;
        }

        public bool addphonenumber()
        {
            //If the user has entered a value
            if (!myset.Contains(NewContactPhoneNbTxtBox.Text))
            {

                if (alreadyExistInDetails(NewContactPhoneNbTxtBox.Text))
                {
                    MessageBox.Show("This number is already stored! \n Please enter another number!");
                    return false;
                }
                else
                { //that does not already exist in the database
                    //and that is correct
                    if (isOnlyNumbers(NewContactPhoneNbTxtBox.Text))
                    {
                        string phonechoice = NewContactPhoneComboBox.Text;
                        int enumDictionary = 0;

                        // i'll insert it, depending on the phone type that the user chose
                        switch (phonechoice)
                        {
                            case "Telephone":
                                enumDictionary = 3;
                                break;
                            case "Mobile":
                                enumDictionary = 4;
                                break;
                            case "Work":
                                enumDictionary = 5;
                                break;
                            case "Fax":
                                enumDictionary = 6;
                                break;
                        }
                        if (!addDico.ContainsValue(NewContactPhoneNbTxtBox.Text))
                        {
                            addDico.Add(enumDictionary, NewContactPhoneNbTxtBox.Text);
                        }
                        //mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', '" + enumDictionary + "', '" + NewContactPhoneNbTxtBox.Text + "')");

                    }
                    else
                    {
                        MessageBox.Show("You need to enter a correct phone number with only numbers (no spaces and no +!!)! \n Re enter a correct phone number!");
                        return false;
                    }
                }
            }
            return true;

        }

        public void addnotes()
        {
            if (NewContactNotesTxtBox.Text != "Notes")
            {
                if (!addDico.ContainsValue(NewContactNotesTxtBox.Text))
                {
                    addDico.Add(16, NewContactNotesTxtBox.Text);
                }
                // mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + idnew + "', 16, '" + NewContactNotesTxtBox.Text + "')");
            }
        }

        //Check if the string already exist in the database
        public bool alreadyExistInDetails(string s)
        {
            MySqlDataReader reader = null;
            reader = mySimpleDataSource.Query("SELECT * FROM ContactDetails WHERE data = '" + s + "'");

            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        public void resetTxtBox()
        {
            NewContactFirstNameTxtBox.Text = "First Name";
            NewContactLastNameTxtBox.Text = "Last Name";
            NewContactCompanyNameTxtBox.Text = "Company Name";
            NewContactBirthDayTxtBox.Text = "dd/mm/yyyy";
            NewContactCityTxtBox.Text = "City";
            NewContactCountryTxtBox.Text = "Country";
            NewContactCountyTxtBox.Text = "County";
            NewContactEmailTxtBox.Text = "Email address";
            NewContactHouseNbTxtBox.Text = "House Number";
            NewContactNotesTxtBox.Text = "Notes";
            NewContactPhoneNbTxtBox.Text = "No spaces or +";
            NewContactPositionTxtBox.Text = "Position";
            NewContactPostalCodeTxtBox.Text = "Postal Code";
            NewContactStreetName1TxtBox.Text = "Street Name";
            NewContactStreetName2TxtBox.Text = "Street Name2";
            NewContactYearInTheCompanyTxtBox.Text = "Year (yyyy)";
        }

        public void initialiseSet()
        {
            myset = new HashSet<string>();

            myset.Add("First Name");
            myset.Add("Last Name");
            myset.Add("Company Name");
            myset.Add("Position");
            myset.Add("House Number");
            myset.Add("Street Name");
            myset.Add("Street Name2");
            myset.Add("Postal Code");
            myset.Add("City");
            myset.Add("County");
            myset.Add("Country");
            myset.Add("Contact ID");
            myset.Add("Email address");
            myset.Add("No spaces or +");
            myset.Add("Notes");
            myset.Add("dd/mm/yyyy");
            myset.Add("Year (yyyy)");
        }


        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {

            int b = 0;
            if (dataGrid.CurrentItem != null)
            {
                object[] a = ((DataRowView)dataGrid.CurrentItem).Row.ItemArray;
                b = (int)a[0];
                ContactIDTxtBox.Text = b + "";

                //  DisplayWindow window3 = new DisplayWindow(this, b);
                //window3.Show();

            }
        }
    }
}