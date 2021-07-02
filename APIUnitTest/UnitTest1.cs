using System;
using System.Resources;
using System.Threading.Tasks;
using APIProject.Controllers;
using Centra.Core;
using Centra.DataModelInfrastructure;
using DataModel;
using DataModelSQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Collections.Generic;

namespace APIUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetProductById()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.Getproduct(1)).Returns(new ShopBridge { Name = "test", Description = "test", Price=20.0 });

            var testingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var product = testingService.GetProduct(1);
            Assert.AreEqual("test", product.Name);
        }

        [TestMethod]
        public void GetProductList()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.GetproductList()).Returns(new List<ShopBridge>{ new ShopBridge { Name="Test", Description="Test" },
                              new ShopBridge { Name="Test", Description="Test" } });

            var testingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var products = testingService.GetAllProdcuts();
            Assert.AreEqual("Test", products[0].Name);
        }


        [TestMethod]
        public void AddProduct()
        {
            var addNewItem = new ShopBridge()
            {
                Name = "Test",
                Description = "Test",
                Price = 202.0
            };

            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(t => t.SaveProductConfiguration(It.IsAny<ShopBridge>())).Returns("Success");

            var testingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var status = testingService.AddNewProduct(addNewItem);
            Assert.AreEqual("Success", status);
        }

        [TestMethod]
        public void UpdateProduct()
        {
            var updateItem = new ShopBridge()
            {
                Id = 1,
                Name = "Testing",
                Description = "Testing",
                Price = 202.0
            };

            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(t => t.UpdateProductConfiguration(It.IsAny<ShopBridge>())).Returns("Success");

            var testingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var status = testingService.UpdateProduct(updateItem);
            Assert.AreEqual("Success", status);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.DeleteProduct(1)).Returns("Success");

            var testingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var status = testingService.DeleteProduct(1);
            Assert.AreEqual("Success", status);
        }
    }
}
