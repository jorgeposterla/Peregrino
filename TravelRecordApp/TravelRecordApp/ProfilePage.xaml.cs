using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var postTable = await Post.Read();

            var categoriesCount = Post.PostCategories(postTable);
            //using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //var postTable = conn.Table<Post>().ToList();
            //var postTable = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            //var categories = (from p in postTable
            //                  orderby p.CategoryId
            //                  select p.CategoryName).Distinct().ToList();
            //This is the same using LINQ Functions
            //var categories2 = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            //Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            //foreach (var category in categories)
            //{
            //    var count = (from post in postTable
            //                 where post.CategoryName == category
            //                 select post).ToList().Count;
            //    //This is the same using LINQ Functions
            //    //var count2 = postTable.Where(p => p.CategoryName == category).ToList().Count;

            //    categoriesCount.Add(category, count);
            //}

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}