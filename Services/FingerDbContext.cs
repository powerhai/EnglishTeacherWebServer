using System;
using System.Collections.Generic;
using System.Linq;
using FingerEnglishWebServer.DatabaseModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FingerEnglishWebServer.Services
{

    public class FingerDbContext
    {

        private readonly MongoSettings mongoSettings;
        IMongoDatabase mDatabase;
        public FingerDbContext(IOptions<MongoSettings> mongoSettings)
        {
            this.mongoSettings = mongoSettings.Value;
            var client = new MongoClient(this.mongoSettings.ConnectString);
            this.mDatabase = client.GetDatabase(this.mongoSettings.DatabaseName);
        }


        public IMongoCollection<BookDbModel> BookCollection
        {
            get
            {
                return mDatabase.GetCollection<BookDbModel>(TableNames.Book);
            }
        }

        public IMongoCollection<TextDbModel> TextCollection
        {
            get { return mDatabase.GetCollection<TextDbModel>(TableNames.Text); }
        }

        public IMongoCollection<PublisherDbModel> PublisherCollection
        {
            get
            {
                return mDatabase.GetCollection<PublisherDbModel>(TableNames.Publisher);
            }
        }

        public IMongoCollection<UserDbModel> UserCollection
        {
            get
            {
                return mDatabase.GetCollection<UserDbModel>(TableNames.User);
            }
        }

        public IMongoCollection<PracticeDbModel> PracticeCollection
        {
            get
            {
                return mDatabase.GetCollection<PracticeDbModel>(TableNames.Practice);
            }
        }

        public IMongoCollection<WordDbModel> WordCollection
        {
            get { return mDatabase.GetCollection<WordDbModel>(TableNames.Word); }
        }

    }

    public class DbInitializer
    {
        public static void Initialize(FingerDbContext dbContext)
        {
            InitializePublisher(dbContext);
            AddUser(dbContext);
            InitializeUser(dbContext);
            UpdateUser(dbContext);
            InitializeText(dbContext);
            InitializeWord(dbContext);
        }

        private static void InitializeWord(FingerDbContext dbContext)
        {
            if (dbContext.WordCollection.AsQueryable().Count() > 0)
                return;
            var w1 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "you", Chinese = "你" };
            dbContext.WordCollection.InsertOne(w1);
            var w2 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "up", Chinese = "上" };
            dbContext.WordCollection.InsertOne(w2);
            var w3 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "No", Chinese = "不" };
            dbContext.WordCollection.InsertOne(w3);
            var w4 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "Stop", Chinese = "停" };
            dbContext.WordCollection.InsertOne(w4);

            var w5 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "Miss", Chinese = "女士" };
            dbContext.WordCollection.InsertOne(w5);

            var w6 = new WordDbModel() { Id = ObjectId.GenerateNewId(), English = "Good", Chinese = "好" };
            dbContext.WordCollection.InsertOne(w6);

            var w7 = new WordDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                English = "morning",
                Chinese = "早上",
                Parts = new WordPartDbModel[] {
                    new WordPartDbModel(){ English = "mor", Chinese = "早"},
                    new WordPartDbModel(){ English = "ning", Chinese = "字根"} 
                 }
            };
            dbContext.WordCollection.InsertOne(w7);

        }

        private static void InitializeText(FingerDbContext dbContext)
        {
            var filter = Builders<TextDbModel>.Filter.Empty;
            if (dbContext.TextCollection.CountDocuments(filter) > 0)
                return;

            var book = dbContext.BookCollection.AsQueryable().First();
            var text = new TextDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                BookId = book.Id,
                Title = "The school day",
                Body = new SentenceDbModel[]{
                    new SentenceDbModel(){
                        English  = "Oliver: ",
                        Chinese = "",
                        SentenceType = SentenceType.UnPracticeSentence
                    },
                    new SentenceDbModel(){
                        English  = "Thank you very much.",
                        Chinese = "非常感谢你",
                        SentenceType = SentenceType.PracticeSentence
                    },
                    new SentenceDbModel(){
                        English  = "Hurry up, John!",
                        Chinese = "快点！",
                        SentenceType = SentenceType.PracticeSentence
                    },
                    new SentenceDbModel(){
                        English  = "John: ",
                        Chinese = "",
                        SentenceType = SentenceType.UnPracticeSentence
                    },
                    new SentenceDbModel(){
                        English  = "No! The light is red. Stop!",
                        Chinese = "不行，现在是红灯，停！",
                        SentenceType = SentenceType.PracticeSentence
                    },
                },
                Words = new string[] { "you", "up", "No", "Stop" }
            };
            dbContext.TextCollection.InsertOne(text);

            var text2 = new TextDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                BookId = book.Id,
                Title = "Morning",
                Body = new SentenceDbModel[]{

                    new SentenceDbModel(){
                        English  = "Good morning class.",
                        Chinese = "同学们早上好",
                        SentenceType = SentenceType.PracticeSentence
                    },
                    new SentenceDbModel(){
                        English  = "Good morning Miss huang.",
                        Chinese = "黄老师早上好",
                        SentenceType = SentenceType.PracticeSentence
                    },
                },
                Words = new string[] { "Good", "Miss" }
            };
            dbContext.TextCollection.InsertOne(text2);
        }
        private static void InitializePublisher(FingerDbContext dbContext)
        {
            var filter = Builders<PublisherDbModel>.Filter.Empty;
            if (dbContext.PublisherCollection.CountDocuments(filter) > 0)
                return;
            var cid = ObjectId.GenerateNewId();
            dbContext.PublisherCollection.InsertOne(new PublisherDbModel()
            {
                Id = cid,
                Title = "人教版"
            });
            dbContext.PublisherCollection.InsertOne(new PublisherDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                Title = "北师大版"
            });

            var book = new BookDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                PublisherId = cid,
                Title = "The 5",
                Grade = Grade.Primary1,
                SchoolLevel = SchoolLevel.Primary
            };

            dbContext.BookCollection.InsertOne(book);
        }

        private static void InitializeUser(FingerDbContext dbContext)
        {
            var filter = Builders<UserDbModel>.Filter.Empty;
            if (dbContext.UserCollection.CountDocuments(filter) > 0)
                return;
            dbContext.UserCollection.Find(filter);
            var publisher = dbContext.PublisherCollection.AsQueryable().First();

            var user = new UserDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "黄新睿",
                UserName = "Stone",
                Sex = Sex.Male,
                School = "永康市实验学校",
                Birthday = new System.DateTime(2009, 08, 10),
                Grade = Grade.Primary6,
                PublisherId = publisher.Id
            };
            user.PracticeSum = new UserPracticeSumDbModel()
            {
                Minutes = 20,
                WordCount = 332,
                PracticeCount = 3,
                LastPracticeCorrectRate = 0.87
            };

            var p = new PracticeDbModel() { Id = ObjectId.GenerateNewId(), UserId = user.Id, Time = DateTime.Now, CorrectRate = 70, Mins = 28, WordCount = 30 };
            var w = new PracticeWordDbModel() { Word = "Chinea", Chinese = "中国", ErrorCount = 3 };
            p.Words.Add(w);
            dbContext.PracticeCollection.InsertOne(p);
            dbContext.UserCollection.InsertOne(user);
        }

        private static void AddUser(FingerDbContext dbContext)
        {
            var user = new UserDbModel()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "黄海",
                UserName = "botom",
                Sex = Sex.Male,
                School = "永康市实验学校",
                Birthday = new System.DateTime(2009, 08, 10),
                Grade = Grade.Primary6,

            };
            var p = new PracticeDbModel() { Id = ObjectId.GenerateNewId(), UserId = user.Id, Time = DateTime.Now, CorrectRate = 70, Mins = 28, WordCount = 30 };
            var w = new PracticeWordDbModel() { Word = "Chinea", Chinese = "中国", ErrorCount = 3 };
            p.Words.Add(w);
            dbContext.PracticeCollection.InsertOne(p);
            user.PracticeSum = new UserPracticeSumDbModel()
            {
                Minutes = 20,
                WordCount = 332,
                PracticeCount = 3,
                LastPracticeCorrectRate = 0.87
            };
            dbContext.UserCollection.InsertOne(user);
        }
        private static void UpdateUser(FingerDbContext dbContext)
        {
            var p = new PracticeDbModel() { Id = ObjectId.GenerateNewId(), Time = DateTime.Now, CorrectRate = 78, Mins = 21, WordCount = 330 };
            p.Words.Add(new PracticeWordDbModel() { Word = "Chinea", Chinese = "中国", ErrorCount = 3 });
            p.Words.Add(new PracticeWordDbModel() { Word = "English", Chinese = "英语", ErrorCount = 3 });
            p.Words.Add(new PracticeWordDbModel() { Word = "Room", Chinese = "房间", ErrorCount = 3 });
            var filter = Builders<UserDbModel>.Filter.Where(a => a.UserName == "Stone");
            //var update = Builders<UserDbModel>.Update.Push<PracticeDbModel>(x => x.Practices, p);
            //var result = dbContext.UserCollection.UpdateOne(filter, update);

        }
    }
}