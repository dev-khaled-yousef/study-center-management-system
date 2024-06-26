﻿using StudyCenterBusiness;
using StudyCenterDesktopUI.GlobalClasses;
using StudyCenterDesktopUI.Login;
using StudyCenterDesktopUI.MainMenu;
using StudyCenterDesktopUI.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudyCenterDesktopUI.Dashboard
{
    public partial class frmDashboard : Form
    {
        private frmLoginScreen _frmLoginForm;
        private frmMainMenu _frmMainMenu;

        public frmDashboard(frmLoginScreen loginScreen, frmMainMenu mainMenu)
        {
            InitializeComponent();

            _CountElements();
            _RefreshUserInfo();
            _RefreshGroupsList();

            gbList.Text = $"Schedule of Today ({DateTime.Now.DayOfWeek})";

            _frmLoginForm = loginScreen;
            _frmMainMenu = mainMenu;
        }

        private void _CountElements()
        {
            lblNumberOfTeachers.Text = clsTeacher.Count().ToString();
            lblNumberOfUsers.Text = clsUser.Count().ToString();
            lblNumberOfClasses.Text = clsClass.Count().ToString();
        }

        private void _RefreshGroupsList()
        {
            dgvGroupsList.DataSource = clsGroup.AllScheduleForToday();

            lblNumberOfRecords.Text = dgvGroupsList.Rows.Count.ToString();

            if (dgvGroupsList.Rows.Count > 0)
            {
                dgvGroupsList.Columns[0].HeaderText = "Teacher";
                dgvGroupsList.Columns[0].Width = 250;

                dgvGroupsList.Columns[1].HeaderText = "Subject";
                dgvGroupsList.Columns[1].Width = 130;

                dgvGroupsList.Columns[2].HeaderText = "Class";
                dgvGroupsList.Columns[2].Width = 170;

                dgvGroupsList.Columns[3].HeaderText = "Group";
                dgvGroupsList.Columns[3].Width = 100;

                dgvGroupsList.Columns[4].HeaderText = "Meeting Days";
                dgvGroupsList.Columns[4].Width = 120;

                dgvGroupsList.Columns[5].HeaderText = "Time";
                dgvGroupsList.Columns[5].Width = 120;
            }
        }

        private void _RefreshUserInfo()
        {
            clsGlobal.CurrentUser = clsUser.FindBy(clsGlobal.CurrentUser?.UserID, clsUser.enFindBy.UserID);

            lblFullName.Text = clsGlobal.CurrentUser?.PersonInfo?.FullName;
            lblEmail.Text = clsGlobal.CurrentUser?.PersonInfo?.Email;
            lblHiUsername.Text = $"Hi {clsGlobal.CurrentUser?.Username ?? "Username"}";
        }

        private void btnShowSubMenu_Click(object sender, EventArgs e)
        {
            // this method will show the context menu by clicking on the left click instead of the right click

            // Get the location of the button on the screen
            Point location = btnShowSubMenu.PointToScreen(new Point(0, btnShowSubMenu.Height));

            // Show the context menu at the calculated location
            contextMenuStrip1.Show(location);
        }

        private void tsmShowUserDetails_Click(object sender, EventArgs e)
        {
            frmShowUserInfo showUserInfo = new frmShowUserInfo(clsGlobal.CurrentUser?.UserID);
            showUserInfo.ShowDialog();

            _RefreshUserInfo();
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(clsGlobal.CurrentUser?.UserID);
            changePassword.ShowDialog();

            _RefreshUserInfo();
        }

        private void tsmSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmMainMenu.Hide();
            _frmLoginForm.Show();
            this.Close();
        }
    }
}
