using Mon2satyProject.Models;
using Mon2satyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.UI;

namespace Mon2satyProject.Controllers.APIs
{
    public class LoginController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public LoginController()
        {
            _context = new Mon2satyDBContext();
        }

        // Get All Logins
        // /api/Login
        [HttpGet]
        public IHttpActionResult GetLogins()
        {
            return Ok(_context.Logins.ToList());
        }


        // POST
        // /api/Login/{LoginObject}
        [HttpPost]
        public IHttpActionResult GetLogin(Login login)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var loginInDB = _context.Logins.SingleOrDefault(l => l.CompanyCode == login.CompanyCode);

            if (loginInDB == null)
                return BadRequest();

            if (login.Password != loginInDB.Password)
                return BadRequest();

            var userInDB = _context.Suppliers.SingleOrDefault(u => u.CompanyCode == loginInDB.CompanyCode);

            return Ok(userInDB);

        }

        [HttpPost]
        [Route("api/LoginInfo")]
        public IHttpActionResult GetInfo(Login login)
        {

            var loginInDB = _context.Logins.SingleOrDefault(l => l.CompanyCode == login.CompanyCode);

            if (loginInDB == null)
                return BadRequest();

            if (login.Password != loginInDB.Password)
                return BadRequest();

            var userInDB = _context.Suppliers.SingleOrDefault(u => u.CompanyCode == loginInDB.CompanyCode);

            var telInDB = _context.SupplierPhones.SingleOrDefault(t => t.SupplierID == userInDB.ID);

            var faxInDB = _context.SupplierFaxes.SingleOrDefault(f => f.SupplierID == userInDB.ID);

            var viewModel = new SupplierTelFaxViewModel()
            {
                Supplier = userInDB,
                SupplierFax = faxInDB.Fax,
                SupplierPhone = telInDB.Phone,
                subCategories=null

            };
            
            return Ok(viewModel);
        }

        [HttpPost]
        [Route("api/susspended")]
        public IHttpActionResult SusspendSupplier(Login login)
        {
            var loginInDB = _context.Logins.SingleOrDefault(l => l.CompanyCode == login.CompanyCode);

            if (loginInDB == null)
                return NotFound();

            var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.CompanyCode == login.CompanyCode);

            if (loginInDB.Susspended == "YES")
            {
                loginInDB.Susspended = "NO";
                supplierInDB.Susspended = "NO";
            }

            else
            {
                loginInDB.Susspended = "YES";
                supplierInDB.Susspended = "YES";
            }

            _context.SaveChanges();

            return Ok(loginInDB);
        }
    }
}

