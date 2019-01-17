using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OBK.Views.AdminView
{
    class MenuDeleteView
    {
        private WebAPI api;
        private Draw draw;
        private Form parentForm;
        private ListView listCategory, listMenu,listView;
        private Button btnMenuDelete;
        private Label lblTitle;
        private Hashtable hashtable;

        public MenuDeleteView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("text", "메뉴 삭제");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(20, 10));
            hashtable.Add("font", new Font("고딕", 30, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblTitle = draw.getLabel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(150, 350));
            hashtable.Add("point", new Point(20, 70));
            hashtable.Add("name", "listCategory");
            hashtable.Add("click", (MouseEventHandler)listCategory_click);
            listCategory = draw.getListView1(hashtable, parentForm);
            listCategory.Columns.Add("", 0, HorizontalAlignment.Center);
            listCategory.Columns.Add("카테고리", 146, HorizontalAlignment.Center);
            listCategory.ColumnWidthChanging += ListCategory_ColumnWidthChanging;
            api = new WebAPI();
            api.ListView(Program.serverUrl+"category/select", listCategory);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(490, 350));
            hashtable.Add("point", new Point(170, 70));
            hashtable.Add("name", "listMenu");
            listMenu = draw.getListView(hashtable, parentForm);
            listMenu.Columns.Add("", 20, HorizontalAlignment.Center);
            listMenu.Columns.Add("메뉴", 445, HorizontalAlignment.Center);
            listMenu.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listMenu.ColumnWidthChanging += ListMenu_ColumnWidthChanging;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 50));
            hashtable.Add("point", new Point(510, 440));
            hashtable.Add("color", Color.FromArgb(246, 246, 246));
            hashtable.Add("name", "btnAdd");
            hashtable.Add("text", "메뉴 삭제");
            hashtable.Add("font", new Font("맑은 고딕", 15, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnMenuDelete_click);
            btnMenuDelete = draw.getButton1(hashtable, parentForm);
        }

        private void btnMenuDelete_click(object sender, EventArgs e)    // 메뉴 삭제
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
                            api.Post(Program.serverUrl + "Menu/delete", hashtable);
                            if (one)
                            {
                                MessageBox.Show("메뉴를 삭제했습니다.");
                                one = false;
                            }
                            listMenu.Items[i].Remove();
                        }
                    }
                }
            }

        }

        private void listCategory_click(object sender, MouseEventArgs e)    // 카테고리 선택하면 해당 매뉴 갖고오기
        {
            api = new WebAPI();

            listView = (ListView)sender;
            ListView.SelectedListViewItemCollection itemGroup = listView.SelectedItems;
            ListViewItem cNoitem = itemGroup[0];

            string cNo = cNoitem.SubItems[0].Text;

            hashtable = new Hashtable();
            hashtable.Add("cNo", cNo);
            api.PostListview(Program.serverUrl + "Menu/nameSelect", hashtable, listMenu);
        }

        private void ListMenu_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 메뉴리스트 칼럼 크기 조정 막음
        {
            e.NewWidth = listMenu.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void ListCategory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 카테고리 리트스 칼럼크기 막음
        {
            e.NewWidth = listCategory.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
