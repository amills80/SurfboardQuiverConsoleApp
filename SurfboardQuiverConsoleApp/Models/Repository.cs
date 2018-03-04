using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfboardQuiverConsoleApp.Models
{
    class Repository
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

        public static void AddSurfboard(Surfboard surfboard)
        {
            using (Context context = GetContext())
            {
                context.Surfboards.Add(surfboard);
                if(surfboard.Builder != null && surfboard.Builder.Id > 0)
                {
                    context.Entry(surfboard.Builder).State = EntityState.Unchanged;
                }
                if(surfboard.Style != null && surfboard.Style.Id > 0)
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

        internal static void AddBoardStyle(BoardStyle shapeInput)
        {
            throw new NotImplementedException();
        }
    }
}