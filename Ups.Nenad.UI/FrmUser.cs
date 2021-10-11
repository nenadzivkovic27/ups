using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ups.Nenad.DataTypes;
using Ups.Nenad.Helper;
using Ups.Nenad.Services;

namespace Ups.Nenad.UI
{
    public partial class FrmUser : Form
    {
        protected IEmployeeDataService _employeeDataService;
        protected IExceptionService _exceptionService;

        protected User _currentUser
        {
            get { return bsUser.Current as User; }
        }

        private void SetupBindings()
        {
            //this should have been done through designer, but not possible in .net core

            bsUser.DataSource = typeof(User);

            txtUserId.DataBindings.Add(new Binding("Text", bsUser, "Id", true));
            txtUserName.DataBindings.Add(new Binding("Text", bsUser, "Name", true));
            txtUserGender.DataBindings.Add(new Binding("Text", bsUser, "Gender", true));
            txtUserEmail.DataBindings.Add(new Binding("Text", bsUser, "Email", true));
            txtUserStatus.DataBindings.Add(new Binding("Text", bsUser, "Status", true));

        }

        public FrmUser(IEmployeeDataService emploeyeeDataService, IExceptionService exceptionService, User user)
        {
            InitializeComponent();
            SetupBindings();

            _employeeDataService = emploeyeeDataService;
            _exceptionService = exceptionService;

            bsUser.DataSource = user;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var validateResult = ValidateUserDetails();
                if (validateResult)
                {
                    var result = Utilities.ShowQuestion("Are you sure?", "Save changes");
                    if (result)
                    {
                        _employeeDataService.SaveEmployee(_currentUser);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.ShowError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateUserDetails()
        {
            bool toRet = true;
            errUser.Clear();
            if (String.IsNullOrWhiteSpace(_currentUser.Name))
            {
                errUser.SetError(txtUserName, "Name is mandatory");
                toRet = false;
            }
            if (String.IsNullOrWhiteSpace(_currentUser.Email))
            {
                errUser.SetError(txtUserEmail, "Email is mandatory");
                toRet = false;
            }
            else
            {
                try
                {
                    new System.Net.Mail.MailAddress(_currentUser.Email);
                }
                catch (FormatException)
                {
                    errUser.SetError(txtUserEmail, "Email format invalid");
                    toRet = false;
                }
            }

            return toRet;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Utilities.ShowError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
