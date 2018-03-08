using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfboardQuiverConsoleApp.Models
{
    public class Repository
    {
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

        public static int GetBoardCount()
        {
            using (Context context = GetContext())
            {
                return context.Surfboards.Count();
            }
        }

        public static IList<Surfboard> GetBoards()
        {
            using (Context context = GetContext())
            {
                return context.Surfboards
                    .Include(sb => sb.Builder)
                    .Include(sb => sb.Style)
                    .OrderByDescending(sb => sb.Length).ToList();
            }
        }

        public static Surfboard GetBoard(int surfboardId)
        {
            using (Context context = GetContext())
            {
                return context.Surfboards
                    .Include(sb => sb.Builder)
                    .Include(sb => sb.Style)
                    .Where(sb => sb.Id == surfboardId)
                    .SingleOrDefault();
            }
        }

        public static IList<Builder> GetBuilders()
        {
            using (Context context = GetContext())
            {
                return context.Builders
                    .Include(sb => sb.Surfboards)
                    .OrderBy(sb => sb.Name)
                    .ToList();
            }
        }

        public static Builder GetBuilder(int builderId)
        {
            using (Context context = GetContext())
            {
                return context.Builders
                    .Where(b => b.Id == builderId)
                    .SingleOrDefault();
            }
        }

        public static int GetBuilderId(Builder builder)
        {
            using (Context context = GetContext())
            {
                var builders = GetBuilders();
                bool BuilderExists = builders.ToList()
                                    .Exists(b => b.Name.ToLower() == builder.Name.ToLower());
                var buildId = (BuilderExists) ?
                        (builders.FirstOrDefault(b => b.Name == builder.Name).Id) : 0;
                return buildId;
            }
        }

        public static IList<BoardStyle> GetBoardStyles()
        {
            using (Context context = GetContext())
            {
                return context.BoardStyles
                    .Include(s => s.Surfboards)
                    .OrderBy(s => s.Name)
                    .ToList();
            }
        }

        public static BoardStyle GetBoardStyle(int styleId)
        {
            using (Context context = GetContext())
            {
                return context.BoardStyles
                    .Where(s => s.Id == styleId)
                    .SingleOrDefault();
            }
        }
        
        public static int GetBoardStyleId(BoardStyle style)
        {
            using (Context context = GetContext())
            {
                var shapes = GetBoardStyles();
                bool ShapeExists = shapes.ToList()
                                    .Exists(b => b.Name.ToLower() == style.Name.ToLower());
                var styleId = (ShapeExists) ?
                        (shapes.FirstOrDefault(b => b.Name == style.Name).Id) : 0;
                return styleId;
            }
            //throw new NotImplementedException();
        }

        public static void AddSurfboard(Surfboard surfboard)
        {
            using (Context context = GetContext())
            {
                //IList<BoardStyle> shapes = GetBoardStyles();

                context.Surfboards.Add(surfboard);
                // TODO: add Builder duplication val in this if clause
                if (surfboard.Builder != null && surfboard.Builder.Id > 0)
                {
                    context.Entry(surfboard.Builder).State = EntityState.Unchanged;
                }
                // TODO: add Style duplication val in this if clause
                if (surfboard.Style != null && surfboard.Style.Id > 0)
                {
                    context.Entry(surfboard.Style).State = EntityState.Unchanged;
                }
                context.SaveChanges();
            }
        }

        public static void UpdateSurfboard(Surfboard surfboard)
        {
            using (Context context = GetContext())
            {
                context.Surfboards.Attach(surfboard);
                context.Entry(surfboard).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteSurfboard(int surfboardId)
        {
            using (Context context = GetContext())
            {
                var surfboard = new Surfboard() { Id = surfboardId };
                context.Entry(surfboard).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        
        //public static void AddBoardStyle(BoardStyle shapeInput)
        //{
        //    using (Context context = GetContext())
        //    {
        //        context.BoardStyles.Add(shapeInput);
        //        if (shapeInput.Name != null && shapeInput.Id > 0)
        //        {
        //            context.Entry(shapeInput.Name).State = EntityState.Unchanged;
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}