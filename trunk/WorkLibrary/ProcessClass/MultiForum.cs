using System;
using System.Collections.Generic;
using System.Text;


namespace WorkLibrary
{
    
    public class MultiForum
    {
        #region "Properties"
       
        public List<HControl> UserName;
        public List<HControl> PassWord;
        public List<HControl> Login;
        public List<HControl> NewThread;
        public List<HControl> Subject;
        public List<HControl> Message;
        public List<HControl> Tags;
        public List<HControl> Mode;
        public List<HControl> Submit;

        #endregion

        public MultiForum(string Type)
        {
            UserName = FieldSetting.GetByField(FieldEnums.UserName, Type);
            PassWord = FieldSetting.GetByField(FieldEnums.Password, Type);
            Login = FieldSetting.GetByField(FieldEnums.Login, Type);
            NewThread = FieldSetting.GetByField(FieldEnums.NewThread, Type);
            Subject = FieldSetting.GetByField(FieldEnums.Subject, Type);
            Message = FieldSetting.GetByField(FieldEnums.Message, Type);
            Tags = FieldSetting.GetByField(FieldEnums.Tags, Type);
            Mode = FieldSetting.GetByField(FieldEnums.Mode, Type);
            Submit = FieldSetting.GetByField(FieldEnums.Submit, Type);
        }

    }
}
