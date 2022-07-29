using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void zero_Click(object sender, EventArgs e)
        {
            input.Text += "0";
        }

        private void one_Click(object sender, EventArgs e)
        {
            input.Text += "1";
        }

        private void two_Click(object sender, EventArgs e)
        {
            input.Text += "2";
        }

        private void three_Click(object sender, EventArgs e)
        {
            input.Text += "3";
        }

        private void four_Click(object sender, EventArgs e)
        {
            input.Text += "4";
        }

        private void five_Click(object sender, EventArgs e)
        {
            input.Text += "5";
        }

        private void six_Click(object sender, EventArgs e)
        {
            input.Text += "6";
        }

        private void seven_Click(object sender, EventArgs e)
        {
            input.Text += "7";
        }

        private void eight_Click(object sender, EventArgs e)
        {
            input.Text += "8";
        }

        private void nine_Click(object sender, EventArgs e)
        {
            input.Text += "9";
        }

        private void point_Click(object sender, EventArgs e)
        {
            input.Text += ".";
        }

        private void plusNumbers_Click(object sender, EventArgs e)
        {
            input.Text += "+";
        }

        private void subtract_Click(object sender, EventArgs e)
        {
            input.Text += "-";
        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            input.Text += "*";
        }

        private void division_Click(object sender, EventArgs e)
        {
            input.Text += "/";
        }

        private void bracketRight_Click(object sender, EventArgs e)
        {
            input.Text += ")";
        }

        private void bracketLeft_Click(object sender, EventArgs e)
        {
            input.Text += "(";
        }



        private void equal_Click_1(object sender, EventArgs e)
        {
            string RPN;
            Calculate calculate = new Calculate();

            RPN = calculate.CalculateExpression(input.Text);
            input.Clear();
            input.Text = RPN;
        }
    }
}







