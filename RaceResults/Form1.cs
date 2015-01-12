using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaceResults
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFile();
            dataGridView1.Sort(colTime, ListSortDirection.Ascending);
            SetRank();
        }
        private void ReadFile()
        {
            //Open a stream from the race results text and read it back
            using (StreamReader sr = File.OpenText("RaceResults.txt"))
            {
                string line = "";
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                //Add line from race results to the datagrid using WorkLine
                {
                    if (counter < 2)
                    {
                        counter++;
                    }
                    else
                    {
                        WorkLine(line);
                    }
                }

            }
        }
        //Function to handle actually parsing logic
        private void WorkLine(string lineData)
        {
            //Break the columns into variables
            string runnerName = (lineData.Substring(0, 8)).Trim();
            int runnerTime = Convert.ToInt16((lineData.Substring(8, 8)).Trim());
            int runnerAge = Convert.ToInt16((lineData.Substring(16)).Trim());
            int runnerAgeGroup = 0;

            //determine age group
            if (runnerAge <= 15)
            {
                runnerAgeGroup = 1;
            }
            else if (runnerAge < 30)
            {
                runnerAgeGroup = 2;
            }
            else
            {
                runnerAgeGroup = 3;
            }

            //Write the results to the data grid

            dataGridView1.Rows.Add(runnerName, runnerTime, runnerAge, runnerAgeGroup, 0);
        }

        //Function to determine rank for a list that has been sorted by time
        private void SetRank()
        {
            //Store Group Rankings
            int group1Rank = 1;
            int group2Rank = 1;
            int group3Rank = 1;
            //Iterate through each row checking age and updating rank, while incrementing the appropriate sotred ranking
            for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
            {
                if (dataGridView1.Rows[rows].Cells["colAgeGroup"].Value.Equals(1))
                {
                    dataGridView1.Rows[rows].Cells["colRank"].Value = group1Rank;
                    group1Rank++;
                }
                else if (dataGridView1.Rows[rows].Cells["colAgeGroup"].Value.Equals(2))
                {
                    dataGridView1.Rows[rows].Cells["colRank"].Value = group2Rank;
                    group2Rank++;
                }
                else
                {
                    dataGridView1.Rows[rows].Cells["colRank"].Value = group3Rank;
                    group3Rank++;
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
