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
    
    public partial class VoiceHistory
    {
        public int Id_VoiceHistory { get; set; }
        public int Id_Caller { get; set; }
        public int Id_Receiver { get; set; }
        public System.DateTime CallDate { get; set; }
        public System.TimeSpan Duration { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}