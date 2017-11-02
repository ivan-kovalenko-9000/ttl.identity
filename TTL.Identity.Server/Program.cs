using IdentityModel;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTL.Identity.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("https://localhost:44333"))
            {
                Console.ReadKey();
            }
        }
    }
}
