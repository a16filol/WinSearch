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
        private List<Application> applications;

        public Program()
        {
            applications = new FilterApplications()
                .GetApplications();
        }

        public void StartApplication(string name)
        {
            foreach(Application app in applications)
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

            foreach (Application app in applications)
            {
                if (app._name.ToLower().Contains(name.ToLower())) results.Add(app);
            }

            return results;
        }
    }
}
