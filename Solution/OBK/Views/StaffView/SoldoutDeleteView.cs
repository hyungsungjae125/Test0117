using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.StaffView
{
    class SoldoutDeleteView
    {
        private WebAPI api;
        private Draw draw;
        private Form parentForm;
        private Hashtable hashtable;
        private ListView listMenu;
        private Button btnSoldoutDelete;

        public SoldoutDeleteView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(665, 250));
            hashtable.Add("point", new Point(10, 10));
            hashtable.Add("name", "주문리스트");
            listMenu = draw.getListView(hashtable, parentForm);
            listMenu.Columns.Add("", 50, HorizontalAlignment.Center);
            listMenu.Columns.Add("메뉴", 600, HorizontalAlignment.Center);
            listMenu.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listMenu.ColumnWidthChanging += ListMenu_ColumnWidthChanging;
            api = new WebAPI();
            api.ListView(Program.serverUrl + "Staff/soldOutDeleteList", listMenu);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(160, 60));
            hashtable.Add("point", new Point(510, 270));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "ok");
            hashtable.Add("text", "품절 제외(취소)");
            hashtable.Add("font", new Font("맑은 고딕", 12, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnSoldoutDelete_click);
            btnSoldoutDelete = draw.getButton1(hashtable, parentForm);
        }

        private void btnSoldoutDelete_click(object o, EventArgs a)
        {
            api = new WebAPI();
            string mName = "";
            bool one = true;

            foreach (ListViewItem listitem in listMenu.Items)
            {
                if (listMenu.Items.Count > 0)
                {
                    for (int i = listMenu.Items.Count - 1; i >= 0; i--)
                    {
                        if (listMenu.Items[i].Checked == true)
                        {
                            mName = listMenu.Items[i].SubItems[1].Text;
                            hashtable = new Hashtable();
                            hashtable.Add("mName", mName);
                            api.Post(Program.serverUrl + "Staff/soldOutDelete", hashtable);
                            listMenu.Items[i].Remove();
                            if (one)
                            {
                                MessageBox.Show("품절취소가 완료되었슴니다.");
                                one = false;
                            }
                        }
                    }
                }
            }
        }

        private void ListMenu_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 메뉴리스트 칼럼 크기 조정 막음
        {
            e.NewWidth = listMenu.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
