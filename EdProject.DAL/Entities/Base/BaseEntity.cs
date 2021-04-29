using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdProject.DAL.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRemoved { get; set; }
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
