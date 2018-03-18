using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookList.Models;
using Microsoft.EntityFrameworkCore;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _db;

        public BooksController(AppDbContext db) {
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }


        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book) {
            if (ModelState.IsValid) {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if(book == null) {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null) {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book) {
            if (id != book.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                _db.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null) {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
        }
    }
}