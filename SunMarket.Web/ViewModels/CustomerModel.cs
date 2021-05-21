using System;

namespace SunMarket.Web.ViewModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAddressModel PrimaryAddress { get; set; }
    }
}