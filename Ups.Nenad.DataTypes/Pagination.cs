using System;

namespace Ups.Nenad.DataTypes
{


    public class Pagination
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public Links Links { get; set; }
    }








}
