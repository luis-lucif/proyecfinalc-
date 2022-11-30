using CoderFinalProject_Emilio_De_Leon.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace CoderFinalProject_Emilio_De_Leon.Controllers
{

    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getallproducts")]

        public dynamic GetProducts()
        {
            String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Producto", connection))
                    {
                        connection.Open();
                        List<Producto> ProductList = new List<Producto>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Producto product = new Producto();
                                    product.Id = int.Parse(reader["Id"].ToString());
                                    product.Descripciones = reader["Descripciones"].ToString();
                                    product.Costo = decimal.Parse(reader["Costo"].ToString());
                                    product.PrecioVenta = decimal.Parse(reader["PrecioVenta"].ToString());
                                    product.Stock = int.Parse(reader["Stock"].ToString());
                                    product.IdUsuario = int.Parse(reader["IdUsuario"].ToString());

                                    ProductList.Add(product);
                                }
                                connection.Close();
                                var ProducListJson = JsonSerializer.Serialize(ProductList);
                                return ProducListJson;
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
