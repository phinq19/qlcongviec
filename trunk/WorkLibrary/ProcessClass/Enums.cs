using System;
using System.Collections.Generic;
using System.Text;

namespace WorkLibrary
{
    public class StatusObj
    {
        public string Status;
        public string Value;
        public string Message;
        
    }
    public class NumCode
    {
        public const string WEB = "WEB";
        public const string POS = "POS";
        public const string UP = "UP";
        public const string UPEX = "UPEX";

    }
    public class ControlType
    {
        public const string TextBox = "textbox";
        public const string TextArea = "textarea";
        public const string Button = "button";
        public const string AHref = "href0";
        public const string AHrefNoText = "href";
        public const string Div = "div";
        public const string Link = "link";
    }

    public class AttributeType
    {
        public const string Id = "id";
        public const string Class = "class";
        public const string Name = "name";
        public const string Value = "value";
        public const string Text = "text";
        public const string Href = "href";
    }
    public class ProcessType
    {
        public const string Goto = "goto";
        public const string Click = "click";
        public const string Fill = "fill";
        public const string Wait = "wait";
        public const string ClickConfirm = "clickconfirm";
    }
    public enum Status
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
    }

    public class FieldEnums
    {
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string Login = "Login";
        public const string NewThread = "NewThread";
        public const string Subject = "Subject";
        public const string Message = "Message";
        public const string Mode = "Mode";
        public const string Tags = "Tags";
        public const string Submit = "Submit";
        public const string SubmitUp = "SubmitUp";
    }

    public enum RunStatus
    {
        Empty = 0,
        Success = 1,
        Error = 2,
        ErrUserName = 3,
        ErrPassword = 4,
        ErrLogin = 5,
        ErrNewThread = 6,
        ErrSubject = 7,
        ErrMessage = 8,
        ErrTag = 9,
        ErrMode = 10,
        ErrSubmit = 11,
        ErrNoKeyword = 12,
        ErrLink = 13,
        ErrUrl = 14,
        ErrTimeOut,
    }
}
