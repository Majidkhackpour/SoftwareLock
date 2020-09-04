using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using Services;

namespace SoftwareLock
{
    public partial class frmClient : MetroForm
    {
        public string ActivationCode { get; set; }
        public frmClient(string serial, bool isShow10)
        {
            InitializeComponent();
            txtSerial.Text = serial;
            if (!string.IsNullOrEmpty(serial)) txtSerial.Enabled = false;
            if (!isShow10) btnSelect.Enabled = false;
        }

        private void txtSerial_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txtSerial);
        }

        private void txtSerial_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txtSerial);
        }

        private void txtActivationCode_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txtActivationCode);
        }

        private void txtActivationCode_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txtActivationCode);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnFinish_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerial.Text))
                {
                    MessageBox.Show("لطفا سریال نرم افزار را وارد نمایید");
                    txtSerial.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtActivationCode.Text))
                {
                    MessageBox.Show("لطفا کد فعالسازی را وارد نمایید");
                    txtSerial.Focus();
                    return;
                }


                var serialList = new List<string>();
                var code = "";
                foreach (var item in txtSerial.Text.ToList())
                {
                    if (code.Length < 2)
                    {
                        code += item;
                        if (code.Length == 2)
                        {
                            serialList.Add(code);
                            code = "";
                        }
                    }
                    else
                    {
                        serialList.Add(code);
                        code = "";
                    }
                }

                var isMatch = true;

                foreach (var item in serialList)
                {
                    var s = item.ParseToInt();

                    switch ((EnAppSerial)s)
                    {
                        case EnAppSerial.Building: break;
                        case EnAppSerial.Sms: break;
                        case EnAppSerial.Divar: break;
                        case EnAppSerial.Telegram: break;
                        case EnAppSerial.WhatsApp: break;
                        case EnAppSerial.WebSite: break;
                        case EnAppSerial.MobileApp: break;
                        case EnAppSerial.AutoBackUp: break;
                        case EnAppSerial.Excel: break;
                        case EnAppSerial.Network: break;
                        default: isMatch = false; break;
                    }
                }

                if (!isMatch)
                {
                    MessageBox.Show("سریال وارد شده معتبر نمی باشد");
                    txtSerial.Focus();
                    return;
                }

                ActivationCode = txtActivationCode.Text;

                var exDate = GenerateActivationCodeClient.GenerateExpireDate(ActivationCode, lblFanni.Text);
                if (string.IsNullOrEmpty(exDate))
                {
                    MessageBox.Show("کدفعالسازی معتبر نمی باشد");
                    return;
                }

                var mySerial = GenerateActivationCodeClient.GenerateCodeOnActive(lblFanni.Text, ActivationCode);
                if (!mySerial)
                {
                    MessageBox.Show("کدفعالسازی معتبر نمی باشد");
                    return;
                }


                clsRegistery.SetRegistery(txtSerial.Text, "U1001ML");
                clsRegistery.SetRegistery(exDate, "U1001MD");

                clsRegistery.DeleteRegistery("U1008FD");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void frmClient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!btnFinish.Focused && !btnCancel.Focused)
                            SendKeys.Send("{Tab}");
                        break;
                    case Keys.F5:
                        btnFinish.PerformClick();
                        break;
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            try
            {
                var code = GenerateActivationCodeClient.ActivationCode();
                lblFanni.Text = string.IsNullOrEmpty(code) ? "مشخصات فنی شناخته نشد !!!" : code;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                var exDate = DateTime.Now.AddDays(10);
                clsRegistery.SetRegistery(exDate.ToString(), "U1008FD");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}
