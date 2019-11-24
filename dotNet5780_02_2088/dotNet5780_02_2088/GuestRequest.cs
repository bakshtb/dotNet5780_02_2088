using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet5780_02_2088
{
    class GuestRequest
    {
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsApproved { get; set; }

        public GuestRequest()
        {
            EntryDate = new DateTime ();
            ReleaseDate = new DateTime();
        }

        public override string ToString()
        {
            return "EntryDate:" + EntryDate.ToString("dd.MM.yyyy") + "\n"+
                   "ReleaseDate: " + ReleaseDate.ToString("dd.MM.yyyy") + "\n"+
                   "IsApproved: " + IsApproved.ToString() +"\n";
        }
    }
}
