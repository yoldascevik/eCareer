using System;

namespace Definition.Contract.Dto
{
    [Serializable]
    public class LanguageDto 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Culture { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
    }
}