using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Mon2satyProject.ViewModels;

namespace Mon2satyProject.Controllers.APIs
{
    public class PublicTendersReportController : ApiController
    {

        private readonly Mon2satyDBContext _context;

        public PublicTendersReportController()
        {
            _context = new Mon2satyDBContext();
        }

        // GET
        // api/PublicTenders
        [HttpGet]
        public IHttpActionResult GetPublicTenders()
        {
            return Ok(_context.SubCategories.Include(c => c.Category).ToList());
        }

        // GET Report About The Public Tenders Report
        // api/PublicTenders/PublicTendersReport
        [Route("PublicTendersReport")]
        [HttpGet]
        public IHttpActionResult GetPublicTendersReport()
        {
            return Ok(_context.SupplierPublicTenders.Include(s => s.Supplier).Include(t => t.PublicTender).ToList());
        }

        [HttpPost]
        [Route("api/confirmPayment")]
        public IHttpActionResult ConfirmPayment(SupplierPublicTender supplierPublicTender)
        {
            //    var supplierTenderInDB = _context.SupplierPublicTenders
            //        .ToList().FindAll(s => s.SupplierID == supplierPublicTender.SupplierID);

            var supplierTenderInDB = _context.SupplierPublicTenders
                .FirstOrDefault(s => s.PublicTenderID == supplierPublicTender.PublicTenderID
                && s.SupplierID == supplierPublicTender.SupplierID);

            if(supplierTenderInDB != null)
            {
                if (supplierTenderInDB.Paid == "YES")
                {
                    supplierTenderInDB.Paid = "NO";
                    _context.SaveChanges();
                }
                else
                {
                    supplierTenderInDB.Paid = "YES";
                    _context.SaveChanges();
                }
                
                return Ok(supplierTenderInDB);
            }

            //for(var i = 0; i < supplierTenderInDB.Count; i++)
            //{
            //    if(supplierTenderInDB[i].PublicTenderID == supplierPublicTender.PublicTenderID)
            //    {
            //        if (supplierTenderInDB[i].Paid == "YES")
            //        {
            //            supplierTenderInDB[i].Paid = "NO";
            //        }
            //        else
            //        {
            //            supplierTenderInDB[i].Paid = "YES";
            //        }
            //        return Ok();
            //    }
            //}
            return NotFound();
        }

        // GET BY SUBCATEGORY
        // api/PublicTenders/{SubCategoryID}
        [HttpGet]
        public IHttpActionResult GetPublicTenders(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var subCategoryInDB = _context.SubCategories.SingleOrDefault(sc => sc.ID == id);

            if (subCategoryInDB == null)
                return NotFound();

            var listOfPublicTenders = _context.PublicTenders.ToList().FindAll(sc => sc.SubCategoryID == subCategoryInDB.ID);

            var listOfSuppliers = _context.SupplierSubCategories.Include(s => s.Supplier)
                .ToList().FindAll(sc => sc.SubCategoryID == subCategoryInDB.ID).Select(supplier => supplier.Supplier);

            var viewModel = new PublicTendersSuppliersViewModel()
            {
                Suppliers = listOfSuppliers,
                PublicTenders = listOfPublicTenders
            };

            return Ok(viewModel);
        }




        [Route("GetPublicTenderReportByID")]
        [HttpGet]
        public IHttpActionResult getPublicTenderById(int id)
        {
            var publicTenderInDb = _context.PublicTenders.SingleOrDefault(pubtender => pubtender.ID == id);
            if (publicTenderInDb == null)
                return NotFound();
            else
            {
                var listOfSupplier = _context.SupplierPublicTenders.Include(s => s.Supplier)
                    .ToList().FindAll(pubtendr => pubtendr.PublicTenderID == publicTenderInDb.ID);
                var supplierChoosen = _context.PublicTenders.SingleOrDefault(p => p.ID == id).Supplier;
                var listOfSupplierandSupplierChoosen = new listOfSupplierandsupplier()
                {
                    suppliers = listOfSupplier,
                    supplier = supplierChoosen
                };

                return Ok(listOfSupplierandSupplierChoosen);

            }


        }





        // POST In SupplierPublicTender
        // api/PublicTenders/{tenderID}, {supplierID}
        public IHttpActionResult CreateSupplierPublicTender(int tenderID, int supplierID)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PublicTenders.SingleOrDefault(pt => pt.ID == tenderID);

            if (tenderInDB == null)
                return NotFound();

            var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == supplierID);

            if (supplierInDB == null)
                return NotFound();

            var publicTenderSupplierObject = new SupplierPublicTender()
            {
                Date = DateTime.Now,
                SupplierID = supplierInDB.ID,
                PublicTenderID = tenderInDB.ID,
            };

            _context.SupplierPublicTenders.Add(publicTenderSupplierObject);

            _context.SaveChanges();

            return Created(Request.RequestUri, publicTenderSupplierObject);
        }

    }
}
