using AutoMapper;
using FingerEnglishWebServer.DatabaseModels;
using MongoDB.Bson;

public class RemaperProfile : Profile
{
    public RemaperProfile()
    {
        CreateMap<BookDbModel, BookRpModel>();
        CreateMap<BookRpModel, BookDbModel>().ForMember(a => a.Id, opt => opt.MapFrom(c => new ObjectId(c.Id)));
        CreateMap<PublisherDbModel, PublisherRpModel>();
        CreateMap<UserDbModel, UserRpModel>();
        CreateMap<PracticeDbModel, PracticeRpModelLight>();
        CreateMap<PracticeDbModel, PracticeRpModelRich>();
        CreateMap<PracticeWordDbModel, PracticeWordRpModel>();
        CreateMap<TextDbModel, TextRpModel>();
        CreateMap<SentenceDbModel, SentenceRpModel>();
        CreateMap<WordDbModel, WordRpModel>();
        CreateMap<WordPartDbModel, WordPartRpModel>();

    }
}