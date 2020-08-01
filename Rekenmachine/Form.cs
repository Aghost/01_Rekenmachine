using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Rekenmachine
{
    public partial class Form : System.Windows.Forms.Form
    {
        bool operatorActive = false;
        bool valutaActive = false;
        string operation = "";
        double result = 0;

        CultureInfo customCulture = new CultureInfo("en-US");

        public Form()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = customCulture;
        }

        public string Calculate_Operation(string input_operator, Double result, string target)
        {
            switch (target)
            {
                case "": target = "0"; break;
                case ".": target = "0.0"; break;
                case "0.": target = "0.0"; break;
                default: break;
             }

            switch (input_operator)
            {
                case "+": target = (result + Double.Parse(target)).ToString(); break;
                case "-": target = (result - Double.Parse(target)).ToString(); break;
                case "*": target = (result * Double.Parse(target)).ToString(); break;
                case "/": target = (result / Double.Parse(target)).ToString(); break;
                case "%": target = ((result * Double.Parse(target) / 100) ).ToString(); break;
                default: break;
            }

            if (valutaActive == true) { target = decimal.Parse(target).ToString("0.00"); return target; }
            else return target;
        }

        private void Number_Event(object sender, EventArgs e)
        {
            if ((textBox.Text == "0") || (operatorActive))
                textBox.Clear();

            Button b = (Button)sender;
            operatorActive = false;

            switch (b.Text)
            {
                case ".": if (!textBox.Text.Contains(".")) {textBox.Text += b.Text ;}; break;
                default: textBox.Text += b.Text; break;
            }
        }

        private void Operator_Event(object sender, EventArgs e)
        {
            operatorActive = true;
            Button b = (Button)sender;

            string newOperation = b.Text;
            lbResult.Text = lbResult.Text + ' ' + Format_Equation(textBox.Text) + ' ' + newOperation;

            result = Double.Parse(Calculate_Operation(operation, result, textBox.Text));
            operation = newOperation;
        }

        private string Format_Equation(string target)
        {
            if (target.EndsWith("."))
                return target.Remove(target.Length - 1, 1);
            else if (target.StartsWith('.'))
                return '0' + target;
            else
                return target;
        }

        private void equals_b_Click(object sender, EventArgs e)
        {
            operatorActive = true;
       
            result = Double.Parse(Calculate_Operation(operation, result, textBox.Text));
            lbEquation.Text = lbResult.Text + ' ' + Format_Equation(textBox.Text) + " = ";

            textBox.Text = result.ToString(customCulture);

            lbEquation.Text += textBox.Text;
                        
            lbResult.Text = "";
            operation = "";
            result = 0;
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

        private void btn_valuta_Click(object sender, EventArgs e)
        {
            valutaActive = !valutaActive;
            if (valutaActive)
                btn_valuta.BackColor = Color.LimeGreen;
            else
                btn_valuta.BackColor = Color.FromName("InactiveBorder");
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length > 0)
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
        }
    }
}