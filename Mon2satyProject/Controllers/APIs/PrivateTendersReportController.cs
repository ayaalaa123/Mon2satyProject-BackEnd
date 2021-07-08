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
    public class PrivateTendersReportController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public PrivateTendersReportController()
        {
            _context = new Mon2satyDBContext();
        }

        [Route("GetPrivateTenderReportByID")]
        [HttpGet]
        public IHttpActionResult getPrivateTenderById(int id)
        {
            var publicTenderInDb = _context.PrivateTenders.SingleOrDefault(pubtender => pubtender.ID == id);
            if (publicTenderInDb == null)
                return NotFound();
            else
            {
                var listOfSupplier = _context.SupplierPrivateTenders.Include(s => s.Supplier)
                    .ToList().FindAll(pubtendr => pubtendr.PrivateTenderID == publicTenderInDb.ID);
                var supplierChoosen = _context.PrivateTenders.SingleOrDefault(p => p.ID == id).Supplier;
                var listOfSupplierandSupplierChoosen = new listOfSupplierandsupplierInPrivate()
                {
                    suppliers = listOfSupplier,
                    supplier = supplierChoosen
                };

                return Ok(listOfSupplierandSupplierChoosen);

            }


        }



        // GET All
        // /api/PrivateTendersReport
        [HttpGet]
        public IHttpActionResult GetTenders()
        {
            return Ok(_context.SupplierPrivateTenders.Include(s => s.Supplier).Include(t => t.PrivateTender).ToList());
        }

        // GET Data About One Private Tender
        // /api/PrivateTendersReport/{tenderID}
        [HttpGet]
        public IHttpActionResult GetTender(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PrivateTenders.SingleOrDefault(t => t.ID == id);

            if (tenderInDB == null)
                return NotFound();

            var listOfSuppliers = _context.SupplierPrivateTenders.Include(supplier => supplier.Supplier).ToList().FindAll(s => s.PrivateTenderID == tenderInDB.ID);

            return Ok(listOfSuppliers);
        }
    }
}
