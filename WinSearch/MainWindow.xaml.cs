using SmartSearch.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartSearch.Modules;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinSearch.Source;
using System.Threading;

namespace WinSearch
{
    public partial class MainWindow : Window
    {
        private Program _program;
        private bool InTray = true;
        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(@"search.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };

            this.Hide();


            new Thread(Hooky).Start();

            _program = new Program();
        }

        
        /**
         * Functionerna nedanför körs från Hooky de behöver en invoker för att fungera.
         * 
         * */
        public void HideWindow()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>  
            {
                this.Hide();
                InTray = true;
            }));   
        }


        public void ShowWindow()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>  
            {
                this.Show();
                InTray = false;
                SearchBox.Focus();
            }));   
        }

        public void RunProgram()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>  
            {
                if (!InTray && SearchBox.IsFocused)
                {
                    _program.StartApplication(ApplicationName.Text);
                    this.Hide();
                    InTray = true;
                    SearchBox.Text = "";
                }
           }));   
        }

        public void NextSearch()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                List<SmartSearch.Modules.Application> apps = _program.SearchForApplications(SearchBox.Text);
                apps = apps.OrderBy(app => app._name).ToList();
                index++;

                if (ApplicationName != null && apps.Count() > index)
                {
                    ApplicationName.Text = apps[index]._name;
                }
                else if (apps.Count() <= index)
                {
                    index = 0;
                    ApplicationName.Text = (apps.Count() > 0 ? apps[index]._name : "No match");
                }
            }));  
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if (ApplicationName != null)
            {
                // Sök efter appar som liknanar searchbox text
                List<SmartSearch.Modules.Application> apps = _program.SearchForApplications(SearchBox.Text);
                index = 0;

                if (apps.Count > 0)
                {
                    ApplicationName.Text = apps[index]._name;
                }
                else
                {
                    ApplicationName.Text = "No match";
                }

                if (SearchBox.Text == "")
                {
                    ApplicationName.Text = "";
                }
            }
        }

        public void Hooky()
        {
            new Hooky(this).Start();
        }

        public void App_Activated(object sender, EventArgs e)
        {
            this.Show();
            SearchBox.Focus();
            InTray = false;

            // För att inte få buggen när skiten inte är markerat.
            this.Activate();
            this.Topmost = true;
        } 

        public void App_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
            InTray = true;
        } 
    }
}
