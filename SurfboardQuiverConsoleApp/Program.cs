using SurfboardQuiverConsoleApp.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SurfboardQuiverConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                //context.Database.Log = (message) => Debug.WriteLine(message);

                //var surfboards = context.Surfboards
                //    .Include(sb => sb.Builder)
                //    .OrderByDescending(sb => sb.Length).ToList();

                //Console.WriteLine("# of boards: {0}", surfboards.Count);
                
                //var longboards = context.Surfboards
                //    .Include(sb => sb.Style)
                //    .Where(sb => sb.Style.Name.ToLower()=="longboard")
                //    .ToList();
                
                //Console.WriteLine("# of longboards: {0}", longboards.Count);

                //foreach (var surfboard in surfboards)
                //{
                //    Console.WriteLine(surfboard.DisplayText);
                //}
            }
        }
        
    }
}
