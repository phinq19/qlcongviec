using System;
using System.Collections.Generic;
using System.Text;

namespace WorkLibrary
{
    public class Entry
    {
        public string Subject;
        public string Message;
        public string Tags;

        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message))
                return true;
            return false;
        }
    }
}
