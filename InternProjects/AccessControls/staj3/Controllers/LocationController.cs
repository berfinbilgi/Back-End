using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

using staj3.Models;


namespace staj3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        
        private static string connectionstring = "Host=localhost;Username=postgres;Password=berfin242;Database=Staj;";
        private static NpgsqlConnection con = new NpgsqlConnection(connectionstring);

        [HttpGet("connection")]
        public string NpgsqlConnection()
        {
            con.Open();

            string create = "CREATE TABLE IF NOT EXISTS location4 (  id SERIAL PRIMARY KEY ," +
               " name character varying(200) NOT NULL," +
               " x DOUBLE PRECISION," +
               " y DOUBLE PRECISION);";
            using var cmd = new NpgsqlCommand(create, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return "Success";
           
        }
       
        [HttpGet]
        public Response Get()
        {
            List<Location> _location = new List<Location>();
            var _response = new Response();
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand( "SELECT * FROM location4 Order BY id ASC",con);
            NpgsqlDataReader reader = command.ExecuteReader();
          
            while (reader.Read())
            {
                Location location = new Location();
                location.Id = reader.GetInt32(0);
                location.Name = reader.GetString(1);
                location.X = reader.GetDouble(2);
                location.Y = reader.GetDouble(3);
                _location.Add(location);

            }
            con.Close();
            _response.Value= _location;
            return _response;
        }


        [HttpPost]
        public Response Post(Location _Location)
        {
            List<Location> _location = new List<Location>();
            var _response = new Response();
            con.Open();
            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM location4", con);
            NpgsqlDataReader reader = command1.ExecuteReader();


            while (reader.Read())
            {
                Location location = new Location();
                location.Id = ((int)reader[0]);
                location.Name = ((string)reader[1]);
                location.X = ((double)reader[2]);
                location.Y = ((double)reader[3]);
                _location.Add(location);
            }
            for( int i = 0; i < _location.Count; i++) 
            {
                if (_Location.Name.ToLower().Equals(_location[i].Name.ToLower()))
                {
                    _response.Result = "You cannot enter values with the same name";
                    con.Close();
                    return _response;
                }
            }
           
            con.Close();

            con.Open();
            
            NpgsqlCommand command = new NpgsqlCommand("insert into location4(name,x,y) values (@name,@x, @y)", con);
            

               
          
            if (_Location.Name != "")
            {
                command.Parameters.AddWithValue("@name", _Location.Name);            
            }
            else
            {
                con.Close();
                _response.Result = "Enter a valid value";
                return _response;
            }
            if (_Location.X != 0 && _Location.Y != 0) { 
            command.Parameters.AddWithValue("@x", _Location.X);
            command.Parameters.AddWithValue("@y", _Location.Y);

            }
            else
            {
                con.Close();
                _response.Result = "Enter a valid value";
                return _response;
            }
            command.ExecuteNonQuery();
            con.Close();
            return _response;

        }


        [HttpDelete("{id}")]

        public string Delete(int id)
        {
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("delete from location4 where id=" +id, con);
            command.ExecuteNonQuery();
            con.Close();
            return "silindi";
        }

        [HttpGet("{id}")]
        public Response Get(int id)
        {
            var _response = new Response();
            List<Location> _location = new List<Location>();
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("select * from location4 where id=" + id, con);
            NpgsqlDataReader reader = command.ExecuteReader();

            Location location = new Location();
            reader.Read();
            location.Id = ((int)reader[0]);
            location.Name = ((string)reader[1]);
            location.X = ((double)reader[2]);
            location.Y = ((double)reader[3]);
            _location.Add(location);
            con.Close();

            _response.Value = location;
            _response.Status = true;
            _response.Result = "found";

            return _response;
        }

        [HttpPut("{id}")]
        public Response Update(Location request)
        {
            var _response = new Response();
            con.Open();
            NpgsqlCommand command1 = new NpgsqlCommand("select * from location4 where id=" + request.Id , con);
            NpgsqlDataReader reader = command1.ExecuteReader();

            Location location = new Location();
            reader.Read();
            location.Id = ((int)reader[0]);
            location.Name = ((string)reader[1]);
            location.X = ((double)reader[2]);
            location.Y = ((double)reader[3]);
            con.Close();

            con.Open();

            NpgsqlCommand command = new NpgsqlCommand("update location4 set id=@id,  name = @name, x = @x, y = @y where id=" + request.Id, con);
 
            command.Parameters.AddWithValue("@id", request.Id);
            if ( request.Name == "string")
            {
                command.Parameters.AddWithValue("@name", location.Name);
               
            }
            else
            {
                if (request.Name == "")
                    _response.Result = "geçerli değer giriniz";

                command.Parameters.AddWithValue("@name", request.Name);
            }
            if ( request.X == 0)
            {
                command.Parameters.AddWithValue("@x", location.X);
               
            }
            else
            {
                    command.Parameters.AddWithValue("@x", request.X);
            }
            if ( request.Y == 0)
            {
                command.Parameters.AddWithValue("@y", location.Y);
                
            }
            else
            {
                command.Parameters.AddWithValue("@y", request.Y);
            }
            command.ExecuteNonQuery();


            con.Close();
            _response.Status = true;
            _response.Result = "kayıt bulundu";
            return _response;

        }
    }
}
