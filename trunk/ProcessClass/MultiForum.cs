using System;
using System.Collections.Generic;
using System.Text;


namespace NewProject
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

        public MultiForum()
        {
            //UserName = new List<HControl>();
            //PassWord = new List<HControl>();
            //Login = new List<HControl>();
            //NewThread = new List<HControl>();
            //Subject = new List<HControl>();
            //Message = new List<HControl>();
            //Tags = new List<HControl>();
            //Mode = new List<HControl>();
            //Submit = new List<HControl>();
            UserName = FieldSetting.GetByField(FieldEnums.UserName);
            PassWord = FieldSetting.GetByField(FieldEnums.Password);
            Login = FieldSetting.GetByField(FieldEnums.Login);
            NewThread = FieldSetting.GetByField(FieldEnums.NewThread);
            Subject = FieldSetting.GetByField(FieldEnums.Subject);
            Message = FieldSetting.GetByField(FieldEnums.Message);
            Tags = FieldSetting.GetByField(FieldEnums.Tags);
            Mode = FieldSetting.GetByField(FieldEnums.Mode);
            Submit = FieldSetting.GetByField(FieldEnums.Submit);
        }

        //public void Init()
        //{
        //    UserName = FieldSetting.GetByField(FieldEnums.UserName);
        //    PassWord = FieldSetting.GetByField(FieldEnums.Password);
        //    Login = FieldSetting.GetByField(FieldEnums.Login);
        //    NewThread = FieldSetting.GetByField(FieldEnums.NewThread);
        //    Subject = FieldSetting.GetByField(FieldEnums.Subject);
        //    Message = FieldSetting.GetByField(FieldEnums.Message);
        //    Tags = FieldSetting.GetByField(FieldEnums.Tags);
        //    Mode = FieldSetting.GetByField(FieldEnums.Mode);
        //    Submit = FieldSetting.GetByField(FieldEnums.Submit);
        //}
    }
}
