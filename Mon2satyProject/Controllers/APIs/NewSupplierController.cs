using Mon2satyProject.Models;
using Mon2satyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Mon2satyProject.Controllers.APIs
{
    public class NewSupplierController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public NewSupplierController()
        {
            _context = new Mon2satyDBContext();
        }


        // For The Admin Of The Website
        public IHttpActionResult GetNewSuppliers()
        {
            return Ok(_context.NewSuppliers.ToList());
        }

        public IHttpActionResult GetNewSupplier(int id)
        {
            if (id == 0)
                return BadRequest();

            var newSupplier = _context.NewSuppliers.SingleOrDefault(ns => ns.ID == id);

            if (newSupplier == null)
                return NotFound();

            return Ok(newSupplier);
        }

        [HttpDelete]
        public IHttpActionResult DeleteNewSupplier(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newSupplierInDB = _context.NewSuppliers.SingleOrDefault(ns => ns.ID == id);

            if (newSupplierInDB == null)
                return NotFound();

            _context.NewSuppliers.Remove(newSupplierInDB);

            _context.SaveChanges();

            return Ok();

        }
        
        [HttpPost]
        [Route("api/Confirmed")]
        public IHttpActionResult Confirmed(NewSupplier newSupplier)
        {
            SupplierController controller = new SupplierController();

            var supplier = new Supplier()
            {
                CompanyName = newSupplier.CompanyName,
                AddedDate = DateTime.Now,
                Address = newSupplier.Address,
                Email = newSupplier.Email,
                ManagerName = newSupplier.ManagerName,
                ManagerPhone = newSupplier.ManagerPhone,
                InfoPaperwork = newSupplier.InfoPaperwork,
                LegalPaperwork = newSupplier.LegalPaperwork,
                Password = newSupplier.Password,
                CompanyCode = newSupplier.CompanyCode
            };

            var login = new Login()
            {
                CompanyCode = newSupplier.CompanyCode,
                Password = newSupplier.Password
            };

            _context.Logins.Add(login);

            var viewModel = new SupplierTelFaxViewModel()
            {
                Supplier = supplier,
                SupplierPhone = newSupplier.Phone,
                SupplierFax = newSupplier.Fax,
                subCategories = _context.SubCategories.ToList().FindAll(ssc => ssc.ID == newSupplier.SubCategoryID)
            };

            var deletedSupplier = _context.NewSuppliers.SingleOrDefault(ds => ds.ID == newSupplier.ID);

            _context.NewSuppliers.Remove(deletedSupplier);

            _context.SaveChanges();

            return controller.CreateSupplier(viewModel);
        }



        // For Only New Commers
        [HttpPost]
        public IHttpActionResult CreateNewSupplier(NewSupplier newSupplier)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            _context.NewSuppliers.Add(newSupplier);

            _context.SaveChanges();

            return Created("/Home", newSupplier);
        }

        [HttpPost]
        [Route("api/updateRegistered")]
        public IHttpActionResult UpdateRegistered(NewSupplier newSupplier)
        {
            var newSupplierInDB = _context.NewSuppliers.SingleOrDefault(ns => ns.Email == newSupplier.Email);

            if (newSupplierInDB == null)
                return NotFound();

            if (newSupplier.ID == 0)
            {
                
                if (newSupplierInDB.CompanyName != newSupplier.CompanyName)
                    return NotFound();

                if (newSupplierInDB.Password != newSupplier.Password)
                    return NotFound();
            }
            else
            {
                newSupplierInDB.InfoPaperwork = newSupplier.InfoPaperwork;
                newSupplierInDB.LegalPaperwork = newSupplier.LegalPaperwork;
                newSupplierInDB.ManagerName = newSupplier.ManagerName;
                newSupplierInDB.Phone = newSupplier.Phone;
                newSupplierInDB.Fax = newSupplier.Fax;
                newSupplierInDB.ManagerPhone = newSupplier.ManagerPhone;
                newSupplierInDB.CompanyName = newSupplier.CompanyName;
                newSupplierInDB.Email = newSupplier.Email;
                newSupplierInDB.Address = newSupplier.Address;
                newSupplierInDB.Password = newSupplier.Password;
                newSupplierInDB.SubCategoryID = newSupplier.SubCategoryID;
                _context.SaveChanges();

            }


            return Ok(newSupplierInDB);
        }

    }
}
