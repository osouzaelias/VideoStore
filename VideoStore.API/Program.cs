using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VideoStore.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main( string[] args )
        {
            CreateHostBuilder( args ).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder( string[] args )
        {
            return Host.CreateDefaultBuilder( args )
                       .ConfigureWebHostDefaults( webBuilder => { webBuilder.UseStartup< Startup >(); } );
        }
    }
}