namespace csharp_ui.Helpers
{
    public static class GoApiConfig
    {
        public static string ApiBaseUrl
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("GO_API_BASE_URL");
                return string.IsNullOrEmpty(url) ? "http://localhost:8080" : url;
            }
        }
    }
    public static class CSharpApiConfig
    {
        public static string ApiBaseUrl
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("CSHARP_API_BASE_URL");
                return string.IsNullOrEmpty(url) ? "http://localhost:8080" : url;
            }
        }
    }
}