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
    public partial class ControlManageProduct : UserControl
    {
        public ControlManageProduct()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=QuanLyCuaHangTienLoi;Trusted_Connection=True");
        private void addCbbCategory()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            try
            {
                cnn.Open();

                cmd = new SqlCommand("Select madanhmuc From danhmuc", cnn);
                dr = cmd.ExecuteReader();

                while (dr.Read())

                {
                    cbbCategories.Items.Add(dr[0]).ToString();
                }
                dr.Close();
                cnn.Close();
                showDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void showDataGrid()
        {
            cnn.Open();
            string query = "select * from sanpham";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, cnn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            var dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            dataGridProduct.DataSource = dataSet.Tables[0];
            cnn.Close();
        }
        private void btnADDProduct_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string query = "insert into sanpham values('" + txtID.Text +
                    "','" + txtName.Text + "','" + txtPrice.Text + "','" + txtUnit.Text + "','" +
                    cbbCategories.Text + "'," + txtQuantity.Text + ")";
                SqlCommand sqlCommand = new SqlCommand(query, cnn);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Thêm sản phẩm thành công");
                cnn.Close();
                showDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string query = "delete sanpham where masp = '" + txtID.Text + "'";
                SqlCommand sqlCommand = new SqlCommand(query, cnn);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Xóa sản phẩm thành công");
                cnn.Close();
                showDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnn.Close();

            }
        }

        private void ControlManageProduct_Load(object sender, EventArgs e)
        {
            addCbbCategory();
            showDataGrid();
        }


        private void dataGridProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridProduct.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dataGridProduct.SelectedRows[0].Cells[1].Value.ToString();
            txtPrice.Text = dataGridProduct.SelectedRows[0].Cells[2].Value.ToString();
            txtUnit.Text = dataGridProduct.SelectedRows[0].Cells[3].Value.ToString();
            cbbCategories.Text = dataGridProduct.SelectedRows[0].Cells[4].Value.ToString();
            txtQuantity.Text = dataGridProduct.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void txtSampleProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                string query = "select * from sanpham where  tensp  like N'%" + txtSampleProduct.Text + "%'";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, cnn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                var dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                dataGridProduct.DataSource = dataSet.Tables[0];
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnn.Close();
            }
        }
    }
}
