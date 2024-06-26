﻿using System;
using System.Windows.Forms;

namespace StudyCenterDesktopUI.Students
{
    public partial class frmShowStudentInfo : Form
    {
        public frmShowStudentInfo(int? studentID)
        {
            InitializeComponent();

            ucStudentCard1.LoadStudentInfoByStudentID(studentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
