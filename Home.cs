using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace UnMe_App
{
    public partial class Home : Form
    {
        private List<int> goalTimes = new List<int>(); // Store goal times from the database
        private int userProgress = 0;
        private int autoProgress = 0;
        private System.Timers.Timer userTimer;
        private System.Timers.Timer autoTimer;
        private int currentGoalIndex = 0; // Track current goal
        private int currentGoalTime;

        public Home()
        {
            InitializeComponent();
            LoadGoalsFromDatabase();
            InitializeTimers();
            GetTotalGoalTime();
        }

        private void LoadGoalsFromDatabase()
        {
            string connectionString = "Server=localhost;Database=unme;Password=root;User ID=root;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT goalTime FROM Goals ORDER BY goalNumber ASC";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            goalTimes.Add(reader.GetInt32("goalTime"));
                        }
                    }
                }
            }

            // Debug output
            Console.WriteLine("Loaded goal times: " + string.Join(", ", goalTimes));
        }

        private void InitializeTimers()
        {
            // User Timer
            userTimer = new System.Timers.Timer();
            userTimer.Interval = 1000; // 1-second intervals
            userTimer.Elapsed += OnUserTimerTick;

            // Auto Timer
            autoTimer = new System.Timers.Timer();
            autoTimer.Interval = 1000; // 1-second intervals
            autoTimer.Elapsed += OnAutoTimerTick;
        }

        private int GetTotalGoalTime()
        {
            // Replace this with actual logic to sum up time for all goals
            return goalTimes.Sum() * 60; // Example: 5 minutes per goal * 5 goals = 300 seconds
        }

        private void OnUserTimerTick(object sender, ElapsedEventArgs e)
        {
            if (currentGoalTime > 0)
            {
                currentGoalTime--;
            }
            else
            {
                // Move to the next goal, if any
                currentGoalIndex++;
                if (currentGoalIndex < goalTimes.Count)
                {
                    currentGoalTime = goalTimes[currentGoalIndex] * 60; // Reset timer for the next goal
                }
                else
                {
                    userTimer.Stop();
                }
            }
        }

        private int elapsedTime = 0; // New variable to track elapsed time in seconds

        private void OnAutoTimerTick(object sender, ElapsedEventArgs e)
        {
            int totalGoalTime = GetTotalGoalTime();

            if (totalGoalTime > 0)
            {
                elapsedTime++; // Increment elapsed time by 1 second each tick

                // Calculate progress as a percentage of total goal time
                autoProgress = (int)(((double)elapsedTime / totalGoalTime) * 100);

                // Update progress bar with the new autoProgress value
                UpdateAutoProgress(autoProgress);

                // Stop the timer once progress reaches or exceeds 100%
                if (autoProgress >= 100)
                {
                    autoTimer.Stop();
                    MessageBox.Show("Auto progress completed!");
                }
            }
            else
            {
                autoTimer.Stop();
                MessageBox.Show("No valid goal time set.");
            }
        }

        public void UpdateUserProgress(int progress)
        {
            if (progressBarUser.Value + progress <= 100)
            {
                progressBarUser.Value += progress;
            }
            else
            {
                progressBarUser.Value = 100;
                MessageBox.Show("Congratulations! All goals completed.");
            }
        }

        public void UpdateAutoProgress(int progress)
        {
            if (progressBarAuto.InvokeRequired)
            {
                progressBarAuto.Invoke(new Action(() => progressBarAuto.Value = progress));
            }
            else
            {
                progressBarAuto.Value = progress;
            }
        }

        private void btnSetgoal_Click(object sender, EventArgs e)
        {
            Goals goalsForm = new Goals();

            this.Hide();

            goalsForm.Show();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Calculate progress per goal based on the actual number of goals
            int progressPerGoal = 100 / goalTimes.Count;
            UpdateUserProgress(progressPerGoal);

            // Move to the next goal, if any
            currentGoalIndex++;
            if (currentGoalIndex < goalTimes.Count)
            {
                currentGoalTime = goalTimes[currentGoalIndex] * 60; // Set time for the next goal
            }
            else
            {
                userTimer.Stop();
                autoTimer.Stop();
                MessageBox.Show("You completed all goals before the auto system!");
            }
        }

        


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (goalTimes.Count == 0)
            {
                MessageBox.Show("No goals set. Please set at least one goal.");
                return;
            }

            currentGoalIndex = 0;
            currentGoalTime = goalTimes[currentGoalIndex] * 60; // Convert minutes to seconds

            userTimer.Start();
            autoTimer.Start();
            Console.WriteLine("Timers started. Total goal time: " + GetTotalGoalTime());
        }
    }
}
