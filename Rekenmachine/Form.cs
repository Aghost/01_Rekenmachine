using System;
using System.Globalization;
using System.Windows.Forms;

namespace Rekenmachine
{
    public partial class Form : System.Windows.Forms.Form
    {
        bool operatorActive = false;
        string operation = "";
        double result = 0;

        CultureInfo customCulture = new CultureInfo("en-US");

        public Form()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = customCulture;
        }

        public string Calculate_Operation(string input_operator, string target)
        {
            switch (input_operator)
            {
                case "+": target = (result + Double.Parse(target)).ToString(); break;
                case "-": target = (result - Double.Parse(target)).ToString(); break;
                case "*": target = (result * Double.Parse(target)).ToString(); break;
                case "/": target = (result / Double.Parse(target)).ToString(); break;
                default: break;
            }
            return target;
        }

        private void Number_Event(object sender, EventArgs e)
        {
            if ((textBox.Text == "0") || (operatorActive))
                textBox.Clear();

            Button b = (Button)sender;
            operatorActive = false;

            if (b.Text == ".")
            {
                if (!textBox.Text.Contains("."))
                    textBox.Text += b.Text;
            }
            else
            {
                textBox.Text += b.Text;
            }
        }

        private void Operator_Event(object sender, EventArgs e)
        {
            operatorActive = true;
            Button b = (Button)sender;
            string newOperation = b.Text;

            lbResult.Text = lbResult.Text + ' ' + textBox.Text + ' ' + newOperation;

            switch(operation)
            {
                case "+": textBox.Text = (result + Double.Parse(textBox.Text)).ToString(); break;
                case "-": textBox.Text = (result - Double.Parse(textBox.Text)).ToString(); break;
                case "*": textBox.Text = (result * Double.Parse(textBox.Text)).ToString(); break;
                case "/": textBox.Text = (result / Double.Parse(textBox.Text)).ToString(); break;
                default: break;
            }

            // Calculate_Operation(operation, textBox.Text);
            result = Double.Parse(textBox.Text);
            operation = newOperation;
        }

        private void CE_Click(object sender, MouseEventArgs e)
        {
            textBox.Text = "0";
        }

        private void C_Click(object sender, MouseEventArgs e)
        {
            textBox.Text = "0";
            lbResult.Text = "";
            lbEquation.Text = "";
            operation = "";
            result = 0;
        }

        private void equals_b_Click(object sender, EventArgs e)
        {
            lbEquation.Text = lbResult.Text + ' ' + textBox.Text + " = ";
            operatorActive = true;

            switch (operation)
            {
                case "+": textBox.Text = (result + Double.Parse(textBox.Text)).ToString(); break;
                case "-": textBox.Text = (result - Double.Parse(textBox.Text)).ToString(); break;
                case "*": textBox.Text = (result * Double.Parse(textBox.Text)).ToString(); break;
                case "/": textBox.Text = (result / Double.Parse(textBox.Text)).ToString(); break;
                default: break;
            }

            result = Double.Parse(textBox.Text);
            textBox.Text = result.ToString(customCulture);
            
            lbEquation.Text += textBox.Text;
                        
            lbResult.Text = "";
            operation = "";
            result = 0;
        }
    }
}