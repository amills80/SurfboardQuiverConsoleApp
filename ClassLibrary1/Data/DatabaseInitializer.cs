using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SurfboardQuiverConsoleApp.Models;

namespace SurfboardQuiverConsoleApp
{
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            //context.Surfboards.Add(new Surfboard()
            //{
            //    Builder = new Builder()
            //    {
            //        Name = "Bird",
            //        //Id = 1
            //    },
            //    Model = "Flat T",
            //    Style = new BoardStyle()
            //    {
            //        Name = "Longboard",
            //        Id = 1
            //    },
            //    Length = 9.0f,
            //    Width = 23.0f,
            //    MaxFins = 3,
            //    //Rating = 4.0f,
            //    Notes = "Rides on almost any Florida beach break"
            //});
            context.Surfboards.Add(new Surfboard()
            {
                Builder = new Builder()
                {
                    Name = "Ken White",
                    //Id = 2
                },
                Model = "Classic T",
                Style = new BoardStyle()
                {
                    Name = "Longboard",
                    Id = 1
                },
                Length = 10.1f,
                Width = 22.5f,
                MaxFins = 1,
                //Rating = 4.0f,
                Notes = "Smooth Loggin'"
            });
            context.Surfboards.Add(new Surfboard()
            {
                Builder = new Builder()
                {
                    Name = "McTavish",
                    //Id = 3
                },
                Model = "Tsumo",
                Style = new BoardStyle()
                {
                    Name = "Midlength",
                    //Id = 2
                },
                Length = 7.2f,
                Width = 21f,
                MaxFins = 3,
                Notes = "Good for when it's good"
            });
            context.Surfboards.Add(new Surfboard()
            {
                Builder = new Builder()
                {
                    Name = "Clean Ocean Sports",
                    //Id = 3
                },
                Model = "Super Fish",
                Style = new BoardStyle()
                {
                    Name = "Fish",
                    //Id = 2
                },
                Length = 7.2f,
                Width = 21.75f,
                MaxFins = 2,
                Notes = "Needs a cleaner face, but fun."
            });
            context.Surfboards.Add(new Surfboard()
            {
                Builder = new Builder()
                {
                    Name = "Penninsula Holding Co.",
                    //Id = 3
                },
                Model = "Polaroid",
                Style = new BoardStyle()
                {
                    Name = "Funboard",
                    //Id = 2
                },
                Length = 8.0f,
                Width = 22f,
                MaxFins = 1,
                Notes = "Need to ride it more"
            });
            context.Surfboards.Add(new Surfboard()
            {
                Builder = new Builder()
                {
                    Name = "Channin",
                    //Id = 3
                },
                Model = "Retro Single",
                Style = new BoardStyle()
                {
                    Name = "Malibu",
                    //Id = 2
                },
                Length = 6.9f,
                Width = 22f,
                MaxFins = 1,
                Notes = "Need to ride it more"
            });

            //context.SaveChanges();

        }

    }
}
