using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;


namespace addressbook_tests_white
{
    public class HelperBase
    {
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
        }
    }
}