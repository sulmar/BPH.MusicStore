using BPH.MusicStore.DAL;
using BPH.MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BPH.MusicStore.ConsoleClient.Properties;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;

namespace BPH.MusicStore.ConsoleClient
{
    class Program
    {


        static void Main(string[] args)
        {

            ObjectContextTest();

           // LazyLoadingTest();

            Optymalization1Test();


            ConcurencyRowVersionTest();

            // ConcurencyTest();


            // NativeTransactionTest();


            // DistributedTransactioTest();

            // ExecuteSQLGetResultsTest();

            // ExecuteSQLTest();

            // ExecuteStoredProcedureTest();

            // UpdateAttachTest();

            // RemoveArtistTest();

            // UpdateArtistTest();

            // GetCountArtist();

            // GetInTest();

            //   GetArtistTest();


            // CheckDatabaseTest();

            //ModelChangedTest();

            //AddAlbumWithExistingArtistTest();

            // DropDatabase();

            // AddAlbumTest();

            // AddArtistTest();

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();

        }

        /// <summary>
        /// Sposób na dostanie się do ObjectContext i przykład wykorzystania do generowania dokumentacji
        /// </summary>
        private static void ObjectContextTest()
        {
            using (var context = new MusicStoreContext())
            {

                ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

                var workspace = objectContext.MetadataWorkspace;

                var tables = workspace.GetItems<EntityType>(DataSpace.OCSpace);

                foreach (var item in tables)
                {
                    Console.WriteLine(item.BaseType);
                }

            }
        }


        private static void LazyLoadingTest()
        {
            using (var context = new MusicStoreContext())
            {
                var artists = context.Artists;


                foreach (var artist in artists)
                {
                    Console.WriteLine(artist);

                    foreach(var album in artist.Albums.ToList())
                    {
                        Console.WriteLine(album.Title);
                    }
                }



            }
        }

        private static void Optymalization1Test()
        {
            using (var context = new MusicStoreContext())
            {
                var albums = context.Albums
                    .Where(a => a.AlbumType == AlbumType.LP)
                    .AsNoTracking()
                    .ToList();


                albums.First().AlbumType = AlbumType.SP;
                context.SaveChanges();


            }
        }

        private static void ConcurencyRowVersionTest()
        {
            using (var context = new MusicStoreContext())
            {


                var artist = context.Artists.Find(1);

                var album = context.Albums
                    .FirstOrDefault(a => a.Title == "EF Core 1.0");

                album.Title = "Concurency is the best!";


                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Ktos zmodyfikował już ten rekord");
                }
            }
        }

