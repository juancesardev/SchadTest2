using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchadTest.Data;
using SchadTest.Models;

namespace SchadTest.Controllers
{
    public class DetailsTempController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetailsTempController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetailsTemp
        public async Task<IActionResult> Index()
        {
              return View(await _context.DetailsTemp.ToListAsync());
        }

        // GET: DetailsTemp/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.DetailsTemp == null)
            {
                return NotFound();
            }

            var details_Temp = await _context.DetailsTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (details_Temp == null)
            {
                return NotFound();
            }

            return View(details_Temp);
        }

        // GET: DetailsTemp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailsTemp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Header_Guid,Header_Id,ServiceId,Description,Qty,Price,Status,DateAdd")] Details_Temp details_Temp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(details_Temp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(details_Temp);
        }

        public async Task<IActionResult> AddServices(int intCustomer, string head, int hId, decimal it, decimal to, decimal st)
        {
            if (!string.IsNullOrEmpty(head) && hId > 0 && it > 0 && to > st)
            {

                /*
                Invoice inv = new Invoice();
                inv.CustomerId = intCustomer;
                inv.TotalItbis = it;
                inv.SubTotal = st;
                inv.Total = to;
                if (inv != null)
                {
                    //_context.Add(inv);
                    //await _context.SaveChangesAsync();
                }
                var itemInvoice = _context.Invoice
                   .OrderByDescending(p => p.CustomerId.Equals(intCustomer) && p.TotalItbis.Equals(it) && p.SubTotal.Equals(st) && p.Total.Equals(to))
                   .FirstOrDefault();
                */
                //int qty_ = invoiceDetail.Qty;
                //decimal Price_ = invoiceDetail.Price;
                int IdInvocie = 0;

                Invoice inv = new Invoice();
                inv.CustomerId  = intCustomer;
                inv.TotalItbis  = it;
                inv.SubTotal    = st;
                inv.Total       = to;

                //calculate itbis - subtotal and total


                if (inv != null)
                {
                    _context.Add(inv);
                    await _context.SaveChangesAsync();
                    IdInvocie = inv.Id;
                }

                if (IdInvocie < 0)
                {

                    return NotFound();
                }
                else  {
                    
                var selectServ = (from x in _context.DetailsTemp
                                  join e in _context.Services on x.ServiceId equals e.Id
                                  join t in _context.HeaderTemp on x.Header_Id equals t.Id
                                  where x.Header_Guid.Equals(head) && x.Header_Id.Equals(hId) && x.Status.Equals(true)
                                  select new
                                  {
                                      InvoiceId = IdInvocie,
                                      CustomerId = IdInvocie,
                                      ServiceId = x.ServiceId,
                                      Description = x.Description,
                                      Qty = x.Qty,
                                      Price = x.Price,
                                      PriceDescription = e.PriceDescription,

                                  }).ToList();

                if (selectServ != null)
                {
                        int next = 0;
                    foreach (var item in selectServ)
                    {
                        InvoiceDetail tempSer = new InvoiceDetail();
                        tempSer.InvoiceId = item.InvoiceId;
                        tempSer.CustomerId = item.CustomerId;
                        tempSer.ServiceId = item.ServiceId;
                        tempSer.Description = item.Description;
                        tempSer.Qty = item.Qty;
                        tempSer.Price = item.Price;
                        tempSer.PriceDescription = item.PriceDescription;
                        tempSer.TotalItbis = (item.Price * (0.18M)) * item.Qty;
                        tempSer.SubTotal = (item.Price * item.Qty);
                        tempSer.Total = tempSer.TotalItbis + tempSer.SubTotal;

                        //bool existS = await _context.InvoiceDetail.AnyAsync(x => x.InvoiceId.Equals(item.InvoiceId) && x.CustomerId.Equals(item.CustomerId) && x.ServiceId.Equals(item.ServiceId) && x.Qty.Equals(item.Qty) && x.Price.Equals(item.Price));

                        if (tempSer != null) {
                            _context.Add(tempSer);
                                await _context.SaveChangesAsync();
                            }

                    }
                    
                        
                    Task.Delay(1000);

                            var details_Temp = await _context.HeaderTemp.FirstOrDefaultAsync(d => d.Id.Equals(hId));
                            if (details_Temp != null)
                            {
                                _context.HeaderTemp.Remove(details_Temp);
                            }

                            var details_TempData = await _context.DetailsTemp.Where(d => d.Header_Id.Equals(hId) && d.Header_Guid.Equals(head)).ToListAsync();
                            if (details_TempData != null)
                            {
                                foreach (Details_Temp itemO in details_TempData)
                                {
                                    _context.DetailsTemp.Remove(itemO);
                                }
                                await _context.SaveChangesAsync();

                            }

                            return LocalRedirect($"~/Customers/Index?intCustomer={intCustomer}&hId={hId}");
                        
                }
            }
            }
            return View();
        }

        // GET: DetailsTemp/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.DetailsTemp == null)
            {
                return NotFound();
            }

            var details_Temp = await _context.DetailsTemp.FindAsync(id);
            if (details_Temp == null)
            {
                return NotFound();
            }
            return View(details_Temp);
        }

        // POST: DetailsTemp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Header_Guid,Header_Id,ServiceId,Description,Qty,Price,Status,DateAdd")] Details_Temp details_Temp)
        {
            if (id != details_Temp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details_Temp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Details_TempExists(details_Temp.Id))
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
            return View(details_Temp);
        }

        // GET: DetailsTemp/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.DetailsTemp == null)
            {
                return NotFound();
            }

            var details_Temp = await _context.DetailsTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (details_Temp == null)
            {
                return NotFound();
            }

            return View(details_Temp);
        }


        // POST: DetailsTemp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, int intCustomer, int hId)
        {
            if (_context.DetailsTemp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DetailsTemp'  is null.");
            }
            var details_Temp = await _context.DetailsTemp.FindAsync(id);
            if (details_Temp != null)
            {
                _context.DetailsTemp.Remove(details_Temp);
            }
            
            await _context.SaveChangesAsync();
            return LocalRedirect($"~/DetailsTemp/NewInvoice?intCustomer={intCustomer}&hId={hId}");
        }


        private bool Details_TempExists(long id)
        {
          return _context.DetailsTemp.Any(e => e.Id == id);
        }



        public async Task<IActionResult> NewInvoice(int intCustomer, int hId)
        {
            Int64 nGuid = 0;
            if (intCustomer < 1) return LocalRedirect($"~/Customers/Index");
            ViewData["intCustomer"] = intCustomer;

            var customer = await _context.Customers
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.Id == intCustomer);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["CustomerName"] = customer.CustName;
            ViewData["CustomerAddress"] = customer.Adress;

            int step = 0;
            if(hId > 0)
            {
                var Hed = await _context.HeaderTemp.FirstOrDefaultAsync(a => a.Id.Equals(hId) && a.Status.Equals(true));
                if (Hed != null)
                {
                    ViewData["Head"] = Hed.Header_Guid;
                    ViewData["HeadId"] = Hed.Id;
                    nGuid = Hed.Id;
                }
                else
                {
                    step = 1;
                }
            }
            else
            {
                step = 1;
            }

            if (step == 1)
            {
                var Hed = await _context.HeaderTemp.FirstOrDefaultAsync(a => a.CustomerId.Equals(intCustomer) && a.Status.Equals(true));
                if (Hed != null)
                {
                    ViewData["Head"] = Hed.Header_Guid;
                    ViewData["HeadId"] = Hed.Id;
                    nGuid = Hed.Id;
                }
                else
                {
                    step = 2;
                }
            }

            if(step == 2)
            {
                //check header
                Header_Temp temp = new Header_Temp();
                temp.Status = true;
                temp.Header_Guid = Guid.NewGuid().ToString();
                temp.CustomerId = intCustomer;
                _context.Add(temp);
                await _context.SaveChangesAsync();

                var head = await _context.HeaderTemp.FirstOrDefaultAsync(s => s.CustomerId.Equals(intCustomer) && s.Status.Equals(true));
                if (head != null)
                {
                    ViewData["Head"] = head.Header_Guid;
                    ViewData["HeadId"] = head.Id;
                    nGuid = head.Id;
                }

            }


            var allServices = await _context.DetailsTemp.Where(s => s.Status.Equals(true) && s.Header_Id.Equals(nGuid)).ToListAsync();
            ViewBag.allServices = allServices;
            int cSer = allServices.Count();
            
            ViewData["ServiceId"] = new SelectList(_context.Services.Where(s => s.Status.Equals(true)), "Id", "Description");



            return View();
        }


        public async Task<IActionResult> Services()
        {
            int cSer = _context.Services.Where(s => s.Status.Equals(true)).Count();
           
            var serv = await _context.Services.Where(s => s.Status.Equals(true)).ToListAsync();

            ViewBag.qtySer = cSer;
            ViewBag.services = serv;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Service(int? id)
        {

            var services = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.PrecioDesc = services.PriceDescription;
            ViewBag.Precio = services.Price.ToString("0.00");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice(string head, Int64 headid, int ServiceId, decimal price, int qty, int intCustomer)
        {
            //validate head
            bool existHead = _context.HeaderTemp.Any(e => e.Id == headid);

            //validate user
            bool existCustomer = _context.Customers.Any(e => e.Id == intCustomer);

            //validate service
            bool existService = _context.Services.Any(s => s.Id.Equals(ServiceId));

            string description = "";
            if(existService == true)
            {
                var serv = await _context.Services.FirstOrDefaultAsync(s => s.Id.Equals(ServiceId));
                description = serv.Description;
            }

            //add service temp

            if (existHead == true && existCustomer == true && existService == true && qty > 0)
            {
                Details_Temp tempSer = new Details_Temp();
                tempSer.Header_Guid = head;
                tempSer.Header_Id = headid;
                tempSer.ServiceId = ServiceId;
                tempSer.Description = description;
                tempSer.Qty = qty;
                tempSer.Price = price;
                tempSer.Status = true;
                tempSer.DateAdd = DateTime.Now;

                _context.Add(tempSer);
                await _context.SaveChangesAsync();
                return LocalRedirect($"~/DetailsTemp/NewInvoice?intCustomer={intCustomer}&hId={headid}");
            }

            return LocalRedirect($"~/DetailsTemp/NewInvoice");
        }
    }
}
