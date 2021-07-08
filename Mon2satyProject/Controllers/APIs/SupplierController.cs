using Mon2satyProject.Models;
using Mon2satyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Security.Cryptography.Xml;

namespace Mon2satyProject.Controllers.APIs
{
    public class SupplierController : ApiController
    {
        private readonly Mon2satyDBContext _context;

        public SupplierController()
        {
            _context = new Mon2satyDBContext();
        }

        // GET api/Supplier
        public IHttpActionResult GetSuppliers()
        {
            return Ok(_context.Suppliers.ToList());
        }

        // GET api/Supplier/ID
        public IHttpActionResult GetSupplier(int id)
        {
            if (id == 0)
                return BadRequest();
 
            var supplier = _context.Suppliers.SingleOrDefault(s => s.ID == id);

            if (supplier == null)
                return NotFound();

            var suppSubCats = _context.SupplierSubCategories.Include(sc => sc.SubCategory).ToList().FindAll(ssc => ssc.SupplierID == id).Select(sc => sc.SubCategory);

            var suppPhone = _context.SupplierPhones.SingleOrDefault(ssc => ssc.SupplierID == id).Phone;

            var suppFax = _context.SupplierFaxes.SingleOrDefault(ssc => ssc.SupplierID == id).Fax;

            var supplierInfo = new SupplierTelFaxViewModel()
            {
                Supplier = supplier,
                subCategories = suppSubCats,
                SupplierFax = suppFax,
                SupplierPhone = suppPhone
            };

            return Ok(supplierInfo);
        }