        private static void ConcurencyTest()
        {
            using (var context = new MusicStoreContext())
            {
                var artist = context.Artists.FirstOrDefault(a => a.FirstName == "Kasia");

                artist.FirstName = "Bartek";



                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Ktos zmodyfikował już ten rekord");

                    e.Entries.Single().Reload();
                }
              
            }
        }

        private static void NativeTransactionTest()
        {
            var album = new Album { Title = "Album 3" };
            var artist = new Artist { FirstName = "Marcin", LastName = "Sulecki" };

            using (var context = new MusicStoreContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Artists.Add(artist);

                        context.SaveChanges();

                        context.Albums.Add(album);

                        context.SaveChanges();

                        transaction.Commit();

                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                    }

                }

            }
        }

        private static void DistributedTransactioTest()
        {
            var album1 = new Album { Title = "Album 1" };
            var album2 = new Album { Title = "Album 2" };
            var artist = new Artist { FirstName = "Marcin", LastName = "Sulecki" };

            using (var context1 = new MusicStoreContext())
            using (var context2 = new MusicStoreContext())
            {
                using (var scope = new TransactionScope())
                {
                    context1.Albums.Add(album1);

                    context1.SaveChanges();

                    context2.Artists.Add(artist);

                    context2.SaveChanges();

                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// Uruchomienie zapytania SQL bez pobierania wyników
        /// </summary>
        private static void ExecuteSQLTest()
        {
            using (var context = new MusicStoreContext())
            {
                var albumIdParameter = new SqlParameter("@AlbumId", 1);
                context.Database.ExecuteSqlCommand("select * from albums where albumId=@AlbumId", albumIdParameter);
            }
        }

        /// <summary>
        /// Uruchomienie zapytania SQL i pobieranie wyników 
        /// </summary>
        private static void ExecuteSQLGetResultsTest()
        {

            using (var context = new MusicStoreContext())
            {

                var albums = context.Database.SqlQuery<Album>(Resources.GetAlbums)
                    .ToList();

            }
        }

        /// <summary>
        /// Uruchomienie procedury składowanej i pobieranie wyników 
        /// </summary>
        private static void ExecuteStoredProcedureTest()
        {
            using (var context = new MusicStoreContext())
            {
                var cityParameter = new SqlParameter("@City", "Gdańsk");

                cityParameter.Direction = System.Data.ParameterDirection.Input;

                var albums = context.Database.SqlQuery<Album>("dbo.uspGetAlbumsByCity @City", cityParameter)
                    .ToList();

            }
        }

        private static void UpdateAttachTest()
        {
            var artist = new Artist
            {
                ArtistId = 100,
                FirstName = "Michael",
                LastName = "Jackson",
                Birthday = DateTime.Parse("1950-01-01"),
            };

            using (var context = new MusicStoreContext())
            {
                context.Artists.Attach(artist);

                context.Entry<Artist>(artist).State = EntityState.Modified;

                context.SaveChanges();

            }
        }

        private static void UpdateArtistTest()
        {
            using(var context = new MusicStoreContext())
            {
                var artist = context.Artists
                    .SingleOrDefault(a=>a.FirstName == "Michael");

                Console.WriteLine(artist);


                var entityState = context.Entry<Artist>(artist).State;


                artist.FirstName = "George";

                var entityState2 = context.Entry<Artist>(artist).State;

                artist.FirstName = "Michael";

                var entityState3 = context.Entry<Artist>(artist).State;


                context.Entry<Artist>(artist).State = EntityState.Deleted;

                context.SaveChanges();


            }
        }

        private static void RemoveArtistTest()
        {
            using (var context = new MusicStoreContext())
            {
                var artist = context.Artists
                    .Include(a=>a.Albums)
                    .SingleOrDefault(a => a.FirstName == "George");

                Console.WriteLine(artist);

                context.Albums.RemoveRange(artist.Albums);

                context.Artists.Remove(artist);

                context.SaveChanges();


            }
        }

        private static void GetInTest()
        {
            using (var context = new MusicStoreContext())
            {

                var ids = new List<int> { 1, 2, 3, 5 };

                var artists = context.Artists
                    .Select(a=>a.ArtistId)
                    .Intersect(ids)
                    .ToList();

            }
                   
        }


        private static void GetCountArtist()
        {
            using (var context = new MusicStoreContext())
            {
                var  count = context.Artists
                    .Where(artist => artist.Birthday.Year > 1965)
                    .Count();


                var foundArtist = context.Artists
                    .OrderBy(a => a.FirstName)
                    .ThenBy(a => a.LastName)
                    .ThenByDescending(a => a.Birthday)
                    .Where(a=>a.FirstName == "Sandra")
                    .ToList();



            }
        }

        private static void GetArtistTest()
        {
            using (var context = new MusicStoreContext())
            {
                var artists = context.Artists
                    .Where(artist => artist.Birthday.Year > 1965);

                artists = artists
                    .Where(artist => artist.ArtistId > 1);

                artists = artists
                    .OrderBy(artist => artist.LastName);


                var results = artists
                    .ToList();   
            }
        }

        private static void CheckDatabaseTest()
        {
            using (var context = new MusicStoreContext())
            {
                if (!context.Database.Exists())
                {
                    Console.WriteLine("Brak bazy danych.");

                }
            }
        }



        private static void ModelChangedTest()
        {
            using (var context = new MusicStoreContext())
            {
                if (!context.Database.CompatibleWithModel(false))
                {
                    Console.WriteLine("Zmienił się model bazy danych.");

                }
            }
        }

        private static void UsingTest()
        {
            using (var album = new Album { Title = "Dispose!" })
            {
                throw new ApplicationException("Test");
            }
        }

        private static void AddAlbumWithExistingArtistTest()
        {
            var artistId = 1;

            using (var context = new MusicStoreContext())
            {

                var artist = context.Artists.Find(artistId);

                var album = new Album
                {
                    Title = "EF Core 1.0",
                    AlbumType = AlbumType.SP,
                    Artist = artist,
                    PublishedDate = DateTime.Parse("2016-01-01"),
                };

                context.Albums.Add(album);

                context.SaveChanges();
            }

            // context.Albums.Add(album);


        }


        private static void AddAlbumTest()
        {
            var album = new Album
            {
                AlbumType = AlbumType.LP,
                Title = "Bad",
                PublishedDate = DateTime.Parse("2016-01-01"),
                Artist = new Artist
                {
                    FirstName = "Michael",
                    LastName ="Jackson",
                }
            };

            var context = new MusicStoreContext();
            context.Albums.Add(album);
            context.SaveChanges();

            Console.WriteLine($"Dodano album o identyfikatorze: {album.AlbumId}");

        }


        private static void DropDatabase()
        {
            var context = new MusicStoreContext();

            Console.WriteLine("Usuwanie bazy danych...");

            context.Database.Delete();
        }

        private static void AddArtistTest()
        {
            var artist = new Artist
            {
                FirstName = "Marcin",
                LastName = "Sulecki",
            };

            var context = new MusicStoreContext();


            Console.WriteLine(context.Database.Connection.ConnectionString);

            context.Artists.Add(artist);

            context.SaveChanges();

            Console.WriteLine(artist.FullName);
        }
    }
}
