using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Reseptionist.models;
using System.IO;
using System.Xml.Serialization;


namespace Reseptionist
{
    /// <summary>
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        MainWindow mw;
        List<byte[]> p;

        public ClientsWindow(MainWindow mw)
        {
            InitializeComponent();
            p = new List<byte[]>();

            this.mw = mw;

            try
            {
                using (ReseptionistContext db = new ReseptionistContext())
                {
                    var users =  db.Database.SqlQuery<CLIENT>("SELECT * FROM SelectFromUsers()").ToList();

                    List<Clent> list = new List<Clent>();

                    foreach(CLIENT u in users)
                    {
                        p.Add(u.PASWRD);
                        list.Add(new Clent(u.ID, u.FIRST_NAME, u.MAIL, BitConverter.ToString(u.PASWRD).Replace("-", string.Empty)));                        
                    }

                    ClientsGrid.ItemsSource = list;
                }               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {

            var selectedUser = ClientsGrid.SelectedItem;
            CLIENT user = new CLIENT();
            Clent client = new Clent();

            try
            {
                if (selectedUser != null)
                {
                    client = (Clent)selectedUser;
                    user.ID = client.ID;
                    user.FIRST_NAME = client.FIRST_NAME;
                    user.MAIL = client.MAIL;
                    user.PASWRD =p.ElementAt( ClientsGrid.Items.IndexOf(selectedUser));
                    mw.Id.Text = user.ID.ToString();
                    mw.Header.Text = user.FIRST_NAME.ToString();

                    this.DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
