using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flairdocs_Workflow_Designer.Models;
using System.Data.Entity;
using Newtonsoft.Json;

namespace Flairdocs_Workflow_Designer.Controllers
{
    public class HomeController : Controller
    {
        // WorkflowContext is the overall access to database
        WorkflowContext db = new WorkflowContext();

        // render the page
        public ActionResult DragNDrop()
        {
            return View();
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Workflow Designer";
            // GetTitles see below
            ViewBag.Titles = GetTitles();
            return View();
        }

        //@param id Guid of the workflow
        public ActionResult Workflow(Guid id)
        {
            Workflow workflow = db.Workflows.Find(id);
            workflow.Steps = workflow.Steps.OrderBy(step => step.Order).ToList();
            foreach (Step step in workflow.Steps)
            {
                step.Reviewers = db.Reviewers.Where(r => r.StepId == step.Id).OrderBy(r => r.Order).ToList();
            }
            ViewBag.Title = workflow.Title;
            ViewBag.Titles = GetTitles();
            return View(workflow);
        }

        public Guid? WorkflowSearch(String title)
        {
            var workflow = from w in db.Workflows
                                where w.Title == title
                                select w;

            if (workflow.Count() > 0)
            {
                return workflow.First().Id;
            }
            return null;
        }

        [HttpPost]
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

        [HttpPost]
        public Guid? SaveStep(Guid workflowId, Guid? stepId, int order)
        {
            Workflow workflow = db.Workflows.Find(workflowId);
            Step step;

            if (!(stepId == Guid.Empty))
            {
                step = db.Steps.Find(stepId);
                if(step != null)
                {
                    step.Order = (short)order;
                }
            }
            else
            {
                step = new Step
                {
                    Id = Guid.NewGuid(),
                    WorkflowId = workflowId,
                    Creation_Date = DateTime.Now,
                    Order = (short)order,
                    Workflow = workflow
                };
                db.Steps.Add(step);
                workflow.Steps.Add(step);
            }

            db.SaveChanges();

            if(step != null)
            {
                return step.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        [HttpPost]
        public Guid? SaveReviewer(Guid stepId, Guid? reviewerId, int order, String role)
        {

            Reviewer reviewer;
            if (!(reviewerId == Guid.Empty))
            {
                reviewer = db.Reviewers.Find(reviewerId);
                if(reviewer != null)
                {
                    reviewer.StepId = stepId;
                    reviewer.Order = (short)order;
                }
            }
            else
            {
                reviewer = new Reviewer
                {
                    Id = Guid.NewGuid(),
                    Order = (short)order,
                    Creation_Date = DateTime.Now,
                    Role = role,
                    StepId = stepId
                };
                db.Reviewers.Add(reviewer);
            }
            db.SaveChanges();

            if(reviewer != null)
            {
                return reviewer.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public void RemoveReviewer(Guid reviewerId)
        {
            Reviewer reviewer = db.Reviewers.Find(reviewerId);
            if(reviewer != null)
            {
                db.Reviewers.Remove(reviewer);
                db.SaveChanges();
            }
        }

        // remove the step
        public void RemoveStep(Guid stepId) {
            // find the step
            Step step = db.Steps.Find(stepId);
            if (step != null) {
                //Query all reviewers associated with the step to be deleted
                IQueryable<Reviewer> query = from r in db.Reviewers
                                                 where r.StepId == stepId
                                                 select r;

                //Convert to a list, otherwise we cannot properly iterate and remove with foreach
                IList<Reviewer> reviewers = query.ToList();
                
                //Delete each reviewer associated with the step
                foreach(Reviewer reviewer in reviewers)
                {
                    RemoveReviewer(reviewer.Id);
                }

                //Remove the step and save changes
                db.Steps.Remove(step);
                db.SaveChanges();
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

        private List<string> GetTitles()
        {
            DbSet<Workflow> workflows = db.Workflows;
            List<string> titles = new List<string>();
            foreach (Workflow w in workflows)
            {
                titles.Add(w.Title);
            }
            return titles;
        }
        
        [HttpPost]
        public String GetReviewer(Guid reviewerId)
        {
            Reviewer reviewer;
            dynamic jsonResult;
            if (!(reviewerId == Guid.Empty))
            {
                reviewer = db.Reviewers.Find(reviewerId);
                reviewer.StepId = Guid.Empty;
                jsonResult = JsonConvert.SerializeObject(reviewer, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            else
            {
                reviewer = null;
                jsonResult = null;
            }
            return jsonResult;
        }
    }
}