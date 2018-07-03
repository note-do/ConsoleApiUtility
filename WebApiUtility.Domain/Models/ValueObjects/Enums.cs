using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUtility.Domain.Models.ValueObjects
{
    public enum SearchOperatorType
    {
        Equals = 1,
        NotEquals = 2,
        Greater = 3,
        Less = 4,
        GreaterOrEqual = 5,
        LessOrEqual = 6,
        Exists = 7,
        NotExists = 8,
        StartsWith = 9,
        Contains = 10,
        NotContains = 11,
        ContainsObject = 12,
        NotContainsObject = 13,
        ContainsWord = 14,
        NotContainsWord = 15
    }
    public enum SearchConditionType
    {
        Attribute = 1,
        Name = 2,
        Level = 3,
        CreatedBy = 6,
        CreationDate = 7,
        ModificationDate = 10,
        ElementLink = 11,
        Descendants = 8,
        Ancestors = 9,
        Id = 15
    }
    public enum FilterTypes
    {
        RootObject = 4,
        Class = 5
    }

    public static class Dictionaries
    {
      public static  Dictionary<string, string> attributes = new Dictionary<string, string>
        {
            {"лимит лицензий","F30DC169-B976-E711-80C7-00155D086902"},
            {"дата возврата","2C8CFFC2-5394-E611-80C1-00155D086902"},
            {"дата выдачи","B9EFEAAF-5394-E611-80C1-00155D086902"},
            {"причина","E2FBBCCA-5394-E611-80C1-00155D086902"},
            {"комментарий","B0055C11-4AD6-E611-80C7-00155D086902"},
            {"сотрудник","3E151241-5394-E611-80C1-00155D086902"},
            {"только сим карта","20270842-5494-E611-80C1-00155D086902"},
            {"рабочие места","E0278685-7790-E611-80C1-00155D086902"},
            {"блокировка телефона","47870780-294D-E711-80C7-00155D086902"},
            {"модель телефона","89198CA4-5494-E611-80C1-00155D086902"},
            {"номер телефона","ABAD80D5-5294-E611-80C1-00155D086902"},
            {"события выдача телефона","F27A05F7-5394-E611-80C1-00155D086902"},
            {"владелец","033ADAF4-20B6-E611-80C1-00155D086902"},
            {"модель монитора","24CE9CEA-1491-E611-80C1-00155D086902"},
            {"серийный номер монитора","51E0DE04-7790-E611-80C1-00155D086902"},
            {"autodesk","CDEC7459-C6C2-E611-80C7-00155D086902"},
            {"office","0D9B00FB-5AC7-E611-80C7-00155D086902"},
            {"ram","F3710F42-37AA-E611-80C1-00155D086902"},
            {"visual Studio","E03076A0-031A-E711-80C7-00155D086902"},
            {"видеоконтроллер","F6E7CC58-16B2-E611-80C1-00155D086902"},
            {"наклейка лицензии Win","1EBF18AB-7186-E711-80C9-00155D086902"},
            {"операционная система","E788C42B-4BC7-E611-80C7-00155D086902"},
            {"кабинет","FFE750C5-7790-E611-80C1-00155D086902"},
            {"удаленный доступ","B060E0DD-6DE4-E611-80C7-00155D086902"},
            {"фио","3076C82D-5394-E611-80C1-00155D086902"}
        };

        public static Dictionary<string, string> systemObjects = new Dictionary<string, string>
        {
            {"autodesk","DA86ACEE-BC76-E711-80C7-00155D086902"},
            {"office","3C52D2D5-5AC7-E611-80C7-00155D086902"},
            {"выдача оборудования","8E4E0196-5394-E611-80C1-00155D086902"},
            {"выдача телефона","E4FEE1E8-5294-E611-80C1-00155D086902"},
            {"кабинет","CE0D5F74-7790-E611-80C1-00155D086902"},
            {"мобильный телефон","F48956BE-5294-E611-80C1-00155D086902"},
            {"монитор","2DABFBE9-7690-E611-80C1-00155D086902"},
            {"системный блок","D7C708A9-31AA-E611-80C1-00155D086902"},
            {"сотрудник","2CB82F21-5394-E611-80C1-00155D086902"}
        };
    }
}
