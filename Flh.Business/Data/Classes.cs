//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flh.Business.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Classes
    {
        public string no { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public long creater { get; set; }
        public long updater { get; set; }
        public bool enabled { get; set; }
        public int order_by { get; set; }
        public string name_en { get; set; }
        public string full_name_en { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Admin Admin1 { get; set; }
    }
}
