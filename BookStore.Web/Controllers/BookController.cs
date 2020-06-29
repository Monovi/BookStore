using AutoMapper;
using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.ModelFactories.Interfaces;
using BookStore.Web.Models.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookModelFactory _bookModelFactory;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService,
            IBookModelFactory bookModelFactory,
            IMapper mapper)
        {
            this._bookService = bookService;
            this._bookModelFactory = bookModelFactory;
            this._mapper = mapper;
        }

        // GET: BookController
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _bookService.GetBooksAsync();
            List<BookModel> model = books.Select(x => _mapper.Map<BookModel>(x)).ToList();
            return View(model);
        }

        // GET: BookController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            BookModel model = _mapper.Map<BookModel>(book);
            return View(model);
        }

        // GET: BookController/Create
        public async Task<IActionResult> Create()
        {
            BookModel model = new BookModel();
            await _bookModelFactory.PrepareBookModel(model);
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book book = _mapper.Map<Book>(model);
                    book = await _bookService.InsertBook(book);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: BookController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            BookModel model = _mapper.Map<BookModel>(book);
            await _bookModelFactory.PrepareBookModel(model);
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookModel model)
        {
            Book book = await _bookService.GetBookById(model.Id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book = _mapper.Map<BookModel, Book>(model, book);
                    book = await _bookService.UpdateBook(book);
                    return RedirectToAction(nameof(Details), new { id = book.Id });
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    await _bookModelFactory.PrepareBookModel(model);
                    return View(model);
                }
            }

            await _bookModelFactory.PrepareBookModel(model);
            return View(model);
        }

        // GET: BookController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            BookModel model = _mapper.Map<BookModel>(book);
            return View(model);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            Book book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                book = await _bookService.DeleteBook(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}
