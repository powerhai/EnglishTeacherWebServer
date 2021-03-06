using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FingerEnglishWebServer.DatabaseModels;
using FingerEnglishWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FingerEnglishWebServer.Controllers
{

    public class TextController : Controller
    {
        private readonly ILogger<PublisherController> mLogger;
        private readonly FingerDbContext mDbContext;
        private readonly IMapper mMapper;

        public TextController(ILogger<PublisherController> logger, FingerDbContext dbContext, IMapper mapper)
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
        public JsonResult GetText(string textId)
        { 
           // var filter = Builders<TextDbModel>.Filter.Empty;
          //  var projection = Builders<TextDbModel>.Projection.Exclude(a=>a.Id);

           // var item = mDbContext.TextCollection.Find<TextDbModel>(filter).Project(projection).First(); 
           
           var item = (from a in mDbContext.TextCollection.AsQueryable()　
           where a.Id == ObjectId.Parse(textId)  
           select new TextDbModel() { Title = a.Title,  Body = a.Body }).First();
            
 
            var us = this.mMapper.Map<TextDbModel, TextRpModel>(item);
            return Json(us);
        }


    }
}