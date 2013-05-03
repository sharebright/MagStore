using System;
using System.Collections.Generic;
using MagStore.Entities.Enums;
using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
	public class User : IRavenEntity
	{
		public string Id { get; set; }
		public string ApplicationName { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordSalt { get; set; }
        public string Title { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateLastLogin { get; set; }
		public IList<string> Roles { get; set; }

		public User()
		{
		    ShoppingCart = new Cart
		        {
		        Id = Guid.NewGuid().ToString(),
                Products = new List<string>(),
                Promotions = new List<string>()
		    };
			Roles = new List<string>();
			Id = "authorization/users/"; // db assigns id
		}

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

        public Cart ShoppingCart { get; set; }


	}
}
