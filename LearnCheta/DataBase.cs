using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.IO;
using System.Windows.Forms;

namespace LearnCheta
{
    public class DataBase
    {
        SqlConnection sqlConnection;
        public async Task<SqlDataReader> GetData()
        {

            var path = System.Windows.Forms.Application.StartupPath;
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.Windows.Forms.Application.StartupPath}\ChetaBase.mdf;Integrated Security=True";
          // Directory.SetCurrentDirectory(mainDirectory);
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [PlanChetov]",sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                
                //while (await reader.ReadAsync())
                //{
                //    ListBoxAllData.Items.Add(reader["Name"].ToString() + "   " + reader["Number"].ToString());
                //}
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //finally
            //{
            //    if (reader != null)
            //        reader.Close();
            //}
            return reader;
        }
    }
}
