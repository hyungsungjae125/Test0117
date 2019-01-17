using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.StaffView
{
    class SoldoutAddView
    {
        private WebAPI api;
        private Draw draw;
        private Form parentForm;
        private Hashtable hashtable;
        private ListView listCategory, listMenu;
        private Button btnSoldoutAdd;
        private string cNo;

        public SoldoutAddView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(150, 250));
            hashtable.Add("point", new Point(10, 10));
            hashtable.Add("name", "listCategory");
            hashtable.Add("click", (MouseEventHandler)listCategory_click);
            listCategory = draw.getListView1(hashtable, parentForm);
            listCategory.Columns.Add("", 0, HorizontalAlignment.Center);
            listCategory.Columns.Add("카테고리", 146, HorizontalAlignment.Center);
            listCategory.ColumnWidthChanging += ListCategory_ColumnWidthChanging;
            api = new WebAPI();
            api.ListView(Program.serverUrl + "category/select", listCategory);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(500, 250));
            hashtable.Add("point", new Point(175, 10));
            hashtable.Add("name", "listMenu");
            listMenu = draw.getListView(hashtable, parentForm);
            listMenu.Columns.Add("", 25, HorizontalAlignment.Center);
            listMenu.Columns.Add("메뉴", 435, HorizontalAlignment.Center);
            listMenu.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listMenu.ColumnWidthChanging += ListMenu_ColumnWidthChanging;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(160, 60));
            hashtable.Add("point", new Point(510, 270));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "ok");
            hashtable.Add("text", "품절추가");
            hashtable.Add("font", new Font("맑은 고딕", 12, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnSoldoutAdd_click);
            btnSoldoutAdd = draw.getButton1(hashtable, parentForm);
        }

        private void btnSoldoutAdd_click(object o, EventArgs a)
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
                            api.Post(Program.serverUrl + "Staff/soldOutAdd", hashtable);
                            listMenu.Items[i].Remove();
                            if (one)
                            {
                                MessageBox.Show("품절추가 완료");
                                one = false;
                            }
                        }
                    }
                }
            }
        }

        private void listCategory_click(object sender, MouseEventArgs e)
        {
            api = new WebAPI();

            ListView listView = (ListView)sender;
            ListView.SelectedListViewItemCollection itemGroup = listView.SelectedItems;
            ListViewItem cNoitem = itemGroup[0];

            cNo = cNoitem.SubItems[0].Text;

            hashtable = new Hashtable();
            hashtable.Add("cNo", cNo);
            api.PostListview(Program.serverUrl + "Staff/soldOutAddList", hashtable, listMenu);
        }

        private void ListCategory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 카테고리 리트스 칼럼크기 막음
        {
            e.NewWidth = listCategory.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void ListMenu_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 메뉴리스트 칼럼 크기 조정 막음
        {
            e.NewWidth = listMenu.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
