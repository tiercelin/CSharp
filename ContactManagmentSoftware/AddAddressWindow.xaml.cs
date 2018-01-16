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
    /// Interaction logic for AddAddressWindow.xaml
    /// </summary>
    public partial class AddAddressWindow : Window
    {
        EditContact myWindow;
        public AddAddressWindow(EditContact EditWindow)
        {
            InitializeComponent();
            myWindow = EditWindow;
        }

        private void HN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SN1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SN2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PC_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Country_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void County_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //Add button !
        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool addcheck = false;
            addcheck = (bool)myWindow.AddCheckBox.IsChecked;
            if (addcheck)
            {
                if (HN.Text != string.Empty && SN1.Text != string.Empty && PC.Text != string.Empty && City.Text != string.Empty && Country.Text != string.Empty && County.Text != string.Empty)
                {
                    if ((MainWindow.isOnlyNumbers(HN.Text) && MainWindow.isLettersAndSpaces(SN1.Text) && MainWindow.isLettersDigitsAndSpaces(PC.Text) && MainWindow.isLettersAndSpaces(City.Text) && MainWindow.isLettersAndSpaces(County.Text) && MainWindow.isLettersAndSpaces(Country.Text)))
                    {
                        if (myWindow.mainWinDow.alreadyExistInDetails(HN.Text) && myWindow.mainWinDow.alreadyExistInDetails(SN1.Text) && myWindow.mainWinDow.alreadyExistInDetails(PC.Text) && myWindow.mainWinDow.alreadyExistInDetails(City.Text) && myWindow.mainWinDow.alreadyExistInDetails(County.Text) && myWindow.mainWinDow.alreadyExistInDetails(Country.Text))
                        {
                            MessageBox.Show("This address is already stored! \n Enter another address please!");
                        }
                        else
                        {
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 9, '" + HN.Text + "')");
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 10, '" + SN1.Text + "')");
                            if (SN2.Text != string.Empty && MainWindow.isLettersAndSpaces(SN2.Text))
                            {
                                myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 11, '" + SN2.Text + "')");
                            }
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 12, '" + PC.Text + "')");
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 13, '" + City.Text + "')");
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 14, '" + County.Text + "')");
                            myWindow.mainWinDow.mySimpleDataSource.Update("INSERT INTO ContactDetails(idkey,idcontact, iddictionary, data) VALUES (null, '" + myWindow.id + "', 15, '" + Country.Text + "')");
                            this.Close();
                        }
                    }
                    else MessageBox.Show("The values are incorrect. Please enter correct values (numbers for house number, letter and spaces for street names, city, county and country, and both for the postcode.");
                }
                else MessageBox.Show("You did not all the values. Careffull to enter every field (except Street name 2)!");
            }
            else MessageBox.Show("You can not add, an address already exist for this contact!!");

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            bool chqngecheck = false;
            chqngecheck = (bool)myWindow.ChangeCheckBox.IsChecked;
            if (chqngecheck)
            {
                if (HN.Text != string.Empty && SN1.Text != string.Empty && PC.Text != string.Empty && City.Text != string.Empty && Country.Text != string.Empty && County.Text != string.Empty)
                {
                    if ((MainWindow.isOnlyNumbers(HN.Text) && MainWindow.isLettersAndSpaces(SN1.Text) && MainWindow.isLettersDigitsAndSpaces(PC.Text) && MainWindow.isLettersAndSpaces(City.Text) && MainWindow.isLettersAndSpaces(County.Text) && MainWindow.isLettersAndSpaces(Country.Text)))
                    {
                        if (myWindow.mainWinDow.alreadyExistInDetails(HN.Text) && myWindow.mainWinDow.alreadyExistInDetails(SN1.Text) && myWindow.mainWinDow.alreadyExistInDetails(PC.Text) && myWindow.mainWinDow.alreadyExistInDetails(City.Text) && myWindow.mainWinDow.alreadyExistInDetails(County.Text) && myWindow.mainWinDow.alreadyExistInDetails(Country.Text))
                        {
                            MessageBox.Show("This address is already stored! \n Enter another address please!");
                        }
                        else
                        {
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + HN.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =9");
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + SN1.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =10");
                            if (SN2.Text != string.Empty && MainWindow.isLettersAndSpaces(SN2.Text))
                            {
                                myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + HN.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =11");
                            }
                            else myWindow.mainWinDow.mySimpleDataSource.Update("DELETE FROM ContactDetails WHERE idcontact='" + myWindow.id + "' AND iddictionary=11");
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + PC.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =12");
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + City.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =13");
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + County.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =14");
                            myWindow.mainWinDow.mySimpleDataSource.Update("UPDATE ContactDetails SET data='" + Country.Text + "' WHERE idcontact='" + myWindow.id + "' AND iddictionary =15");
                            this.Close();
                        }
                    }
                    else MessageBox.Show("The values are incorrect. Please enter correct values (numbers for house number, letter and spaces for street names, city, county and country, and both for the postcode.");
                }
                else MessageBox.Show("You did not all the values. Careffull to enter every field (except Street name 2)!");
            }
            else MessageBox.Show("You need to add an address to be able to modify it!!");
        }
    }
}
