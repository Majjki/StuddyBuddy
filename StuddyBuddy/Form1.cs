using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StuddyBuddy
{
    public partial class Form1 : Form
    {
        //tasklist /fi "pid eq 4444" 
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);


        //SetForegroundWindow(Handle.ToInt32());

        float timeLeft = 4; 

        public Form1()
        {
            InitializeComponent();
            Label.CheckForIllegalCrossThreadCalls = false;
            Process[] processlist = Process.GetProcesses();
            string processNames = "";
           

            foreach (Process theprocess in processlist)
            {
                processNames = ("Process ID: {"+ theprocess.Id + "}" + "  :  " + theprocess.ProcessName + "   " ); 
                listBox1.Items.Add(processNames);
            }
            
        }

        private void updateCurrProcess() {
            IntPtr focusedWindow = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(focusedWindow, out pid);
            label2.Text = (pid.ToString());

            label1.Text = Process.GetProcessById((int)Convert.ToInt32(pid)).ProcessName.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Text.ToLower() == "chrome") {
                timeLeft -= timer1.Interval/1000f;
                label3.Text = ((int)timeLeft).ToString();
            }
            if(timeLeft <= 0) {
                foreach(var process in Process.GetProcessesByName("chrome")) {
                    BringToFront();
                }
            }
            updateCurrProcess();
        }
    }
}
