using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Data.Entities
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BoxRequest
    {
        [Key]
        public int RequestID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [ForeignKey("RequestType")]
        public int RequestTypeID { get; set; }

        public int RequestBy { get; set; }
        public string NotedBy { get; set; }
        public DateTime DateNeeded { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int Quantity { get; set; }
        public string PartCode { get; set; }
        public string ItemDescription { get; set; }
        public string Material { get; set; }
        public string SpecialInstructions { get; set; }
        public string Illustration { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual RequestType RequestType { get; set; }
    }

    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }
    }

    public class NatureOfProject
    {
        [Key]
        public int ProjectID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public bool NewItem { get; set; }
        public bool ExistingItem { get; set; }
        public bool CustomerSuppliedDrawing { get; set; }
        public bool CustomerSuppliedSample { get; set; }
        public bool CustomerSuppliedProduct { get; set; }
        public string RevisionNumber { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class OtherTest
    {
        [Key]
        public int OtherTestID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string TestType { get; set; }
        public string SpecialRequest { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class PrintingDetails
    {
        [Key]
        public int PrintingID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public int PrintColorCount { get; set; }
        public string PrintingTolerance { get; set; }
        public string PrintProcess { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class QualityCheck
    {
        [Key]
        public int QCID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string QAType { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class RequestStatus
    {
        [Key]
        public int StatusID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string Status { get; set; }

        [ForeignKey("ReviewedByUser")]
        public int? ReviewedBy { get; set; }

        public DateTime ReviewDate { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
        public virtual User ReviewedByUser { get; set; }
    }

    public class RequestType
    {
        [Key]
        public int RequestTypeID { get; set; }

        [Required, StringLength(255)]
        public string TypeName { get; set; }
    }

    public class SalesInCharge
    {
        [Key]
        public int SalesInChargeID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string Name { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class SpecialInstruction
    {
        [Key]
        public int InstructionID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string Description { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class TaskProgress
    {
        [Key]
        public int TaskID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string TaskType { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime DateFinished { get; set; }
        public string Actual { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class Testing
    {
        [Key]
        public int TestID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string TestType { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class TestRequirement
    {
        [Key]
        public int TestRequirementID { get; set; }

        [ForeignKey("BoxRequest")]
        public int RequestID { get; set; }

        public string TestType { get; set; }
        public string TestValue { get; set; }
        public string TestSequence { get; set; }

        public virtual BoxRequest BoxRequest { get; set; }
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }
    }
}