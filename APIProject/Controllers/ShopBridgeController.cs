using APIProject.Filter;
using Centra.Core;
using Centra.DataModelInfrastructure;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIProject.Controllers
{
    [CustomExceptionFilter]
    public class ShopBridgeController : ApiController
    {
        [Route("ShopBridge/GetAllProduct")]
        [HttpGet]
        public async Task<IEnumerable<ShopBridge>> GetAllProduct()
        {
            return await ShopBridgeConfigurationManager.Instance.GetAllProdcuts();
        }

        [Route("ShopBridge/GetProduct/{id}")]
        [HttpGet]
        public async Task<ShopBridge> GetProductById(int id)
        {
            return await ShopBridgeConfigurationManager.Instance.GetProduct(id);
        }

        [Route("ShopBridge/AddProduct")]
        [HttpPost]
        public async Task<string> AddProduct([FromBody]ShopBridge product)
        {
            if (!ModelState.IsValid)
            {
                return "One of the property value wrong or exceeded.";
            }
            return await ShopBridgeConfigurationManager.Instance.AddNewProduct(product);
        }

        [Route("ShopBridge/UpdateProduct")]
        [HttpPost]
        public async Task<string> UpdateProduct([FromBody]ShopBridge product)
        {
            if (!ModelState.IsValid)
            {
                return "One of the property value wrong or exceeded.";
            }
            return await ShopBridgeConfigurationManager.Instance.UpdateProduct(product);
        }

        [Route("ShopBridge/DeleteProduct/{id}")]
        [HttpDelete]
        public async Task<string> DeleteProduct(int id)
        {
            return await ShopBridgeConfigurationManager.Instance.DeleteProduct(id);
        }
    }
}
