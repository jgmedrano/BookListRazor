using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookListRazor.Models;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var dbBook = await _db.Books.FindAsync(Book.Id);
                dbBook.Name = Book.Name;
                dbBook.Author = Book.Author;
                dbBook.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
