using System;
using Starcounter;
using Abdullah.wwwroot.Abdullah;

namespace Abdullah
{
    class Program
    {
        static void Main()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());
            HttpHandlers.SetupHttpHandlers();
        }
    }
}