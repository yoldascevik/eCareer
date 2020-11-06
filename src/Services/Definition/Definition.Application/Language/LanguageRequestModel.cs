using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Language
{
    public class LanguageRequestModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string NativeName { get; set; }
        
        [Required]
        public string Culture { get; set; }
        
        [Required]
        public string ParentCulture { get; set; }
        
        [Required]
        public string TwoLetterISOLanguageName { get; set; }
        
        [Required]
        public string ThreeLetterISOLanguageName { get; set; }
    }
}