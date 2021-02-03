using Baddy.Constants;
using System;
using System.Globalization;

namespace Baddy.Models
{
    public class Profile
    {
        public string AdditionalEmail { get; set; }
        public string Balance { get; set; }
        public string CardExpiry { get; set; }
        public string CardNumber { get; set; }
        public string City { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string RefundInfo { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string Surname { get; set; }

        public string CardExpiryToString => DateTime.TryParseExact(CardExpiry, DateConstants.ShortDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
            ? result.ToString(DateConstants.LongDateFormat)
            : CardExpiry;
    }
}
