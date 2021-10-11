using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ups.Nenad.DataTypes
{

    public class GetUsersResponse
    {
        public Meta Meta { get; set; }
        public List<User> Data { get; set; }
    }


}
