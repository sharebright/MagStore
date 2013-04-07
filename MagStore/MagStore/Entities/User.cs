using System;
using MagStore.Data.Interfaces;
using MagStore.Entities.Enums;

namespace MagStore.Entities
{
    public class User : IRavenEntity
    {
        public User()
        {
            ShoppingCart = new Cart();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PasswordEncryption { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HouseNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public bool AgreedToMarketing { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public AccountLevel AccountLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime Create { get; set; }

        public Cart ShoppingCart { get; set; }
    }
}