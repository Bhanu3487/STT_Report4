using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace Lab12_2 
{
    public partial class Form1 : Form
    {
        // Timer to check time every second
        private System.Windows.Forms.Timer _updateTimer;
        // Variable to store the target alarm time
        private DateTime _targetAlarmTime;
        // Flag to know if the alarm is currently running
        private bool _isAlarmRunning = false;
        // Random number generator for background colors
        private Random _randomColorGenerator = new Random();
        // Store the original background color
        private Color _originalBackColor;

        public Form1()
        {
            InitializeComponent();
            // Store the form's original background color
            _originalBackColor = this.BackColor;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Prevent starting if already running
            if (_isAlarmRunning)
            {
                MessageBox.Show("Alarm is already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // --- Input Parsing and Validation ---
            if (TimeSpan.TryParse(txtTime.Text, out TimeSpan timeOfDay))
            {
                // --- Calculate Target Time ---
                DateTime now = DateTime.Now;
                DateTime todayTarget = now.Date + timeOfDay; // Target time if set for today

                // If the calculated time for today has already passed, set it for tomorrow
                if (todayTarget <= now)
                {
                    _targetAlarmTime = todayTarget.AddDays(1);
                    MessageBox.Show($"Time {timeOfDay:hh\\:mm\\:ss} has already passed today. Setting alarm for tomorrow.", "Alarm Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _targetAlarmTime = todayTarget;
                    MessageBox.Show($"Alarm set for {timeOfDay:hh\\:mm\\:ss} today.", "Alarm Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // --- Prepare and Start Timer ---
                InitializeTimer(); // Set up the timer
                _updateTimer.Start();
                _isAlarmRunning = true;

                // Disable controls while running
                txtTime.Enabled = false;
                btnStart.Enabled = false; 
                // btnStart.Text = "Stop Alarm"; // Alternative UX
            }
            else
            {
                // --- Handle Invalid Input ---
                MessageBox.Show("Invalid time format entered. Please use HH:MM:SS.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtTime.Focus();
                txtTime.SelectAll();
            }
        }

        private void InitializeTimer()
        {
            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 1000; // Set interval to 1000 milliseconds (1 second)
            _updateTimer.Tick += UpdateTimer_Tick; // Assign the event handler method
        }

        // This method runs every second when the timer is active
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            // Check if the target time has been reached or passed
            // Use >= for safety, in case the exact second tick is missed
            if (now >= _targetAlarmTime)
            {
                // --- Target Time Reached ---
                _updateTimer.Stop(); // Stop the timer
                _isAlarmRunning = false;

                // Optional: Play a sound
                // SystemSounds.Exclamation.Play();

                // Restore original background color
                this.BackColor = _originalBackColor;

                // Show the final message
                MessageBox.Show($"Alarm! Target time {_targetAlarmTime:HH:mm:ss} reached.",
                                "Alarm Triggered",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                // Re-enable controls
                txtTime.Enabled = true;
                btnStart.Enabled = true;
                // btnStart.Text = "Start Alarm"; // Change back if you used "Stop"

                // Clean up timer resources
                _updateTimer.Dispose();
                _updateTimer = null;
            }
            else
            {
                // --- Target Time Not Reached - Change Color ---
                this.BackColor = Color.FromArgb(
                    _randomColorGenerator.Next(256), // Random Red (0-255)
                    _randomColorGenerator.Next(256), // Random Green (0-255)
                    _randomColorGenerator.Next(256)  // Random Blue (0-255)
                );
            }
        }

        // Ensure timer is stopped if the form closes unexpectedly
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_updateTimer != null)
            {
                _updateTimer.Stop();
                _updateTimer.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}