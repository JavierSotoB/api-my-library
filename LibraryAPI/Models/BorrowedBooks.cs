//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BorrowedBooks
    {
        public int borrowed_book_id { get; set; }
        public int book_id { get; set; }
        public int user_id { get; set; }
        public System.DateTime borrowedDate { get; set; }
        public System.DateTime returnedDate { get; set; }
        public bool ontime { get; set; }
        public bool status { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Books Books { get; set; }
    }
}
