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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Data;
using GUI.models;
using System.Data.Entity.Core.Objects;

namespace GUI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (HotelContext db = new HotelContext())
            {
                Object[] param = { };

                try
                {

                    db.Database.ExecuteSqlCommand("SetClientServivesIrrelevant", param);
                    db.Database.ExecuteSqlCommand("SetClientsRoomsAvaliable", param);

                    db.Database.ExecuteSqlCommand("CREATE OR ALTER TRIGGER ReservationInsert " +
                                                                   "ON RESERVATION " +
                                                                   "AFTER INSERT " +
                                                                   "AS " +
                                                                   "UPDATE hotel.dbo.ROOM SET ISAVALIABLE = 0" +
                                                                   "WHERE  ROOM_NUMBER = (SELECT ROOM_NUMBER FROM RESERVATION WHERE ID = @@IDENTITY)");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //REGISTER NEW CLIENT
        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            string firstName = userName.Text + " " + userLastname.Text;
            CLIENT client = new CLIENT(firstName, newUserMail.Text, newUserPassword.Password);
           
            XmlSerializer formatter = new XmlSerializer(typeof(CLIENT));
            string xml = null;

            try
            {


                using (FileStream fs = new FileStream("../../../../xml/CLIENT/newClient.xml", FileMode.Truncate))
                {
                    formatter.Serialize(fs, client);
                }

                using (FileStream fs = new FileStream("../../../../xml/CLIENT/newClient.xml", FileMode.OpenOrCreate))
                {
                    StreamReader reader = new StreamReader(fs);
                    xml = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            using (ClientContext db = new ClientContext())
            {
                try
                {

                    var param = new SqlParameter
                    {
                        ParameterName = "@request",
                        SqlDbType = System.Data.SqlDbType.Xml,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = xml
                    };

                    var result = db.Database.ExecuteSqlCommand("RegisterUser @request", param) ;

                    if (result == 1)
                    {
                        ReservationWindow window = new ReservationWindow(client);
                        window.Show();
                        this.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("Procedure RegisterUser @request connot be executed");
                }

            }
        }
     


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            string firstName = "UserX";
            string mail = userMail.Text;
            string password = userPassword.Password;

            CLIENT client = new CLIENT(firstName, mail, password);

            XmlSerializer formatter = new XmlSerializer(typeof(CLIENT));
            string xml;

            try
            {

                using (FileStream fs = new FileStream("../../../../xml/CLIENT/Client.xml", FileMode.Truncate))
                {
                    formatter.Serialize(fs, client);
                }

                using (FileStream fs = new FileStream("../../../../xml/CLIENT/Client.xml", FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fs);
                    xml = reader.ReadToEnd();
                }

                using (ClientContext db = new ClientContext())
                {

                        var param = new SqlParameter
                        {
                            ParameterName = "@request",
                            SqlDbType = System.Data.SqlDbType.Xml,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = xml
                        };

                        var result = new SqlParameter
                        {
                            ParameterName = "@r",
                            SqlDbType = System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Output,
                        };

                    try
                    {

                        db.Database.ExecuteSqlCommand("Login_sp @request, @r out", param, result);

                        if ((bool)result.Value == true)
                        {
                            ReservationWindow window = new ReservationWindow(client);
                            window.Show();
                            this.Close();
                        }


                    }
                    catch 
                    {
                        MessageBox.Show("Procedure Login_sp @request, @r out connot be executed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
