using CoderFinalProject_Emilio_De_Leon.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace CoderFinalProject_Emilio_De_Leon.Controllers
{
    [ApiController]
    [Route("soldproducts")]
    public class SoldProductController : ControllerBase
    {
        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getallsoldproducts")]

        public dynamic GetSoldProducts()
        {
            String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ProductoVendido", connection))
                    {
                        connection.Open();
                        List<SoldProduct> SoldProductList = new List<SoldProduct>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    SoldProduct soldProduct = new SoldProduct();
                                    soldProduct.Id = int.Parse(reader["Id"].ToString());
                                    soldProduct.Stock = int.Parse(reader["Stock"].ToString());
                                    soldProduct.IdProducto = int.Parse(reader["IdProducto"].ToString());
                                    soldProduct.IdVenta = int.Parse(reader["IdVenta"].ToString());

                                    SoldProductList.Add(soldProduct);
                                }
                                connection.Close();
                                var SoldProductListJson = JsonSerializer.Serialize(SoldProductList);
                                return SoldProductListJson;
                            }
                            else
                            {
                                return "No data";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
