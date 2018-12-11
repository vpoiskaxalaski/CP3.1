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
using System.Data.SqlClient;


namespace GUI
{

    public partial class AddData : Window
    {
        CLIENT client;
        short number;
        string type;
        decimal cost;
        string mail;

        public AddData(short n,string m,  string t, decimal c, CLIENT client)
        {
            InitializeComponent();

            number = n;
            mail = m;
            type = t;
            cost = c;
            this.client = client;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int ClientId = 0;
            try
            {
                using (HotelContext db = new HotelContext())
                {
                    string sqlQuery = "SELECT dbo.GetUserId ( {0} )";
                    Object[] param = { mail };

                    try
                    {
                        ClientId = db.Database.SqlQuery<int>(sqlQuery, param).FirstOrDefault();
                    }
                    catch 
                    {
                        MessageBox.Show("Function dbo.GetUserId cannot be executed");
                    }
                }

                using (ClientContext db = new ClientContext())
                {
                    var param1 = new SqlParameter
                    {
                        ParameterName = "@room_number",
                        SqlDbType = System.Data.SqlDbType.SmallInt,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = number
                    };

                    var param2 = new SqlParameter
                    {
                        ParameterName = "@client_id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = ClientId
                    };

                    var param3 = new SqlParameter
                    {
                        ParameterName = "@come",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = CommingDate.SelectedDate
                    };

                    var param4 = new SqlParameter
                    {
                        ParameterName = "@leave",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = LeavingDate.SelectedDate
                    };

                    try
                    {
                        var result = db.Database.ExecuteSqlCommand("BookRoom_sp @room_number, @client_id, @come, @leave", param1, param2, param3, param4);
                    }
                    catch 
                    {
                        MessageBox.Show("Procedure BookRoom_sp @room_number, @client_id, @come, @leave connot be executed");
                    }

                    ReservationWindow window = new ReservationWindow(client);
                    window.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
