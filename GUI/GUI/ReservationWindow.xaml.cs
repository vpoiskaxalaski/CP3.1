using GUI.models;
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
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace GUI
{

    public partial class ReservationWindow : Window
    {
        CLIENT client;

        public ReservationWindow(CLIENT c)
        {
            InitializeComponent();

            client = c;

            
                try
                {
                    using (ClientContext db = new ClientContext())
                    {
                        db.Database.ExecuteSqlCommand("GetAvaliableRooms_sp");
                    }

                    List<ROOM> rooms = new List<ROOM>();

                    XmlSerializer formatter = new XmlSerializer(typeof(List<ROOM>));

                    using (FileStream fs = new FileStream("../../../../xml/ROOM/result.xml", FileMode.OpenOrCreate))
                    {
                        rooms = (List<ROOM>)formatter.Deserialize(fs);
                    }
                   
                    RoomsGrid.ItemsSource = rooms;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }          
        }

        private void ReservRoom_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = RoomsGrid.SelectedItem;

            try
            {
                if ((selectedRoom as ROOM) != null)
                {
                    ROOM r = (ROOM)selectedRoom;
                    AddData window = new AddData(r.ROOM_NUMBER, client.MAIL, r.ROOM_TYPE, r.COST, client);
                    window.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            Services servicesWindow = new Services(client);
            servicesWindow.Show();           
           
        }
    }
}
