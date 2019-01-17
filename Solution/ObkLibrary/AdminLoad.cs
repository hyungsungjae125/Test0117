using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObkLibrary
{
    public class AdminLoad
    {
        private Form parentForm;
        private object oDB;

        public AdminLoad(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        public AdminLoad(Form parentForm, object oDB)
        {
            this.parentForm = parentForm;
            this.oDB = oDB;
        }

        public Form GetHandler(string viewName)
        {
            switch (viewName)
            {
                case "adminmenu":
                    return GetAdminMenuLoad();
                case "login":
                    return GetLoginLoad();
                case "income":
                    return GetIncomeLoad();
                case "menuadd":
                    return GetMenuAddLoad();
                case "menudelete":
                    return GetMenuDeleteLoad();
                case "menuedit":
                    return GetMenuEditLoad();
                case "menuincome":
                    return GetMenuIncomeLoad();
                case "menusetting":
                    return GetMenuSettingLoad();
                case "monthly":
                    return GetMonthlyLoad();
                case "soldoutlist":
                    return GetSoldoutListLoad();
                default:
                    return null;
            }
        }
        private Form GetLoginLoad()    // LoginForm(로그인)
        {
            parentForm.Size = new Size(400, 500);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "메뉴추가화면";
            parentForm.BackColor = Color.White;
            return parentForm;
        }

        private Form GetAdminMenuLoad()    // AdminMenuForm(관리자메뉴)
        {
            parentForm.Size = new Size(600, 700);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "메인화면";
            return parentForm;
        }

        private Form GetIncomeLoad()  // IncomeForm(매출현황)
        {
            parentForm.Size = new Size(900, 600);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "매출메인화면";
            return parentForm;
        }

        private Form GetMonthlyLoad()      // MonthlyForm(월별매출)
        {
            parentForm.Size = new Size(680, 520);
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "월별매출화면";
            parentForm.BackColor = Color.White;
            return parentForm;
        }

        private Form GetMenuIncomeLoad()   // MenuIncomeForm(메뉴별매출)
        {
            parentForm.Size = new Size(680, 520);
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "메뉴별매출화면";
            parentForm.BackColor = Color.White;
            return parentForm;
        }

        private Form GetMenuSettingLoad()  // MenuSettingForm(메뉴관리)
        {
            parentForm.Size = new Size(900, 600);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            return parentForm;
        }

        private Form GetMenuAddLoad()      // MenuAddForm(메뉴추가)
        {
            parentForm.Size = new Size(680, 520);
            parentForm.IsMdiContainer = false;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.BackColor = Color.White;
            return parentForm;
        }

        private Form GetMenuEditLoad()     // MenuEditForm(메뉴수정)
        {
            parentForm.Size = new Size(680, 520);
            parentForm.IsMdiContainer = false;
            parentForm.BackColor = Color.White;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            return parentForm;
        }

        private Form GetMenuDeleteLoad()   // MenuDeleteForm(메뉴삭제)
        {
            parentForm.Size = new Size(680, 520);
            parentForm.BackColor = Color.White;
            parentForm.IsMdiContainer = false;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            return parentForm;
        }

        private Form GetSoldoutListLoad()  // SoldoutListForm(품절목록)
        {
            parentForm.Size = new Size(900, 600);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.BackColor = Color.White;
            parentForm.Text = "품절목록메인화면";
            return parentForm;
        }
    }
}
