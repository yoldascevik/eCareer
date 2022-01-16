namespace Definition.HttpClient;

public class ApiEndpointOptions
{
    public ApiEndpointOptions()
    {
        DefaultVersion = "1.0";
    }
        
    public ApiEndpointOptions(string apiUrl, string defaultVersion)
        : this()
    {
        ApiUrl = apiUrl;
        DefaultVersion = defaultVersion;
    }

    public string ApiUrl { get; set; }
    public string DefaultVersion { get; set; }
}