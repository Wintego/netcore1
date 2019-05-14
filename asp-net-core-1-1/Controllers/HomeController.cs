using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core_1_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core_1_1.Controllers
{
    public class HomeController : Controller
    {
        List<Employee> Employees = new List<Employee>
        {
            new Employee { Id=0, FirstName="Имя0", SurName="Фамилия0", Patronymic="Отчество0", Age=0 },
            new Employee { Id=1, FirstName="Имя1", SurName="Фамилия1", Patronymic="Отчество1", Age=1 },
            new Employee { Id=2, FirstName="Имя2", SurName="Фамилия2", Patronymic="Отчество2", Age=2 }
        };
        public IActionResult Index()
        {
            return View(Employees);
        }
        public IActionResult Details(int id)
        {
            return View(Employees.FirstOrDefault(e => e.Id == id));
        }
    }
}