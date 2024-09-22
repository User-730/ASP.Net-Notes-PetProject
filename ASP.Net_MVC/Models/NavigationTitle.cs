namespace ASP.Net_MVC.Models
{
    public class NavigationTitle
    {
        public string navName { get; set; }
        public string controllerName { get; set; }

        public NavigationTitle(string controllerName, string navName)
        {
            this.navName = navName;
            this.controllerName = controllerName;
        }
    }
}
