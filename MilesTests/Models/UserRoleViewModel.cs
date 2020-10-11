﻿using System.Collections.Generic;

namespace MilesBackOffice.Web.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }



        public string Name { get; set; }



        public string UserName { get; set; }



        public bool IsSelected { get; set; }



        public IEnumerable<string> Roles { get; set; }
    }
}
