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

        /*
         * Returns the landing page of the workflow designer
         * */
        public ActionResult Index()
        {
            ViewBag.Title = "Workflow Designer";
            // GetTitles see below
            ViewBag.Titles = GetTitles();
            return View();
        }

        /*
         * Returns the view for an individual workflow instance.  This page is used to edit a workflow.
         * */
        //@param id: Guid of the workflow
        public ActionResult Workflow(Guid id)
        {
            //Find the workflow
            Workflow workflow = db.Workflows.Find(id);
            if (workflow != null)
            {
                //Order the steps and reviewers properly
                workflow.Steps = workflow.Steps.OrderBy(step => step.Order).ToList();
                foreach (Step step in workflow.Steps)
                {
                    step.Reviewers = db.Reviewers.Where(r => r.StepId == step.Id).OrderBy(r => r.Order).ToList();
                }
                ViewBag.Title = workflow.Title;
                ViewBag.Titles = GetTitles();
                return View(workflow);
            }
            return null;
        }

        /*
         * Returns the workflowId for a given workflow title.  Can be used to assist with searching.
         * */
         //@param title: Title of a workflow
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
        /*
         * Method to create a new workflow within the database. The title should be unique.
         * */
         //@param title: A unique title for the new workflow
         //@param description: A description of the workflow
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

        /*
         * Method to save/update a particular step in a workflow.
         * */
         //@param workflowId: A valid workflowId to associate the step
         //@param stepId: A valid stepId if it already exists, or the Empty Guid
         //@param order: The order in which the step appears in the workflow
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
        /*
         * Method to save/update a particular reviewer in a workflow step.
         * */
        //@param stepId: A valid stepId to associate the reviewer
        //@param reviewerId: A valid reviewerId if it already exists, or the Empty Guid
        //@param order: The order in which the reviewer appears in the step
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

        /*
         * Method to remove a reviewer from a workflow step.
         * */
         //@param reviewerId: A valid reviewerId of a reviewer to be removed.
        public void RemoveReviewer(Guid reviewerId)
        {
            Reviewer reviewer = db.Reviewers.Find(reviewerId);
            if(reviewer != null)
            {
                db.Reviewers.Remove(reviewer);
                db.SaveChanges();
            }
        }

        /*
         * Method to remove a step from a workflow.  Consequently all reviewers within that step will also be removed.
         * */
        //@param stepId: A valid stepId of a step to be removed.
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

        /*
         * Check if a title already exists for some workflow in the system.
         * */
         //@param title: A title to check if exists.
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


        // this method will return all titles in the workflow table 
        // This list of string will be passed to front-end, at the initialization of webpage
        // searching for workflow could be done locally then
        // Every time a new workflow is added, local string will be updated correspondingly
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
        
        /*
         * Method to return a reviewer instance to the front end as a JSON Object.  Used for attribute extraction
         * */
         //@param reviewerId: A valid reviewerId for the reviewer to retrieve.
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