using OfficeMvcWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OfficeMvcWebApp.Controllers.api
{
    public class EmployeeController : ApiController
    {
        //connection string point to the database (server name, database name, security policy)
        string connectionString = "Data Source=server name;Initial Catalog=OfficeDatabase;Integrated Security=True;Pooling=False";
        // GET: api/Employee
        public IHttpActionResult Get()
        {
            //try-catch for exceptions
            try
            {
                //using and cleaning of SqlConnection object
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    List<Employee> employees = GetAllEmployees(connection);
                    return Ok(new { employees });
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/Employee/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Employee employee = GetEmployee(connection,id);
                    return Ok(new { employee });
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST: api/Employee
        public IHttpActionResult Post([FromBody] Employee employee)
        {
            //try-catch for exceptions
            try
            {
                //using and cleaning of SqlConnection object
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int rows = AddNewEmployee(employee, connection);
                    return Ok(new { rows });
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/Employee/5
        public IHttpActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int rows = UpdateEmployee(employee,id, connection);
                    return Ok(new { rows });
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int rows = DeleteEmployee(connection,id);
                    return Ok(new { rows });
                }
            }
            catch (SqlException sqlEx)
            {
                return BadRequest(sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // functions using the ADO.NET
        /// <summary>
        /// retriving the data from Employees table, using SELECT
        /// </summary>
        /// <param type="SqlConnection" name="connection"></param>
        /// <returns> List<Employee> list of emplyees object</returns>
        private static List<Employee> GetAllEmployees(SqlConnection connection)
        {
            List<Employee> listOfEmploeeys = new List<Employee>();
            connection.Open();
            string query = @"SELECT * FROM Employees";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataFromDB = command.ExecuteReader();
            if (dataFromDB.HasRows)
            {
                //pop data into the list
                while (dataFromDB.Read())
                {
                    listOfEmploeeys.Add(new Employee(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetInt32(3), dataFromDB.GetDateTime(4), dataFromDB.GetString(5)));
                }
            }
            connection.Close();
            return listOfEmploeeys;
        }
        /// <returns> List<Employee> list of emplyees object</returns>
        private static Employee GetEmployee(SqlConnection connection,int id)
        {
            List<Employee> listOfEmploeeys = new List<Employee>();
            connection.Open();
            string query = $@"SELECT * FROM Employees WHERE Id = {id}";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataFromDB = command.ExecuteReader();
            if (dataFromDB.HasRows)
            {
                while (dataFromDB.Read())
                {
                    listOfEmploeeys.Add(new Employee(dataFromDB.GetString(1), dataFromDB.GetString(2), dataFromDB.GetInt32(3), dataFromDB.GetDateTime(4), dataFromDB.GetString(5)));
                }
            }
            connection.Close();
            return listOfEmploeeys[0];
        }
        // adding new entry to the Employees table, using INSERT INTO
        private static int AddNewEmployee(Employee employee, SqlConnection connection)
        {
            connection.Open();
            string query = $@"INSERT INTO Employees(first_name,last_name,age,birthday,email)
                                    VALUES('{employee.FirstName}','{employee.LastName}',{employee.Age},'{employee.Birthday}','{employee.Email}')";
            SqlCommand command = new SqlCommand(query, connection);
            int rowsEffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsEffected;
        }
        // updating an existing entry in the Employees table, using UPDATE-SET
        private static int UpdateEmployee(Employee employee, int id, SqlConnection connection)
        {
            connection.Open();
            string query = $@"UPDATE Employees 
                                SET first_name = '{employee.FirstName}', last_name='{employee.LastName}', age={employee.Age}, birthday='{employee.Birthday}', email='{employee.Email}'
            WHERE Id = {id}";
            SqlCommand command = new SqlCommand(query, connection);
            int rowsEffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsEffected;

        }
        // deleting an existing entry in Employees table, using DELETE
        private static int DeleteEmployee(SqlConnection connection, int id)
        {
            connection.Open();
            string query = $@"DELETE FROM Employees
                                    WHERE Id = {id}";
            SqlCommand command = new SqlCommand(query, connection);
            int rowEffected = command.ExecuteNonQuery();
            connection.Close();
            return rowEffected;
        }
    }
}
