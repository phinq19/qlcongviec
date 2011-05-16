using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkLibrary
{
    class CustomAttributes : System.Attribute
    {
        public string TableName;
        public string PrimaryKey;
        public bool Identity;

        public CustomAttributes()
        {
        } 
    }
}
