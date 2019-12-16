using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAmazon
{
    class Customer
    {
        public string MID { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        // set phone# parameters
        // phone is a string, as is conventional
        // because phone numbers are not used for calculations
        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (value.Length == 10)
                {
                    _phone = value;
                }
            }

        }
        // set email parameters
        // assuming most email lengths are greater than 7
        // yes, most emails are at least 11 characters, but in case
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value.Contains("@") && value.Length > 7)
                {
                    _email = value;
                }
            }
        }
        // setting gender parameters
        private char _gender;
        public char Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (char.ToLower(value) == 'm' || char.ToLower(value) == 'f')
                {
                    _gender = char.ToUpper(value);
                }
            }
        }

        public Customer() { }

        public Customer(string memberID, string first, string last, string phone, string email, char gender)
        {
            MID = memberID;
            First = first;
            Last = last;
            Phone = phone;
            Email = email;
            Gender = gender;
        }

        // any necessary methods would go here
        public string getFullName()
        {
            return First + " " + Last;
        }
    }
}
