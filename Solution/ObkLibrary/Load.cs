using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObkLibrary
{
    public class Load
    {
        private Form parentForm;

        public Load(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        public Form GetHandler(string viewName)
        {
            switch (viewName)
            {
                case "main":
                    return GetMainLoad();
                case "bill":
                    return GetBillLoad();
                case "choice":
                    return GetChoiceLoad();
                case "menu":
                    return GetMenuLoad();
                case "pay":
                    return GetPayLoad();
                case "user":
                    return GetUserLoad();

                default:
                    return null;
            }
        }

        private Form GetMainLoad()
        {
            parentForm.Size = new Size(500, 400);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "메인화면";
            return parentForm;
        }
        private Form GetBillLoad()    // 영수증
        {
            parentForm.Size = new Size(500, 500);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            return parentForm;
        }
        private Form GetChoiceLoad()
        {
            parentForm.Size = new Size(450, 550);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "선택화면";
            parentForm.BackColor = Color.White;
            return parentForm;
        }
        private Form GetMenuLoad()
        {
            parentForm.Size = new Size(1000, 300);
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.AutoScroll = true;
            parentForm.Text = "메뉴";
            return parentForm;
        }
        private Form GetPayLoad()      // 결제
        {
            parentForm.IsMdiContainer = false;
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.BackColor = Color.White;
            parentForm.Size = new Size(500, 400);
            return parentForm;
        }
        private Form GetUserLoad()
        {
            parentForm.Size = new Size(800, 900);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "사용자화면";
            return parentForm;
        }

    }
}
