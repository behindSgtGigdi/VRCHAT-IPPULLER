using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GigdiPuller;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GigdiPuller
{
    public partial class instancepullercreator : Form
    {


        public instancepullercreator()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(instanceid.Text);
            MessageBox.Show("Successfully Copied To Clipboard!");
        }

        private void Scripts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string userId = userid.Text; // Get user ID from the input field
            string youtubeUrl = method.Text; // Get YouTube URL from the input field

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(youtubeUrl))
            {
                MessageBox.Show("Please provide both User ID and YouTube URL.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Create the API URL with query parameters
                string apiUrl = $"https://vrchatapi.onrender.com/vrchat/api/v1/create-endpoint?userId={userId}&youtubeUrl={Uri.EscapeDataString(youtubeUrl)}";

                // Use a handler to include the cookies from the current session
                var handler = new HttpClientHandler();
                handler.CookieContainer = CookieHelper.CookieContainer; // Use the cookies stored in the shared container

                using (HttpClient client = new HttpClient(handler))
                {
                    // Send the GET request with the session cookie
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful

                    // Read and parse the response
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    // Get the relevant values from the response
                    string grabUrl = result.grabUrl;
                    string trackUrl = result.trackUrl;
                    string endpointId = result.endpointId;

                    // Display or use these values
                    instanceid.Text = endpointId; // Set the instance ID in the corresponding textbox
                    label8.Text = "Youtube URL: " + youtubeUrl; // Optionally set the YouTube URL or any other relevant field
                    pullurl.Text = grabUrl; // Set the grab URL in the textbox

                    // Display a success message
                    MessageBox.Show("Endpoint created successfully!\nGrab URL: " + grabUrl + "\nTrack URL: " + trackUrl, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("There was an error connecting to the API. Please try again later.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(pullurl.Text);
            MessageBox.Show("Successfully Copied To Clipboard!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            instancepuller main = new instancepuller();
            main.Show();
        }

        private void Script_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To start. Simply Create A New Instance using the menu here. Then join a world with a working video player and then invite the person you are pulling to the world. Once they join copy the pull url and paste it into the url box on the map. once finished come back here and enter your instance ID to the pulling menu and there is the information.");
        }

        private void OpenYouTubeChannel_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start("http://discord.gg/7cyrKZcj8W");
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void key_Click(object sender, EventArgs e)
        {

        }

        private void level_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void OpenYouTubeChannel_Click_1(object sender, EventArgs e)
        {
            Process.Start("http://discord.gg/7cyrKZcj8W");
        }

        private void News_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("coming soon.");
        }
    }
}