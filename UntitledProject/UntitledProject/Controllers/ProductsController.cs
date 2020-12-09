using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UntitledProject.Models;

namespace UntitledProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UntitledProjectContext _context;
        private UserManager<AppUser> userMgr;
        private IWebHostEnvironment Environment;

        public ProductsController(UntitledProjectContext context, UserManager<AppUser> userManager, IWebHostEnvironment _environment)
        {
            _context = context;
            userMgr = userManager;
            Environment = _environment;
        }

        // GET: api/Products
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        [HttpGet("{user}/{username}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetOwnersProduct(string username)
        {
            AppUser user = await userMgr.FindByNameAsync(username);

            return await _context.Product.Where(p => p.Offerer == user).ToListAsync();
        }

        [HttpGet("{productpic}/{productId}")]
        public IActionResult GetProductPic(int productId)
        {
            Product product = _context.Product.FirstOrDefault(p => p.ProductId == productId);
            string wwwPath = this.Environment.WebRootPath;
            var uploads = Path.Combine(wwwPath, "img\\products");
            var file = Path.Combine(uploads, product.ImageName);
            Byte[] b = System.IO.File.ReadAllBytes(file);
            return File(b, "image/jpeg");
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        string wwwPath = this.Environment.WebRootPath;
                        var uploads = Path.Combine(wwwPath, "img\\products");

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');

                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                product.ImageName = file.FileName;
                            }


                        }
                    }
                }
                AppUser usr = _context.AppUser.FirstOrDefault(a => a.Id == userMgr.GetUserId(User));
                product.Offerer = usr;
                _context.Product.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }
            else
            {
                return null;
            }
            
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
