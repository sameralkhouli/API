using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inlamningsuppgift;
using Inlamningsuppgift.Models.Entities;
using Inlamningsuppgift.Models;

namespace Inlamningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductController(SqlContext context)
        {
            _context = context;
        }



        // GET: api/User
        [HttpGet]
        //[UseApiKey]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetUsers()
        {

            var items = new List<ProductModel>();
            foreach (var i in await _context.Products.ToListAsync())
                items.Add(new ProductModel(i.Id, i.ProductName, i.Disc, i.Price));
            return items;
        }



        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            return productEntity;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[UseAdminApiKey]
        public async Task<IActionResult> PutUserEntity(int id, ProductUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var productEntity = await _context.Products.FindAsync(id);
            productEntity.ProductName = model.ProductName;
            productEntity.Disc = model.Disc;
            productEntity.Price = model.Price;
            productEntity.Note = model.Note;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[UseAdminApiKey]
        public async Task<ActionResult<ProductModel>> PostUserEntity(ProductCreateModel model)
        {
            if (await _context.Products.AnyAsync(x => x.ProductName == model.ProductName))
                return BadRequest();

            var productEntity = new ProductEntity(model.ProductName, model.Disc, model.Price, model.Note);

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return new ProductModel(productEntity.Id, productEntity.ProductName, productEntity.Disc, productEntity.Price);
        }



        // DELETE: api/Product/5
        [HttpDelete("{id}")]
       // [UseAdminApiKey]
        public async Task<IActionResult> DeleteUserEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
