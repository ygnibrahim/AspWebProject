using AspWebProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspWebProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CustomersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            return View(await _appDbContext.Customers.ToListAsync());
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }


            return View(customer);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Email,Age")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Add(customer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _appDbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Name,Email,Age")] Customer customer)
        {

            if (id != customer.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _appDbContext.Update(customer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(customer);
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _appDbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            try
            {
                _appDbContext.Customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","ERROR!");
                return View(customer);
            }
        }
    }
}