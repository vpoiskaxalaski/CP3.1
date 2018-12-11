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
using GUI.models;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;


namespace GUI
{

    public partial class Services : Window
    {
        CLIENT client;

        public Services(CLIENT c )
        {
            InitializeComponent();

            client = c;

            try
            {
                using (HotelContext db = new HotelContext())
                {
                    db.Database.ExecuteSqlCommand("GetServices_sp");
                }

                List<ADDSERVISE> serv = new List<ADDSERVISE>();
                XmlSerializer formatter = new XmlSerializer(typeof(List<ADDSERVISE>));

                using (FileStream fs = new FileStream("../../../../xml/ADDSERVISE/result.xml", FileMode.OpenOrCreate))
                {
                    serv = (List<ADDSERVISE>)formatter.Deserialize(fs);
                }

                serv.RemoveAt(0);
                ServicesGrid.ItemsSource = serv;

            }
            catch (Exception ex)
            {
               //MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime come = new DateTime();
            DateTime leave = new DateTime();
            string servName="NULL";
            int ClientId = 0;

            var selectedService = ServicesGrid.SelectedItem;

            try
            {
                if ((selectedService as ADDSERVISE) != null)
                {
                    ADDSERVISE s = (ADDSERVISE)selectedService;
                    servName = s.SERV_NAME;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            using (HotelContext db = new HotelContext())
            {
                try
                {
                    string sqlQuery = "SELECT dbo.GetUserId ( {0} )";
                    Object[] param = { client.MAIL };
                    ClientId = db.Database.SqlQuery<int>(sqlQuery, param).FirstOrDefault();

                    string sqlQuery1 = "SELECT dbo.GetCommingDate ( {0} )";
                    Object[] param1 = { ClientId };
                    come = db.Database.SqlQuery<DateTime>(sqlQuery1, param1).FirstOrDefault();

                    string sqlQuery2 = "SELECT dbo.GetLeavingDate ( {0} )";
                    Object[] param2 = { ClientId };
                    leave = db.Database.SqlQuery<DateTime>(sqlQuery2, param2).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            using (ClientContext db = new ClientContext())
            {
                try
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = "@service",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = servName
                    };

                    var param2 = new SqlParameter
                    {
                        ParameterName = "@client",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = ClientId
                    };

                    var param3 = new SqlParameter
                    {
                        ParameterName = "@comming",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = come
                    };

                    var param4 = new SqlParameter
                    {
                        ParameterName = "@leaving",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = leave
                    };

                    var result = db.Database.ExecuteSqlCommand("AddServices_sp  @service, @client, @comming, @leaving", param1, param2, param3, param4);

                    if (result == 1)
                    {
                        MessageBox.Show("Service added successfuly");
                    }
                    else
                    {
                        MessageBox.Show("Service add ended with error");
                    }

                }
                catch 
                {
                    MessageBox.Show("Procedure AddServices_sp  @service, @client, @comming, @leaving connot be executed");
                }

            }
        }
    }
}
