using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;
        public PostDetailPage()
        {

        }
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
            coordinatesLabel.Text = $"{selectedPost.Latitude}, {selectedPost.Longitude}";
            distanceLabel.Text = $"{selectedPost.Distance} meters";
        }

        async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            Post.Update(selectedPost);
            await DisplayAlert("Success!", "Experience successfully updated", "Ok");
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Update(selectedPost);

            //    if (rows > 0)
            //    {
            //        DisplayAlert("Success!", "Experience successfully updated", "Ok");
            //    }
            //    else
            //    {
            //        DisplayAlert("Failure", "Experience failed to be updated", "Ok");
            //    }
            //}
        }

        async void deleteButton_Clicked(object sender, EventArgs e)
        {
            Post.Delete(selectedPost);
            await DisplayAlert("Success!", "Experience successfully deleted", "Ok");
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Delete(selectedPost);

            //    if (rows > 0)
            //    {
            //        DisplayAlert("Success!", "Experience successfully deleted", "Ok");
            //    }
            //    else
            //    {
            //        DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            //    }
            //}
        }
    }
}