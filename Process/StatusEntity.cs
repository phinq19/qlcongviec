using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace NewProject
{
    public class StatusEntity
    {
        public string Forum;
        public RunStatus Status;
        public string Message;

        public StatusEntity(string _forum, RunStatus _status)
        {
            Forum = _forum;
            Status = _status;
            Message = getStatus();
        }

        public StatusEntity(string _forum, RunStatus _status, string _message)
        {
            Forum = _forum;
            Status = _status;
            Message = _message;
        }

        public string getStatus()
        {
            string status = "";
            switch (Status)
            {
                case RunStatus.ErrUserName:
                    status = "Không tìm thấy textbox user name";
                    break;
                case RunStatus.ErrPassword:
                    status = "Không tìm thấy textbox password";
                    break;
                case RunStatus.ErrLogin:
                    status = "Không tìm thấy button login";
                    break;
                case RunStatus.ErrNoKeyword:
                    status = "Không tìm thấy keyword trong danh sách";
                    break;
                case RunStatus.ErrNewThread:
                    status = "Không tìm thấy link tạo bài viết mới";
                    break;
                case RunStatus.ErrSubject:
                    status = "Không tìm thấy textbox subject";
                    break;
                case RunStatus.ErrMessage:
                    status = "Không tìm thấy textarea message";
                    break;
                case RunStatus.ErrSubmit:
                    status = "Không tìm thấy nút gửi bài viết";
                    break;
                case RunStatus.Error:
                    status = "Lỗi hệ thống";
                    break;
                case RunStatus.ErrTimeOut:
                    status = "Lỗi do trình duyệt";
                    break;
            }
            return status;
        }

        public static DataTable ConvertToTable(List<StatusEntity> status)
        {
            try
            {
                if (status == null) return null;

                DataTable table = new DataTable();
                table.Columns.Add("Url");
                table.Columns.Add("Status");
                table.Columns.Add("Message");
                for (int i = 0; i < status.Count; i++)
                {
                    DataRow row = table.NewRow();
                    row["Url"] = status[i].Forum;
                    row["Status"] = status[i].getStatus();
                    row["Message"] = status[i].Message;

                    table.Rows.Add(row);
                }

                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
