
using Centra.DataModelInfrastructure;
using Core.Helper;
using DataModel;
using System;
using System.Collections.Generic;

namespace Centra.Core
{
    public class ShopBridgeConfigurationManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static ShopBridgeConfigurationManager _instance;

        /// <summary>
        /// The instance synchronize lock
        /// </summary>
        private static readonly object _instanceSyncLock = new object();

        /// <summary>
        /// The smart monitor configuration repository
        /// </summary>
        public readonly IShopBridgeConfiguration _shopBridgeConfigurationRepository;


        /// <summary>
        /// The connection string
        /// </summary>
        public static string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopBridgeConfigurationManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="activityLogger">The activity logger.</param>
        public ShopBridgeConfigurationManager(IShopBridgeConfiguration repository, string connectionString)
        {
            _shopBridgeConfigurationRepository = repository;
            _connectionString = connectionString;
        }

        /// <summary>The <see cref="Core"/> singleton instance.</summary>
        public static ShopBridgeConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceSyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ShopBridgeConfigurationManager(GetInstance.Get<IShopBridgeConfiguration>(), _connectionString);
                        }
                    }
                }
                return _instance;
            }
        }

        public List<ShopBridge> GetAllProdcuts()
        {
            return _shopBridgeConfigurationRepository.GetproductList();
        }


        public string AddNewProduct(ShopBridge product)
        {
            return _shopBridgeConfigurationRepository.SaveProductConfiguration(product);
        }

        public string UpdateProduct(ShopBridge product)
        {
            return _shopBridgeConfigurationRepository.UpdateProductConfiguration(product);
        }

        public string DeleteProduct(int id)
        {
            return _shopBridgeConfigurationRepository.DeleteProduct(id);
        }

        public ShopBridge GetProduct(string name)
        {
            return _shopBridgeConfigurationRepository.Getproduct(name);
        }


    }
}