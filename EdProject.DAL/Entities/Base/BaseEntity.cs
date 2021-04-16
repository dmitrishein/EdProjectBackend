﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EdProject.DAL.Entities.Base
{
    public abstract class BaseEntity
    {  
        [Key]
        public long Id { get; set; }
        private DateTime _creationTime { get; set; }
        public bool IsRemoved { get; set; }

        public BaseEntity()
        {
            IsRemoved = false;
            _creationTime = DateTime.Now;
        }
    }
}
