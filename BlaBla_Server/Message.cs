//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlaBla_Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int Id_Message { get; set; }
        public int Id_Sender { get; set; }
        public int Id_Receiver { get; set; }
        public string MessageData { get; set; }
        public System.DateTime SendDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
