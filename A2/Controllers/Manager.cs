using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASSIGNMENT_2.EntityModels;
using ASSIGNMENT_2.Models;

namespace ASSIGNMENT_2.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                //Assignment 2- Milstone 1 Mappings
                cfg.CreateMap<Album, AlbumBaseViewModel>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();

                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();

                cfg.CreateMap<Track, TrackBaseViewModel>();

                cfg.CreateMap<TrackAddViewModel, Track>();

                cfg.CreateMap<Track, TrackWithDetailViewModel>();

                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();

                //assignment 2 milestone 2

                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();

                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();

                cfg.CreateMap<PlaylistEditTracksViewModel, Playlist>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
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

        //Methods - Milestone1

        //1
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(e => e.Title));
        }

        //2
        public AlbumBaseViewModel AlbumGetById(int id)
        {
            var obj = ds.Albums.Find(id);
            return obj == null ? null : mapper.Map<Album, AlbumBaseViewModel>(obj);
        }

        //3
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(ds.MediaTypes.OrderBy(e => e.Name));
        }

        //4
        public MediaTypeBaseViewModel MediaTypeGetById(int id)
        {
            var obj = ds.MediaTypes.Find(id);
            return obj == null ? null : mapper.Map<MediaType, MediaTypeBaseViewModel>(obj);
        }

        //5
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(e => e.Name));
        }

        //6
        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var obj = ds.Tracks.Include("Album.Artist").Include("MediaType").OrderBy(e => e.TrackId).ThenBy(e => e.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(obj);
        }

        //7
        public TrackWithDetailViewModel TrackGetByIdWithDetail(int id)
        {
            var obj = ds.Tracks.Include("Album.Artist").Include("MediaType").SingleOrDefault(t => t.TrackId == id);
            return obj == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(obj);
        }

        //8 - TO BE IMPLEMENTED LATER (Track Add)

        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newItem)
        {
            var a = ds.Albums.Find(newItem.AlbumId);
            var b = ds.MediaTypes.Find(newItem.MediaTypeId);

            if (a == null || b == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));
                // Set the associated item property
                addedItem.Album = a;
                addedItem.MediaType = b;
                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }
        }

        //milestone 2

        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(ds.Playlists.Include("Tracks").OrderBy(e => e.Name));
        }

        public PlaylistBaseViewModel PlaylistGetById(int? id)
        {
            var obj = ds.Playlists.Include("Tracks").SingleOrDefault(t => t.PlaylistId == id);
            return obj == null ? null : mapper.Map<Playlist, PlaylistBaseViewModel>(obj);
        }

        public PlaylistBaseViewModel PlaylistEdit(PlaylistEditTracksViewModel playlist)
        {   
            var o = ds.Playlists.Include("Tracks")
                .SingleOrDefault(e => e.PlaylistId == playlist.PlaylistId);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.Tracks.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in playlist.TrackIds)
                {
                    var a = ds.Tracks.Find(item);
                    o.Tracks.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<Playlist, PlaylistBaseViewModel>(o);
            }
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(e => e.Name));
        }


    }
}