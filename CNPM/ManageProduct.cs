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
    public partial class ManageProduct : Form
    {
        public ManageProduct()
        {
            InitializeComponent();
        }

   

        private void btnCashier_Click(object sender, EventArgs e)
        {
            ManageCashier manageCashier = new ManageCashier();
            manageCashier.Show();
            this.Hide();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ManageCategory manageCategory = new ManageCategory();
            manageCategory.Show();
            this.Hide();
        }
        SqlConnection cnn = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=QuanLyCuaHangTienLoi;Trusted_Connection=True");

        private void addCategory()
        {
            
        }
        private void btnADDProduct_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string query = "select tendanhmuc from danhmuc";
                SqlDataAdapter sqlDataAdapter= new SqlDataAdapter(query, cnn);
                var dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                cbbCategories.Items.Add(dataSet.Tables);
                MessageBox.Show(dataSet.Tables.ToString());
                cnn.Close();
                //"insert into taikhoan values('" + txtID.Text +
                //   "','" + txtName.Text + "'," + txtPrice.Text + "," + txtAge.Text + ",'" +
                //   cbbAccountType.Text + "')"
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
