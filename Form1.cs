using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsAppAPI
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();   
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                HttpResponseMessage response = await client.GetAsync("http://localhost/PHP/API.php?type=GET_orders");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var orderData = new { productname = txtproduct.Text, quantity = txtquantity.Text, totalprice = txtprice.Text, customerID = txtcustomer.Text, orderdate = txtorder.Text };
            string json = JsonConvert.SerializeObject(orderData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost/PHP/API.php?type=POST_orders", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;

                // Clear text fields
                txtproduct.Text = "";
                txtquantity.Text = "";
                txtprice.Text = "";
                txtcustomer.Text = "";
                txtorder.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
