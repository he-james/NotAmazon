using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAmazon
{
    class Review
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Contains(' '))
                {
                    _name = value;
                }
            }
        }
        private int _rating;
        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (value <= 5 || value >= 1)
                {
                    _rating = value;
                }
            }
        }
        public string ReviewBody { get; set; }

        public Review() { }
        public Review(string fullname, int rating, string reviewbody)
        {
            Name = fullname;
            Rating = rating;
            ReviewBody = reviewbody;
        }

    }
}
