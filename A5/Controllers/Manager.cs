using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment5.Models;
using System.Security.Claims;

namespace Assignment5.Controllers
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

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<Artist, ArtistWithMediaInfoViewModel>();
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
                cfg.CreateMap<TrackWithDetailViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackViewModel>();
                cfg.CreateMap<TrackEditViewModel, Track>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackEditFormViewModel>();

                //mediatype
                cfg.CreateMap<MediaItem, MediaItemBaseViewModel>();
                cfg.CreateMap<MediaItem, MediaItemWithDetailViewModel>();
                cfg.CreateMap<MediaItemBaseViewModel, MediaItemAddFormViewModel>();
                cfg.CreateMap<MediaItemAddViewModel, MediaItem>();

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

        //ASSIGNMENT 5 Methods

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

        //3.ArtistAdd
        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            // Attempt to add the new item
            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));
            //configure the executive 
            addedItem.Executive = User.Name;

            ds.SaveChanges();

            return mapper.Map<Artist, ArtistBaseViewModel>(addedItem);

        }
        //4- Artist Get one - modified to accomodate media items

        public ArtistWithMediaInfoViewModel ArtistGetById(int id)
        {
            var o = ds.Artists.Include("Albums").Include("MediaItems").SingleOrDefault(p => p.Id == id);
            return (o == null) ? null : mapper.Map<Artist, ArtistWithMediaInfoViewModel>(o);
        } 

        public ArtistWithMediaInfoViewModel ArtistGetMediaItem(int id)
        {
            var o = ds.Artists.Include("MediaItems").SingleOrDefault(p => p.Id == id);
            return (o == null) ? null : mapper.Map<Artist, ArtistWithMediaInfoViewModel>(o);
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

             return obj == null ? null : mapper.Map<Album, AlbumWithDetailViewModel>(obj);
         } 


        //7. AlbumAdd
        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            var artistAlbum = ds.Artists.Find(newItem.Id);

            if (artistAlbum == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                addedAlbum.Artists.Add(artistAlbum);
                addedAlbum.Coordinator = User.Name;
                
                ds.SaveChanges();
                return (addedAlbum == null) ? null : mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum);
            }
        }

        //Track
        //8. TrackGetAll()
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.Include("Albums").OrderBy(e => e.Name));
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

            if (album == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));

                addedTrack.AudioContentType = newItem.TrackUpload.ContentType;
                byte[] trackBytes = new byte[newItem.TrackUpload.ContentLength];
                newItem.TrackUpload.InputStream.Read(trackBytes, 0, newItem.TrackUpload.ContentLength);

                addedTrack.Audio = trackBytes;

                //configure the clerk
                addedTrack.Clerk = User.Name;
                addedTrack.Albums.Add(album);
                // Set the associated item property


                ds.SaveChanges();

                return (addedTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
            }
        }


        //11 - TrackEdit - Not working properly 
         public TrackWithDetailViewModel TrackEdit(TrackEditViewModel track)
         {
             // Attempt to fetch the object.
             var obj = ds.Tracks.Find(track.Id);
             if (obj == null)
             {
                 // Track was not found, return null.
                 return null;
             }
             else
             {
                 obj.AudioContentType = track.TrackUpload.ContentType;
                 byte[] trackBytes = new byte[track.TrackUpload.ContentLength];
                 track.TrackUpload.InputStream.Read(trackBytes, 0, track.TrackUpload.ContentLength);
                 obj.Audio = trackBytes;

                 ds.SaveChanges();
                 return (obj == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(obj);
             }

         } 


        //12- Fetch Clip - This will enable the audio player once a track has been added
        public TrackViewModel AudioGetById(int id)
         {
             var o = ds.Tracks.Find(id);
             return (o == null) ? null : mapper.Map<Track, TrackViewModel>(o);
         }

        //13 - Fetch Media Item 
        public MediaItemWithDetailViewModel MediaGetById(int id)
        {
            var o = ds.MediaItems.Find(id);
            return (o == null) ? null : mapper.Map<MediaItem, MediaItemWithDetailViewModel>(o);
        }

        //14. Track Delete

        public bool TrackDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Tracks.Find(id);
            if (itemToDelete == null)
                return false;
            else
            {
                // Remove the object
                ds.Tracks.Remove(itemToDelete);
                ds.SaveChanges();
                return true;
            }
        }

       
        //15 - Media Item Add
         public ArtistWithMediaInfoViewModel MediaItemAdd(MediaItemAddViewModel newItem)
         {
             var artist = ds.Artists.Find(newItem.ArtistId);

             if (artist == null)
             {
                 return null;
             }
             else
             {
                 // Attempt to add the new item
                 var addedMedia = ds.MediaItems.Add(mapper.Map<MediaItemAddViewModel, MediaItem>(newItem));

                 addedMedia.ContentType = newItem.Upload.ContentType;
                 byte[] mediaBytes = new byte[newItem.Upload.ContentLength];
                 newItem.Upload.InputStream.Read(mediaBytes, 0, newItem.Upload.ContentLength);

                 addedMedia.Content = mediaBytes;
                 artist.MediaItems.Add(addedMedia);

                 ds.SaveChanges();

                 return (artist == null) ? null : mapper.Map<Artist, ArtistWithMediaInfoViewModel>(artist);
             }
         }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method


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
                // Add role claims here

                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                //ds.SaveChanges();
                //done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Pop_star.jpg/444px-Pop_star.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                       
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
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

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
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