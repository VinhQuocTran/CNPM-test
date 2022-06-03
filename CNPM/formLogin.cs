﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class formLogin : Form
    {
        public static string matk = "";
        public formLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=QuanLyCuaHangTienLoi;Trusted_Connection=True");

            cnn.Open();
                string user = txtUsername.Text;
                string pass = txtPassword.Text;
                string accountType = cbbAccountType.Text;
                using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM taikhoan WHERE tentk=@username AND matkhau=@password and loaitk=@accType", cnn))
                {
                    StrQuer.Parameters.AddWithValue("@username", user);
                    StrQuer.Parameters.AddWithValue("@password", pass);
                    StrQuer.Parameters.AddWithValue("@accType", accountType);
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    if(dr.HasRows)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        if (dr.Read())
                            matk = dr["matk"].ToString();
                        if(cbbAccountType.Text=="Admin")
                        {
                            newFormMainManager controlManage = new newFormMainManager();
                            this.Hide();
                            controlManage.Show();
                        }
                        else
                        {
                            newFormMainCashier formCashier = new newFormMainCashier();
                            this.Hide();
                            formCashier.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại thông tin đăng nhập");
                    }
                }
                cnn.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUsername.Text = "";
            cbbAccountType.Text = "";
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
