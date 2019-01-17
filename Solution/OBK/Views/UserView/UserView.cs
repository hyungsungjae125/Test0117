using OBK.Forms;
using ObkLibrary;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace OBK.Views
{
    public class UserView
    {
        private WebAPI api;
        private Panel head, menu, bottom;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;
        private Draw draw;
        private Button btn1, btn2, btn3, btn4, btn11, btn12, btn13;
        private ListView lv;
        private Label label;
        private int menuclick = 1;
        private string selectOrder = "";

        public UserView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            api = new WebAPI();
            Program.maxoNum = api.MaxoNum(Program.serverUrl + "orderlist/selectMaxoNum");
            //=====panel 선언부분========head,menus,bottom
            hashtable = new Hashtable();
            hashtable.Add("type", "");
            hashtable.Add("size", new Size(900, 100));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.LightYellow);
            hashtable.Add("name", "head");
            head = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(785, 500));
            hashtable.Add("point", new Point(0, 100));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "menus");
            menu = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(900, 300));
            hashtable.Add("point", new Point(0, 600));
            hashtable.Add("color", Color.LightYellow);
            hashtable.Add("name", "bottom");
            bottom = draw.getPanel(hashtable, parentForm);

            //=========head패널에 버튼부분==============
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(160, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("click", (EventHandler)btn_click);
            hashtable.Add("font", new Font("맑은 고딕",15,FontStyle.Regular));
            api = new WebAPI();
            ArrayList buttonlist = api.CategoryButton(Program.serverUrl + "category/select", hashtable);
            for (int i = 0; i < buttonlist.Count; i++)
            {
                Hashtable ht = (Hashtable)buttonlist[i];
                switch (i)
                {
                    case 0:
                        
                        btn1 = draw.getButton1(ht, head);
                        break;
                    case 1:
                        btn2 = draw.getButton1(ht, head);
                        break;
                    case 2:
                        btn3 = draw.getButton1(ht, head);
                        break;
                    case 3:
                        btn4 = draw.getButton1(ht, head);
                        break;
                    default: break;
                }

            }
            //============== bottom패널에 리스트뷰와 버튼================
            hashtable = new Hashtable();
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "listView");
            hashtable.Add("point", new Point(5, 5));
            hashtable.Add("size", new Size(600, 220));
            hashtable.Add("click", (MouseEventHandler)listView_click);
            lv = draw.getListView1(hashtable, bottom);
            lv.Columns.Add("", 0, HorizontalAlignment.Center);
            lv.Columns.Add("메뉴이름", 160, HorizontalAlignment.Center);
            lv.Columns.Add("샷추가", 70, HorizontalAlignment.Center);
            lv.Columns.Add("휘핑", 90, HorizontalAlignment.Center);
            lv.Columns.Add("단가", 80, HorizontalAlignment.Center);
            lv.Columns.Add("수량", 70, HorizontalAlignment.Center);
            lv.Columns.Add("금액", 125, HorizontalAlignment.Center);
            lv.Columns.Add("주문번호", 0, HorizontalAlignment.Center);
            lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lv.ColumnWidthChanging += ListView_ColumnWidthChanging;
            api = new WebAPI();
            api.ListView(Program.serverUrl + "orderlist/select", lv);

            hashtable = new Hashtable();
            hashtable.Add("text", "");
            hashtable.Add("width", 610);
            hashtable.Add("point", new Point(0, 220));
            //hashtable.Add("point", new Point(410, 222));
            hashtable.Add("name", "totalprice");
            hashtable.Add("font", new Font("맑은 고딕", 17, FontStyle.Bold));
            label = draw.getLabel1(hashtable, bottom);
            label.Height = 30;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 60));
            hashtable.Add("point", new Point(620, 20));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn11");
            hashtable.Add("text", "선택삭제");
            hashtable.Add("click", (EventHandler)btn11_click);
            hashtable.Add("font",new Font("맑은고딕", 15, FontStyle.Regular));
            btn11 = draw.getButton1(hashtable, bottom);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 60));
            hashtable.Add("point", new Point(620, 100));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn12");
            hashtable.Add("text", "전체삭제");
            hashtable.Add("click", (EventHandler)btn12_click);
            hashtable.Add("font", new Font("맑은고딕", 15, FontStyle.Regular));
            btn12 = draw.getButton1(hashtable, bottom);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 60));
            hashtable.Add("point", new Point(620, 180));
            hashtable.Add("color", Color.LightGray);
            hashtable.Add("name", "btn13");
            hashtable.Add("text", "주문결제");
            hashtable.Add("click", (EventHandler)btn13_click);
            hashtable.Add("font", new Font("맑은고딕", 15, FontStyle.Regular));
            btn13 = draw.getButton1(hashtable, bottom);

            btn1.BackColor = Color.White;
            // form 초기화
            if (tagetForm != null) tagetForm.Dispose();
            // form 호출
            tagetForm = draw.getMdiForm(parentForm, new MenuForm(parentForm, menuclick, this), menu);
            tagetForm.Show();
            OrderlistLoad();

        }

        private void Lv_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listview = (ListView)sender;
            SelectedListViewItemCollection col = listview.SelectedItems;
            selectOrder = col[0].SubItems[7].Text;
        }

        private void OrderlistLoad()
        {
            int allprice = 0;
            ListViewItemCollection col = lv.Items;
            for (int j = 0; j < col.Count; j++)
            {
                for (int k = 0; k < col[j].SubItems.Count; k++)
                {
                    col[j].SubItems[k].Font = new Font("맑은고딕", 12, FontStyle.Regular);
                }
            }
            for (int i = 0; i < lv.Items.Count; i++)
            {
                //lv.Items[i].SubItems[5].Text = (Convert.ToInt32(lv.Items[i].SubItems[5].Text) + Convert.ToInt32(lv.Items[i].SubItems[2].Text) * 500).ToString();
                allprice += Convert.ToInt32(lv.Items[i].SubItems[5].Text) * Convert.ToInt32(lv.Items[i].SubItems[4].Text);
            }
            label.Text = "총 가격 : " + allprice + "원";
        }

        private void btn11_click(object sender, EventArgs e)    //선택삭제 하는 클릭 이벤트
        {
            api = new WebAPI();
            Hashtable ht = new Hashtable();
            ht.Add("oNo", selectOrder);
            api.Post(Program.serverUrl + "orderlist/deleteOrder", ht);
            this.tt();
        }

        private void btn12_click(object sender, EventArgs e)    // 전체삭제 이벤트
        {
            api = new WebAPI();
            Hashtable ht = new Hashtable();
            ht.Add("oNum", Program.maxoNum);
            api.Post(Program.serverUrl + "orderlist/deleteOrderAll", ht);
            this.tt();
        }

        private void btn13_click(object sender, EventArgs e)    // 주문결제 버튼
        {
            if (lv.Items.Count == 0)
            {
                MessageBox.Show("상품을 선택해주세요.");
                return;
            }

            PayForm pf = new PayForm();
            pf.StartPosition = FormStartPosition.Manual;
            pf.Location = new Point(parentForm.Location.X + (parentForm.Width / 2) - (pf.Width / 4), parentForm.Location.Y + (parentForm.Height / 2) - (pf.Height / 2));
            pf.ShowDialog();
            tt();
        }

        //private void exit_click(object sender, FormClosedEventArgs e)
        //{
        //    parentForm.Visible = false;
        //    UserForm userForm = new UserForm();
        //    userForm.StartPosition = FormStartPosition.CenterParent;
        //    userForm.Show();
        //}

        private void listView_click(object sender, MouseEventArgs e)
        {
            ListView listview = (ListView)sender;
            SelectedListViewItemCollection col = listview.SelectedItems;
            selectOrder = col[0].SubItems[7].Text;
        }

        private void btn_click(object sender, EventArgs e) //카테고리 버튼 클릭 이벤트
        {
            Button b = (Button)sender;
            switch (b.Name)
            {
                case "btn1":
                    menuclick = 1;
                    btn1.BackColor = Color.White;
                    btn2.BackColor = Color.LightGray;
                    btn3.BackColor = Color.LightGray;
                    btn4.BackColor = Color.LightGray;

                    // form 초기화
                    if (tagetForm != null) tagetForm.Dispose();
                    // form 호출
                    tagetForm = draw.getMdiForm(parentForm, new MenuForm(parentForm, menuclick, this), menu);
                    tagetForm.Show();
                    break;
                case "btn2":
                    menuclick = 2;
                    btn1.BackColor = Color.LightGray;
                    btn2.BackColor = Color.White;
                    btn3.BackColor = Color.LightGray;
                    btn4.BackColor = Color.LightGray;

                    // form 초기화
                    if (tagetForm != null) tagetForm.Dispose();
                    // form 호출
                    tagetForm = draw.getMdiForm(parentForm, new MenuForm(parentForm, menuclick, this), menu);
                    tagetForm.Show();
                    break;
                case "btn3":
                    menuclick = 3;
                    btn1.BackColor = Color.LightGray;
                    btn2.BackColor = Color.LightGray;
                    btn3.BackColor = Color.White;
                    btn4.BackColor = Color.LightGray;

                    // form 초기화
                    if (tagetForm != null) tagetForm.Dispose();
                    // form 호출
                    tagetForm = draw.getMdiForm(parentForm, new MenuForm(parentForm, menuclick, this), menu);
                    tagetForm.Show();
                    break;
                case "btn4":
                    menuclick = 4;
                    btn1.BackColor = Color.LightGray;
                    btn2.BackColor = Color.LightGray;
                    btn3.BackColor = Color.LightGray;
                    btn4.BackColor = Color.White;

                    // form 초기화
                    if (tagetForm != null) tagetForm.Dispose();
                    // form 호출
                    tagetForm = draw.getMdiForm(parentForm, new MenuForm(parentForm, menuclick, this), menu);
                    tagetForm.Show();
                    break;
                default:
                    break;
            }
        }

        public void tt()
        {
            api.ListView(Program.serverUrl + "orderlist/select", lv);
            OrderlistLoad();
        }

        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)    // 카테고리 리트스 칼럼크기 막음
        {
            e.NewWidth = lv.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
