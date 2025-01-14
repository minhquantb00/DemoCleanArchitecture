﻿using DemoApiDotNet.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApiDotNet.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateTime { get; set; }
        [MaybeNull]
        public DateTime UpdateTime { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<Permission>? Users { get; set; }
        public ConstantEnums.UserStatusEnum UserStatus { get; set; } = ConstantEnums.UserStatusEnum.UnActivated;
    }
}
