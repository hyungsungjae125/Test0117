using ObkLibrary;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OBK.Views.AdminView
{
    class MenuEditView
    {
        private WebAPI api;
        private Draw draw;
        private Form parentForm;
        private Label lblTitle, lblMenu, lblPrice, lblImage;
        private CheckBox cboxHot, cboxSize, cboxShot, cboxWhip;
        private TextBox txtMenu, txtPrice, txtImg;
        private ListView listCategory, listMenu, listView;
        private Panel panelOne, panelTwo;
        private Button btnEdit, btnImgAdd, btnEditOk, btnEditCancel;
        private Hashtable hashtable;
        private Image image;
        private string mName = "";
        private string cNo;
        private string fileName;
        private string ext;
        private PictureBox pbImage;

        public MenuEditView(Form parentForm)    // 생성자
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()  // 화면 갖고오기
        {
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(680, 520));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "panelOne");
            panelOne = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("text", "메뉴 수정");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(20, 10));
            hashtable.Add("font", new Font("고딕", 30, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblTitle = draw.getLabel(hashtable, panelOne);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.WhiteSmoke);
            hashtable.Add("size", new Size(150, 350));
            hashtable.Add("point", new Point(20, 70));
            hashtable.Add("name", "listCategory");
            hashtable.Add("click", (MouseEventHandler)listCategory_click);
            listCategory = draw.getListView1(hashtable, panelOne);
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
            hashtable.Add("click", (MouseEventHandler)listMenu_Click);
            listMenu = draw.getListView1(hashtable, panelOne);
            listMenu.Columns.Add("", 0, HorizontalAlignment.Center);
            listMenu.Columns.Add("메뉴", 450, HorizontalAlignment.Center);
            listMenu.ColumnWidthChanging += ListMenu_ColumnWidthChanging;
            listMenu.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 50));
            hashtable.Add("point", new Point(510, 440));
            hashtable.Add("color", Color.FromArgb(246, 246, 246));
            hashtable.Add("name", "btnEdit");
            hashtable.Add("text", "선택 수정");
            hashtable.Add("font", new Font("맑은 고딕", 15, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnEdit_click);
            btnEdit = draw.getButton1(hashtable, panelOne);

            //======================================================================

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(680, 520));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "panelTwo");
            panelTwo = draw.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("text", "메뉴명 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 80));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblMenu = draw.getLabel1(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 80));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtMenu");
            txtMenu = draw.getTextBox(hashtable, panelTwo);

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("text", "가격 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 170));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblPrice = draw.getLabel1(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 170));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtPrice");
            txtPrice = draw.getTextBox(hashtable, panelTwo);

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("text", "이미지 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 270));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblImage = draw.getLabel1(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 270));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtImg");
            txtImg = draw.getTextBox(hashtable, panelTwo);
            txtImg.ReadOnly = true;
            txtImg.Visible = false;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(50, 50));
            hashtable.Add("point", new Point(420, 265));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btnAdd");
            hashtable.Add("text", "");
            hashtable.Add("font", new Font("맑은 고딕", 10, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnImgAdd_Click);
            btnImgAdd = draw.getButton1(hashtable, panelTwo);
            btnImgAdd.BackgroundImage = Properties.Resources.attach_image;
            btnImgAdd.BackgroundImageLayout = ImageLayout.Stretch;

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(100, 360));
            hashtable.Add("text", "핫/아이스");
            hashtable.Add("name", "cboxHot");
            cboxHot = draw.getCheckBox(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(250, 360));
            hashtable.Add("text", "사이즈");
            hashtable.Add("name", "cboxSize");
            cboxSize = draw.getCheckBox(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(400, 360));
            hashtable.Add("text", "샷");
            hashtable.Add("name", "cboxShot");
            cboxShot = draw.getCheckBox(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(510, 360));
            hashtable.Add("text", "휘핑");
            hashtable.Add("name", "cboxWhip");
            cboxWhip = draw.getCheckBox(hashtable, panelTwo);

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 50));
            hashtable.Add("point", new Point(350, 440));
            hashtable.Add("color", Color.FromArgb(246, 246, 246));
            hashtable.Add("name", "btnEditOk");
            hashtable.Add("text", "취소");
            hashtable.Add("font", new Font("맑은 고딕", 15, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnEditCancel_Click);
            btnEditCancel = draw.getButton1(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 50));
            hashtable.Add("point", new Point(510, 440));
            hashtable.Add("color", Color.FromArgb(246, 246, 246));
            hashtable.Add("name", "btnEditOk");
            hashtable.Add("text", "수정하기");
            hashtable.Add("font", new Font("맑은 고딕", 15, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnEditOk_Click);
            btnEditOk = draw.getButton1(hashtable, panelTwo);

            hashtable = new Hashtable();
            hashtable.Add("image", null);
            hashtable.Add("size", new Size(160, 100));
            hashtable.Add("point", new Point(230, 240));
            hashtable.Add("color", Color.White);
            pbImage = draw.getPictureBox(hashtable, panelTwo);
            pbImage.BorderStyle = BorderStyle.FixedSingle;
            pbImage.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private void listCategory_click(object sender, MouseEventArgs e)    // 카테고리 클릭시 옆에 메뉴명 출력
        {
            api = new WebAPI();

            listView = (ListView)sender;
            ListView.SelectedListViewItemCollection itemGroup = listView.SelectedItems;
            ListViewItem cNoitem = itemGroup[0];

            cNo = cNoitem.SubItems[0].Text;

            hashtable = new Hashtable();
            hashtable.Add("cNo", cNo);
            api.PostListview(Program.serverUrl + "Menu/nameSelect", hashtable, listMenu);
        }

        private void listMenu_Click(object sender, MouseEventArgs e)    // mName 갖고오기
        {
            listView = (ListView)sender;
            ListView.SelectedListViewItemCollection itemGroup = listView.SelectedItems;
            ListViewItem mNameitem = itemGroup[0];

            mName = mNameitem.SubItems[1].Text;
        }

        private void btnEdit_click(object sender, EventArgs e)
        {
            if (mName == "")
            {
                MessageBox.Show("수정할 메뉴를 선택해 주세요.");
            }
            else
            {
                panelOne.Visible = false;
                panelTwo.Show();

                hashtable = new Hashtable();
                hashtable.Add("mName", mName);
                api.MenuEdeitSelect(Program.serverUrl + "Menu/menuEdeitSelect", hashtable, txtMenu, txtPrice, txtImg, cboxHot, cboxSize, cboxShot, cboxWhip);
                WebClient wc = new WebClient();
                pbImage.BackgroundImage = Image.FromStream(wc.OpenRead(txtImg.Text));
            }
        }

        private void btnImgAdd_Click(object sender, EventArgs e)    // 이미지 로컬에서 불러오기
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images only. |*.png; *.jpg; *.jpeg; *.gif;";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFile.FileName;
                txtImg.Text = filePath;
                image = Image.FromFile(filePath);
                pbImage.BackgroundImage = image;
                fileName = openFile.SafeFileName;
                ext = fileName.Substring(fileName.LastIndexOf("."));

                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("fileName", fileName);
            }
            else { }
        }

        private void btnEditOk_Click(object sender, EventArgs e)    // 수정 update문
        {
            if (txtMenu.Text == "" || txtPrice.Text == "" || txtImg.Text == "")
            {
                MessageBox.Show("모두 입력해주세요.");
            }
            else
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();
                string fileData;

                if (txtImg.Text.LastIndexOf("/") == -1)
                {
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imgData = ms.ToArray();

                    fileData = Convert.ToBase64String(imgData);

                    fileName = txtImg.Text.Substring(txtImg.Text.LastIndexOf("\\") + 1);
                }
                else
                {
                    fileData = txtImg.Text;
                    fileName = txtImg.Text.Substring(txtImg.Text.LastIndexOf("/") + 1);
                }

                nameValue.Add("fileName", fileName);
                nameValue.Add("fileData", fileData);
                nameValue.Add("mName", mName);
                nameValue.Add("NewmName", txtMenu.Text);
                nameValue.Add("mPrice", txtPrice.Text);

                if (cboxHot.Checked) nameValue.Add("DegreeYn", "1");
                else nameValue.Add("DegreeYn", "0");

                if (cboxSize.Checked) nameValue.Add("SizeYn", "1");
                else nameValue.Add("SizeYn", "0");

                if (cboxShot.Checked) nameValue.Add("ShotYn", "1");
                else nameValue.Add("ShotYn", "0");

                if (cboxWhip.Checked) nameValue.Add("CreamYn", "1");
                else nameValue.Add("CreamYn", "0");

                byte[] result = wc.UploadValues(Program.serverUrl + "Menu/menuEdeit", "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                if (resultStr == "1")
                {
                    MessageBox.Show("메뉴 수정이 완료되었습니다.");
                    txtMenu.Text = "";
                    txtPrice.Text = "";
                    txtImg.Text = "";
                    cboxHot.Checked = false;
                    cboxShot.Checked = false;
                    cboxSize.Checked = false;
                    cboxWhip.Checked = false;
                    pbImage.BackgroundImage = null;

                    api = new WebAPI();
                    hashtable = new Hashtable();
                    hashtable.Add("cNo", cNo);
                    api.PostListview(Program.serverUrl + "Menu/nameSelect", hashtable, listMenu);

                    panelTwo.Visible = false;
                    panelOne.Show();
                }
                else
                {
                    MessageBox.Show("다시 입력해 주세요.");
                }
            }
        }

        private void btnEditCancel_Click(object sender, EventArgs e)    // 취소버튼 (뒤로가기)
        {
            txtMenu.Text = "";
            txtPrice.Text = "";
            txtImg.Text = "";
            cboxHot.CheckState = CheckState.Unchecked;
            cboxSize.CheckState = CheckState.Unchecked;
            cboxShot.CheckState = CheckState.Unchecked;
            cboxWhip.CheckState = CheckState.Unchecked;

            panelTwo.Visible = false;
            panelOne.Show();
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