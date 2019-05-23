using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core_1.Infrastructure.Interfaces;
using asp_net_core_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core_1.Controllers
{
    public class EmployeesController : Controller
    {
        IEmployeesData Employees;
        public EmployeesController(IEmployeesData employees)
        {
            this.Employees = employees;
        }

        public IActionResult Index()
        {
            return View(Employees.GetAll());
        }
        public IActionResult Details(int id)
        {
            var employee = Employees.GetById(id);
            if (employee is null) return NotFound();
            return View(employee);
        }
        public IActionResult Edit(int? id)
        {
            if (id is null) return View();
            return View(Employees.GetById((int)id));
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            //if (!ModelState.IsValid) return View(employee);
            if (employee.Id > 0)
            {
                var db_employee = Employees.GetById(employee.Id);
                if (db_employee is null) return NotFound();
                db_employee.FirstName = employee.FirstName;
                db_employee.SurName = employee.SurName;
                db_employee.Patronymic = employee.Patronymic;
                db_employee.Age = employee.Age;
            }
            else Employees.AddNew(employee);
            Employees.Save();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var emp = Employees.GetById(id);
            if (emp is null) return NotFound();
            Employees.Delete(id);

            return RedirectToAction("Index");
        }
    }
}