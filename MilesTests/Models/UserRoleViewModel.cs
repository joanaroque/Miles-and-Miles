using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilesBackOffice.Web.Models
{
    public class UserRoleViewModel : EditUserViewModel
    {
        public string UserId { get; set; }



        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }




        public IEnumerable<string> Roles { get; set; }
    }
}
