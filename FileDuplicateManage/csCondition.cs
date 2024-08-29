using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDuplicateManage
{
    public class csCondition
    {
        public string conditionText { get; set; }
        public bool use { get; set; }

        public csCondition()
        {
            conditionText = "";
            use = false;
        }

        public csCondition(string text)
        {
            conditionText = text;
            use = true;
        }
    }
}
