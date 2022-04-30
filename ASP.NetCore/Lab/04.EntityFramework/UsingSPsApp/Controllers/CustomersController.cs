using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsingSPsApp.DB;

namespace UsingSPsApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TestDBContext _context;

        public CustomersController(TestDBContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.FromSqlRaw("sp_GetAllCustomers").ToListAsync());
            //return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var p0 = new SqlParameter("@CustomerId", id);

            var customer = (await _context.Customers.FromSqlRaw("sp_GetCustomerByID @CustomerId", p0)
                                    .ToListAsync())[0];

            //var customer = await _context.Customers
            //    .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,City,Country")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var p0 = new SqlParameter("@Name", customer.Name);
                var p1 = new SqlParameter("@City", customer.City);
                var p2 = new SqlParameter("@Country", customer.Country);
                await Task.Run(() =>
                      _context.Database.ExecuteSqlRawAsync("sp_AddCustomer @Name,@City,@Country", p0, p1, p2)
                );

                //_context.Add(customer);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p0 = new SqlParameter("@CustomerId", id);

            var customer = (await _context.Customers.FromSqlRaw("sp_GetCustomerByID @CustomerId", p0)
                                    .ToListAsync())[0];

            //var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name,City,Country")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var p0 = new SqlParameter("@CustomerId", customer.CustomerId);
                    var p1 = new SqlParameter("@Name", customer.Name);
                    var p2 = new SqlParameter("@City", customer.City);
                    var p3 = new SqlParameter("@Country", customer.Country);

                    await Task.Run(() =>
                    _context.Database.ExecuteSqlRawAsync("sp_UpdateCustomer @CustomerId, @Name,@City,@Country", p0, p1, p2, p3)
                    );

                    //_context.Update(customer);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p0 = new SqlParameter("@CustomerId", id);

            var customer = (await _context.Customers.FromSqlRaw("sp_GetCustomerByID @CustomerId", p0)
                              .ToListAsync())[0];

            //var customer = await _context.Customers
            //    .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p0 = new SqlParameter("@CustomerId", id);

            var customer = (await _context.Customers.FromSqlRaw("sp_GetCustomerByID @CustomerId", p0)
                                    .ToListAsync())[0];

            //var customer = await _context.Customer.FindAsync(id);

            await Task.Run(() =>
              _context.Database.ExecuteSqlRaw("sp_DeleteCustomer @CustomerId", p0)
              );

            //_context.Customer.Remove(customer);
            //await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            //return _context.Customer.Any(e => e.CustomerId == id);
            var p0 = new SqlParameter("@CustomerId", id);
            var customer = _context.Customers.FromSqlRaw("sp_GetCustomerByID @CustomerId", p0).FirstOrDefaultAsync();
            return (customer != null);

        }
    }
}
