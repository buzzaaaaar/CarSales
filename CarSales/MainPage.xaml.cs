using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CarSales
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Programmer: Kavindu Illankoon
        //Date Submitted: 06/12/2019
        //Assignment CarSales3
                
        public MainPage()
        {
            this.InitializeComponent();
        }

        //arrays 
        String[] vehicleMakes = new string[8];
        String[] names = new string[10];
        int[] phonenums = new int[10];

        // constants
        const double GST_RATE = 0.1;
        const double TWO_YEARS = 0.05, THREE_YEARS = 0.1, FIVE_YEARS = 0.2;
        const double WINDOW_TINT = 150, DUCO_PROTECT = 180, GPS = 320, SOUND_SYS = 350;
        const double UNDER25 = 0.2, OVER25 = 0.1;
        //variables
        string name;
        int phone;
        double subAmount = 0, gstAmount = 0, finalAmount = 0,
        vehiclePrice = 0, tradeIn = 0, vehicleWarranty = 0, optionalExtras = 0, accidentInsurance = 0;

        // page loaded event    
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vehicleMakes = new string[] { "Toyota", "Holden", "Mitsubishi", "Ford", "BMW", "Mazda", "Volkswagen", "Mini" };
            names = new string[] { "Robert Manion", "Lauren Lopez", "Jon Matterson", "Jaime Lyn Beauty", "Corey Dorris", "Joey Ritcher", "Mariah Faith", "Jeff Blim", "Joe Walker", "Dylan Saunders" };
            phonenums = new int[] { 0478827628, 0478666703, 0474615232, 047620862, 0475716654, 0477394384, 0475983372, 0474853429, 0477204180, 0430294089 };
        }

        //save saveButton_Click
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (customerNameTextBox.Text == "") //validates if the textbox is empty
            {
                var dialogMessage = new MessageDialog("Error! Please enter the customer name. "); //exception message popup box
                await dialogMessage.ShowAsync();
                customerNameTextBox.Focus(FocusState.Programmatic);//focus back on the inputted textbox
                customerNameTextBox.SelectAll();//selects all characters of the textbox
                return;
            }
            if (phoneTextBox.Text == "") //validates if the textbox is empty
            {
                var dialogMessage = new MessageDialog("Error! Please enter the customer phone number. "); //exception message popup box
                await dialogMessage.ShowAsync();
                phoneTextBox.Focus(FocusState.Programmatic);//focus back on the inputted textbox
                phoneTextBox.SelectAll();//selects all characters of the textbox
                return;
            }
            //try if the input data fits its data type 
            try
            {
                string names = customerNameTextBox.Text;
            }
            catch (Exception theException)// catches if the data fails, and displays an exception message 
            {
                var dialogMessage = new MessageDialog("Please enter customer name again." + theException.Message); //exception message popup box
                await dialogMessage.ShowAsync();
                customerNameTextBox.Focus(FocusState.Programmatic);//focus back on the inputted textbox
                customerNameTextBox.SelectAll();//selects all characters of the textbox
                return;//returns to the program 
            }
            //try if the input data fits its data type 
            try
            {
                phone = int.Parse(phoneTextBox.Text);
            }
            catch (Exception theException)// catches if the data fails, and displays an exception message 
            {
                var dialogMessage = new MessageDialog("Please enter customer phone number again." + theException.Message); //exception message popup box
                await dialogMessage.ShowAsync();
                phoneTextBox.Focus(FocusState.Programmatic);//focus back on the inputted textbox
                phoneTextBox.SelectAll();//selects all characters of the textbox
                return;//returns to the program 
            }
            
            // for loop for add the strings to the array 
            //names[names.Length - 1] = customerNameTextBox.Text;

            //disables the customer name and phone textboxes
            phoneTextBox.IsEnabled = false;
            customerNameTextBox.IsEnabled = false;

            vehiclePriceTextBox.Focus(FocusState.Programmatic); //sets focus on the Vehicle Price Textbox
        }

        //reset ResetButton_ClickArgs e)
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //clears all variables
            name = " ";
            phone = 0;
            subAmount = 0;
            gstAmount = 0;
            finalAmount = 0;
            vehiclePrice = 0;
            tradeIn = 0;
            vehicleWarranty = 0;
            optionalExtras = 0;
            accidentInsurance = 0;
            //Clears all data
            phoneTextBox.Text = "";
            customerNameTextBox.Text = "";
            vehiclePriceTextBox.Text = "";
            tradeInTextBox.Text = "";
            subAmountTextBox.Text = "";
            gstAmountTextBox.Text = "";
            finalAmountTextBox.Text = "";
            summaryTextBox.Text = "";
            //customer name and phone is enabled
            phoneTextBox.IsEnabled = true;
            customerNameTextBox.IsEnabled = true;
            // vehicle warranty is in default option
            warrantyComboBox.SelectedItem = null;
            // Optional extras are cleared 
            windowTintCheckBox.IsChecked = false;
            ducoProtectCheckBox.IsChecked = false;
            gpsCheckBox.IsChecked = false;
            soundSystemCheckBox.IsChecked = false;
            // toggle switch is turned off
            yesnoToggleSwitch.IsOn = false;
            //accident insurance is cleared
            under25RadioButton.IsChecked = false;
            over25RadioButton.IsChecked = false;
            //sets focus on customer name textbox
            customerNameTextBox.Focus(FocusState.Programmatic);
        }

        //summary summaryButton_Click
        private async void summaryButton_Click(object sender, RoutedEventArgs e)
        {

            //try if the input data fits its data type 
            try
            {
                vehiclePrice = double.Parse(vehiclePriceTextBox.Text);
            }
            catch (Exception theException)// catches if the data fails, and displays an exception message 
            {
                var dialogMessage = new MessageDialog("Error! Please enter numbers. " + theException.Message); //exception message popup box
                await dialogMessage.ShowAsync();
                vehiclePriceTextBox.Focus(FocusState.Programmatic);//focus back on the inputted textbox
                vehiclePriceTextBox.SelectAll();//selects all characters of the textbox
                return;//returns to the program 
            }
            try
            {
                tradeIn = double.Parse(tradeInTextBox.Text);//try if the input data fits its data type         
            }
            catch (Exception theException)// catches if the data fails, and displays an exception message 
            {
                var dialogMessage = new MessageDialog("Error! Please enter numbers. " + theException.Message);//exception message popup box
                await dialogMessage.ShowAsync();
                tradeInTextBox.Focus(FocusState.Programmatic);//focus on the inputted textbox
                tradeInTextBox.SelectAll();//selects all characters of the textbox
                return;//returns to the program 
            }

            if (vehiclePrice <= 0)
            { //validate vehicle price < 0
                var dialogMessage = new MessageDialog("Invalid amount, The vehicle price should be more than zero. Re-enter again."); //error message popup box
                await dialogMessage.ShowAsync();
                vehiclePriceTextBox.Focus(FocusState.Programmatic); //focus on the textbox
                vehiclePriceTextBox.SelectAll();//selects all characters of the textbox
                return; //returns to program
            }
            if (vehiclePrice < tradeIn)
            { //validate vehicle price < trade in
                var dialogMessage = new MessageDialog("Invalid amount, The vehicle price should be more than the trade in price. Re-enter again."); //error message popup box
                await dialogMessage.ShowAsync();
                vehiclePriceTextBox.Focus(FocusState.Programmatic);//focus on the textbox
                vehiclePriceTextBox.SelectAll();//selects all characters of the textbox
                return;
            }
            if (tradeIn < 0)
            { //validate trade in < 0
                var dialogMessage = new MessageDialog("Invalid amount, The Trade In cost should can't be less than zero. Re-enter again."); //error message popup box
                await dialogMessage.ShowAsync();
                vehiclePriceTextBox.Focus(FocusState.Programmatic); //focus back on the textbox
                vehiclePriceTextBox.SelectAll();//selects all characters of the textbox
                return;
            }

            vehicleWarranty = calcVehicleWarranty(vehiclePrice);
            optionalExtras = calcOptionalExtras();
            accidentInsurance = calcAccidentInsurance(vehiclePrice, optionalExtras);
            // sub amount calculation
            subAmount = vehiclePrice + vehicleWarranty + optionalExtras + accidentInsurance - tradeIn;

            subAmountTextBox.Text = subAmount.ToString("C");

            //gst amount calculation
            gstAmount = subAmount * GST_RATE;

            gstAmountTextBox.Text = gstAmount.ToString("C");

            //final amount calculation
            finalAmount = subAmount + gstAmount;

            finalAmountTextBox.Text = finalAmount.ToString("C");

            //displays summary text box
            summaryTextBox.Text = "Customer Name:\n" + name +
                                   "\nPhone Number:\n " + phone.ToString("D10") +
                                   "\nVehicle Price: " + vehiclePrice.ToString("C") +
                                   "\nTrade In: " + tradeIn.ToString("C") +
                                   "\nVehicle Warranty: " + vehicleWarranty.ToString("C") +
                                   "\nOptional Extras: " + optionalExtras.ToString("C") +
                                   "\nAccident Insurance: " + accidentInsurance.ToString("C") +
                                   "\nSub Amount: " + subAmount.ToString("C") +
                                   "\nGST Amount: " + gstAmount.ToString("C") +
                                   "\nFinal Amount: " + finalAmount.ToString("C");
        }

        //Vehicle Warranty
        private double calcVehicleWarranty(double vehiclePrice)
        {
            double vehicleWarranty = 0;

            if (warrantyComboBox.SelectedIndex == 0)
            {
                vehicleWarranty = 0;

            }
            else if (warrantyComboBox.SelectedIndex == 1)
            {
                vehicleWarranty = vehiclePrice * TWO_YEARS;

            }
            else if (warrantyComboBox.SelectedIndex == 2)
            {
                vehicleWarranty = vehiclePrice * THREE_YEARS;

            }
            else if (warrantyComboBox.SelectedIndex == 3)
            {
                vehicleWarranty = vehiclePrice * FIVE_YEARS;
            }
            return vehicleWarranty;
        }

        //Optional Extras
        private double calcOptionalExtras()
        {
            double optionalExtras = 0;
            // Calculate the commission
            if (windowTintCheckBox.IsChecked == true)
            {
                optionalExtras = optionalExtras + WINDOW_TINT;
            }
            if (ducoProtectCheckBox.IsChecked == true)
            {
                optionalExtras = optionalExtras + DUCO_PROTECT;
            }
            if (gpsCheckBox.IsChecked == true)
            {
                optionalExtras = optionalExtras + GPS;
            }
            if (soundSystemCheckBox.IsChecked == true)
            {
                optionalExtras = optionalExtras + SOUND_SYS;
            }


            return optionalExtras;
        }

        //toggle switch
        private void YesnoToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (yesnoToggleSwitch.IsOn == true)
            {
                radioButtonStackPanel.Visibility = Visibility.Visible;
                under25RadioButton.IsEnabled = true;
                over25RadioButton.IsEnabled = true;
                under25RadioButton.IsChecked = true;
            }
            else
            {
                radioButtonStackPanel.Visibility = Visibility.Collapsed;
                under25RadioButton.IsEnabled = false;
                over25RadioButton.IsEnabled = false;
            }
        }


        //Accident Insurance
        private double calcAccidentInsurance(double vehiclePrice, double optionalExtras)
        {
            double accidentInsurance = 0;
            if (under25RadioButton.IsEnabled == true || over25RadioButton.IsEnabled == true)
            {
                if (under25RadioButton.IsChecked == true)
                {
                    accidentInsurance = (vehiclePrice + optionalExtras) * UNDER25;
                }
                else if (over25RadioButton.IsChecked == true)
                {
                    accidentInsurance = (vehiclePrice + optionalExtras) * OVER25;
                }
            }
            else
            {
                accidentInsurance = accidentInsurance * 0;
            }
            return accidentInsurance;
        }
        
        // Displays all the customers' names and their corresponding phone numbers
        private void AllNamesButton_Click(object sender, RoutedEventArgs e)
        {
            string output = "";
            for (int index = 0; index < names.Length; index++) // loop through the array
            {
               
                output = output + names[index] + " - " + phonenums[index].ToString("D10") + "\n"; // create a concatenated string to output all the names
               
            }
            summaryTextBox.Text = "List of customers: \n" + output;// outputs the entire list
        }
        
        // Searches on a customer's name and displays its phone number by skimming through the array until the search string is found
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;// to track position in array 
            bool found = false;// found will be true or false depending if name found 
       
            string criteria = customerNameTextBox.Text.ToUpper();// input search criteria from user and convert to uppercase to make checking a string easier 
            if (customerNameTextBox.Text == "") //check if search textbox empty 
            {
                var dialogMessage = new MessageDialog("Please enter a name into the name textbox");
               await dialogMessage.ShowAsync();
                customerNameTextBox.Focus(FocusState.Programmatic);
                return;
           }

            // do sequential search of string array until match found or end of array reached 
            while (!found && counter < names.Length)// while not found and not end of array 
            {
                if (criteria == names[counter].ToUpper())// checks if the name is found 
                    found = true;
                else
                    counter++;// if there's no match move to next element in array 
            }  // end while loop 

           if (counter < names.Length)    // if a name has been found  
           {
                AllNamesButton_Click(sender, e);
                phoneTextBox.Text = phonenums[counter].ToString("D10");
               var dialogMessage = new MessageDialog(criteria + " - " + phonenums[counter].ToString("D10"));
               await dialogMessage.ShowAsync();
               return;
                
            }
            else   // not found 
            {
                AllNamesButton_Click(sender, e);
                var dialogMessage = new MessageDialog(criteria + " NOT FOUND");
                await dialogMessage.ShowAsync();
                return;
            }
        }
        
        //  Delete a customer's name and their phone number from the list
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;// used to keep track of position in array 
            bool found = false;// boolean to keep track if city found 

            if (customerNameTextBox.Text == "")//check if search textbox empty 
            {
                var dialogMessage = new MessageDialog("Please enter a name into the delete box");
                await dialogMessage.ShowAsync();
                customerNameTextBox.Focus(FocusState.Programmatic);
                return;
            }
        
            string criteria = customerNameTextBox.Text.ToUpper();// input search criteria from user 

            // do sequential search of string array until match found or end of array reached 
            while (!found && counter < names.Length)
            {
                if (criteria == names[counter].ToUpper())
                    found = true;
                else
                    counter++;// if there's no match move to next element in array 
           
            }// end loop

           

            if (counter < names.Length)    // if found the city exists, delete from the array by replacing the one to delete with the next city etc 
            {
                for (int i = counter; i < names.Length - 1; i++)
                {
                    names[i] = names[i + 1]; //copy the next item in the array to the previous position 
                    phonenums[i] = phonenums[i + 1];
                }
                criteria = names[counter].ToUpper() + " with " + phonenums[counter].ToString("D10"); // adds the following number of the customer to the criteria
                Array.Resize(ref names, names.Length - 1);  // RESIZE city Array by -1  
                AllNamesButton_Click(sender, e);
                var dialogMessage = new MessageDialog("Customer " + criteria + " has been deleted,\n customer list now updated to length " + names.Length);
                await dialogMessage.ShowAsync();
                customerNameTextBox.Text = " ";
            }
            else
            {
                AllNamesButton_Click(sender, e);
                var dialogMessage = new MessageDialog(criteria + " NOT FOUND. Cannot delete customer name");
                await dialogMessage.ShowAsync();
                return;
            }
        }
        
        // Display all the entire list of vehicle makes 
        private void AllMakesButton_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(vehicleMakes);
            string output = "";
            for (int index = 0; index < vehicleMakes.Length; index++) // loop through the array
            {
                output = output + vehicleMakes[index] + "\n"; // create a concatenated string to output all the names
            }
           summaryTextBox.Text = "List of vehicle Makes: \n" + output;// outputs the entire list
        }

        
        // Searches the vehicle make by dividing the search interval by lower half and upper half. 
        // Thus covering the whole array
        // It checks if the search key is in either the lower half and upper half. Repeatedly checkin until the value is found.
        private async void binarySearchButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;// to track position in array 
            bool found = false;// found will be true or false depending if name found 
            string criteria = vehicleMakeTextBox.Text.ToUpper();// input search criteria from user and convert to uppercase to make checking a string easier 
            
            if (vehicleMakeTextBox.Text=="") //check if search textbox empty 
            { 
                var dialogMessage = new MessageDialog("Please enter a vehicle make into the search box"); 
                await dialogMessage.ShowAsync(); 
                vehicleMakeTextBox.Focus(FocusState.Programmatic); 
                return; 
            } 

            // do sequential search of string array until match found or end of array reached 
            while (!found && counter < vehicleMakes.Length)// while not found and not end of array 
            { 
                if (criteria == vehicleMakes[counter].ToUpper())// check if the name is found 
                found = true; 
                else 
                counter++;// if no match move to next element in array 
            }  // end while loop 

            if (counter<vehicleMakes.Length)    // if a name has been found  
            {
                AllMakesButton_Click(sender, e);
                var dialogMessage = new MessageDialog(criteria + " has an index of " + counter );
                await dialogMessage.ShowAsync();     
                return;
               

            } 
            else   // not found 
            {
                AllMakesButton_Click(sender, e);
                var dialogMessage = new MessageDialog(criteria + " NOT FOUND");
                await dialogMessage.ShowAsync();
                return;
            } 
        }


        private async void InsertMakeButton_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0; // used to keep track of position in array 
            bool found = false;  // boolean to keep track if city found 
            
            string criteria = vehicleMakeTextBox.Text.ToUpper();  // input search criteria from user and convert to uppercase 
            if (vehicleMakeTextBox.Text == "")  
            { 
                var dialogMessage = new MessageDialog("Please enter a vehicle make into the search box"); 
                await dialogMessage.ShowAsync(); 
                vehicleMakeTextBox.Focus(FocusState.Programmatic); 
                return; 
            } 
            // do sequential search of string array until match found or end of array reached 
            while (!found && counter < vehicleMakes.Length)  
            { 
                if (criteria == vehicleMakes[counter].ToUpper()) 
                found = true; 
                else 
                counter++; 
            } 
            if (counter<vehicleMakes.Length) // if found the vehicle make already exists, do not add to array 
            {
                var dialogMessage = new MessageDialog(criteria + " ALREADY EXISTS.");
                AllMakesButton_Click(sender, e); // call the previuos method to display the vehicle makes
                await dialogMessage.ShowAsync();
                vehicleMakeTextBox.Focus(FocusState.Programmatic);
                return;

            } 
            else 
            { 
                Array.Resize(ref vehicleMakes, vehicleMakes.Length + 1);  // RESIZE vehicle make Array by 1 
                var dialogMessage = new MessageDialog("Vehicle makes array now updated to length " +vehicleMakes.Length); 
                await dialogMessage.ShowAsync();
                vehicleMakes[vehicleMakes.Length - 1] = vehicleMakeTextBox.Text; // assign the vehicle make entered to the last element in the array
                AllMakesButton_Click(sender, e); // call the previuos method to display the vehicle makes
            } 
        }
        

    }
}
    