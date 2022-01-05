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
                List<Employee> employees = dataContext.Employees.ToList();
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
                Employee employee = dataContext.Employees.First((employeeItem) => employeeItem.Id == id);
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
                dataContext.Employees.InsertOnSubmit(employee);
                dataContext.SubmitChanges();
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
                Employee employeeToUpdate = dataContext.Employees.Single(employeeItem => employeeItem.Id == id);
                employeeToUpdate = employee;
                dataContext.SubmitChanges();
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
                Employee employee = dataContext.Employees.First(employeeItem => employeeItem.Id == id);
                dataContext.Employees.DeleteOnSubmit(employee);
                dataContext.SubmitChanges();
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
    }
}
