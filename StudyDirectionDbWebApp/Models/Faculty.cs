//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace StudyDirectionDbWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Faculty
    {
        public Faculty()
        {
            this.Chairs = new HashSet<Chair>();
        }
    
        public int FacultyId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    
        public virtual ICollection<Chair> Chairs { get; set; }
    }
}