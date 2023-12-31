﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Common
{
    public class Procedures
    {
        /// <summary>
        /// ten Proc lay tat ca ban ghi
        /// </summary>
        /// Created by: tmthuc (27/6/2023)
        public static string GET_ALL = "Proc_{0}_GetAll";

        public static string GET_LIST = "Proc_{0}_Filter";
        /// <summary>
        /// ten Proc lay 1 ban ghi theo id
        /// </summary>
        /// Created by: tmthuc (27/6/2023)
        public static string GET_BY_ID = "Proc_{0}_GetById";

        /// <summary>
        /// ten proc them moi 1 ban ghi
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// ten proc cap nhat 1 ban ghi
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// ten proc xoa 1 ban ghi theo id
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public static string DELETE = "Proc_{0}_DeleteById";

        /// <summary>
        /// ten proc xoa nhieu ban ghi
        /// </summary>
        /// Created by: vdtien (20/6/2023)
        public static string DELETE_MULTI = "Proc_{0}_DeleteMulti";
    }
}
