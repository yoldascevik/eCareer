namespace Definition.HttpClient
{
    public class ApiEndpointOptions
    {
        public ApiEndpointOptions()
        {
            Version = "1";
        }
        
        public ApiEndpointOptions(string apiUrl, string version)
            : this()
        {
            ApiUrl = apiUrl;
            Version = version;
        }

        public string ApiUrl { get; set; }
        public string Version { get; set; }
    }
}