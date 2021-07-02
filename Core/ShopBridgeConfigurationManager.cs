
using Centra.DataModelInfrastructure;
using Core.Helper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// The shop bridge configuration repository
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

        public async Task<List<ShopBridge>> GetAllProdcuts()
        {
            return await Task.Run(() => _shopBridgeConfigurationRepository.GetproductList());
        }


        public async Task<string> AddNewProduct(ShopBridge product)
        {
            return await Task.Run(() => _shopBridgeConfigurationRepository.SaveProductConfiguration(product));
        }

        public async Task<string> UpdateProduct(ShopBridge product)
        {
            return await Task.Run(() => _shopBridgeConfigurationRepository.UpdateProductConfiguration(product));
        }

        public async Task<string> DeleteProduct(int id)
        {
            return await Task.Run(() => _shopBridgeConfigurationRepository.DeleteProduct(id));
        }

        public async Task<ShopBridge> GetProduct(int id)
        {
            return await Task.Run(() => _shopBridgeConfigurationRepository.Getproduct(id));
        }


    }
}