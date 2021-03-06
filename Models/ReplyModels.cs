

using System;
using System.Collections.Generic;

public class BookRpModel
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class PublisherRpModel
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class UserRpModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public Sex Sex { get; set; }
    public string School { get; set; }
    public DateTime Birthday { get; set; }
    public Grade Grade { get; set; }
    public string PublisherId { get; set; }
}

public class GradeRpModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class SexRpModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class SchoolLevelRpModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class PracticeRpModelLight
{
    public String Id { get; set; }
    public DateTime Time { get; set; }
    public double CorrectRate { get; set; }
    public int Mins { get; set; }
    public int WordCount { get; set; }
}
public class PracticeWordRpModel
{
    public String Word { get; set; }
    public String Chinese { get; set; }
    public int ErrorCount { get; set; }
}
public class PracticeRpModelRich : PracticeRpModelLight
{
    public List<PracticeWordRpModel> Words { get; set; }
}

public class TextRpModel{ 
        public String Id { get; set; }
        public string Title { get; set; }
        public SentenceRpModel[] Body{get;set;}
}



public class SentenceRpModel{ 
    public String English{get;set;}
    public string Chinese{get;set;}
    public SentenceType SentenceType   {get; set;} 
}

public class PracticeArticleRpModel{
    public String Id{get;set;}
    public string[] Words { get; set; }
 }

    public class WordRpModel
    { 
        public string English { get; set; }
        public string Chinese { get; set; }
        public string Discrption { get; set; }
        public WordPartRpModel[] Parts { get; set; } 
    }
 
    public class WordPartRpModel
    {
        public string English { get; set; }
        public string Chinese { get; set; }
    }