//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InstaArt.DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class conversation_members
    {
        public int id { get; set; }
        public int id_conversation { get; set; }
        public int id_member { get; set; }
    
        public virtual conversation conversation { get; set; }
        public virtual users users { get; set; }
    }
}
