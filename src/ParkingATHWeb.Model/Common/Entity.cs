﻿using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Model.Common
{
    public abstract class Entity<T> : IEntity<T>
        where T: struct
    {
        [Key]
        public T Id { get; set; }
    }
}
