using GraniteHouse.Data;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using GraniteHouse.Utility;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using GraniteHouse.Models.Queries;
using AutoMapper;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        //private readonly HostingEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        [BindProperty]
        public ProductsViewModel ProductVM { get; set; }
        public ProductsController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;


            ProductVM = new ProductsViewModel()
            {
                ProductTypes = _db.ProductTypess.ToList(),
                SpecialTags = _db.SpecialTagss.ToList(),
                Products = new Models.Products()
            };
        }
        public async Task<IActionResult> Index()
        {
            var products = _db.Productss.Include(m => m.ProductTypes).Include(m => m.SpecialTags);
            var result = await products.AsNoTracking().ToListAsync();

            var vm = _mapper.Map<List<ProductVMEntitet>>(result);

            return View(vm);
        }


        ////primjer Create bez bind***********************
        ///
        //public IActionResult Create() 
        //{
        //    ViewData["ProductTypes"] = new SelectList(_db.ProductTypess, "Id", "Name");
        //    ViewData["SpecialTags"] = new SelectList(_db.SpecialTagss, "Id", "SpecialName");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Products products)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Add(products);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(products);
        //}


        //GET Create Action Method
        public IActionResult Create()
        {
            return View(ProductVM);
        }


        //POST Create Action Mathod
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductVM);
            }

            _db.Productss.Add(ProductVM.Products);
            await _db.SaveChangesAsync();

            //Image beging saved

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var productsFromDb = _db.Productss.Find(ProductVM.Products.Id);

            if (files.Count != 0)
            {
                //Image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, ProductVM.Products.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductVM.Products.Id + extension;
            }
            else
            {
                //when user does not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ProductVM.Products.Id + @".png");
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductVM.Products.Id + ".png";
            }
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //primjer Edit bez bind-a*****

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ProductsViewModel productsView = new ProductsViewModel();
        //    productsView.Products = await _db.Productss.Include(m => m.ProductTypes).Include(m => m.SpecialTags).SingleOrDefaultAsync(m => m.Id == id);

        //    ViewData["ProductTypes"] = new SelectList(_db.ProductTypess, "Id", "Name");
        //    ViewData["SpecialTags"] = new SelectList(_db.SpecialTagss, "Id", "SpecialName");
        //    if (productsView.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productsView);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id,Products products)
        //{
        //    if (id != products.Id) 
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _db.Update(products);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(products);
        //}

        //GET Edit action method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductVM.Products = await _db.Productss.Include(m => m.ProductTypes).Include(m => m.SpecialTags).SingleOrDefaultAsync(m => m.Id == id);

            if (ProductVM.Products == null)
            {
                return NotFound();
            }

            return View(ProductVM);
        }

        //POST Edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var productFromDb = _db.Productss.Where(m => m.Id == ProductVM.Products.Id).FirstOrDefault();

                if (files.Count > 0 && files[0] != null)
                {
                    //if user uploads a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(productFromDb.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, ProductVM.Products.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, ProductVM.Products.Id + extension_old));
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, ProductVM.Products.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    ProductVM.Products.Image = @"\" + SD.ImageFolder + @"\" + ProductVM.Products.Id + extension_new;
                }

                if (ProductVM.Products.Image != null)
                {
                    productFromDb.Image = ProductVM.Products.Image;
                }

                productFromDb.Name = ProductVM.Products.Name;
                productFromDb.Price = ProductVM.Products.Price;
                productFromDb.ShadeColor = ProductVM.Products.ShadeColor;
                productFromDb.SpecialTagId = ProductVM.Products.SpecialTagId;
                productFromDb.ProductTypeId = ProductVM.Products.ProductTypeId;
                productFromDb.Avaliable = ProductVM.Products.Avaliable;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(ProductVM);
        }

        //GET Details action method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Productss.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<ProductVMEntitet>(product);

            return View(vm);
        }

        //GET Delete action method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Productss.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<ProductVMEntitet>(product);

            return View(vm);
        }

        //POST Delete action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) 
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Products product = await _db.Productss.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(product.Image);

                if (System.IO.File.Exists(Path.Combine(uploads, product.Id + extension))) 
                {
                    System.IO.File.Delete(Path.Combine(uploads, product.Id + extension));
                }

                _db.Productss.Remove(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
