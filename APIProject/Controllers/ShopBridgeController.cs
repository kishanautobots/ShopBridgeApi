using APIProject.Filter;
using Centra.Core;
using Centra.DataModelInfrastructure;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProject.Controllers
{
    [CustomExceptionFilter]
    public class ShopBridgeController : ApiController
    {
        [Route("ShopBridge/GetAllProduct")]
        [HttpGet]
        public IEnumerable<ShopBridge> GetAllProduct()
        {
            return ShopBridgeConfigurationManager.Instance.GetAllProdcuts();
        }

        [Route("ShopBridge/AddProduct")]
        [HttpPost]
        public string AddProduct([FromBody]ShopBridge product)
        {
            if (!ModelState.IsValid)
            {
                return "One of the property value wrong or exceeded.";
            }
            return ShopBridgeConfigurationManager.Instance.AddNewProduct(product);
        }

        [Route("ShopBridge/UpdateProduct")]
        [HttpPost]
        public string UpdateProduct([FromBody]ShopBridge product)
        {
            if (!ModelState.IsValid)
            {
                return "One of the property value wrong or exceeded.";
            }
            return ShopBridgeConfigurationManager.Instance.UpdateProduct(product);
        }

        [Route("ShopBridge/DeleteProduct/{id}")]
        [HttpDelete]
        public string DeleteProduct(int id)
        {
            return ShopBridgeConfigurationManager.Instance.DeleteProduct(id);
        }
    }
}
