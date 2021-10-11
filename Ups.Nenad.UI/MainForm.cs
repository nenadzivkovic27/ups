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
    public partial class MainForm : Form
    {
        protected IEmployeeDataService _employeeDataService;
        protected IExceptionService _exceptionService;
        protected Pagination _pageInfo;
        protected string _searchTerm;

        protected User _currentEmployee
        {
            get { return bsUsers.Current as User; }
        }

        private void SetupBindings()
        {
            bsUsers.DataSource = typeof(User);
            dgvEmployees.DataSource = bsUsers;
        }

        public MainForm(IEmployeeDataService employeeDataService, IExceptionService exceptionService)
        {
            InitializeComponent();
            SetupBindings();

            _employeeDataService = employeeDataService;
            _exceptionService = exceptionService;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadEmployees();
            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadEmployees()
        {
            var response = _employeeDataService.GetEmployees(_pageInfo?.Page, _searchTerm);
            _pageInfo = response.Meta?.Pagination;
            bsUsers.DataSource = response.Data;
            SetPageInfo();
        }

        private void SetPageInfo()
        {
            lblPage.Text = $"Showing page {_pageInfo.Page} out of {_pageInfo.Pages}";
        }

        private void btnGetEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadEmployees();
            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                FrmUser frmUser = new FrmUser(_employeeDataService, _exceptionService, _currentEmployee);
                var dialogResult = frmUser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    LoadEmployees();
                }
               
            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (_currentEmployee != null)
                {
                    string question = string.Format("Are you sure you want to delete user {0}?", _currentEmployee);
                    if (Utilities.ShowQuestion(question, "Delete employee?"))
                    {
                        _employeeDataService.DeleteEmployee(_currentEmployee);
                        LoadEmployees();
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                FrmUser frmUser = new FrmUser(_employeeDataService, _exceptionService, new User());
                var dialogResult = frmUser.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    LoadEmployees();
                }

            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (_pageInfo.Page > 1) _pageInfo.Page--;
                LoadEmployees();


            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (_pageInfo.Page < _pageInfo.Pages) _pageInfo.Page++;
                LoadEmployees();

            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _pageInfo = null;
                _searchTerm = txtSearch.Text;
                LoadEmployees();

            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _pageInfo.Page = 1;
                LoadEmployees();

            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _pageInfo.Page = _pageInfo.Pages;
                LoadEmployees();

            }
            catch (Exception ex)
            {
                _exceptionService.HandleException(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
