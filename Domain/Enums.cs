

using System.ComponentModel;

public enum Grade
{
    [DescriptionAttribute("小学一年级")]
    Primary1 = 1,
    [DescriptionAttribute("小学二年级")]
    Primary2 = 2,
    [DescriptionAttribute("小学三年级")]
    Primary3 = 3,
    [DescriptionAttribute("小学四年级")]
    Primary4 = 4,
    [DescriptionAttribute("小学五年级")]
    Primary5 = 5,
    [DescriptionAttribute("小学六年级")]
    Primary6 = 6,

    [DescriptionAttribute("初一")]
    Middle1 = 7,
    [DescriptionAttribute("初二")]
    Middle2 = 8,
    [DescriptionAttribute("初三")]
    Middle3 = 9,
    [DescriptionAttribute("高一")]
    High1 = 10,
    [DescriptionAttribute("高二")]
    High2 = 11,
    [DescriptionAttribute("高三")]
    High3 = 12,
    [DescriptionAttribute("成人")]
    Adult = 20

}

public enum SchoolLevel
{
    [DescriptionAttribute("小学")]
    Primary = 1,
    [DescriptionAttribute("初中")]
    Middle = 2,
    [DescriptionAttribute("高中")]
    High = 3,
    [DescriptionAttribute("成人")]
    Adult = 4
}

public enum Sex
{
    [DescriptionAttribute("男")]
    Male = 0,
    [DescriptionAttribute("女")]
    Female = 1
}

public enum SentenceType{
    PracticeSentence,
    UnPracticeSentence
}
