using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using System.Windows.Forms;

namespace MathQuiz.App
{
    public partial class Form1 : Form
    {
        private int tried;
        private int correct;
        private int numberOfQuestions;
        private int maxNumberOfQuestions = 15;
        private double answer;
        private bool timeOver;
        private int timeLeft;
        private Timer timer;
        private Calculation calc;
        private Random random;

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            
            calc = new Calculation();

            random = new Random();

            reset();
        }

        public void uncheckQuestionTypes(CheckBox cbun1, CheckBox cbun2, CheckBox cbun3)
        {
            cbun1.CheckState = CheckState.Unchecked;
            cbun2.CheckState = CheckState.Unchecked;
            cbun3.CheckState = CheckState.Unchecked;
        }

        private void cb_addition_MouseClick(object sender, MouseEventArgs e)
        {
            uncheckQuestionTypes(cb_subtraction, cb_division, cb_multiplication);
        }

        private void cb_subtraction_MouseClick(object sender, MouseEventArgs e)
        {
            uncheckQuestionTypes(cb_addition, cb_division, cb_multiplication);
        }

        private void cb_division_MouseClick(object sender, MouseEventArgs e)
        {
            uncheckQuestionTypes(cb_addition, cb_subtraction, cb_multiplication);
        }

        private void cb_multiplication_MouseClick(object sender, MouseEventArgs e)
        {
            uncheckQuestionTypes(cb_addition, cb_subtraction, cb_division);
        }
                                
        private void rb_on_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_on.Checked)
            {
                txb_timer.Show();
            }
        }

        private void rb_off_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_off.Checked)
            {
                txb_timer.Hide();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_checkAnswer_Click(object sender, EventArgs e)
        {
            tried++;

            var answerString = txb_question.Text;
            var ans = answerString.Split('=');
            answer = double.Parse(ans[1].Trim());

            if (calc.ensureAnswerCorrect(answer))
            {
                correct++;
                MessageBox.Show("Correct", "Correct", MessageBoxButtons.OK);

                if (MessageBoxButtons.OK == 0)
                {
                    checkToContinue();
                }
            }
            else
            {
                MessageBox.Show("Incorrect", "Incorrect", MessageBoxButtons.OK);

                if (MessageBoxButtons.OK == 0)
                {
                    checkToContinue();
                }
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            startQuiz();
        }

        public void startQuiz()
        {
            setNumberOfQuestions();

            if (rb_on.Checked)
            {
                timer.Enabled = true;
                timer.Tick += Timer_Tick;
            }
            else
            {
                timer.Enabled = false;
            }

            hideButtons();

            nextQuestion();
        }

        public void endQuiz()
        {
            timer.Stop();
            timer.Dispose();

            MessageBox.Show("Quiz over...", "End", MessageBoxButtons.OK, MessageBoxIcon.Information);

            reset();

            showButtons();
        }

        public void reset()
        {
            tried = 0;
            correct = 0;

            timeOver = false;
            timeLeft = 30;

            cb_addition.Checked = true;
            rb_random.Checked = true;
            rb_on.Checked = true;

            txb_timer.ForeColor = Color.Black;
            txb_timer.Text = "";
            txb_tried.Text = $"{tried}";
            txb_correct.Text = $"{correct}";
            txb_question.Text = "";

            lbl_questionCount.Text = "0 / 0";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft <= 15)
            {
                txb_timer.ForeColor = Color.Red;
            }
            txb_timer.Text = $"{timeLeft}";
            timeLeft--;

            if (timeLeft == 0 || tried == numberOfQuestions)
            {
                timeOver = true;
                endQuiz();
            }
        }

        public void setNumberOfQuestions()
        {
            if (rb_1.Checked)
            {
                numberOfQuestions = 1;
            } 
            else if (rb_2.Checked)
            {
                numberOfQuestions = 2;
            } 
            else if (rb_3.Checked)
            {
                numberOfQuestions = 3;
            } 
            else if (rb_4.Checked)
            {
                numberOfQuestions = 4;
            } 
            else if (rb_5.Checked)
            {
                numberOfQuestions = 5;
            } 
            else if (rb_6.Checked)
            {
                numberOfQuestions = 6;
            } 
            else if (rb_7.Checked)
            {
                numberOfQuestions = 7;
            } 
            else if (rb_8.Checked)
            {
                numberOfQuestions = 8;
            } 
            else if (rb_9.Checked)
            {
                numberOfQuestions = 9;
            } 
            else
            {
                numberOfQuestions = random.Next(1, maxNumberOfQuestions + 1);
            }
        }

        public void showButtons()
        {
            btn_exit.Visible = true;
            btn_start.Visible = true;
        }

        public void hideButtons()
        {
            btn_exit.Visible = false;
            btn_start.Visible = false;
        }

        public void displayQuestionCount()
        {
            lbl_questionCount.Text = $"{tried + 1} / {numberOfQuestions}";
            txb_tried.Text = $"{tried}";
            txb_correct.Text = $"{correct}";
        }

        public void nextQuestion()
        {
            displayQuestionCount();

            if (cb_addition.Checked)
            {
                txb_question.Text = $"{calc.add()}";
            } 
            else if (cb_subtraction.Checked)
            {
                txb_question.Text = $"{calc.subtract()}";
            } 
            else if (cb_division.Checked)
            {
                txb_question.Text = $"{calc.divide()}";
            }
            else
            {
                txb_question.Text = $"{calc.multiply()}";
            }
        }

        public void checkToContinue()
        {
            if (rb_on.Checked)
            {
                if (!timeOver)
                {
                    nextQuestion();
                }
                else
                {
                    endQuiz();
                }
            }
            else
            {
                if (tried != numberOfQuestions)
                {
                    nextQuestion();
                }
                else
                {
                    endQuiz();
                }
            }
        }
    }
}
