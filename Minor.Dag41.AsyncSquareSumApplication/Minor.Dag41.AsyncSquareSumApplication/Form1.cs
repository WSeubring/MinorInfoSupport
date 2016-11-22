using InfoSupport.Threading.MathLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minor.Dag41.AsyncSquareSumApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SlowMath math = new SlowMath();

            int num1 = int.Parse(textBox1.Text);
            int num2 = int.Parse(textBox2.Text);
            int num3 = int.Parse(textBox3.Text);
            var thread = new Thread(()=>SumOfSquare(num1, num2, num3));
            thread.Start();
        }

        private void SumOfSquare(int num1, int num2, int num3)
        {
            var math = new SlowMath();
            var arnum1 = math.BeginSquare(num1, null, null);
            var arnum2 = math.BeginSquare(num2, null, null);
            var arnum3 = math.BeginSquare(num3, null, null);

            var square1 = math.EndSquare(arnum1);
            var square2 = math.EndSquare(arnum2);
            var square3 = math.EndSquare(arnum3);

            var sum = square1 + square3 + square2;

            Action print = () => textBox4.Text = sum.ToString();
            this.Invoke(print);
        }
    }
}
