﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flairdocs_Workflow_Designer.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WorkflowContext : DbContext
    {
        public WorkflowContext()
            : base("name=WorkflowContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Transition> Transitions { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
    }
}
