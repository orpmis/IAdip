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
    
    public partial class messages
    {
        public int id { get; set; }
        public int id_sender { get; set; }
        public int id_conversation { get; set; }
        public string message { get; set; }
        public System.DateTime send_time { get; set; }
        public byte isRead { get; set; }
    
        public virtual conversation conversation { get; set; }
        public virtual users users { get; set; }
    }
}
