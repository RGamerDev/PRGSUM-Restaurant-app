﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant_App
{
    public partial class W_Spec : Form
    {
        private List<int> Bill = new List<int>();
        private int bill;
        //Using abstract data type
        private List<int> list = new List<int>();
        public W_Spec()
        {
            InitializeComponent();
        }

        #region Methods
        /// <summary>
        /// Adding item to listbox and list
        /// </summary>
        private void addItem()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " R100 x" + trackBar1.Value.ToString());
                    list.Add(100 * trackBar1.Value);
                    listBox1.SelectedIndex++;
                    break;

                case 1:
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " R60 x" + trackBar1.Value.ToString());
                    list.Add(60 * trackBar1.Value);
                    listBox1.SelectedIndex++;
                    break;

                case 2:
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " R120 x" + trackBar1.Value.ToString());
                    list.Add(120 * trackBar1.Value);
                    listBox1.SelectedIndex++;
                    break;

                case 3:
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " R70 x" + trackBar1.Value.ToString());
                    list.Add(70 * trackBar1.Value);
                    listBox1.SelectedIndex++;
                    break;

                case 4:
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString() + " R70 x" + trackBar1.Value.ToString());
                    list.Add(70 * trackBar1.Value);
                    listBox1.SelectedIndex++;
                    break;

                case -1:
                default:
                    MessageBox.Show("Please select an item from the menu");
                    break;
            }
        }
        /// <summary>
        /// Deletes an item from the list
        /// </summary>
        private void Delete()
        {

            if (listBox1.Items.Count > 1)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    list.RemoveAt(listBox1.SelectedIndex);
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    listBox1.SelectedIndex++;
                }
                else
                {
                    MessageBox.Show("Please select an item from the menu to delete.");
                }
            }
            else if (listBox1.Items.Count != 0)
            {
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("There are no items to delete");
            }
        }

        /// <summary>
        /// Checking out
        /// </summary>
        private void Checkout()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SessionContext.ConnectionString))
                {
                    
                    con.Open();

                    if (con.State == ConnectionState.Open)
                    {
                        using (SqlCommand com = new SqlCommand())
                        {
                            com.Connection = con;
                            com.CommandText = "SELECT Menu";
                        }
                    }
                }

                if (MessageBox.Show("Are sure you want to proceed? You won't be able to change the bill if you proceed",
                                    "Confirmation",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (int item in list)
                    {
                        bill += item;
                    }

                    label6.Text += "R" + bill.ToString();

                    MessageBox.Show("Bill is completed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnAdd.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    trackBar1.Enabled = false;
                    comboBox1.Enabled = false;
                }
            }
            catch (SqlException sEx)
            {
                MessageBox.Show(sEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error ocurred... please try again: " + Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Clears the bill
        /// </summary>
        private void newBill()
        {
            if (MessageBox.Show("Are you sure? All current items on the list will be lost?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                btnAdd.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                trackBar1.Enabled = true;
                trackBar1.Value = 1;
                comboBox1.Enabled = true;
                comboBox1.SelectedIndex = -1;
                list.Clear();
                listBox1.Items.Clear();
                label6.Text = "Total: ";
            }
        }

        private void loadTable()
        {
            label1.Text += SessionContext.Table;
            label5.Text = trackBar1.Value.ToString();
        }

        private void changeLbl()
        {
            label5.Text = trackBar1.Value.ToString();
        }
        #endregion

        #region Event Handlers
        private void W_Spec_Load(object sender, EventArgs e)
        {
            loadTable();
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            changeLbl();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            addItem();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Checkout();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            newBill();
        }
        #endregion
    }
}
