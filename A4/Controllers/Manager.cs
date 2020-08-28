using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;
using System.Security.Claims;


namespace Assignment4.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                //artist
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistAddFormViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();

                //album
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumBaseViewModel, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                //genre
                cfg.CreateMap<Genre, GenreBaseViewModel>();

                //track
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();



                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        //manager methods

        //Genre
        // 1. GenreGetAlll() 
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(e => e.Name));
        }

        //Artist
        //2. ArtistGetAll()
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(e => e.Name));
        }

        //3. Artist - get one (with detail)
        public ArtistWithDetailViewModel ArtistGetById(int id)
        {
            var obj = ds.Artists.Include("Albums").SingleOrDefault(t => t.Id == id);
            return obj == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(obj);
        }

        //4. Artist Add
        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            // Attempt to add the new item
            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));
            //configure the executive 
            addedItem.Executive = User.Name;

            ds.SaveChanges();

            return mapper.Map<Artist, ArtistBaseViewModel>(addedItem);

        }


        //Album
        //5. AlbumGetAll()
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(e => e.Name));
        }

        //6. Album - Get one (with detail)
        public AlbumWithDetailViewModel AlbumGetById(int id)
        {
            var obj = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(t => t.Id == id);
            //var obj = ds.Albums.Include("Tracks").Include("Artists").SingleOrDefault(t => t.Id == id);
            return obj == null ? null : mapper.Map<Album, AlbumWithDetailViewModel>(obj);
        }

        //7. Album Add
        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            //artists
            var myList = new List<Artist>(); //empty artist list

            foreach (var item in newItem.ArtistIds)
            {
                var a = ds.Artists.Find(item);
                if(a != null)
                {
                    myList.Add(a);
                }
                else
                {
                    return null;
                }
            }

            //tracks
            var myTracks = new List<Track>(); //empty track list

            foreach (var item in newItem.TrackIds)
            {
                var b = ds.Tracks.Find(item);
                if (b != null)
                {
                    myTracks.Add(b);
                }
                else
                {
                    return null;
                }
            }

            //attempt to add the new album
            var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));

            //add artists to the album's artists collection
            foreach (var item in newItem.ArtistIds)
            {
                var a = ds.Artists.Find(item);

                addedItem.Artists.Add(a);
                a.Albums.Add(addedItem);
            }

            //add tracks to the album's tracks collection
            foreach (var item in newItem.TrackIds)
            {
                var t = ds.Tracks.Find(item);

                addedItem.Tracks.Add(t);
                t.Albums.Add(addedItem);
            }
            //set the coordinator
            addedItem.Coordinator = User.Name;

            addedItem.Artists = myList;
            addedItem.Tracks = myTracks;


            //save changes
            ds.SaveChanges();

            //return the album
           return mapper.Map<Album, AlbumWithDetailViewModel>(addedItem);
        } 

        
        //Track
        //8. TrackGetAll()
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(e => e.Name));
        }

        //9. Track - Get One
        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var obj = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.Id == id);

            if (obj == null)
            {
                return null;
            }
            else
            {
                 
                var result = mapper.Map<Track, TrackWithDetailViewModel>(obj);
                result.AlbumNames = obj.Albums.Select(a => a.Name);
                return result;

            }
        }

        //10. Track - Add New

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newItem)
        {
            var album = ds.Albums.Find(newItem.AlbumId);
            
            if (album == null )
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));

                //configure the clerk
                addedTrack.Clerk = User.Name;

                // Set the associated item property
                addedTrack.Albums.Add(album);

                ds.SaveChanges();

                return (addedTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
            }
        }

        //new method - tracks for a specific artist

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var o = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);

            if(o == null) { return null; }

            var c = new List<Track>();

            foreach (var album in o.Albums)
            {
                c.AddRange(album.Tracks);
            }
            c = c.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>
                (c.OrderBy(e => e.Name));

        }


        //--------------------------------------------------------------------------------------//

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method


        //newly added load methods - assignment 4


        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here - assignment 4
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }
        //loadGenre()
        public bool LoadGenre()
        {
            if (ds.Genres.Count() == 0)
            {
                // Add new genres
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Instrumental" });
                ds.Genres.Add(new Genre { Name = "Hip Hop" });
                ds.Genres.Add(new Genre { Name = "International" });
                ds.Genres.Add(new Genre { Name = "Romance" });
                ds.Genres.Add(new Genre { Name = "Dance" });
                ds.Genres.Add(new Genre { Name = "Soul Music" });
                ds.Genres.Add(new Genre { Name = "Rap" });
                ds.Genres.Add(new Genre { Name = "Disco" });

                ds.SaveChanges();
                return true;
            }

            return false;
        }

        //loadArtists()
        public bool LoadArtist()
        {
            if (ds.Artists.Count() == 0)
            {
                // Add new artists
                ds.Artists.Add(new Artist
                {
                    Name = "Camilla Cabello",
                    BirthName = "Karla",
                    BirthOrStartDate = new DateTime(1997, 03, 03),
                    Executive = "exec@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/1/14/Camila_Cabello_AMAs_2019.png"

                });

                ds.Artists.Add(new Artist
                {
                    Name = "Ed Sheeran",
                    BirthName = "Angelo Mysterioso",
                    BirthOrStartDate = new DateTime(1991, 02, 17),
                    Executive = "exec@example.com",
                    Genre = "Romance",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/91/Eddyyy.jpg"

                });

                ds.Artists.Add(new Artist
                {
                    Name = "Jason Derulo",
                    BirthName = "Jason Joel Desrouleaux",
                    BirthOrStartDate = new DateTime(1989, 09, 21),
                    Executive = "exec@example.com",
                    Genre = "Hip Hop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/c/c3/Jddddddd.jpg"

                });

                ds.SaveChanges();
                return true;
            }

            return false;
        }

        //LoadAlbum()
        public bool LoadAlbum()
        {
            if (ds.Albums.Count() == 0)
            {
                //add new albums

                //camilla cabello - 2 albums
                var camilla = ds.Artists.SingleOrDefault(a => a.Name == "Camilla Cabello");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { camilla },
                    Name = "Camilla",
                    ReleaseDate = new DateTime(2018, 01, 12),
                    Coordinator = "coord@example.com",
                    Genre = "Dance",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Blahblah.jpg/600px-Blahblah.jpg"

                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { camilla },
                    Name = "Romance",
                    ReleaseDate = new DateTime(2019, 12, 06),
                    Coordinator = "coord@example.com",
                    Genre = "Romance",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/9/92/Senoritaaa.jpg"
                });

                //ed sheeran - 2 albums
                var ed = ds.Artists.SingleOrDefault(a => a.Name == "Ed Sheeran");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { ed },
                    Name = "Divide",
                    ReleaseDate = new DateTime(2017, 03, 03),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/3/33/Againblahblah.png"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { ed },
                    Name = "No.6 Collaborations Project",
                    ReleaseDate = new DateTime(2019, 07, 12),
                    Coordinator = "coord@example.com",
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/9/91/No.6_collaborate.png"
                });

                //jason derulo - 2 albums
                var jason = ds.Artists.SingleOrDefault(a => a.Name == "Jason Derulo");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { jason },
                    Name = "Everything Is 4",
                    ReleaseDate = new DateTime(2015, 05, 29),
                    Coordinator = "coord@example.com",
                    Genre = "Dance",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/5/52/Jd%27s_album.png"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { jason },
                    Name = "Tattoos",
                    ReleaseDate = new DateTime(2013, 09, 20),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/commons/2/21/Tatoosssss.jpg"
                });


                ds.SaveChanges();
                return true;
            }

            return false;
        }

        //LoadTrack()
        public bool LoadTrack()
        {
            if (ds.Tracks.Count() == 0)
            {
                // 8  tracks for camilla cabello
                var cabello1 = ds.Albums.SingleOrDefault(a => a.Name == "Camilla");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello1 },
                    Name = "Havana",
                    Composers = "Young Thug",
                    Genre = "Latin pop",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello1 },
                    Name = "Never be the same",
                    Composers = "Jacob",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello1 },
                    Name = "Real friends",
                    Composers = "Darren",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello1 },
                    Name = "Consquences",
                    Composers = "Tom",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });


                var cabello2 = ds.Albums.SingleOrDefault(a => a.Name == "Romance");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello2 },
                    Name = "Senorita",
                    Composers = "Shawn Mendes",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello2 },
                    Name = "Shameless",
                    Composers = "Taylor",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello2 },
                    Name = "Liar",
                    Composers = "Slim Jim",
                    Genre = "Rock",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { cabello2 },
                    Name = "Easy",
                    Composers = "Snoop Dogg",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"

                });


                // 8 tracks for ed sheeran
                var ed1 = ds.Albums.SingleOrDefault(a => a.Name == "Divide");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed1 },
                    Name = "Perfect",
                    Composers = "Mike Ross",
                    Genre = "Romance",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed1 },
                    Name = "Shape of you",
                    Composers = "Cardi B",
                    Genre = "Rock",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed1 },
                    Name = "Beautiful people",
                    Composers = "Shiran",
                    Genre = "Soul Music",
                    Clerk = "clerk@example.com"

                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed1 },
                    Name = "I don't care",
                    Composers = "Justin Bieber",
                    Genre = "Dance",
                    Clerk = "clerk@example.com"
                });

                var ed2 = ds.Albums.SingleOrDefault(a => a.Name == "No.6 Collaborations Project");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed2 },
                    Name = "Happier",
                    Composers = "Marshmello",
                    Genre = "Soul Music",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed2 },
                    Name = "Feels",
                    Composers = "J Hus",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed2 },
                    Name = "Antisocial",
                    Composers = "Akon",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { ed2 },
                    Name = "Castle on hill",
                    Composers = "Adam Levine",
                    Genre = "Dance",
                    Clerk = "clerk@example.com"
                });

                // 8 tracks for jason derulo
                var jason1 = ds.Albums.SingleOrDefault(a => a.Name == "Everything Is 4");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason1 },
                    Name = "Broke",
                    Composers = "Keith Urban",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason1 },
                    Name = "The other side",
                    Composers = "David Guetta",
                    Genre = "Dance",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason1 },
                    Name = "Ridin solo",
                    Composers = "Iyaz",
                    Genre = "Rock",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason1 },
                    Name = "Try me",
                    Composers = "Jennifer",
                    Genre = "Romance",
                    Clerk = "clerk@example.com"
                });

                var jason2 = ds.Albums.SingleOrDefault(a => a.Name == "Tattoos");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason2 },
                    Name = "Fire",
                    Composers = "Pitbull",
                    Genre = "Hip Hop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason2 },
                    Name = "In my head",
                    Composers = "Daft Punk",
                    Genre = "Rock",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason2 },
                    Name = "Vertigo",
                    Composers = "Jordin",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { jason2 },
                    Name = "Side fx",
                    Composers = "A-Game",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.SaveChanges();
                return true;
            }

            return false;
        }


        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}