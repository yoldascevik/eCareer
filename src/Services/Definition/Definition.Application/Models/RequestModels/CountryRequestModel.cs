using System.ComponentModel.DataAnnotations;

namespace Definition.Application.Models.RequestModels
{
    public class CountryRequestModel
    {
        [Required] 
        public string Name { get; set; }
        [Required] 
        public string Iso2 { get; set; }
        [Required] 
        public string Iso3 { get; set; }
        [Required] 
        public string PhoneCode { get; set; }
        [Required]
        public string Capital { get; set; }
        [Required] 
        public string Currency { get; set; }
        [Required]
        public string Native { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
    }
}