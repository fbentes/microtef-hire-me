//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StonePaymentsServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public System.Guid Id { get; set; }
        public System.Guid Card { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public byte Number { get; set; }
    
        public virtual Card Card1 { get; set; }
    }
}