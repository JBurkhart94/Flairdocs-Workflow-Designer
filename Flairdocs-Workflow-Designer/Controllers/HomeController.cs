using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flairdocs_Workflow_Designer.Models;

namespace Flairdocs_Workflow_Designer.Controllers
{
    public class HomeController : Controller
    {

        WorkflowContext db = new WorkflowContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Workflow(Guid id)
        {
            Workflow workflow = db.Workflows.Find(id);
            return View(workflow);
        }

        public Guid? Create(String title, String description)
        {
            if (!TitleExists(title))
            {
                //Create Workflow and AuditLog
                AuditLog auditLog = new AuditLog
                {
                    Id = Guid.NewGuid()
                };
                db.AuditLogs.Add(auditLog);

                Workflow workflow = new Models.Workflow
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Description = description,
                    Creation_Date = DateTime.Now,
                    AuditLog = auditLog
                };
                db.Workflows.Add(workflow);

                //Save
                db.SaveChanges();
                return workflow.Id;
            }
            else
            {
                return null;
            }
        }

        //Check if a workflow exists with the given name
        public Boolean TitleExists(String title)
        {
            var exists = from w in db.Workflows
                         where w.Title == title
                         select w;

            if (exists.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}