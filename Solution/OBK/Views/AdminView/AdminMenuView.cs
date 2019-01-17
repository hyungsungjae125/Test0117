using OBK.Forms.AdminForm;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.AdminView
{
    class AdminMenuView
    {
        private Draw draw;
        private Form parentForm, tagetForm;
        private Label lb_main;
        private Button btn1, btn2, btn3;
        private Hashtable hashtable;

        public AdminMenuView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("text", "관리자 모드");
            hashtable.Add("point", new Point(160, 70));
            hashtable.Add("font", new Font("맑은고딕", 45, FontStyle.Bold));
            hashtable.Add("name", "lb_main");
            lb_main = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 80));
            hashtable.Add("point", new Point(50, 250));
            hashtable.Add("color", Color.FromArgb(214, 205, 194));
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "매출 현황");
            hashtable.Add("font", new Font("맑은 고딕", 20, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn1_click);
            btn1 = draw.getButton1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 80));
            hashtable.Add("point", new Point(50, 400));
            hashtable.Add("color", Color.FromArgb(214, 205, 194));
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "메뉴 관리");
            hashtable.Add("font", new Font("맑은 고딕", 20, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn2_click);
            btn2 = draw.getButton1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 80));
            hashtable.Add("point", new Point(50, 540));
            hashtable.Add("color", Color.FromArgb(214, 205, 194));
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "품절 목록");
            hashtable.Add("font", new Font("맑은 고딕", 20, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn3_click);
            btn3 = draw.getButton1(hashtable, parentForm);
        }

        private void btn1_click(object sender, EventArgs e) // 매출 현황
        {
            parentForm.Visible = false;

            tagetForm = new IncomeForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }

        private void btn2_click(object sender, EventArgs e) // 메뉴 관리
        {
            parentForm.Visible = false;

            tagetForm = new MenuSettingForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }

        private void btn3_click(object sender, EventArgs e) // 품절 목록
        {
            parentForm.Visible = false;

            tagetForm = new SoldoutListForm();
            tagetForm.StartPosition = FormStartPosition.CenterParent;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);
            tagetForm.Show();
        }

        private void Exit_click(object sender, FormClosedEventArgs e)   // 부모 폼 닫음
        {
            parentForm.Close();
        }
    }
}
