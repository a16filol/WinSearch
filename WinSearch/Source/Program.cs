using SmartSearch.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Source
{
    class Program
    {
        private FilterApplications applications;

        public Program()
        {
            applications = new FilterApplications();
        }

        public void StartApplication(string name)
        {
            foreach(Application app in applications.GetApplications())
            {
                if(app._name == name)
                {
                    app.StartApplication();
                    break;
                }
            }
        }

        public List<Application> SearchForApplications(string name)
        {
            List<Application> results = new List<Application>();

            foreach (Application app in applications.GetApplications())
            {
                if (app._name.ToLower().Contains(name.ToLower())) results.Add(app);
            }

            return results;
        }

        public void UpdateApplications()
        {
            applications.SetAllApplications();
        }
    }
}
