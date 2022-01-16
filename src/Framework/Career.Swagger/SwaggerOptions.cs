namespace Career.Swagger;

public class SwaggerOptions
{
    public string JsonRoute { get; set; }
    public string Title { get; set; }
    public string Version { get; set; }
    public string RoutePrefix { get; set; }
    public bool IncludeXmlComments { get; set; }
    public string Description { get; set; }
    public string UIEndpoint { get; set; }
}