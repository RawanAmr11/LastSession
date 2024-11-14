using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// <summary>
    /// Interaction logic for ModifyRecords.xaml
    /// </summary>
    public partial class ModifyRecords : Page
    {
        ExpenseEntities db = new ExpenseEntities();
        public ModifyRecords()
        {
            InitializeComponent();
            ModifyDG.ItemsSource = db.ExpenseReports.ToList();
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if(txtID.Text != "")
            {
                MessageBox.Show("ID generated automatically");
            }

            ExpenseReport rec = new ExpenseReport();
            rec.Names = txtName.Text;
            rec.ExpenseType = txtType.Text;
            rec.Amount = int.Parse(txtAmount.Text);
            rec.Department = txtDep.Text;

            db.ExpenseReports.Add(rec); 
            db.SaveChanges();

            MessageBox.Show("Data saved successfully");

        }

        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            ExpenseReport rec = new ExpenseReport();
            int idFromTxt = int.Parse(txtID.Text);
            rec = db.ExpenseReports.First(x => x.ID == idFromTxt);
            rec.Names = txtName.Text;
            rec.ExpenseType = txtType.Text;
            rec.Amount = int.Parse(txtAmount.Text);
            rec.Department = txtDep.Text;

            db.ExpenseReports.AddOrUpdate(rec);

            MessageBox.Show("Data Updated");

        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            int idFromTxt = int.Parse(txtID.Text);
            ExpenseReport rec = db.ExpenseReports.Remove(db.ExpenseReports.First(x => x.ID == idFromTxt));
            MessageBox.Show("Record Deleted");
            db.SaveChanges();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            ModifyDG.ItemsSource = db.ExpenseReports.ToList();
        }
    }
}
