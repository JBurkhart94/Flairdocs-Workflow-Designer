//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Step
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Step()
        {
            this.Reviewers = new HashSet<Reviewer>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid WorkflowId { get; set; }
        public System.DateTime Creation_Date { get; set; }
    
        public virtual Workflow Workflow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviewer> Reviewers { get; set; }
    }
}
