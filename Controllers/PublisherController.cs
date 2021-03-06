using System.Collections.Generic;
using AutoMapper;
using FingerEnglishWebServer.DatabaseModels;
using FingerEnglishWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FingerEnglishWebServer.Controllers
{
    
    public class PublisherController : Controller
    {
        private readonly ILogger<PublisherController> mLogger;
        private readonly FingerDbContext mDbContext;
        private readonly IMapper mMapper;

        public PublisherController(ILogger<PublisherController> logger, FingerDbContext dbContext, IMapper mapper)
        {
            this.mLogger = logger;
            this.mDbContext = dbContext;
            this.mMapper = mapper;
        }

        public JsonResult Index()
        {

            return Json(new PublisherDbModel());
        }

        [HttpGet]
        public JsonResult GetAllPublishers()
        {
            var ps = this.mDbContext.PublisherCollection.AsQueryable().ToList();
            var list = this.mMapper.Map<List<PublisherDbModel>, List<PublisherRpModel>>(ps);
            return Json(list);
        }


    }
}