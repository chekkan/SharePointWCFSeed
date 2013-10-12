using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharePointWCFSeed.Examples.DevelopersAppWPF.SP2010Site;

namespace SharePointWCFSeed.Examples.DevelopersAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdGetDevelopers_Click(object sender, RoutedEventArgs e)
        {
            var dc = new PortalHomeDataContext(new Uri("http://sp2010/_vti_bin/ListData.svc"));

            // if using inside the LAN
            dc.Credentials = System.Net.CredentialCache.DefaultCredentials;
            
            var source = dc.Developers;

            lstDevelopers.Items.Clear();

            foreach (var dev in source)
            {
                string devName = dev.FirstName + " " + dev.LastName;
                lstDevelopers.Items.Add(devName);
            }
        }

        private void cmdAddDeveloper_Click(object sender, RoutedEventArgs e)
        {
            var dc = new PortalHomeDataContext(new Uri("http://sp2010/_vti_bin/ListData.svc"));

            // if using inside the LAN
            dc.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //dc.Credentials = new System.Net.NetworkCredential("username", "password", "domain");

            var source = dc.Developers;

            dc.AddToDevelopers(new DevelopersItem() { FirstName = txtFirstName.Text, LastName = txtLastName.Text });

            txtFirstName.Text = txtLastName.Text = string.Empty;

            dc.SaveChanges();
        }
    }
}
