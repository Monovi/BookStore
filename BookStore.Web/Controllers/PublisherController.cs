using AutoMapper;
using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using BookStore.Web.Models.Publishers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService,
            IMapper mapper)
        {
            this._publisherService = publisherService;
            this._mapper = mapper;
        }

        // GET: PublisherController
        public async Task<IActionResult> Index()
        {
            List<Publisher> publishers = await _publisherService.GetPublishersAsync();
            List<PublisherModel> model = publishers.Select(x => _mapper.Map<PublisherModel>(x)).ToList();
            return View(model);
        }

        // GET: PublisherController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Publisher publisher = await _publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            PublisherModel model = _mapper.Map<PublisherModel>(publisher);
            return View(model);
        }

        // GET: PublisherController/Create
        public ActionResult Create()
        {
            PublisherModel model = new PublisherModel();
            return View(model);
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublisherModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Publisher publisher = _mapper.Map<Publisher>(model);
                    publisher = await _publisherService.InsertPublisher(publisher);
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

        // GET: PublisherController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Publisher publisher = await _publisherService.GetPublisherById(id);
            if (publisher==null)
            {
                return RedirectToAction(nameof(Index));
            }

            PublisherModel model = _mapper.Map<PublisherModel>(publisher);
            return View(model);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublisherModel model)
        {
            Publisher publisher = await _publisherService.GetPublisherById(model.Id);
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    publisher = _mapper.Map<PublisherModel, Publisher>(model, publisher);
                    publisher = await _publisherService.UpdatePublisher(publisher);
                    return RedirectToAction(nameof(Details), new { id = publisher.Id });
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: PublisherController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Publisher publisher = await _publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            PublisherModel model = _mapper.Map<PublisherModel>(publisher);
            return View(model);
        }

        // POST: PublisherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            Publisher publisher = await _publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                publisher = await _publisherService.DeletePublisher(publisher);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}
