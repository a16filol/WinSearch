using Microsoft.Win32;
using SmartSearch.Modules.Blacklist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSearch.Modules
{
    class FilterApplications
    {
        private DefaultBlacklist _blacklist;
        private List<Application> applications = new List<Application>();

        public FilterApplications()
        {
            // Get default values for blacklist
            _blacklist = new DefaultBlacklist();
            _blacklist.AddUserBlacklist();

            SetAllApplications();
            applications = FilterBlacklistApplications(_blacklist.FullBlacklist);
        }

        /**
         * This function opens the regesties to access the application names.
         * 
         * return void
         **/
        private void SetAllApplications()
        {
            // Get all aplications from path
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + "\\Programs";

            string[] apps = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);

            foreach(String path in apps)
            {
                applications.Add(new Application(path));
            }
        }

        /**
         * Filter out the application that are in the blacklist.
         * 
         * return List<Application> returnes the filterd list of Applications
         **/
        private List<Application> FilterBlacklistApplications(List<string> blacklist)
        {
            List<Application> temp = new List<Application>();
            foreach(Application app in applications)
            {
                if (app._name != null && !blacklist.Any(app._name.Contains))
                {
                    temp.Add(app);
                }

            }
            return temp;
        }

        public List<Application> GetApplications()
        {
            return applications;
        }
    }
}
