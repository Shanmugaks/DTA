using System;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Collections.Generic;

namespace NumberService.OwinSelfHost
{
    /// <summary>
    /// Description for Main class
    /// </summary>
    public class main
    {
        /// <summary>
        /// Description for main method
        /// </summary>
        static void Main(string[] args)
        {
            const string serverHostAddress = "http://localhost:1234/";

            using (WebApp.Start<Startup>(url: serverHostAddress))
            {
                Console.WriteLine("Light Weight Self hosted Server( NumberService) - Web API running  @ " + serverHostAddress);
                while (true) ;
            }
        }
    }
}