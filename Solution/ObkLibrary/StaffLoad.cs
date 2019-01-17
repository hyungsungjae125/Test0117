using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObkLibrary
{
    public class StaffLoad
    {
        private Form parentForm;

        public StaffLoad(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        public Form GetHandler(string viewName)
        {
            switch (viewName)
            {
                case "staff":
                    return GetStaffLoad();
                case "OrderList":
                    return GetOrderListLoad();
                case "SoldoutAdd":
                    return GetSoldoutAddLoad();
                case "SoldoutDelete":
                    return GetSoldoutDelete();
                default:
                    return null;
            }
        }

        private Form GetStaffLoad()
        {
            parentForm.Size = new Size(700, 480);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "직원용";
            return parentForm;
        }

        private Form GetOrderListLoad()
        {
            parentForm.Size = new Size(684, 341);
            parentForm.BackColor = Color.White;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            return parentForm;
        }

        private Form GetSoldoutAddLoad()
        {
            parentForm.Size = new Size(684, 341);
            parentForm.BackColor = Color.White;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            return parentForm;
        }

        private Form GetSoldoutDelete()
        {
            parentForm.Size = new Size(684, 341);
            parentForm.BackColor = Color.White;
            parentForm.FormBorderStyle = FormBorderStyle.None;
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            return parentForm;
        }

    }
}
