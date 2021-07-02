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
        public void GetProductByName()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.Getproduct("test")).Returns(new ShopBridge { Name = "test", Description = "test" });

            var myTestingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var clientNames = myTestingService.GetProduct("test");
            Assert.AreEqual("test", clientNames.Name);
        }

        [TestMethod]
        public void GetProductList()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.GetproductList()).Returns(new List<ShopBridge>{ new ShopBridge { Name="Test", Description="Test" },
                              new ShopBridge { Name="Test", Description="Test" } });

            var myTestingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var clientNames = myTestingService.GetAllProdcuts();
            Assert.AreEqual("Test", clientNames[0].Name);
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

            var myTestingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var clientNames = myTestingService.AddNewProduct(addNewItem);
            Assert.AreEqual("Success", clientNames);
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

            var myTestingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var clientNames = myTestingService.UpdateProduct(updateItem);
            Assert.AreEqual("Success", clientNames);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            var dbReposMock = new Mock<IShopBridgeConfiguration>();
            dbReposMock.Setup(r => r.DeleteProduct(1)).Returns("Success");

            var myTestingService = new ShopBridgeConfigurationManager(dbReposMock.Object, "");

            var clientNames = myTestingService.DeleteProduct(1);
            Assert.AreEqual("Success", clientNames);
        }
    }
}
