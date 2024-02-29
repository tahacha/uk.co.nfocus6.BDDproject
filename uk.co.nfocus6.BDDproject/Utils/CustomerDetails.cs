using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus6.BDDproject.Utils
{
    internal class CustomerDetails
    {
        private string _firstName;
        private string _lastName;
        private string _streetAddress;
        private string _city;
        private string _postcode;
        private string _phone;
        public CustomerDetails(string firstName, string lastName, string streetAddress, string city, string postcode, string phone)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._streetAddress = streetAddress;
            this._city = city;
            this._postcode = postcode;
            this._phone = phone;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _lastName;
        }

        public string GetAddress()
        {
            return _streetAddress;
        }

        public string GetCity()
        {
            return _city;
        }

        public string GetPostcode()
        {
            return _postcode;
        }

        public string GetPhone()
        {
            return _phone;
        }
    }
}
