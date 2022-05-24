using Microsoft.AspNetCore.Mvc;
using ToDoList.sourceChanger;

namespace ToDoList.Controllers
{
    public class SourceSwitchController : Controller
    {
        [HttpPost]
        public IActionResult ChangeCurrentSource(string source, string controller, string action)
        {
            CurrentStorage.SetCurrentSource(source);
            return RedirectToAction(action, controller);
        }
    }
}
