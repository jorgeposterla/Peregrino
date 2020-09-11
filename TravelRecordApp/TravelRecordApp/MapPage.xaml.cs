using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        //private bool hasLocationPermission = false;
        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(TimeSpan.Zero, 100);

            GetLocation();

            var posts = await Post.Read();
            DisplayInMap(posts);
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);

                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }

        void Locator_PositionChanged(object render, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        public async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            MoveMap(position);
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }
    }
}