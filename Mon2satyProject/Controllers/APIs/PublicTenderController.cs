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
    public class PublicTenderController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public PublicTenderController()
        {
            _context = new Mon2satyDBContext();
        }

        // GET Tenders
        // /api/PublicTender
        [HttpGet]
        public IHttpActionResult GetTenders()
        {
            return Ok(_context.PublicTenders.Include(s => s.Supplier).Include(sc => sc.SubCategory).ToList());
        }

        // GET Public Tender
        // /api/PublicTender/{ID}
        [HttpGet]
        public IHttpActionResult GetTender(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tender = _context.PublicTenders.SingleOrDefault(t => t.ID == id);

            if (tender == null)
                return NotFound();

            return Ok(tender);
        }


        // GET Public Tenders By The Categories
        // /api/publictenders/{supplierID id}
        [HttpGet]
        [Route("api/publictenderCat/{id}")]
        public IHttpActionResult GetTenders(int id)
        {
            #region LastVersion
            //var subCat = _context.SupplierSubCategories.FirstOrDefault(s => s.SupplierID == id);

            //var categoryInDB = _context.SubCategories.Include(c => c.Category).SingleOrDefault(sc => sc.ID == subCat.SubCategoryID).Category;

            //if (categoryInDB == null)
            //    return NotFound();

            //var subCategoriesInDB = _context.SubCategories.ToList().FindAll(c => c.CategoryID == categoryInDB.ID);

            //if (subCategoriesInDB.Count() == 0)
            //    return NotFound();

            //var publicTenders = new List<PublicTender>();

            //var publicTendersInDB = _context.PublicTenders.ToList().FindAll(s => s.SubCategory.CategoryID == categoryInDB.ID);
            #endregion

            var publicTendersInDB = _context.SupplierPublicTenders
                .Include(t => t.PublicTender).Include(sdata => sdata.Supplier).ToList().FindAll(s => s.SupplierID == id);

            if (publicTendersInDB == null)
                return Ok();

            return Ok(publicTendersInDB);
        }



        // PUT {Update Public Tender}
        // /api/PublicTender/{ID, PublicTenderObject}
        [HttpPut]
        public IHttpActionResult UpdateTender(int id, PublicTender tender)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PublicTenders.SingleOrDefault(t => t.ID == id);

            if (tenderInDB == null)
                return NotFound();

            tenderInDB.SupplierID = tender.SupplierID;

            _context.SaveChanges();

            return Ok(tenderInDB);
        }

        // PUT {Update Public Tender}
        // /api/PublicTender/{PublicTenderObject}
        // Overloading For PUT 
        [HttpPut]
        public IHttpActionResult UpdateTender(PublicTender tender)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tenderInDB = _context.PublicTenders.SingleOrDefault(t => t.ID == tender.ID);

            if (tenderInDB == null)
                return NotFound();

            tenderInDB.SupplierID = tender.SupplierID;

            _context.SaveChanges();

            return Ok(tenderInDB);
        }

        // POST {Create Public Tender}
        // /api/PublicTender/{PublicTenderObject}
        [HttpPost]
        public IHttpActionResult CreateTender(PublicTender tender)
        {
            //if (!ModelState.IsValid)
            //  return BadRequest();

            var subCat = _context.SubCategories.SingleOrDefault(sc => sc.ID == tender.SubCategoryID);

            if (subCat == null)
                return BadRequest();

            tender.Date = DateTime.Now;

            _context.PublicTenders.Add(tender);

            _context.SaveChanges();

            var listOfSuppliers = new List<Supplier>();

            var suppliers = _context.SupplierSubCategories.Include(s => s.Supplier).Include(sc => sc.SubCategory).ToList();

            for(var i = 0; i < suppliers.Count; i++)
            {
                if(suppliers[i].SubCategoryID == subCat.ID)
                {
                    _context.SupplierPublicTenders.Add(new SupplierPublicTender()
                    {
                        Date = DateTime.Now,
                        PublicTenderID = tender.ID,
                        PublicTender = tender,
                        SupplierID = suppliers[i].SupplierID,
                        Supplier = suppliers[i].Supplier
                    });
                    _context.SaveChanges();
                }
            }

            return Created("/Home", tender);
        }


    }
}