        // POST api/Supplier
        [HttpPost]
        public IHttpActionResult CreateSupplier(SupplierTelFaxViewModel supplierTelFaxViewModel)
        {
            var subCats = new List<int>();
            
            foreach(var sc in supplierTelFaxViewModel.subCategories)
            {
                subCats.Add(sc.ID);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest();

            if (subCats.Count == 0)
                return BadRequest();

            var supplier = new Supplier(supplierTelFaxViewModel.Supplier);

            supplier.Susspended = "NO";

            _context.Suppliers.Add(supplier);

            var supplierPhone = new SupplierPhones()
            {
                Phone = supplierTelFaxViewModel.SupplierPhone,
                SupplierID = supplier.ID,
                Supplier = supplier
            };
            
            _context.SupplierPhones.Add(supplierPhone);

            var supplierFax = new SupplierFaxes()
            {
                Fax = supplierTelFaxViewModel.SupplierFax,
                SupplierID = supplier.ID,
                Supplier = supplier
            };

            _context.SupplierFaxes.Add(supplierFax);
            
            var login = new Login()
            {
                //Susspended = "NO",
                CompanyCode = supplier.CompanyCode,
                Password = supplier.Password
            };

            //login.Susspended = "NO";

            var listOfSubCats = supplierTelFaxViewModel.subCategories;

            var subCatSupp = new SubCategory();

            foreach (var subCat in listOfSubCats)
            {
                subCatSupp = subCat;
                subCatSupp.ID = subCat.ID;

                _context.SupplierSubCategories.Add(new SupplierSubCategories()
                {
                    SubCategoryID = subCat.ID,
                    SupplierID = supplier.ID
                });
            }

            var listOfPublicTenders = new List<PublicTender>();

            var publicTendersInDB = _context.PublicTenders.ToList();

            for (var i = 0; i < publicTendersInDB.Count; i++)
            {
                if(publicTendersInDB[i].SubCategoryID == subCatSupp.ID)
                {
                    listOfPublicTenders.Add(publicTendersInDB[i]);
                }
            }

            _context.SaveChanges();

            for (var i = 0; i < listOfPublicTenders.Count ; i++)
            {
                _context.SupplierPublicTenders.Add(
                    new SupplierPublicTender
                    {
                        Date = DateTime.Now,
                        SupplierID = supplier.ID,
                        Supplier = supplier,
                        PublicTenderID = listOfPublicTenders[i].ID,
                        PublicTender = listOfPublicTenders[i]
                    });
                _context.SaveChanges();
            }


            //_context.Logins.Add(login);

            return Created("", supplier);
        }
        //post supplier withinadmin
        [Route("postAdmin")]
        [HttpPost]
        public IHttpActionResult post(SupplierTelFaxViewModel supplierTelFaxViewModel)
        {
            var subCats = new List<int>();

            foreach (var sc in supplierTelFaxViewModel.subCategories)
            {
                subCats.Add(sc.ID);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest();

            if (subCats.Count == 0)
                return BadRequest();

            var supplier = new Supplier(supplierTelFaxViewModel.Supplier);

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            var supplierPhone = new SupplierPhones()
            {
                Phone = supplierTelFaxViewModel.SupplierPhone,
                SupplierID = supplier.ID
            };

            var supplierFax = new SupplierFaxes()
            {
                Fax = supplierTelFaxViewModel.SupplierFax,
                SupplierID = supplier.ID
            };

            var login = new Login()
            {
                CompanyCode = supplier.CompanyCode,
                Password = supplier.Password
            };

            _context.Logins.Add(login);


            _context.SupplierPhones.Add(supplierPhone);
            _context.SaveChanges();

            _context.SupplierFaxes.Add(supplierFax);
            _context.SaveChanges();

            var listOfSubCats = supplierTelFaxViewModel.subCategories;

            foreach (var subCat in listOfSubCats)
            {
                _context.SupplierSubCategories.Add(new SupplierSubCategories()
                {
                    SubCategoryID = subCat.ID,
                    SupplierID = supplier.ID
                });
            }

            _context.SaveChanges();

            return Created("", supplier);
        }



        [HttpPost]
        [Route("api/applyForTender")]
        public IHttpActionResult ApplyForTender(SupplierPublicTender supplierPublicTender)
        {
            //var supplierExists = _context.SupplierApplyForTenders
            //    .Include(t => t.PublicTender).SingleOrDefault(s => s.SupplierID == supplierApplyForTender.SupplierID);

            ////var tender = _context.SupplierApplyForTenders.Include(tt => tt.PublicTender).SingleOrDefault(t => t.TenderID == supplierApplyForTender.TenderID);

            //if (supplierExists.PublicTender != null)
            //    return Ok(supplierExists);

            //var tenderInDB = _context.PublicTenders.SingleOrDefault(t => t.ID == supplierApplyForTender.TenderID);

            //if (tenderInDB == null)
            //    return NotFound();

            //var supplierTender = new SupplierApplyForTender()
            //{
            //    SupplierID = supplierApplyForTender.SupplierID,
            //    TenderID = supplierApplyForTender.TenderID
            //};

            //_context.SupplierApplyForTenders.Add(supplierTender);

            //_context.SaveChanges();

            var supplierTender = _context.SupplierPublicTenders.Include(s => s.Supplier).Include(t => t.PublicTender)
                .ToList().FindAll(st => st.SupplierID == supplierPublicTender.SupplierID);

            for (var i = 0; i < supplierTender.Count; i++)
            {
                if(supplierTender[i].PublicTenderID == supplierPublicTender.PublicTenderID)
                {
                    supplierTender[i].Applied = "YES";
                    _context.SaveChanges();
                    return Ok(supplierTender);
                }
            }
            return NotFound();
        }

        // PUT api/Supplier/ID
        [HttpPut]
        public IHttpActionResult UpdateSupplier(SupplierTelFaxViewModel supplierTelFaxViewModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == supplierTelFaxViewModel.Supplier.ID);

            if (supplierInDB == null)
                return NotFound();

            #region ManualMapper
            supplierInDB.CompanyName = supplierTelFaxViewModel.Supplier.CompanyName;
            supplierInDB.Address = supplierTelFaxViewModel.Supplier.Address;
            supplierInDB.CompanyCode = supplierTelFaxViewModel.Supplier.CompanyCode;
            supplierInDB.Email = supplierTelFaxViewModel.Supplier.Email;
            supplierInDB.LegalPaperwork = supplierTelFaxViewModel.Supplier.LegalPaperwork;
            supplierInDB.InfoPaperwork = supplierTelFaxViewModel.Supplier.InfoPaperwork;
            supplierInDB.ManagerName = supplierTelFaxViewModel.Supplier.ManagerName;
            supplierInDB.ManagerPhone = supplierTelFaxViewModel.Supplier.ManagerPhone;
            supplierInDB.Password = supplierTelFaxViewModel.Supplier.Password;
            supplierInDB.AddedDate = supplierTelFaxViewModel.Supplier.AddedDate;

            var phone = _context.SupplierPhones.SingleOrDefault(p => p.SupplierID == supplierInDB.ID);

            var fax = _context.SupplierFaxes.SingleOrDefault(f => f.SupplierID == supplierInDB.ID);

            if(phone == null)
            {
                phone.Supplier = supplierTelFaxViewModel.Supplier;
                phone.Phone = supplierTelFaxViewModel.SupplierPhone;
            }
            else
            {
                phone.Phone = supplierTelFaxViewModel.SupplierPhone;
            }

            if (fax == null)
            {
                fax.Supplier = supplierTelFaxViewModel.Supplier;
                fax.Fax = supplierTelFaxViewModel.SupplierFax;
            }
            else
            {
                fax.Fax = supplierTelFaxViewModel.SupplierFax;
            }
            #endregion

            _context.SaveChanges();

            return Ok(supplierInDB);
        }

        #region UpdateSupplierWithDifferent
        // PUT api/Supplier/ID
        //[HttpPut]
        //public IHttpActionResult EditSupplier(Supplier supplier)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == supplier.ID);

        //    if (supplierInDB == null)
        //        return NotFound();

        //    supplierInDB.CompanyName = supplier.CompanyName;
        //    supplierInDB.Address = supplier.Address;
        //    supplierInDB.CompanyCode = supplier.CompanyCode;
        //    supplierInDB.Email = supplier.Email;
        //    supplierInDB.LegalPaperwork = supplier.LegalPaperwork;
        //    supplierInDB.InfoPaperwork = supplier.InfoPaperwork;
        //    supplierInDB.ManagerName = supplier.ManagerName;
        //    supplierInDB.ManagerPhone = supplier.ManagerPhone;
        //    supplierInDB.Password = supplier.Password;
        //    supplierInDB.AddedDate = supplier.AddedDate;

        //    _context.SaveChanges();

        //    return Ok();
        //}

        // DELETE api/Supplier/ID
        #endregion

        [HttpDelete]
        public IHttpActionResult DeleteSupplier(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var supplierInDB = _context.Suppliers.SingleOrDefault(s => s.ID == id);

            if (supplierInDB == null)
                return NotFound();

            var loginInDB = _context.Logins.SingleOrDefault(l => l.CompanyCode == supplierInDB.CompanyCode);

            _context.Logins.Remove(loginInDB);

            _context.Suppliers.Remove(supplierInDB);

            _context.SaveChanges();

            return Ok();

        }
    }
}
