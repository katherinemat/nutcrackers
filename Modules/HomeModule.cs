using Nancy;
using System.Collections.Generic;
using System;

namespace Nutcrackers
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Post["/"] = _ => {
        string userInput = Request.Form["name"];
        Nutcracker newNutcracker = new Nutcracker(userInput);
        newNutcracker.Save();
        string newName = newNutcracker.GetName();
        List<Nutcracker> allNutcrackers = Nutcracker.GetAll();
        return View["index.cshtml", allNutcrackers];

      };
    }
  }
}
