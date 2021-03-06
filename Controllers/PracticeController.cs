using System;
using System.Collections.Generic;
using System.IO;
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
    public class PracticeController : Controller
    {

        private readonly ILogger<PublisherController> mLogger;
        private readonly FingerDbContext mDbContext;
        private readonly IMapper mMapper;

        public PracticeController(ILogger<PublisherController> logger, FingerDbContext dbContext, IMapper mapper)
        {
            this.mLogger = logger;
            this.mDbContext = dbContext;
            this.mMapper = mapper;
        }

        [HttpGet]
        public JsonResult GetUserPractices(String userId)
        {
            var items = this.mDbContext.PracticeCollection.AsQueryable().Where(x => x.UserId == new ObjectId(userId))
            .ToList();
            var models = this.mMapper.Map<List<PracticeRpModelLight>>(items);
            return Json(models);
        }

        [HttpGet]
        public JsonResult GetPractice(String practiceId)
        {
            var items = this.mDbContext.PracticeCollection.AsQueryable().First(a => a.Id == ObjectId.Parse(practiceId));
            var models = this.mMapper.Map<PracticeRpModelRich>(items);
            return Json(models);
        }

        [HttpGet]
        public JsonResult GetUserPracticeSum(String userId)
        {
            var model = this.mDbContext.UserCollection.AsQueryable().First().PracticeSum;
            return Json(model);
        }


        [HttpGet]
        public JsonResult GetPracticeTexts()
        {
            var items = this.mDbContext.TextCollection.AsQueryable().Select(a => new PracticeArticleRpModel()
            {
                Id = a.Id.ToString(),
                Words = a.Words
            }).ToList();
            //var oitems = this.mMapper.Map<List<TextDbModel>, List<TextRpModel>>(items);
            return Json(items);
        }

        [HttpPost()]
        public ActionResult SavePractice([FromBody] SavePracticeModel model)
        {
            var pr = new PracticeDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                UserId = this.mDbContext.UserCollection.AsQueryable().First().Id,
                Time = DateTime.Now,
                CorrectRate = model.CorrectRate,
                Mins = model.Minutes,
                WordCount = model.Words.Count,
                Words = model.Words.Select(a =>
                    new PracticeWordDbModel()
                    {
                        Word = a.English,
                        Chinese = a.Chinese,
                        ErrorCount = a.IsError ? 1 : 0
                    }).ToList()
            };
            this.mDbContext.PracticeCollection.InsertOne(pr);
            
            return Ok();
        }
    }

    [Serializable
    ]
    public class SaveWord
    {
        public string English { get; set; }
        public string Chinese { get; set; }
        public bool IsError { get; set; }
    }
    [Serializable]
    public class SavePracticeModel
    {
        public int Minutes { get; set; }
        public double CorrectRate { get; set; }

        public List<SaveWord> Words { get; set; }

    }
}