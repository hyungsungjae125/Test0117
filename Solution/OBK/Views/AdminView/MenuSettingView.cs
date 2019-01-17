using OBK.Forms.AdminForm;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.AdminView
{
    class MenuSettingView
    {
        private Hashtable hashtable;
        private Draw draw;
        private Form parentForm, tagetForm;
        private Button btnAdd, btnEdit, btnDelete, btnMain;
        private Panel all, contents;

        public MenuSettingView(Form parentForm)
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
            hashtable.Add("name", "btnAdd");
            hashtable.Add("text", "메뉴\n추가");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnAdd_Click);
            btnAdd = draw.getButton1(hashtable, all);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 100));
            hashtable.Add("point", new Point(20, 140));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btnEdit");
            hashtable.Add("text", "메뉴\n수정");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnEdit_Click);
            btnEdit = draw.getButton1(hashtable, all);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 100));
            hashtable.Add("point", new Point(20, 260));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btnDelete");
            hashtable.Add("text", "메뉴\n삭제");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnDelete_Click);
            btnDelete = draw.getButton1(hashtable, all);


            hashtable = new Hashtable();
            hashtable.Add("size", new Size(140, 60));
            hashtable.Add("point", new Point(20, 480));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btnMain");
            hashtable.Add("text", "메인화면");
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnMain_Click);
            btnMain = draw.getButton1(hashtable, all);

            hashtable = new Hashtable();
            hashtable.Add("type", "");
            hashtable.Add("size", new Size(680, 520));
            hashtable.Add("point", new Point(180, 20));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "head");
            contents = draw.getPanel(hashtable, all);
            contents.BorderStyle = BorderStyle.FixedSingle;

            if (tagetForm != null) tagetForm.Dispose();
            tagetForm = draw.getMdiForm(parentForm, new MenuAddForm(), contents);
            tagetForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnEdit.BackColor = Color.LightGray;
            btnDelete.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm, new MenuAddForm(), contents);
            tagetForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.LightGray;
            btnEdit.BackColor = Color.FromArgb(46, 204, 113);
            btnDelete.BackColor = Color.LightGray;

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm, new MenuEditForm(), contents);
            tagetForm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.LightGray;
            btnEdit.BackColor = Color.LightGray;
            btnDelete.BackColor = Color.FromArgb(46, 204, 113);

            if (tagetForm != null) tagetForm.Dispose();

            tagetForm = draw.getMdiForm(parentForm, new MenuDeleteForm(), contents);
            tagetForm.Show();
        }


        private void btnMain_Click(object sender, EventArgs e)
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
