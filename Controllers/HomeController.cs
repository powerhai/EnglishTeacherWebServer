using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FingerEnglishWebServer.Models;
using FingerEnglishWebServer.Services;
using FingerEnglishWebServer.DatabaseModels;
using MongoDB.Bson;
using MongoDB.Driver;
using AutoMapper;

namespace FingerEnglishWebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FingerDbContext dbContext;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, FingerDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

     
        public JsonResult Index()
        {
            var publisher = new PublisherDbModel() { Id = ObjectId.GenerateNewId(), Title = "人教版" };
            var book = new BookDbModel() { Id = ObjectId.GenerateNewId() , Title = "china", PublisherId = publisher.Id };
            this.dbContext.PublisherCollection.InsertOne(publisher);
            this.dbContext.BookCollection.InsertOne(book);
        
           // var book = this.dbContext.BookCollection.AsQueryable().First() ;
           // var publisher = this.dbContext.PublisherCollection.AsQueryable().First(a=>a.Id == book.PublisherId);
             
            var ccc = mapper.Map<BookRpModel>(book);
            var ddd = mapper.Map<BookDbModel>(ccc);
            return Json(ccc );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
