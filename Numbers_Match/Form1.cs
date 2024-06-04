using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Numbers_Match
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        //the counter is To decide where to store the button......
        byte _counter = 0;
        int _Score = 0;
        int _CurrentTime = 0;
        struct stHoldedNumber
        {
            public System.Windows.Forms.Button B1;
            public System.Windows.Forms.Button B2;
        }

        class clsSettings
        {
            public int GameTime;
            public int ScoreTarget;

            public clsSettings(int _GameTime, int _ScoreTarget)
            {
                GameTime = _GameTime;
                ScoreTarget = _ScoreTarget;
            }
        }

        class clsStats
        {
            public int WrongMoves;
            public int CorrectMoves;

            public clsStats(int _WrongMoves, int _CorrectMoves)
            {
                WrongMoves = _WrongMoves;
                CorrectMoves = _CorrectMoves;
            }

        }

        clsStats Stats = new clsStats(0, 0);

        clsSettings Settings = new clsSettings(0, 0);

        stHoldedNumber HoldedNumber;      


        void PutNumbersInStruct(object sender)
        {
            if (_counter == 0)
            {
                HoldedNumber.B1 = ((System.Windows.Forms.Button)(sender));
                _counter++;
            }            
            else
            {
                HoldedNumber.B2 = ((System.Windows.Forms.Button)(sender));
                _counter = 0;
            }
        }

        void ResetStruct()
        {
            HoldedNumber.B1 = null;
            HoldedNumber.B2 = null;
            _counter = 0;
        }

        bool AreNumbersValid(int Button1Val, int Button2Val)
        {
            if ((Button1Val == Button2Val) || (Button1Val + Button2Val) == 10)
            {
                _Score = _Score + 2;
                lblScore.Text = _Score.ToString();
                Stats.CorrectMoves++;
                return true;
            }

            Stats.WrongMoves++;

            return false;

        }

        bool AreNumbersPositionValid(string Button1Name, string Button2Name)
        {
            int Button1Number = Convert.ToInt16(Button1Name.Substring(6, Button1Name.Length - 6));
            int Button2Number = Convert.ToInt16(Button2Name.Substring(6, Button2Name.Length - 6));

            if ((Button1Number + 1) == Button2Number || (Button2Number + 1) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }

            if ((Button1Number + 20) == Button2Number || (Button2Number + 20) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }

            if ((Button1Number + 19) == Button2Number || (Button2Number - 19) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }

            if ((Button1Number + 21) == Button2Number || (Button2Number - 21) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }

            if ((Button1Number - 21) == Button2Number || (Button2Number + 21) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }

            if ((Button1Number - 19) == Button2Number || (Button2Number + 19) == Button1Number)
            {
                if (AreNumbersValid(Convert.ToInt16(HoldedNumber.B1.Text), Convert.ToInt16(HoldedNumber.B2.Text)))
                    return true;
            }


            return false;


        }

        private void button_Click(object sender, EventArgs e)
        {
            PutNumbersInStruct(sender);
            if (HoldedNumber.B1 != null && HoldedNumber.B2 != null)
            {
                if (AreNumbersPositionValid(HoldedNumber.B1.Name, HoldedNumber.B2.Name))
                {
                    HoldedNumber.B1.Enabled = false;
                    HoldedNumber.B2.Enabled = false;
                    ResetStruct();
                }

            }

        }

        void EnableOrDisableSettings(bool Enable)
        {
            txtGameTime.Enabled = Enable;
            txtScoreTarget.Enabled = Enable;
            btnStart.Enabled = Enable;
        }

        bool AreSettingsValid()
        {
            if (string.IsNullOrEmpty(txtGameTime.Text) || string.IsNullOrEmpty(txtScoreTarget.Text))
                return false;

            return true;
        }

        void DisableAllButtons(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                System.Windows.Forms.Button tb = c as System.Windows.Forms.Button;

                if (tb != null)
                    tb.Enabled = false;
            }
        }

        void EnableAllButtons(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                System.Windows.Forms.Button tb = c as System.Windows.Forms.Button;

                if (tb != null)
                    tb.Enabled = true;
            }
        }

        private void FillButtonsWithRndNums(Control parent)
        {
            Random Rnd = new Random();
            foreach (Control c in parent.Controls)
            {
                System.Windows.Forms.Button tb = c as System.Windows.Forms.Button;

                if (tb != null)
                    tb.Text = Rnd.Next(0,9).ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisableAllButtons(gbButtons);
        }

        void ResetSettings()
        {
            Settings = new clsSettings(0, 0);
            txtGameTime.Text = "";
            txtScoreTarget.Text = "";
            timer1.Enabled = false;
            progressBar1.Value = 0;          
            lblTime.Text = 0.ToString();
            lblScore.Text = 0.ToString();
            btnStart.Enabled = true;
            _CurrentTime = 0;
            _Score = 0;
            
        }

        void ShowStats()
        {
            lblRightMoves.Text = Stats.CorrectMoves.ToString();
            lblWrongMoves.Text = Stats.WrongMoves.ToString();
            lblStatsScore.Text = _Score.ToString();

        }

        void ResetStats()
        {
            Stats = new clsStats(0, 0);
            lblRightMoves.Text = Stats.CorrectMoves.ToString();
            lblWrongMoves.Text = Stats.WrongMoves.ToString();
            lblStatsScore.Text = _Score.ToString();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are You Sure You Wanna Shuffle The Numbers And Start Over?\n" +
                "Notice : Your Score Will Be Reseted.","Shuffle?",MessageBoxButtons.OKCancel) 
                == DialogResult.OK) 
            {
                FillButtonsWithRndNums(gbButtons);
                _Score = 0;
                lblScore.Text = _Score.ToString();
                EnableAllButtons(gbButtons);
                ResetStats();
                ResetSettings();
                EnableOrDisableSettings(true);
                DisableAllButtons(gbButtons);
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!AreSettingsValid())
            {
                MessageBox.Show("Please Enter Valid Settings");
                return;
            }

            Settings.GameTime = Convert.ToInt16(txtGameTime.Text);
            Settings.ScoreTarget = Convert.ToInt16(txtScoreTarget.Text);


            FillButtonsWithRndNums(gbButtons);
            EnableAllButtons(gbButtons);
            EnableOrDisableSettings(false);
            ResetStats();

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Settings.GameTime;
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _CurrentTime++;
            progressBar1.Value++;
            lblTime.Text = progressBar1.Value.ToString();
            lblTime.Refresh();

            if (_Score >= Settings.ScoreTarget)
            {
                timer1.Enabled = false;
                MessageBox.Show("Good Job!\nYour Score Is " + Settings.ScoreTarget);
                ShowStats();
                DisableAllButtons(gbButtons);
                ResetSettings();
                EnableOrDisableSettings(true);
                return;
            }


            if (_CurrentTime == Settings.GameTime)
            {
                timer1.Enabled = false;
                if (_Score >= Settings.ScoreTarget)
                {
                    MessageBox.Show("Time's Up!\nCongrats! You Won");
                }
                else
                {
                    MessageBox.Show("Time's Up!\nYou Failed! :(");

                }

                ShowStats();
                DisableAllButtons(gbButtons);
                ResetSettings();
                EnableOrDisableSettings(true);
            }

        }
    }
}
