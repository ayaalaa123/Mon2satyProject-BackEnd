using Mon2satyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Mon2satyProject.Controllers.APIs
{
    public class PrivateTenderController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public PrivateTenderController()
        {
            _context = new Mon2satyDBContext();
        }

        // GET Tenders
        // /api/PrivateTender
        [HttpGet]
        public IHttpActionResult GetTenders()
        {
            return Ok(_context.PrivateTenders.Include(s => s.Supplier).Include(sc => sc.SubCategory).ToList());
        }

        // GET Private Tender
        // /api/PrivateTender/{ID}
        [HttpGet]
        public IHttpActionResult GetTender(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tender = _context.PrivateTenders.Include(s => s.Supplier).Include(sc => sc.SubCategory).SingleOrDefault(t => t.ID == id);

            if (tender == null)
                return NotFound();

            return Ok(tender);
        }

        // GET SupplierPrivateTenders 
        // /api/supplierTenders/{supplierID}
        [HttpGet]
        [Route("api/suppliertender/{id}")]
        public IHttpActionResult GetSupplierTenders(int id)
        {
            var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == id);

            if (supplierInDB == null)
                return NotFound();

            //var supplierPrivateTenders = _context.PrivateTenders.Include(sc => sc.SubCategory)
            //    .ToList().FindAll(s => s.SupplierID == id);

            var supplierPrivateTenders = _context.SupplierPrivateTenders.Include(t => t.PrivateTender.SubCategory).ToList().FindAll(s => s.SupplierID == id);

            if (supplierPrivateTenders == null)
                return NotFound();

            return Ok(supplierPrivateTenders);
        }

        #region GET Private Tender By SubCategories
        // GET Private Tender By SubCategories
        // api/PrivateTender/{SubCategory Object}
        //[HttpGet]
        //public IHttpActionResult GetTenders(SubCategory category)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var catInDB = _context.SubCategories.SingleOrDefault(sc => sc.ID == category.ID);

        //    if (catInDB == null)
        //        return NotFound();

        //    var listOfTenders = _context.PrivateTenders.ToList().FindAll(t => t.SubCategoryID == catInDB.ID);

        //    return Ok(listOfTenders);

        //}
        #endregion

        // PUT {Update Private Tender}
        // /api/PrivateTender/{ID, PrivateTenderObject}
        [HttpPut]
        public IHttpActionResult UpdateTender(int id, PrivateTender tender)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PrivateTenders.SingleOrDefault(t => t.ID == id);

            if (tenderInDB == null)
                return NotFound();

            tenderInDB.SupplierID = tender.SupplierID;

            _context.SaveChanges();

            return Ok(tenderInDB);
        }

        // PUT {Update Private Tender}
        // /api/PrivateTender/{PrivateTenderObject}
        // Overloading For PUT 
        [HttpPut]
        public IHttpActionResult UpdateTender(PrivateTender tender)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PrivateTenders.SingleOrDefault(t => t.ID == tender.ID);

            if (tenderInDB == null)
                return NotFound();

            tenderInDB.SupplierID = tender.SupplierID;

            _context.SaveChanges();

            return Ok(tenderInDB);
        }

        // POST {Create Private Tender}
        // /api/PrivateTender/{PrivateTenderObject}
        [HttpPost]
        public IHttpActionResult CreateTender(PrivateTender tender)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var subCat = _context.SubCategories.SingleOrDefault(s => s.ID == tender.SubCategoryID);

            if (subCat == null)
                return BadRequest();

            ////tender.SubCategoryID = tender.SubCategory.ID;

            tender.Date = DateTime.Now;

            _context.PrivateTenders.Add(tender);

            _context.SaveChanges();

            var listOfSuppliers = _context.SupplierSubCategories.ToList().FindAll(ssc => ssc.SubCategoryID == subCat.ID).Select(s => s.SupplierID);

            foreach (var supplier in listOfSuppliers)
            {
                _context.SupplierPrivateTenders.Add(new SupplierPrivateTender()
                {
                    SupplierID = supplier,
                    PrivateTenderID = tender.ID,
                    Date = DateTime.Now
                });
            }

            _context.SaveChanges();

            return Created("/Home", tender);
        }
        //public IHttpActionResult CreateTender(PrivateTender tender)
        //{
        //    //if (!ModelState.IsValid)
        //    //    return BadRequest();
        //    var subCat = _context.SubCategories.SingleOrDefault(sc => sc.ID == tender.SubCategory.ID);

        //    if (subCat == null)
        //        return BadRequest();

        //    //tender.SubCategoryID = tender.SubCategory.ID;

        //    tender.Date = DateTime.Now;

        //    _context.PrivateTenders.Add(tender);

        //    _context.SaveChanges();

        //    var listOfSuppliers = _context.SupplierSubCategories
        //        .ToList().FindAll(ssc => ssc.SubCategoryID == subCat.ID).Select(s => s.SupplierID);

        //    foreach(var supplier in listOfSuppliers)
        //    {
        //        _context.SupplierPrivateTenders.Add(new SupplierPrivateTender()
        //        {
        //            SupplierID = supplier,
        //            PrivateTenderID = tender.ID,
        //            Date = DateTime.Now
        //        });
        //    }

        //    _context.SaveChanges();

        //    return Created("/Home", tender);
        //}

    }
   
}
