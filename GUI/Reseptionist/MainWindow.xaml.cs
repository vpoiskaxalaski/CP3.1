using System;
using System.Windows;
using Reseptionist.models;
using System.Data.SqlClient;

namespace Reseptionist
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (ReseptionistContext db = new ReseptionistContext())
            {

                Object[] param = { };
                try
                {

                    db.Database.ExecuteSqlCommand("SetClientServivesIrrelevant", param);
                    db.Database.ExecuteSqlCommand("SetClientsRoomsAvaliable", param);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow(this);
            clientsWindow.ShowDialog();


            using (ReseptionistContext db = new ReseptionistContext())
            {
               

                var param1 = new SqlParameter
                {
                    ParameterName = "@clientId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = int.Parse(Id.Text)
                };

                var param2 = new SqlParameter
                {
                    ParameterName = "@bill",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Direction = System.Data.ParameterDirection.Output
                };

                try
                {
                    db.Database.ExecuteSqlCommand("GetClientBill @clientId, @bill out", param1, param2);
                }
                catch 
                {
                    MessageBox.Show("Procedure GetClientBill @clientId, @bill out cannot be executed");
                }

                Result.Text = param2.Value.ToString();
            }
        }

        private void Income_Click(object sender, RoutedEventArgs e)
        {
            using(ReseptionistContext db = new ReseptionistContext())
            {
                var param1 = new SqlParameter
                {
                    ParameterName = "@start",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Start.SelectedDate
                };

                var param2 = new SqlParameter
                {
                    ParameterName = "@end",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Stop.SelectedDate
                };

                var param3 = new SqlParameter
                {
                    ParameterName = "@income",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Direction = System.Data.ParameterDirection.Output
                };

                try
                {
                    db.Database.ExecuteSqlCommand("DeterminateIncome_sp @start, @end, @income out", param1, param2, param3);

                    Header.Text = $"Income \n\r from {Start.SelectedDate} to {Stop.SelectedDate} \n\r equals:";
                    Result.Text = param3.Value.ToString();
                }
                catch
                {
                    MessageBox.Show("Procedure DeterminateIncome_sp @start, @end, @income out cannot be executed");
                }
            }
        }
    }
}
