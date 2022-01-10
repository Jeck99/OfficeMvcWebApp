using OfficeMvcWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OfficeMvcWebApp.Controllers.api
{
    public class EmployeeController : ApiController
    {
        //connection string point to the database (server name, database name, security policy)
        //static string connectionString = "Data Source=LAPTOP-U0L5HKOT;Initial Catalog=OfficeDb;Integrated Security=True;Pooling=False";
        //static EmployeeDataClassesDataContext dataContext = new EmployeeDataClassesDataContext(connectionString);
        static PersonModel dataContext = new PersonModel();
        // GET: api/Employee
        public IHttpActionResult Get()
        {
            //try-catch for exceptions
            try
            {
                List<Employees> employees = dataContext.Employees.ToList();
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
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Employees employee =await dataContext.Employees.FindAsync(id);
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
        public async Task<IHttpActionResult> Post([FromBody] Employees employee)
        {
            //try-catch for exceptions
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                dataContext.Employees.Add(employee);
                await dataContext.SaveChangesAsync();
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
        public async Task< IHttpActionResult> Put(int id, [FromBody] Employees employee)
        {
            try
            {
                Employees employeeToUpdate = await dataContext.Employees.FindAsync(id);
                employeeToUpdate.first_name = employee.first_name;
                employeeToUpdate.last_name = employee.last_name;
                employeeToUpdate.birthday = employee.birthday;
                employeeToUpdate.age = employee.age;
                employeeToUpdate.email = employee.email;
                await dataContext.SaveChangesAsync();
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
        public async Task< IHttpActionResult> Delete(int id)
        {
            try
            {
                Employees employee = await dataContext.Employees.FindAsync(id);
                dataContext.Employees.Remove(employee);
                await dataContext.SaveChangesAsync();
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
