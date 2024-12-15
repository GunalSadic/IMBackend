namespace BackendIM
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://localhost:5106", "http://192.168.1.107:5106")
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                });
    }
}