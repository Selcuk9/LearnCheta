using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Http;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace LearnCheta
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var dic = Directory.GetParent(@"..\..\id_card_icon_124180.ico").ToString();
            //Uri iconUri = new Uri(dic.ToString() + "/id_card_icon_124180.ico");
            //Icon = BitmapFrame.Create(iconUri);
            InitializeComponent();
          
        }
        private void NameTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var click = ((System.Windows.Controls.TextBox)sender);
            if (click.Uid == "0")
            {
                click.Foreground = System.Windows.Media.Brushes.Black;
                click.Text = null;
                click.Uid = "1";
            }
        }

        private DataBase dataBase = new DataBase();

        private async void ButtonAllData(object sender, RoutedEventArgs e)
        { 
            var reader = await dataBase.GetData();
            if(ListBoxAllData.Items.Count>0)
            ListBoxAllData.Items.Clear();
            if (((System.Windows.Controls.Button)sender).Uid == "1" && !string.IsNullOrEmpty(IdTextBox.Text) && !string.IsNullOrWhiteSpace(IdTextBox.Text))
                while (await reader.ReadAsync())
                {
                    if (IdTextBox.Text == reader["Number"].ToString())
                    {
                        ListBoxAllData.Items.Clear();
                        ListBoxAllData.Items.Add(reader["Name"].ToString()+ "  " + reader["Number"].ToString());
                        break;
                    }
                }
            else
            while (await reader.ReadAsync())
            {
                ListBoxAllData.Items.Add(reader["Name"].ToString() + "   " + reader["Number"].ToString());
            }
            if (ListBoxAllData.Items.Count == 0)
                ListBoxAllData.Items.Add("Счет не найден!");
            //GetData();
            //Fill();
        }

        //public async void GetData()
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\79899\source\repos\LearnCheta\LearnCheta\ChetaBase.mdf;Integrated Security=True";
        //    sqlConnection = new SqlConnection(connectionString);
        //    await sqlConnection.OpenAsync();
        //    SqlDataReader reader = null;
        //    SqlCommand command = new SqlCommand("SELECT * FROM [PlanChetov]", sqlConnection);
        //    try
        //    {
        //        reader = await command.ExecuteReaderAsync();
        //            ListBoxAllData.Items.Clear();

        //        while (await reader.ReadAsync())
        //        {
        //            ListBoxAllData.Items.Add(reader["Name"].ToString() + "   " + reader["Number"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //            reader.Close();
        //    }
        //}
        //async void Fill()
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\79899\source\repos\LearnCheta\LearnCheta\ChetaBase.mdf;Integrated Security=True";
        //    sqlConnection = new SqlConnection(connectionString);
        //     await sqlConnection.OpenAsync();
        //    string path = @"C:\Users\79899\Desktop\PlanChetov.txt";
        //    string[] planChetov = File.ReadAllLines(path);
        //    List<string> numberList = new List<string>();
        //    List<string> nameList = new List<string>();
        //    int j = 0;
        //    for (int i = 0; i < planChetov.Length; i++)
        //    {
        //        if(planChetov[i][0]== 'С' || planChetov[i][0] == 'С')
        //        {

        //            numberList.Add(planChetov[i].Substring(5,2));
        //            nameList.Add(planChetov[i].Remove(planChetov[i].Length - 1, 1).Remove(0, 9));
        //            SqlCommand command = new SqlCommand("INSERT INTO [PlanChetov] (Name, Number)VALUES(@Name, @Number)", sqlConnection);
        //            command.Parameters.AddWithValue("Name", nameList[j]);
        //            command.Parameters.AddWithValue("Number", numberList[j]);
        //            j++;
        //            await command.ExecuteNonQueryAsync();

        //        }
        //    }
        //} Заполнн заполнение базы

        private void ExitClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        //    if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
        //        sqlConnection.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button1.IsEnabled = false;
            var reader = await dataBase.GetData();
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = SystemIcons.Asterisk; /*new Icon(@"..\..\id_card_icon_124180.ico");*/

            ni.Visible = true;
            ni.BalloonTipTitle = "Учи";
            ni.BalloonTipIcon = ToolTipIcon.Info;

            while (await reader.ReadAsync())
            {

                for (int i = 0; i < 2; i++)
                {
                    ni.BalloonTipText = $"{reader["Name"]}  {reader["Number"]}";
                    ListBoxAllData.Items.Clear();
                    ListBoxAllData.Items.Add(reader["Name"].ToString() + "  " + reader["Number"].ToString());
                    ni.ShowBalloonTip(1000);
                    await Task.Delay(60000 * 3);
                }
            }
            reader.Close();

            //await Task.Run(() => Learning());
            //Button1.IsEnabled = false;
        }
        async void  Learning()
        {
            var reader = await dataBase.GetData();
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = SystemIcons.Asterisk; /*new Icon(@"..\..\id_card_icon_124180.ico");*/
            
            ni.Visible = true;
            ni.BalloonTipTitle = "Учи";
            ni.BalloonTipIcon = ToolTipIcon.Info;
            
            while (await reader.ReadAsync())
            {
                
                for (int i = 0; i < 2; i++)
                {
                    ni.BalloonTipText = $"{reader["Name"]}  {reader["Number"]}";
                    ListBoxAllData.Items.Clear();
                    ListBoxAllData.Items.Add(reader["Name"].ToString() + "  " + reader["Number"].ToString());
                    ni.ShowBalloonTip(1000);
                    Thread.Sleep(60000*3);
                }
            }
            reader.Close();

        }
    }
}
