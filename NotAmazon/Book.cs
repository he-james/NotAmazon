using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAmazon
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        private char _rented;
        public char Rented
        {
            get
            {
                return _rented;
            }
            set
            {
                if (value == '0' || value == '1')
                {
                    _rented = value;
                }
            }
        }
        public string RentedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

        public Book() { }

        public Book(string title, string author, double price, string isbn, string publisher, char rented, string rented_by, DateTime start_date, DateTime end_date)
        {
            Title = title;
            Author = author;
            Price = price;
            ISBN = isbn;
            Publisher = publisher;
            Rented = rented;
            RentedBy = rented_by;
            StartDate = start_date;
            EndDate = end_date;
        }

        // any necessary methods would go here
        // 
    }
}
