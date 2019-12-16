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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace NotAmazon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        List<Book> bookList;
        List<Customer> customerList;
        public MainWindow()
        {
            InitializeComponent();
            bookList = new List<Book>();
            ImportBookData();

            customerList = new List<Customer>();
            ImportCustomerData();

            string strBooksPath = @"..\..\Data\Books.txt";
            string[] ListOfBooks = File.ReadAllLines(strBooksPath);
            foreach (var line in ListOfBooks)
            {
                string[] BookLine = line.Split('|');
                cboReviewBook.Items.Add(BookLine[0] + " by " + BookLine[1]);
            }

            string[] Books = File.ReadAllLines(strBooksPath);
            foreach (string b in Books)
            {
                string[] book = b.Split('|');
                string sBookAuthor = book[0] + " by " + book[1];
                lstBook1.Items.Add(sBookAuthor);
                lstBook2.Items.Add(sBookAuthor);
            }
        }

        private void ImportBookData()
        {
            string strFilePath = @"..\..\Data\Books.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    Book bookNew;
                    if (rawData[7] != "")
                    {
                        bookNew = new Book(rawData[0], rawData[1], Convert.ToDouble(rawData[2]), rawData[3], rawData[4], Convert.ToChar(rawData[5]), rawData[6], Convert.ToDateTime(rawData[7]), Convert.ToDateTime(rawData[8]));
                    }
                    else
                    {
                        bookNew = new Book(rawData[0], rawData[1], Convert.ToDouble(rawData[2]), rawData[3], rawData[4], Convert.ToChar(rawData[5]), rawData[6], DateTime.Today, DateTime.Today);
                    }
                    bookList.Add(bookNew);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgBook.ItemsSource = bookList;
            dtgBook.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();

        }

        private void ImportCustomerData()
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    customerList.Add(customerNew);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();

        }

        private void btnUpdateTitle_Click(object sender, RoutedEventArgs e)
        {
            double dNewPrice;
            string strFilePath = @"..\..\Data\Books.txt";
            string[] ListOfBooks = File.ReadAllLines(strFilePath);
            List<Book> Books = new List<Book>();

            foreach (string b in ListOfBooks)
            {
                string[] sb = b.Split('|');
                Book book = new Book(sb[0], sb[1], Convert.ToDouble(sb[2]), sb[3], sb[4], Convert.ToChar(sb[5]), sb[6], Convert.ToDateTime(sb[7]), Convert.ToDateTime(sb[8]));
                Books.Add(book);
            }

            if (txtNewTitle.Text == "" && txtNewAuthor.Text == "" && txtNewPrice.Text == "")
            {
                MessageBox.Show("(ERROR: No Input) Please input changes.");
                return;
            }

            if (txtNewTitle.Text != "")
            {
                Books[dtgBook.SelectedIndex].Title = txtNewTitle.Text;
            }
            if (txtNewAuthor.Text != "")
            {
                Books[dtgBook.SelectedIndex].Author = txtNewAuthor.Text;
            }
            if (double.TryParse(txtNewPrice.Text, out dNewPrice))
            {
                Books[dtgBook.SelectedIndex].Price = dNewPrice;
            }
            if (txtNewISBN.Text != "")
            {
                Books[dtgBook.SelectedIndex].ISBN = txtNewISBN.Text;
            }
            if (txtNewPublisher.Text != "")
            {
                Books[dtgBook.SelectedIndex].Publisher = txtNewPublisher.Text;
            }

            bookList.Clear();
            File.WriteAllText(strFilePath, "");

            StreamWriter bookWriter = new StreamWriter(strFilePath, true);
            foreach (Book book in Books)
            {
                string sBookUpdate = book.Title + "|" + book.Author + "|" + book.Price + "|" + book.ISBN + "|" + book.Publisher + "|" + book.Rented + "|" + book.RentedBy + "|" + book.StartDate + "|" + book.EndDate;
                bookWriter.WriteLine(sBookUpdate);
                bookList.Add(book);
            }
            bookWriter.Close();

            dtgBook.ItemsSource = bookList;
            dtgBook.Items.Refresh();

            MessageBox.Show("Book successfully modified!");
            txtNewPrice.Text = "";
            txtNewAuthor.Text = "";
            txtNewTitle.Text = "";
        }

        private void btnUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            string strNewAuthor = txtNewAuthor.Text;
            if (txtNewAuthor.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Empty Author Input, Please Try Again.");
                return;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            double dblNewPrice;
            bool bNewPrice = double.TryParse(txtNewPrice.Text, out dblNewPrice);

            if (txtNewPrice.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Empty Price Input, Please Try Again.");
                return;
            }
            else if (!bNewPrice || dblNewPrice <= 0)
            {
                MessageBox.Show("Invalid Age Input, Price Must a Numerical Value above 0, Please Try Again.");
                return;
            }
        }

        private void ImportBookData1()
        {
            string strFilePath = @"..\..\Data\Books.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    Book bookNew;
                    if (rawData[7] != "")
                    {
                        bookNew = new Book(rawData[0], rawData[1], Convert.ToDouble(rawData[2]), rawData[3], rawData[4], Convert.ToChar(rawData[5]), rawData[6], Convert.ToDateTime(rawData[7]), Convert.ToDateTime(rawData[8]));
                    }
                    else
                    {
                        bookNew = new Book(rawData[0], rawData[1], Convert.ToDouble(rawData[2]), rawData[3], rawData[4], Convert.ToChar(rawData[5]), rawData[6], DateTime.Today, DateTime.Today);
                    }
                    bookList.Add(bookNew);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgBook.ItemsSource = bookList;
            dtgBook.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();

        }

        private void btnWriteReview_Click(object sender, RoutedEventArgs e)
        {
            string sRating = cboWriteRating.Text;
            string strFilePath = @"..\..\Data\Reviews.txt";
            Int32.TryParse(sRating, out int iRating);
            Review newReview = new Review(cboReviewBook.Text, iRating, txtWriteReview.Text);

            if (newReview.Name != string.Empty && iRating > 0 && txtWriteReview.Text != string.Empty )
            {
                try
                {
                    string sNewReview = newReview.Name + "|" + newReview.Rating.ToString() + "|" + newReview.ReviewBody;
                    StreamWriter reviewWriter = new StreamWriter(strFilePath, true);
                    reviewWriter.WriteLine(sNewReview);

                    reviewWriter.Close();

                    cboReviewBook.Text = "--";
                    cboWriteRating.Text = "--";
                    txtWriteReview.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter a valid review!" + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid review!");
            }
            

        }
        int currentReview = 1;

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strFilePath = @"..\..\Data\Reviews.txt";
                string[] allReviews = File.ReadAllLines(strFilePath);

                string[] SPrintReview = allReviews[currentReview].Split('|');
                txtViewBook.Text = SPrintReview[0];
                txtViewRating.Text = SPrintReview[1];
                txtViewReview.Text = SPrintReview[2];

                currentReview++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("We're all out of reviews! Returning to the first one! " + ex.Message);
                currentReview = 1;
            }

        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            txtMID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            radM.IsChecked = false;
            radF.IsChecked = false;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            StreamWriter customerWriter = new StreamWriter(strFilePath, true);
            char gender;
            if (txtMID.Text != string.Empty && txtFirstName.Text != string.Empty && txtLastName.Text != string.Empty && txtPhone.Text != string.Empty && txtEmail.Text != string.Empty && (radF.IsChecked == true || radM.IsChecked == true))
            {
                if (radF.IsChecked == true)
                {
                    gender = 'F';
                }
                else
                {
                    gender = 'M';
                }
                customerWriter.WriteLine(txtMID.Text + "|" + txtFirstName.Text + "|" + txtLastName.Text + "|" + txtPhone.Text + "|" + txtEmail.Text + "|" + gender);
                customerWriter.Close();
                txtMID.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                radM.IsChecked = false;
                radF.IsChecked = false;
                ImportCustomerData();
            }
            else
            {
                MessageBox.Show("To add a new customer, please fill out all the fields!");
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // 1. a. declare variables
            DateTime datToday = DateTime.Today;
            DateTime datStart1, datEnd1, datStart2, datEnd2;
            TimeSpan timWeeksBook1, timWeeksBook2;
            double dDaysBook1, dDaysBook2;
            string sWeeks1, sWeeks2;
            double dWeeklyPrice1, dWeeklyPrice2;
            int iWeeks1, iWeeks2;
            double dSubtotal, dTax, dTotal;

            dWeeklyPrice2 = 0;
            iWeeks2 = 0;

            // b. assign routing, create list with book details
            string strFilePath = @"..\..\Data\Books.txt";
            string[] SBookList = File.ReadAllLines(strFilePath);
            var SBookPrice = new List<string>();
            int iBook = 0;
            foreach (string b in SBookList)
            {
                string[] book = b.Split('|');
                SBookPrice.Add(book[2]);
                iBook++;
            }

            // 2. verify books exist
            if (lstBook1.SelectedValue == null)
            {
                MessageBox.Show("(ERROR: First Book) You must select a first book!");
                return;
            }


            // 3. begin validating inputs
            // a. check if datestart1 and dateend1 exist
            if (dateStart1 != null && dateEnd1 != null)
            {
                // b. convert datestart1 from datepicker to datetime
                try
                {
                DateTime? _datStart1 = dateStart1.SelectedDate;
                datStart1 = (DateTime)_datStart1;
                DateTime? _datEnd1 = dateEnd1.SelectedDate;
                datEnd1 = (DateTime)_datEnd1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter a valid date. ERROR: " + ex);
                    return;
                }

                // c. check if datstart1 is before today
                if (datStart1 < datToday)
                {
                    MessageBox.Show("(ERROR: First book) Please select a starting date today or in the future");
                    return;
                }

                // d. check if datend1 is before datstart1
                if (datEnd1 < datStart1)
                {
                    MessageBox.Show("(ERROR: First book) Please select an end date that is after the starting date.");
                    return;
                }

                // e. set time values 1
                timWeeksBook1 = (datEnd1 - datStart1);
                dDaysBook1 = timWeeksBook1.Days;
                sWeeks1 = Convert.ToString(Math.Ceiling(dDaysBook1 / 7));

                // f. set WPF textbox 1
                txtWeeks1.Text = sWeeks1;

                // g. get rental number and set it to text
                txtWeeklyPrice1.Text = SBookPrice[lstBook1.SelectedIndex];

                // h. convert week and price values to numeric data types
                dWeeklyPrice1 = Convert.ToDouble(txtWeeklyPrice1.Text);
                iWeeks1 = Convert.ToInt32(txtWeeks1.Text);
                if (iWeeks1 == 0)
                {
                    MessageBox.Show("(ERROR: First book) Please input a date range of a week or greater.");
                    return;
                }

                // i. check if datestart2 and dateend2 exists and repeat
                if (dateStart2.SelectedDate != null && dateEnd2.SelectedDate != null)
                {
                    try
                    {
                        DateTime? _datStart2 = dateStart2.SelectedDate;
                        datStart2 = (DateTime)_datStart2;
                        DateTime? _datEnd2 = dateEnd2.SelectedDate;
                        datEnd2 = (DateTime)_datEnd2;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("(ERROR: Invalid Date) Please enter a valid date. ERROR: " + ex);
                        return;
                    }

                    if (lstBook2.SelectedValue == null)
                    {
                        MessageBox.Show("(ERROR: Second Book) You must select a second book before you select rental dates!");
                        return;
                    }

                    if (datStart2 < datToday)
                    {
                        MessageBox.Show("(ERROR: Second book) Please select a starting date today or in the future");
                        return;
                    }

                    if (datEnd2 < datStart2)
                    {
                        MessageBox.Show("(ERROR: Second book) Please select an end date that is after the starting date.");
                        return;
                    }

                    timWeeksBook2 = (datEnd2 - datStart2);
                    dDaysBook2 = timWeeksBook2.Days;
                    sWeeks2 = Convert.ToString(Math.Ceiling(dDaysBook2 / 7));
                    txtWeeks2.Text = sWeeks2;

                    txtWeeklyPrice2.Text = SBookPrice[lstBook2.SelectedIndex];

                    dWeeklyPrice2 = Convert.ToDouble(txtWeeklyPrice2.Text);
                    iWeeks2 = Convert.ToInt32(txtWeeks2.Text);
                    if (iWeeks2 == 0)
                    {
                        MessageBox.Show("(ERROR: Second book) Please input a date range of a week or greater.");
                        return;
                    }
                }
            }
            else //error message
            {
                MessageBox.Show("(ERROR: Invalid Date) Please pick a start and end date for your first book.");
                return;
            }

            //Price
            dSubtotal = dWeeklyPrice1 * iWeeks1 + dWeeklyPrice2 * iWeeks2;
            txtSubtotal.Text = Convert.ToString(dSubtotal);

            dTax = Math.Round(dSubtotal * .07, 2);
            txtTax.Text = Convert.ToString(dTax);

            dTotal = dTax + dSubtotal;
            txtTotal.Text = Convert.ToString(dTotal);
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {

            //create List<Customer> to query customers
            string strCustomersPath = @"..\..\Data\Customers.txt";
            string[] ListOfCustomers = File.ReadAllLines(strCustomersPath);
            List<Customer> Customers = new List<Customer>();
            foreach (string Customer in ListOfCustomers)
            {
                string[] CustomerDetails = Customer.Split('|');
                Customer customer = new Customer(Convert.ToString(CustomerDetails[0]), CustomerDetails[1], CustomerDetails[2], CustomerDetails[3], CustomerDetails[4], Convert.ToChar(CustomerDetails[5]));
                Customers.Add(customer);
            }

            //create List<Book> to query and remove books
            string strBookPath = @"..\..\Data\Books.txt";
            string[] ListOfBooks = File.ReadAllLines(strBookPath);
            List<Book> Books = new List<Book>();
            foreach (string book in ListOfBooks)
            {
                string[] BookDetails = book.Split('|');
                Book newbook = new Book(BookDetails[0], BookDetails[1], Convert.ToDouble(BookDetails[2]), BookDetails[3], BookDetails[4], Convert.ToChar(BookDetails[5]),BookDetails[6],Convert.ToDateTime(BookDetails[7]), Convert.ToDateTime(BookDetails[8]));
                Books.Add(newbook);
            }

            // Check if all boxes are filled
            if (txtRent_First.Text.Trim() == string.Empty || txtRent_Last.Text.Trim() == string.Empty || txtRent_MID.Text.Trim() == string.Empty)
            {
                MessageBox.Show("(ERROR: Empty Fields) All Customer Info Should Be Filled, Please Try Again.");
                return;
            }
            else
            {
                int _found = 0;
                Customer rentingcustomer;
                foreach(Customer customer in Customers)
                {
                    // confirm customer exists
                    if (txtRent_First.Text == customer.First && txtRent_Last.Text == customer.Last && txtRent_MID.Text == customer.MID)
                    {
                        _found = 1;
                        rentingcustomer = customer;
                        break;
                    }
                }
                // if customer exists, then we take the book, save it, and remove it from the Book class list to re-add with changes
                if (_found == 1)
                {
                    Book book1 = new Book();
                    Book book2 = new Book();
                    if (lstBook1.SelectedIndex >= 0)
                    {
                        book1 = Books[lstBook1.SelectedIndex];
                        Books.RemoveAt(lstBook1.SelectedIndex);

                        book1.Rented = '1';
                        book1.RentedBy = txtRent_First + " " + txtRent_Last;
                        DateTime? _dateend1 = dateEnd1.SelectedDate;
                        DateTime? _datestart1 = dateStart1.SelectedDate;
                        DateTime datEnd1 = Convert.ToDateTime(_dateend1);
                        DateTime datStart1 = Convert.ToDateTime(_datestart1);

                        book1.StartDate = datStart1;
                        book1.EndDate = datEnd1;

                        Books.Add(book1);

                        if (lstBook2.SelectedIndex >= 0)
                        {
                            if (lstBook1.SelectedIndex > lstBook2.SelectedIndex)
                            {
                                book2 = Books[lstBook2.SelectedIndex];
                                Books.RemoveAt(lstBook2.SelectedIndex);
                            }
                            else if (lstBook1.SelectedIndex < lstBook2.SelectedIndex)
                            {
                                book2 = Books[lstBook2.SelectedIndex - 1];
                                Books.RemoveAt(lstBook2.SelectedIndex - 1);
                            }

                            book2.Rented = '1';
                            book2.RentedBy = txtRent_First + " " + txtRent_Last;
                            DateTime? _dateend2 = dateEnd2.SelectedDate;
                            DateTime? _datestart2 = dateStart2.SelectedDate;
                            DateTime datEnd2 = Convert.ToDateTime(_dateend2);
                            DateTime datStart2 = Convert.ToDateTime(_datestart2);

                            book2.StartDate = datStart2;
                            book2.EndDate = datEnd2;

                            Books.Add(book2);
                        }
                    }

                    File.WriteAllText(strBookPath, "");
                    StreamWriter bookWriter = new StreamWriter(strBookPath, true);
                    foreach (Book book in Books)
                    {
                        string sBookUpdate = book.Title + "|" + book.Author + "|" + book.Price + "|" + book.ISBN + "|" + book.Publisher + "|" + book.Rented + "|" + book.RentedBy + "|" + book.StartDate + "|" + book.EndDate;
                        bookWriter.WriteLine(sBookUpdate);
                    }
                    bookWriter.Close();

                    MessageBox.Show("Your book(s) were rented! Thank you. :)");
                    lstBook1.SelectedIndex = 0;
                    lstBook2.SelectedIndex = 0;
                    dateEnd1.SelectedDate = DateTime.Now;
                    dateEnd2.SelectedDate = DateTime.Now;
                    dateStart1.SelectedDate = DateTime.Now;
                    dateStart2.SelectedDate = DateTime.Now;
                    txtWeeklyPrice1.Text = "";
                    txtWeeklyPrice2.Text = "";
                    txtWeeks1.Text = "";
                    txtWeeks2.Text = "";
                    txtSubtotal.Text = "";
                    txtTax.Text = "";
                    txtTotal.Text = "";
                    txtRent_First.Text = "";
                    txtRent_Last.Text = "";
                    txtRent_MID.Text = "";


                }
                else
                {
                    _found = 0;
                    MessageBox.Show("(ERROR: Customer Not Found) Please reenter your customer information or sign up under 'Manage Customers'.");
                    return;
                }
            }
            
        }

        private void btnGetStarted_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void txtMID_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.MID.Contains(txtMID.Text))
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.First.Contains(txtFirstName.Text))
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.Last.Contains(txtLastName.Text))
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.Phone.Contains(txtPhone.Text))
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.Email.Contains(txtEmail.Text))
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void radM_Checked(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.Gender == 'M')
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void radF_Checked(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Customers.txt";
            string strLine = "";
            string[] rawData;
            customerList.Clear();
            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');
                    Customer customerNew;
                    customerNew = new Customer(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], Convert.ToChar(rawData[5]));
                    if (customerNew.Gender == 'F')
                    {
                        customerList.Add(customerNew);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            //set the source of the datagrid and refresh
            dtgSearchCustomers.ItemsSource = customerList;
            dtgSearchCustomers.Items.Refresh();

            //refresh the grid and enrollment information when switching back to the Read tab
            dtgBook.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = @"..\..\Data\Books.txt";

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, true);
                bookList.RemoveAt(dtgBook.SelectedIndex);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process: " + ex.Message);
            }

            MessageBox.Show("Book Deleted!");

            //set the source of the datagrid and refresh
            dtgBook.ItemsSource = bookList;
            dtgBook.Items.Refresh();
        }
    }
}
