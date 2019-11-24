using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet5780_02_2088
{
    class HostingUnit : IComparable
    {
        static int stSerialKey = 10000000;
        public int HostingUnitKey { get; private set; }
        bool[,] Diary; //diary of nights, false = Catch

        public HostingUnit()
        {
            HostingUnitKey = stSerialKey++;
            Diary = new bool[12, 31];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    Diary[i, j] = true;
                }
            }
        }

        public override string ToString()
        {
            string ret = "";
            bool isStart = false;
            ret+="Hosting unit serial key: " + HostingUnitKey +"\n\n";
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (!Diary[i, j] && !isStart)
                    {
                        isStart = true;
                        ret += "Start: "+ (j + 1)+"."+ (i + 1)+"\t"; // i+1 because i get valuse 0..11 [same to j]
                    }
                    if (Diary[i, j] && isStart)
                    {
                        isStart = false;
                        ret += "End: " + (j + 1) + "." + (i + 1) + "\n";
                    }
                }
            }
            ret += "\n----------------------\n";

            return ret;
        }
        public bool ApproveRequest(GuestRequest guestReq)
        {
            bool IsApproved = true;
            int sumOfNights = guestReq.ReleaseDate.Day - guestReq.EntryDate.Day + 31 * (guestReq.ReleaseDate.Month - guestReq.EntryDate.Month);
            int d = guestReq.EntryDate.Day - 1, m = guestReq.EntryDate.Month - 1; //because the index in the matrix

            for (int i = 0; i < sumOfNights && IsApproved; i++)
            {
                if (!Diary[m, d++])
                {
                    IsApproved = false;
                }
                if (d >= 31)// index in matrix in days is from 0 to 30
                {
                    d = 0; // index in matrix begin in 0
                    m++;
                }
            }
            d = guestReq.EntryDate.Day - 1;
            m = guestReq.EntryDate.Month - 1; 
            for (int i = 0; i < sumOfNights; i++)
            {
                Diary[m, d++] = false;
                if (d >= 31)// index in matrix in days is from 0 to 30
                {
                    d = 0; // index in matrix begin in 0
                    m++;
                }
            }

            guestReq.IsApproved = IsApproved;
            return IsApproved;
        }

        public int GetAnnualBusyDays()
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (Diary[i, j])
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
        public float GetAnnualBusyPercentage()
        {
            return ((float)GetAnnualBusyDays() / (12 * 31)) * 100;
        }

        public int CompareTo(Object Obj)
        {
            return this.GetAnnualBusyDays().CompareTo(((HostingUnit)Obj).GetAnnualBusyDays());
        }
    }
}
