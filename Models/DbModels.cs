using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FingerEnglishWebServer.DatabaseModels
{

    public class TableNames
    {
        public const string Book = "Book";
        public const string Publisher = "Publisher";
        public const string User = "User";
        public const string Practice = "Practice";
        public const string Text = "Text";
        public const string Word = "Word";
    }


    public class BookDbModel
    {
        [BsonIgnore]
        public string IdString
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                this.Id = new ObjectId(value);
            }
        }
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }

        public ObjectId PublisherId { get; set; }
        public Grade Grade { get; set; }
        public SchoolLevel SchoolLevel { get; set; }
    }


    public class PublisherDbModel
    {

        [BsonIgnore]
        public string IdString
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                this.Id = new ObjectId(value);
            }
        }
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
    }

    public class TextDbModel
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public ObjectId BookId { get; set; }
        public SentenceDbModel[] Body { get; set; }
        public String[] Words { get; set; }
    }

    public class SentenceDbModel
    {
        public String English { get; set; }
        public String Chinese { get; set; }
        public SentenceType SentenceType { get; set; }
    }

    public class WordDbModel
    {
        public ObjectId Id { get;set; }
        public string English { get; set; }
        public string Chinese { get; set; }
        public string Discrption { get; set; }
        public WordPartDbModel[] Parts { get; set; }

    }
    public class WordPartDbModel
    {
        public string English { get; set; }
        public string Chinese { get; set; }
    }
    public class UserDbModel
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public Sex Sex { get; set; }
        public string School { get; set; }
        public DateTime Birthday { get; set; }
        public Grade Grade { get; set; }
        public ObjectId? PublisherId { get; set; }
        public UserPracticeSumDbModel PracticeSum { get; set; }

    }

    public class UserPracticeSumDbModel
    { 
        public int Minutes { get; set; }
        public int WordCount { get; set; }
        public int PracticeCount { get; set; }
        public double LastPracticeCorrectRate { get; set; }

    }

    public class PracticeDbModel
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public DateTime Time { get; set; }
        public double CorrectRate { get; set; }
        public int Mins { get; set; }
        public int WordCount { get; set; }
        public List<PracticeWordDbModel> Words { get; set; } = new List<PracticeWordDbModel>();

    }

    public class PracticeWordDbModel
    {
        public String Word { get; set; }
        public String Chinese { get; set; }
        public int ErrorCount { get; set; }
    }


}