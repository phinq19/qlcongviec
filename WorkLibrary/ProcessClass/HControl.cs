using System;
using System.Collections.Generic;
using System.Text;

namespace WorkLibrary
{
    public class HControl
    {
        public string Value;
        public string Attribute;
        public string Control;
        public bool IsDefault;

        public HControl()
        {
            IsDefault = true;
        }

        public void Init(string field, string tfield)
        {
            Value = field;
            Attribute = tfield;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Attribute))
                return false;
            return true;
        }

        public bool IsValid2()
        {
            if (string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Attribute) || string.IsNullOrEmpty(Control))
                return false;
            return true;
        }
    }
}
