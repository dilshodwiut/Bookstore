using Bookstore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        string baseUrl = "http://ec2-18-212-163-74.compute-1.amazonaws.com/";

        // GET: Book
        public async Task<ActionResult> Index()
        {
            List<Book> BookInfo = new List<Book>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Book");

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;

                    BookInfo = JsonConvert.DeserializeObject<List<Book>>(Response);
                }

                return View(BookInfo);
            }
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int ID)
        {
            Book book = new Book();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Book/" + ID);

                if (response.IsSuccessStatusCode)
                {
                    var bookResponse = response.Content.ReadAsStringAsync().Result;

                    book = JsonConvert.DeserializeObject<Book>(bookResponse);
                }
            }
            return View("Details", book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress=new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(book);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/Book", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int ID)
        {
            Book book = new Book();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Book/" + ID);

                if (response.IsSuccessStatusCode)
                {
                    var bookResponse = response.Content.ReadAsStringAsync().Result;

                    book = JsonConvert.DeserializeObject<Book>(bookResponse);
                }
            }
            return View("Create", book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Book book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(book);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + "api/Book", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Create", book);

            }
        }

        // DELETE: Book/Delete/5
        public async Task<ActionResult> Delete(int ID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("api/Book/" + ID);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }
    }
}
