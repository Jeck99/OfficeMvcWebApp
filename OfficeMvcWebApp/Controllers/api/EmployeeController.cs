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
        static string connectionString = "Data Source=server name;Initial Catalog=OfficeDatabase;Integrated Security=True;Pooling=False";
        static EmployeeDataClassesDataContext dataContext = new EmployeeDataClassesDataContext(connectionString);
        // GET: api/Employee
        public IHttpActionResult Get()
        {
            //try-catch for exceptions
            try
            {
                List<Employee> employees = GetAllEmployees();
                return Ok(new { employees });
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
                Employee employee = GetEmployee(id);
                return Ok(new { employee });
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
                AddNewEmployee(employee);
                return Ok("success");
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
                UpdateEmployee(employee, id);
                return Ok("success");

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
                DeleteEmployee(id);
                return Ok("success");
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

        // functions using the Linq to SQL
        /// retriving the data from Employees table, using SELECT
        private static List<Employee> GetAllEmployees()
        {
            List<Employee> listOfEmploeeys = new List<Employee>();
            var resultQuery = from employee in dataContext.Employees select employee;
            if (resultQuery.Any())
            {
                listOfEmploeeys = resultQuery.ToList();
            }
            return listOfEmploeeys;
        }
        /// <returns> List<Employee> list of emplyees object</returns>
        private static Employee GetEmployee(int id)
        {
            Employee employeeResult = new Employee();
            var resultQuery = from employee in dataContext.Employees where employee.Id == id select employee;
            if (resultQuery.Any())
            {
                employeeResult = resultQuery.ToList()[0];
            }
            return employeeResult;
        }
        // adding new entry to the Employees table
        private static void AddNewEmployee(Employee employee)
        {
            dataContext.Employees.InsertOnSubmit(employee);
            dataContext.SubmitChanges();
        }
        // updating an existing entry in the Employees table
        private static void UpdateEmployee(Employee employee, int id)
        {
            Employee employeeToUpdate = dataContext.Employees.FirstOrDefault(employeeItem => employeeItem.Id == id);
            employeeToUpdate = employee;
            dataContext.SubmitChanges();
        }
        // deleting an existing entry in Employees table
        private static void DeleteEmployee(int id)
        {
            Employee employeeToUpdate = dataContext.Employees.FirstOrDefault(employeeItem => employeeItem.Id == id);
            dataContext.Employees.DeleteOnSubmit(employeeToUpdate);
            dataContext.SubmitChanges();
        }
    }
}
