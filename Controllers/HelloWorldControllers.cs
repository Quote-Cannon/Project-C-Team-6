using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace AuthSystem.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        // Every public method in a controller is callable as an HTTP endpoint.
        // The first URL segment determines the controller class to run (so localhost{port}/HelloWorld maps to the HelloWorldController).
        // The second URL segment determines the action method on the class (so localhost{port}/HelloWorld/Index would cause the Index()
        // of the HelloWorldController class to run).
        // Controller always should have an index (controller looks at index by default if there is no specific method name called). 
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Welcome()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/

        /*public string Welcome(string name, numTimes = 1)
        {
            // return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
            // numTimes = 1 indicates that default value is 1 if no value is passed.
            // HtmlEncoder.Default.Encode is to proect the app from malicious input (namely JavaScript).
            // /HelloWorld/Welcome?name=Rick&numTimes=4 ? = sepaprator, & = separates field values
        }*/

    }
}
