using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Login1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public BlankPage1()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
               "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new
               SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Userdata>();
            conn.CreateTable<UserNotes>();

            
        }
        string us;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (class1)e.Parameter;
            textBlock1.Text = "Hello " + parameters.username+ ",";
            us = parameters.username;
            var query = conn.Table<Userdata>();
            // parameters.Name
            // parameters.Text
            // ...
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var s = conn.Insert(new Userdata()
            {

                Username = us,
                Site= textBox.Text,
                SiteUsername = textBox1.Text,
                Password = passwordBox.Password
            });



        }

       

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBlockx.Text = "Passwords,";
            listView1.Visibility = Visibility.Collapsed;
            var query = conn.Table<Userdata>();
            conn.CreateTable<Userdata1>();
            foreach (var me in query)
            {
                if (me.Username == us)
                {
                    var s = conn.Insert(new Userdata1()
                    {

                      
                        Site = me.Site,
                        SiteUsername = me.SiteUsername,
                        Password = me.Password
                    });
                }
            }
            var query2 = conn.Table<Userdata1>();
            listView.ItemsSource = query2;
            listView.Visibility = Visibility.Visible;
            conn.DropTable<Userdata1>();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int cou = 0;
            string ab = "";
            foreach (var m in textBox2.Text)
            {
                cou++;
                if (cou > 38)
                {
                    cou = 0;
                    ab += "\n";
                    
                }
                ab += m;
            }
            var s = conn.Insert(new UserNotes()
            {
                Username = us,
                Notes = ab
                
            });
            




        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textBlockx.Text = "Notes,";
            listView.Visibility = Visibility.Collapsed;
            var query = conn.Table<UserNotes>();
            conn.CreateTable<UserNotes1>();
            foreach (var me in query)
            {
                if (me.Username == us)
                {
                    var s = conn.Insert(new UserNotes1()
                    {


                        Notes = me.Notes,
                    });
                }
            }
            var query2 = conn.Table<UserNotes1>();
            listView1.ItemsSource = query2;
            listView1.Visibility = Visibility.Visible;
           
            conn.DropTable<UserNotes1>();

        }

        
    }
    public class Userdata
    {
        public string Username { get; set; }
        public string Site { get; set; }
        public string SiteUsername { get; set; }
        public string Password { get; set; }
    }
    public class Userdata1
    {
        public string Site { get; set; }
        public string SiteUsername { get; set; }
        public string Password { get; set; }
    }
    public class UserNotes
    {
        public string Username { get; set; }
        public string Notes { get; set; }
    }
    public class UserNotes1
    {
        public string Notes { get; set; }
    }



}
