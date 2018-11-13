using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsertController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            // Gets and prints all books in database
            string r = null;
            using (var context = new LibraryContext())
            {
              var books = context.Book
                .Include(p => p.Publisher);
              foreach(var book in books)
              {
                var data = new StringBuilder();
                data.AppendLine($"ISBN: {book.ISBN}");
                data.AppendLine($"Title: {book.Title}");
                data.AppendLine($"Publisher: {book.Publisher.Name}");
                Console.WriteLine(data.ToString());
                r += data.ToString() + ", \n";
              }
            }
            return "result: " + r;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            using(var context = new LibraryContext())
            {
              // Creates the database if not exists
              context.Database.EnsureCreated();

              // Adds a publisher
              var publisher = new Publisher
              {
                Name = "Mariner Books"
              };
              context.Publisher.Add(publisher);

              // Adds some books
              context.Book.Add(new Book
              {
                ISBN = "978-0544003415",
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Language = "English",
                Pages = 1216,
                Publisher = publisher
              });
              context.Book.Add(new Book
              {
                ISBN = "978-0547247762",
                Title = "The Sealed Letter",
                Author = "Emma Donoghue",
                Language = "English",
                Pages = 416,
                Publisher = publisher
              });

              // Saves changes
              context.SaveChanges();
            }
            return "inserted";
        }

    }
}
