using OBK.Forms.AdminForm;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.AdminView
{
    class IncomeView
    {

        private Draw draw;
        private Form parentForm, tagetForm;
        private Panel contents, all;
        private Button btn_month, btn_menu, btn_mainwindow;
        private Hashtable hashtable;

        public IncomeView(Form parentForm)
        {
            this.parentForm = parentForm;
            //db = new MYsql();
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("type", "");
            hashtable.Add("size", new Size(900, 600));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "head");
            all = draw.getPanel(hashtable, parentForm);
   
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 100));
            hashtable.Add("point", new Point(20, 20));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn_month");
            hashtable.Add("text", "월별\n매출");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn_month_click);
            btn_month = draw.getButton1(hashtable, all);
            btn_month.BackColor = Color.FromArgb(46, 204, 113);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 100));
            hashtable.Add("point", new Point(20, 140));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn_menu");
            hashtable.Add("text", "메뉴별\n매출");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn_menu_click);
            btn_menu = draw.getButton1(hashtable, all);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 60));
            hashtable.Add("point", new Point(20, 480));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn_mainwindow");
            hashtable.Add("text", "메인화면");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btn_mainwindow_click);
            btn_mainwindow = draw.getButton1(hashtable, all);

            hashtable = new Hashtable();
            hashtable.Add("type", "");
            hashtable.Add("size", new Size(680, 520));
            hashtable.Add("point", new Point(180, 20));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "head");
            contents = draw.getPanel(hashtable, all);
            contents.BorderStyle = BorderStyle.FixedSingle;

            if (tagetForm != null) tagetForm.Dispose();
            tagetForm = draw.getMdiForm(parentForm, new MonthlyForm(), contents);
            tagetForm.Show();
        }

        private void btn_month_click(object sender, EventArgs e)
        {
            btn_month.BackColor = Color.FromArgb(46, 204, 113);
            btn_menu.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();
            tagetForm = draw.getMdiForm(parentForm, new MonthlyForm(), contents);
            tagetForm.Show();
        }

        private void btn_menu_click(object sender, EventArgs e)
        {
            btn_menu.BackColor = Color.FromArgb(46, 204, 113);
            btn_month.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();
            tagetForm = draw.getMdiForm(parentForm, new MenuIncomeForm(), contents);
            tagetForm.Show();
        }

        private void btn_mainwindow_click(object sender, EventArgs e)
        {
            parentForm.Visible = false;
            tagetForm = new AdminMenuForm();
            tagetForm.StartPosition = parentForm.StartPosition;
            tagetForm.FormClosed += new FormClosedEventHandler(Exit_click);

            tagetForm.Show();
        }

        private void Exit_click(object sender, FormClosedEventArgs e)
        {
            parentForm.Close();
        }
    }
}
