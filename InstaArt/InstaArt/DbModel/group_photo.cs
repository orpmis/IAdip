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
    
    public partial class group_photo
    {
        public int id { get; set; }
        public int id_group { get; set; }
        public int id_photo { get; set; }
    
        public virtual group group { get; set; }
        public virtual photos photos { get; set; }
    }
}