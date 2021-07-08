using Mon2satyProject.Models;
using Mon2satyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mon2satyProject.Controllers.APIs
{
    public class CategoriesController : ApiController
    {
        private readonly Mon2satyDBContext _context;
        
        public CategoriesController()
        {
            _context = new Mon2satyDBContext();        
        }

        // /api/Categories
        [ActionName("Categories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(_context.Categories.ToList());
        }

        // /api/Categories/ID
        [HttpGet]
        [ActionName("GetSubCategories")]
        public IHttpActionResult GetFilterCategories(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cat = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (cat == null)
                return NotFound();

            // We Only Need Name And ID For The SubCategory
            var listOfSubCats = _context.SubCategories.ToList().FindAll(sb => sb.CategoryID == cat.ID).Select(s => new { s.Name, s.ID });

            var viewModel = new
            {
                Category = cat,
                SubCats = listOfSubCats
            };



            if (cat == null)
                return NotFound();

            return Ok(viewModel);
        }

        [HttpGet]
        [ActionName("GetSuppliers")]
        [Route("Categories/GetSuppliers/{id}")]
        public IHttpActionResult GetSubCats(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var subCat = _context.SubCategories.SingleOrDefault(sc => sc.ID == id);

            if (subCat == null)
                return NotFound();

            var listOfSupp = _context.SupplierSubCategories.Include(s => s.Supplier).ToList()
                .FindAll(su => su.SubCategoryID == id).Select(supplier => new { supplier.Supplier.ID, supplier.Supplier.CompanyName });

            var viewModel = new
            {
                subCategory = subCat,
                listOfSuppliers = listOfSupp
            };

            return Ok(viewModel);
        }

        [HttpPost]
        public IHttpActionResult CreateCategory(Category category)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            _context.Categories.Add(category);

            _context.SaveChanges();

            return Created(Request.RequestUri, category);

        }

        [HttpPost]
        [Route("api/CreateSubCategory")]
        public IHttpActionResult CreateSubCategory(SubCategory subCategory)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            var category = _context.Categories.SingleOrDefault(c => c.ID == subCategory.CategoryID);

            if (category == null)
                return NotFound();

            _context.SubCategories.Add(subCategory);

            _context.SaveChanges();

            return Ok(subCategory);
        }

        [HttpGet]
        [Route("getSubCategory")]
        public IHttpActionResult GetSubCategory()
        {
            return Ok(_context.SubCategories.ToList());
        }

        // GET Supplier subcategory
        [HttpGet]
        [Route("api/suppliersubcat/{id}")]
        public IHttpActionResult GetSupplierSubCat(int id)
        {
            var subCat = _context.SupplierSubCategories.FirstOrDefault(sc => sc.SupplierID == id);

            return Ok(subCat);
        }
    }

}
