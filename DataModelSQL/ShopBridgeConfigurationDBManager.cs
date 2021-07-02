using Centra.DataModelInfrastructure;
using DataModel;
using DataModelSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelSQL
{
    public class ShopBridgeConfigurationDBManager : IShopBridgeConfiguration
    {
        /// <summary>
        /// Save product detail
        /// </summary>
        /// <param name="productConfiguration">product detail</param>
        /// <returns></returns>
        public string SaveProductConfiguration(ShopBridge productConfiguration)
        {
            string status = "Error";
            if (productConfiguration == null)
            {
                throw new ArgumentNullException("ShopBridge detail null");
            }

            string query = string.Format(@"Insert into dbo.shop_bridge_products (name, description, price, added_date_time) values('{0}', '{1}', {2}, GetUtcDate())"
                                , productConfiguration.Name, productConfiguration.Description, productConfiguration.Price);

            try
            {
                SqlHelper.Instance.ExecuteNonQuery(query);
                status = "Success";
            }
            catch (Exception ex)
            {
                status = "Error";
            }

            return status;
        }

        /// <summary>
        /// Update product detail
        /// </summary>
        /// <param name="productConfiguration"> product detail</param>
        /// <returns></returns>
        public string UpdateProductConfiguration(ShopBridge productConfiguration)
        {
            string status = "Error";
            if (productConfiguration == null)
            {
                throw new ArgumentNullException("ShopBridge detail null");
            }

            string query = string.Format(@"Update dbo.shop_bridge_products set name = '{0}', description='{1}', price={2}, modified_date_time= GetUtcDate() where
                                            id = {3}", productConfiguration.Name, productConfiguration.Description, productConfiguration.Price, productConfiguration.Id);
            try
            {
                SqlHelper.Instance.ExecuteNonQuery(query);
                status = "Success";
            }
            catch (Exception ex)
            {
                status = "Error";
            }
            return status;
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteProduct(int id)
        {
            string status = "Error";
            try
            {
                SqlHelper.Instance.ExecuteNonQuery("delete from dbo.shop_bridge_products where id = " + id);
                status = "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }

            return status;
        }

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        public List<ShopBridge> GetproductList()
        {
            List<ShopBridge> productList = null;
            try
            {
                using (SqlDataReader reader = (SqlDataReader)SqlHelper.Instance.ExecuteDataReader("select * from dbo.shop_bridge_products", CommandType.Text))
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            if (productList == null)
                                productList = new List<ShopBridge>();

                            productList.Add(new ShopBridge
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDouble(3),
                                AddedDateTime = reader.GetDateTime(4)
                            });

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return productList;
            }

            return productList;
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ShopBridge Getproduct(int id)
        {
            ShopBridge productList = null;
            try
            {
                using (SqlDataReader reader = (SqlDataReader)SqlHelper.Instance.ExecuteDataReader("select * from dbo.shop_bridge_products where id = " + id, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            if (productList == null)
                                productList = new ShopBridge()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    Price = reader.GetDouble(3),
                                    AddedDateTime = reader.GetDateTime(4)
                                };

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return productList;
            }

            return productList;
        }
    }
}
