//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Baocao_chuyende.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageBlog
    {
        public int id { get; set; }
        public Nullable<int> idBlog { get; set; }
        public string imageBlog1 { get; set; }
    
        public virtual Blog Blog { get; set; }
    }
}
