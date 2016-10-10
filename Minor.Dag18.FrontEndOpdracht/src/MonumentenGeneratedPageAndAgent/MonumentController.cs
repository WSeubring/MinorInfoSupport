using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonumentenGeneratedPageAndAgent.Models;

namespace MonumentenGeneratedPageAndAgent
{
    public class MonumentController : Controller
    {
        private readonly MonumentenGeneratedPageAndAgentContext _context;

        public MonumentController(MonumentenGeneratedPageAndAgentContext context)
        {
            _context = context;    
        }

        // GET: Monument
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monument.ToListAsync());
        }

        // GET: Monument/Details/5
        public async Task<IActionResult> Details(int?? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monument = await _context.Monument.SingleOrDefaultAsync(m => m.Id == id);
            if (monument == null)
            {
                return NotFound();
            }

            return View(monument);
        }

        // GET: Monument/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam")] Monument monument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monument);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(monument);
        }

        // GET: Monument/Edit/5
        public async Task<IActionResult> Edit(int?? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monument = await _context.Monument.SingleOrDefaultAsync(m => m.Id == id);
            if (monument == null)
            {
                return NotFound();
            }
            return View(monument);
        }

        // POST: Monument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Naam")] Monument monument)
        {
            if (id != monument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonumentExists(monument.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(monument);
        }

        // GET: Monument/Delete/5
        public async Task<IActionResult> Delete(int?? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monument = await _context.Monument.SingleOrDefaultAsync(m => m.Id == id);
            if (monument == null)
            {
                return NotFound();
            }

            return View(monument);
        }

        // POST: Monument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var monument = await _context.Monument.SingleOrDefaultAsync(m => m.Id == id);
            _context.Monument.Remove(monument);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MonumentExists(int? id)
        {
            return _context.Monument.Any(e => e.Id == id);
        }
    }
}
