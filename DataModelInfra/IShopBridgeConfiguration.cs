
using DataModel;
using System;
using System.Collections.Generic;

namespace Centra.DataModelInfrastructure
{
    public interface IShopBridgeConfiguration
    {
        string SaveProductConfiguration(ShopBridge productConfiguration);

        string DeleteProduct(int id);

        string UpdateProductConfiguration(ShopBridge productConfiguration);

        List<ShopBridge> GetproductList();

        ShopBridge Getproduct(int id);

    }
}