using System;
using FluentAssertions;
using MagStore.Entities;
using MagStore.Entities.Enums;
using NUnit.Framework;

namespace MagStore.Test.Users
{
    public class CreatedUserWithShoppingCart : UserCoordinatorBase
    {
        [Test]
        public void ShouldCreateUserWithEmptyShoppingCart()
        {
            var id = Guid.NewGuid();
            Cart cart = new Cart();
            var user = new User
            {
                Id = id,
                ShoppingCart = cart
            };

            Store.GetCoordinator<User>().Save(user);

            var loadedUser = Store.GetCoordinator<User>().Load(id);
            loadedUser.ShoppingCart.Should().NotBeNull();
        }
    }
    public class CreatingUsersWithElevatedPrivileges : UserCoordinatorBase
    {
        [Test]
        public void ShouldCreateAdminUserWithElevatedLevel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new User
            {
                Id = id,
                AccountLevel = AccountLevel.Elevated
            };
            // Act
            Store.GetCoordinator<User>().Save( user );
            // Assert
            var loadedUser = Store.GetCoordinator<User>().Load(id);
            loadedUser.AccountLevel.Should().Be( AccountLevel.Elevated );
        }
    }
}