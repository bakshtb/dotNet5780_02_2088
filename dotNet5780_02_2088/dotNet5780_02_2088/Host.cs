using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace dotNet5780_02_2088
{
    class Host : IEnumerable
    {
        int HostKey;
        public List<HostingUnit> HostingUnitCollection { get;private set; }

        public Host(int id , int num)
        {
            HostingUnitCollection = new List<HostingUnit>();
            HostKey = id;
            for (int i = 0; i < num; i++)
            {
                HostingUnitCollection.Add(new HostingUnit());
            }
        }
        public override string ToString()
        {
            string ret = "Host Key: " + this.HostKey + "\n";

            for (int i = 0; i < HostingUnitCollection.Count ; i++)
            {
                ret += HostingUnitCollection[i].ToString();
            }
            return ret;
        }

        private long SubmitRequest(GuestRequest guestReq)
        {
            int ret = -1;
            bool isFound = false;
            for (int i = 0; i < HostingUnitCollection.Count && !isFound; i++)
            {
                if (HostingUnitCollection[i].ApproveRequest(guestReq))
                {
                    isFound = true;
                    ret = HostingUnitCollection[i].HostingUnitKey;
                }
            }
            return ret;
        }
        public int GetHostAnnualBusyDays()
        {
            int sum = 0;

            for (int i = 0; i < HostingUnitCollection.Count; i++)
            {
                sum += HostingUnitCollection[i].GetAnnualBusyDays();
            }
            return sum;
        }
        public void SortUnits()
        {
            HostingUnitCollection.Sort();
        }

        public bool AssignRequests(params GuestRequest[] listReq)
        {
            bool ret = true;

            for (int i = 0; i < listReq.Length && ret; i++)
            {
                long Key = (long)SubmitRequest(listReq[i]);
                if (Key != -1)
                {
                    bool isFound = false;
                    for (int j = 0; j < HostingUnitCollection.Count && !isFound; j++)
                    {
                        if (HostingUnitCollection[j].HostingUnitKey == Key)
                        {
                            isFound = true;
                            HostingUnitCollection[j].ApproveRequest(listReq[i]);
                        }
                    }
                }
                else
                    ret = false;
            }
            return ret;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)HostingUnitCollection).GetEnumerator();
        }

        public HostingUnit this[int i]
        {
            get { return HostingUnitCollection[i]; }
            set { HostingUnitCollection[i] = value; }
        }
    }
}
