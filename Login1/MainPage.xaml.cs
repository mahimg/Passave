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
using System.Data;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Login1
{

    public sealed partial class MainPage : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public MainPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
               "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new
               SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Logindata>();
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<Logindata>();
            var dummy = 0;
            textBlock3.Text = "";
            foreach (var message in query)
            {
                if(textBox.Text == message.Username)
                {
                    dummy = 1;
                    if(textBox1.Password == message.Password)
                    {
                        textBlock3.Text = "User found! Login Successful";
                        var parameters = new class1();
                        parameters.username = textBox.Text;

                        this.Frame.Navigate(typeof(BlankPage1), parameters);

                    }
                    else
                    {
                        textBlock3.Text = "Password is incorrect!";
                    }
                    break;

                }
            }
            if (dummy == 0)
            {
                textBlock3.Text = "User not found! Register first";

            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            var query = conn.Table<Logindata>();
            var dummy = 0;
            textBlock3.Text = "";
            foreach (var message in query)
            {
                if (textBox.Text == message.Username)
                {
                    dummy = 1;
                    textBlock3.Text = "Username already taken";
                    break;

                }
            }
            if(dummy == 0)
            {
                textBlock3.Text = "Registered, click on Login";
                var s = conn.Insert(new Logindata()
                {
                    Username = textBox.Text,
                    Password = textBox1.Password
                });
            }


            

        }

        
    }

    public class Logindata
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class class1
    {
        public string username { get; set; }
    }

}
