﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnMe_App
{
    public partial class Goal4 : Form
    {
        public Goal4()
        {
            InitializeComponent();
        }

        private void SaveGoal(int goalNumber, string goalName, int goalTime)
        {
            // Updated to use MySqlConnection instead of SqlConnection
            using (var connection = new MySqlConnection("Server=localhost;Database=unme;Password=root;User ID=root;"))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Goals (goalNumber, goalName, goalTime) VALUES (@goalNumber, @goalName, @goalTime)";
                using (var command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@goalNumber", goalNumber);
                    command.Parameters.AddWithValue("@goalName", goalName);
                    command.Parameters.AddWithValue("@goalTime", goalTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        
        

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Goals goalsForm = new Goals();

            this.Hide();

            goalsForm.Show();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Get user input
            string goalName = txtGoal4.Text; // The name of the goal
            int goalTime;

            // Check if the user selected a time from the ComboBox
            if (cboGoal4.SelectedItem != null)
            {
                goalTime = int.Parse(cboGoal4.SelectedItem.ToString());

                // Save the goal to the database
                SaveGoal(4, goalName, goalTime); // Goal number 1
                MessageBox.Show("Goal 4 saved successfully!"); // Confirmation message
            }
            else
            {
                MessageBox.Show("Please select a time for the goal."); // Error message
            }
        }

        private void btnGoBack_Click_1(object sender, EventArgs e)
        {
            Goals goalsForm = new Goals();

            this.Hide();

            goalsForm.Show();
        }
    }
}
