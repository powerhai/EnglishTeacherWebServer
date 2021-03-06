using System.Collections.Generic;
using AutoMapper;
using FingerEnglishWebServer.DatabaseModels;
using FingerEnglishWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FingerEnglishWebServer.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ILogger<PublisherController> mLogger;
        private readonly FingerDbContext mDbContext;
        private readonly IMapper mMapper;

        public UserController(ILogger<PublisherController> logger, FingerDbContext dbContext, IMapper mapper)
        {
            this.mLogger = logger;
            this.mDbContext = dbContext;
            this.mMapper = mapper;
        }

   

        [HttpGet]
        public JsonResult GetUserProfile()
        {
            var ps = this.mDbContext.UserCollection.AsQueryable().First();
            var us = this.mMapper.Map<UserDbModel, UserRpModel>(ps);
            return Json(us);
        }

        [HttpGet]
        public ContentResult LoginUser(){ 
            var user = this.mDbContext.UserCollection.AsQueryable().First();
            return Content(user.Id.ToString());
        }

    }
}