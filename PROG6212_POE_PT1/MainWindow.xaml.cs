using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PROG6212_POE_PT1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Module> modules = new List<Module>();
        List<Module> filtered;

        public Module md = new Module();
        public MainWindow()
        {
            InitializeComponent();

            
        }

        public class Module
        {
            public string moduleName { get; set; }
            public string moduleCode { get; set; }
            public int No_ofCredits { get; set; }
            public int Hours { get; set; }
            public int Weeks { get; set; }
            public string startDate { get; set; }
            public int self { get; set; }
            public int remain { get; set; }
        }

        
        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            //validation 
            if (tb1.Text.Length == 0 || tb2.Text.Length == 0 || tb3.Text.Length == 0 || tb4.Text.Length == 0)
            {
                MessageBox.Show("Fields cannot be Left Blank >>>>");
            }
            else
            {
                // confirm if entry is correct before sending to the Datagrid
                if (MessageBox.Show("Is the entry correct?", "Cofirm Entry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // pull info from the boxes
                    Module md = new Module();
                    md.moduleName = tb2.Text;
                    md.moduleCode = tb1.Text;
                    md.No_ofCredits = Convert.ToInt32(this.tb3.Text);
                    md.Hours = Convert.ToInt32(this.tb4.Text);

                    cb1.Items.Add(md.moduleCode);

                    modules.Add(md);



                    //Info from the text boxes are added into the datagrid
                    Datagrid1.Items.Add(md);

                    //Clear all the textboxes
                    //tb1.Text = "";
                    //tb2.Text = "";
                    //tb3.Text = "";
                    //tb4.Text = "";
                }
            }
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            // Clear the datagrid
            Datagrid1.Items.Clear();
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            // pull info from the boxes
            Module md = new Module();
            md.Weeks = Convert.ToInt32(this.tb5.Text);
            md.startDate = date1.Text;
            md.moduleCode = cb1.Text;
            md.self = CalculateMod.Class1.SelfStudies(Convert.ToInt32(tb3.Text), Convert.ToInt32(tb5.Text), Convert.ToInt32(tb4.Text));

            Datagrid2.Items.Add(md);
        }

        private void bt6_Click(object sender, RoutedEventArgs e)
        {

            // pull info from the boxes
            Module md = new Module();
            md.self = CalculateMod.Class1.SelfStudies(Convert.ToInt32(tb3.Text), Convert.ToInt32(tb5.Text), Convert.ToInt32(tb4.Text));
            md.remain = CalculateMod.Class1.remainingHours(md.self, Convert.ToInt32(tb6.Text));

            tb7.Text = Convert.ToString(md.remain);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Datagrid1.Items.Clear();
            ComboBoxItem cbi = (ComboBoxItem)cb2.SelectedItem;
            if(cb2.SelectedItem != null)
            {
                string opt = cbi.Content.ToString();
                switch (opt)
                {
                    case "Ascending order by Module Name": filtered = modules.OrderBy(md => md.moduleName).ToList();

                        //using linq
                        filtered = (from f in modules
                                    orderby md.moduleName
                                    select f).ToList();
                        foreach (Module m in filtered)
                        {
                            Datagrid1.Items.Add(m);
                        }

                        break;

                    case "Descending Order by Hours per Week":
                        filtered = modules.OrderBy(md => md.Hours).ToList();
                        filtered = (from f in modules
                                    orderby md.moduleName
                                    select f).ToList();
                        foreach (Module m in filtered)
                        {
                            Datagrid1.Items.Add(m);
                        }

                        break;
                }
                





            }
        }
    }
}
