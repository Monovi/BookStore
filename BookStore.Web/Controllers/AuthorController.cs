using AutoMapper;
using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Models.Authors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService,
            IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
        }

        // GET: AuthorController
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _authorService.GetAuthorsAsync();
            List<AuthorModel> model = authors.Select(a => _mapper.Map<AuthorModel>(a)).ToList();
            return View(model);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Author author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            AuthorModel model = _mapper.Map<AuthorModel>(author);
            return View(model);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            AuthorModel model = new AuthorModel();
            return View(model);
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Author author = _mapper.Map<Author>(model);
                    author = await _authorService.InsertAuthor(author);
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

        // GET: AuthorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Author author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            AuthorModel model = _mapper.Map<AuthorModel>(author);
            return View(model);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorModel model)
        {
            Author author = await _authorService.GetAuthorById(model.Id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    author = _mapper.Map<AuthorModel, Author>(model, author);
                    author = await _authorService.UpdateAuthor(author);
                    return RedirectToAction(nameof(Details), new { id = author.Id });
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Author author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            AuthorModel model = _mapper.Map<AuthorModel>(author);
            return View(model);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            Author author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                author = await _authorService.DeleteAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}
