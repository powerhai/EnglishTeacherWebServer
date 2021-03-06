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

    public class WordController : Controller
    {
        private readonly ILogger<PublisherController> mLogger;
        private readonly FingerDbContext mDbContext;
        private readonly IMapper mMapper;

        public WordController(ILogger<PublisherController> logger, FingerDbContext dbContext, IMapper mapper)
        {
            this.mLogger = logger;
            this.mDbContext = dbContext;
            this.mMapper = mapper;
        }
 
        [HttpGet]
        public JsonResult GetWord(string word)
        { 
            var item =  this.mDbContext.WordCollection.AsQueryable().First(a=>a.English == word);
            var us = this.mMapper.Map<WordDbModel, WordRpModel>(item);
            return Json(us);
        } 
    }
}