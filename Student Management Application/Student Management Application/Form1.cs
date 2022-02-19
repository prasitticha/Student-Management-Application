using System.Text;

namespace Student_Management_Application
{
    public partial class Form1 : Form
    {
        GPACalculator oSMA = new GPACalculator();

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv) | *.csv";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] readallline = File.ReadAllLines(openFileDialog.FileName);

                for(int i = 0; i < readallline.Length; i++)
                {
                    string studentRAW = readallline[i];
                    string[] studentSplited = studentRAW.Split(',');
                    Student student = new Student(studentSplited[0], studentSplited[1], studentSplited[2]);
                    //addDataToGridView(student);
                    //TODO: Add Stodent object to DataGridView
                }

                //TODO : valculate max, min, gpax

            }
        }
        private void addDataToGridView(string id, string name, string major)
        {
            this.dataGridView1.Rows.Add(new string[] { "id", "name", "major" });
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV(*.csv)|*.csv";
                bool fileError = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string column = "";
                            string[] outputCSV = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                column += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCSV[0] += column;
                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCSV[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }
                            File.WriteAllLines(saveFileDialog.FileName, outputCSV, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
        }

        private void buttonADD_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = textBoxId.Text;
            dataGridView1.Rows[n].Cells[1].Value = textBoxName.Text;
            dataGridView1.Rows[n].Cells[2].Value = comboBoxMajor.Text;
            dataGridView1.Rows[n].Cells[3].Value = textBoxGPA.Text;

            string input = this.textBoxGPA.Text;
            string name = this.textBoxName.Text;

            double dInpu = Convert.ToDouble(input);
            oSMA.addGPA(dInpu, name);

            double gpax = oSMA.gatGPAx();
            textBoxGPAx.Text = gpax.ToString();

            double max = oSMA.getMax();
            textBoxMaxGPA.Text = max.ToString();

            double min = oSMA.gatMin();
            textBoxMinGPA.Text = min.ToString();
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            //TODO add data to data gridView
            //TODO calculate GPAx, Max, Min
        }
        */
    }
}