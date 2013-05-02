using System;
using FluentAssertions;
using MagStore.Entities;
using MagStore.Entities.Enums;
using MagStore.Infrastructure;
using MagStore.Infrastructure.Interfaces;
using NUnit.Framework;

namespace MagStore.Test.Users
{
    public class CreatedUserWithShoppingCart : UserCoordinatorBase
    {
        [Test]
        public void ShouldCreateUserWithEmptyShoppingCart()
        {
            var id = Guid.NewGuid().ToString();
            Cart cart = new Cart();
            var user = new User
            {
                Id = id,
                ShoppingCart = cart
            };

            Shop.GetCoordinator<User>().Save(user);

            var loadedUser = Shop.GetCoordinator<User>().Load(id);
            loadedUser.ShoppingCart.Should().NotBeNull();
        }
    }
    public class CreatingUsersWithElevatedPrivileges : UserCoordinatorBase
    {
        [Test]
        public void ShouldCreateAdminUserWithElevatedLevel()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                AccountLevel = AccountLevel.Elevated
            };
            // Act
            Shop.GetCoordinator<User>().Save( user );
            // Assert
            var loadedUser = Shop.GetCoordinator<User>().Load(id);
            loadedUser.AccountLevel.Should().Be( AccountLevel.Elevated );
        }
    }
}