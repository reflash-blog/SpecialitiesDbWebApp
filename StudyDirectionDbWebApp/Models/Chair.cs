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
    
    public partial class Chair
    {
        public Chair()
        {
            this.Specialities = new HashSet<Speciality>();
        }
    
        public int FacultyId { get; set; }
        public int ChairId { get; set; }
        [MaxLength(50)]
        public string ChairName { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Speciality> Specialities { get; set; }
    }
}