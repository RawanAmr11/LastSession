 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

namespace ExpenseIt
{
    public partial class ExpenseReportPage : Page
    {
        Person _person = new Person();

        public ExpenseReportPage()
        {

        }
        public ExpenseReportPage(string data)
        {
            InitializeComponent();
            var subStrings = data.Split(' ');
            string personName = subStrings.Last();
            _person.Name=personName;
            this.DataContext = _person;

            showTheReport();
        }

        public class  Person
        {
            public string Name { get; set; }
        }

        private void showTheReport()
        {
            SqlConnection conn = new SqlConnection("Data Source= DESKTOP-NSG3E37; Initial Catalog= expense; Integrated Security= true");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from expense_report where Names=@Name";
            cmd.Parameters.Add("@Name", _person.Name);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Expense_Report");
            adapter.Fill(dt);

            expenseData.ItemsSource = dt.DefaultView;

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                depData.Content = reader["Department"].ToString();
            }
            

        }
    }
}
