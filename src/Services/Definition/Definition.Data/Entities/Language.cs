namespace Definition.Data.Entities;

public class Language : LookupDocument
{
    public string Name { get; set; }
    public string NativeName { get; set; }
    public string Culture { get; set; }
    public string ParentCulture { get; set; }
    public string TwoLetterISOLanguageName { get; set; }
    public string ThreeLetterISOLanguageName { get; set; }
}