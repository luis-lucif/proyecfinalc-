using CoderFinalProject_Emilio_De_Leon.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace CoderFinalProject_Emilio_De_Leon.Controllers
{
    [ApiController]
    [Route("sells")]
    public class SellController : ControllerBase
    {
        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getsells")]

        public dynamic GetSells()
        {
            String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Venta", connection))
                    {
                        connection.Open();
                        List<Sell> SellList = new List<Sell>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Sell sell = new Sell();
                                    sell.Id = int.Parse(reader["Id"].ToString());
                                    sell.Comentarios = reader["Comentarios"].ToString();
                                    sell.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
   
                                    SellList.Add(sell);
                                }
                                connection.Close();
                                var SellListJson = JsonSerializer.Serialize(SellList);
                                return SellListJson;
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
