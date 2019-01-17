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
    class MenuAddView
    {
        private Draw draw;
        private Form parentForm;
        private Label lblCategory, lblMenu, lblPrice, lblImage;
        private CheckBox cboxHot, cboxSize, cboxShot, cboxWhip;
        private ComboBox comboCategory;
        private TextBox txtMenu, txtPrice, txtImg;
        private Button btnImgAdd, btnAdd;
        private Hashtable hashtable;
        private Image image;
        private string fileName;
        private string ext;
        private PictureBox pbImage;

        public MenuAddView(Form parentForm)
        {
            this.parentForm = parentForm;
            draw = new Draw();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("text", "카테고리 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 30));
            hashtable.Add("font", new Font("맑은 고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblCategory = draw.getLabel1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 30));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "comboCategory");
            comboCategory = draw.getComboBox(hashtable, parentForm);
            WebAPI api = new WebAPI();
            api.SelectCategory(Program.serverUrl+"category/select", comboCategory);
            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("text", "메뉴명 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 110));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblMenu = draw.getLabel1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 110));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtMenu");
            txtMenu = draw.getTextBox(hashtable, parentForm);

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("text", "가격 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 190));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblPrice = draw.getLabel1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 190));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtPrice");
            txtPrice = draw.getTextBox(hashtable, parentForm);

            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("text", "이미지 : ");
            hashtable.Add("width", 110);
            hashtable.Add("point", new Point(100, 270));
            hashtable.Add("font", new Font("고딕", 18, FontStyle.Bold));
            hashtable.Add("name", "주문번호");
            lblImage = draw.getLabel1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(230, 270));
            hashtable.Add("font", new Font("맑은고딕", 14, FontStyle.Bold));
            hashtable.Add("name", "txtImg");
            txtImg = draw.getTextBox(hashtable, parentForm);
            txtImg.ReadOnly = true;

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(50, 50));
            hashtable.Add("point", new Point(550, 260));
            hashtable.Add("color", Color.FromArgb(255, 255, 255));
            hashtable.Add("name", "btnAdd");
            hashtable.Add("text", "");
            hashtable.Add("font", new Font("맑은 고딕", 10, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnImgAdd_Click);
            btnImgAdd = draw.getButton1(hashtable, parentForm);
            btnImgAdd.BackgroundImage = Properties.Resources.attach_image;
            btnImgAdd.BackgroundImageLayout = ImageLayout.Stretch;
            //==============================================================================

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(100, 360));
            hashtable.Add("text", "핫/아이스");
            hashtable.Add("name", "cboxHot");
            cboxHot = draw.getCheckBox(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(250, 360));
            hashtable.Add("text", "사이즈");
            hashtable.Add("name", "cboxSize");
            cboxSize = draw.getCheckBox(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(400, 360));
            hashtable.Add("text", "샷");
            hashtable.Add("name", "cboxShot");
            cboxShot = draw.getCheckBox(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("point", new Point(510, 360));
            hashtable.Add("text", "휘핑");
            hashtable.Add("name", "cboxWhip");
            cboxWhip = draw.getCheckBox(hashtable, parentForm);

            //==============================================================================


            hashtable = new Hashtable();
            hashtable.Add("size", new Size(150, 50));
            hashtable.Add("point", new Point(490, 440));
            hashtable.Add("color", Color.FromArgb(246, 246, 246));
            hashtable.Add("name", "btnAdd");
            hashtable.Add("text", "메뉴 등록");
            hashtable.Add("font", new Font("맑은 고딕", 15, FontStyle.Regular));
            hashtable.Add("click", (EventHandler)btnAdd_Click);
            btnAdd = draw.getButton1(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("image",null);
            hashtable.Add("size",new Size(100,60));
            hashtable.Add("point",new Point(230,300));
            hashtable.Add("color",Color.White);
            pbImage = draw.getPictureBox(hashtable, parentForm);
            pbImage.BorderStyle = BorderStyle.FixedSingle;
            pbImage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnImgAdd_Click(object sender, EventArgs e)    // 이미지 선택하는 부분
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

        private void btnAdd_Click(object sender, EventArgs e)   // 메뉴 추가하는 부분 + 이미지 보내기
        {
            if (comboCategory.SelectedIndex + 1 == 0 || txtMenu.Text == "" || txtPrice.Text == "" || txtImg.Text == "")
            {
                MessageBox.Show("모두 입력해주세요.");
            }
            else
            {
                WebClient wc = new WebClient();
                NameValueCollection nameValue = new NameValueCollection();
                nameValue.Add("fileName", fileName);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imgData = ms.ToArray();

                string fileData = Convert.ToBase64String(imgData);
                nameValue.Add("fileData", fileData);

                nameValue.Add("cNo", (comboCategory.SelectedIndex + 1).ToString());
                nameValue.Add("mName", txtMenu.Text);
                nameValue.Add("mPrice", txtPrice.Text);

                if (cboxHot.Checked)
                {
                    nameValue.Add("DegreeYn", "1");
                }
                else
                {
                    nameValue.Add("DegreeYn", "0");
                }

                if (cboxSize.Checked)
                {
                    nameValue.Add("SizeYn", "1");
                }
                else
                {
                    nameValue.Add("SizeYn", "0");
                }

                if (cboxShot.Checked)
                {
                    nameValue.Add("ShotYn", "1");
                }
                else
                {
                    nameValue.Add("ShotYn", "0");
                }

                if (cboxWhip.Checked)
                {
                    nameValue.Add("CreamYn", "1");
                }
                else
                {
                    nameValue.Add("CreamYn", "0");
                }
                byte[] result = wc.UploadValues(Program.serverUrl + "Menu/add", "POST", nameValue);
                string resultStr = Encoding.UTF8.GetString(result);

                if (resultStr == "1")
                {
                    MessageBox.Show("메뉴를 추가했습니다.");
                    comboCategory.SelectedIndex = -1;
                    txtMenu.Text = "";
                    txtPrice.Text = "";
                    txtImg.Text = "";
                    cboxHot.Checked = false;
                    cboxShot.Checked = false;
                    cboxSize.Checked = false;
                    cboxWhip.Checked = false;
                    pbImage.BackgroundImage = null;
                }
                else
                {
                    MessageBox.Show("다시 입력해 주세요.");
                }
            }
        }
    }
}
