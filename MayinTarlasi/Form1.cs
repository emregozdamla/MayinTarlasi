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


namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int columnNumber = 10;
        public static int rowNumber = 10;
        public static bool gameOver = false;
        public static bool winned = false;
        static FButton[,] btnArray = new FButton[columnNumber, rowNumber];
        public static void Show()
        {
            int i = 0;
            int j = 0;
            while (i < columnNumber)
            {
                j = 0;
                while (j < rowNumber)
                {
                    if (btnArray[i, j].IsMined == true)
                    {
                        
                    }
                    if (btnArray[i, j].IsMined == false)
                    {
                        btnArray[i, j].Text = "S";
                        
                    }
                    j++;
                }
                i++;
            }
        }
        public static bool Winned()
        {
            int count = 0;
            for (int i = 0; i < columnNumber; i++)
            {
                for (int j = 0; j < rowNumber; j++)
                {
                    if ((btnArray[i,j]).IsOpened == true) count++;
                }


            }
            if (count >= (((columnNumber * rowNumber) * 90) / 100)) return true;
            else return false;

        }
        public static void DetermineCounts()
        {
            int i = 0;
            int j = 0;
            while(i<columnNumber)
            {
                j = 0;
                while(j<rowNumber)
                {
                    try 
                    {
                        if (btnArray[i-1,j].IsMined==true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }catch(Exception o) { }
                    try
                    {
                        if (btnArray[i + 1, j].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i , j-1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i, j + 1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i-1, j - 1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i - 1, j + 1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i + 1, j - 1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    try
                    {
                        if (btnArray[i + 1, j + 1].IsMined == true)
                        {
                            btnArray[i, j].Count += 1;
                        }
                    }
                    catch (Exception o) { }
                    j++;

                } i++;
            }
        }
        public static void Open(FButton btn)
        {
            int a = 0;
            int b = 0;
            int k = 0;
            int l = 0;
            while(k<columnNumber)
            {
                l = 0;
                while(l<rowNumber)
                {
                    if (btnArray[k,l]==btn)
                    { a = k;b = l; }
                    l++;
                }
                k++;
            }
            if(a<0 || a>=columnNumber || b<0 ||b>=rowNumber 
                || btnArray[a,b].IsOpened==true || btnArray[a,b].Enabled==false) return;
            else if (btnArray[a,b].Count==0 && btnArray[a,b].IsMined==false)
            {
                btnArray[a, b].IsOpened = true;
                btnArray[a, b].Enabled = false;
                btnArray[a, b].BackgroundImage = null;
                btnArray[a, b].IsFlagged = false;
                btnArray[a, b].Text = "";

                try { Open(btnArray[a - 1, b]); } catch(Exception o) { }
                try { Open(btnArray[a + 1, b]); } catch (Exception o) { }
                try { Open(btnArray[a, b - 1]); } catch (Exception o) { }
                try { Open(btnArray[a, b + 1]); } catch (Exception o) { }
                try { Open(btnArray[a - 1, b - 1]); } catch (Exception o) { }
                try { Open(btnArray[a - 1, b + 1]); } catch (Exception o) { }
                try { Open(btnArray[a + 1, b - 1]); } catch (Exception o) { }
                try { Open(btnArray[a + 1, b + 1]); } catch (Exception o) { }
                
            
                return;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            tableLayoutPanel1.Visible = true;
            timer1.Start();
            

            int i = 0;
            int j = 0;
            while (i<columnNumber)
            {
                j = 0;
                while(j<rowNumber)
                {
                    FButton btn = new FButton();
                    btn.Click += btn_Click;
                    btn.MouseUp += btn_MouseUp;
                    btn.Size = new Size(50, 50);
                    btnArray[i,j]=btn;
                    tableLayoutPanel1.Controls.Add(btn);
                    j++;
                }
                i++;
                
            }
            i = 0;
            while(i<(columnNumber*rowNumber)/10)
            {
                Random rnd1 = new Random();
                int a=  rnd1.Next(columnNumber);
                int b = rnd1.Next(rowNumber);
                if (btnArray[a,b].IsMined==false)
                {
                    btnArray[a, b].IsMined = true;
                }
                else 
                {
                    i--;
                }
                i++;
            }
            DetermineCounts();
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            FButton btn = (FButton) sender;
            if(e.Button==MouseButtons.Right)
            {
                if(btn.IsOpened==false && btn.IsFlagged==false)
                {
                    //btn.Text = "f";
                    btn.BackgroundImage = Image.FromFile("flag.png");
                    btn.IsFlagged = true;
                }
                else if (btn.IsOpened == false && btn.IsFlagged == true)
                {
                    //btn.Text = "f";
                    btn.BackgroundImage = null;
                    btn.IsFlagged = false;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            FButton btn = (FButton)sender;
            int k = 0;
            int l = 0;
            if(btn.IsMined==true)
            {
                gameOver = true;
            }
            if (btn.IsMined==false)
            {
                if (btn.Count > 0)
                { btn.Text = Convert.ToString(btn.Count); btn.Enabled = false;
                    btn.IsOpened = true; btn.Font = new Font("Verdana", 16);
                    btn.BackgroundImage = null; btn.IsFlagged = false;
                }
                if (btn.Count == 0)
                { Open(btn);  }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Show();
            
            if (Winned() == true)
            {
                int i = 0;
                int j = 0;
                while (i < columnNumber)
                {
                    j = 0;
                    while (j < rowNumber)
                    {
                        if (btnArray[i, j].IsMined == true)
                        {
                            //btnArray[i, j].Text = "R";
                            btnArray[i, j].BackgroundImage = Image.FromFile("rose.png");
                            btnArray[i, j].Enabled = false;
                        }
                        if (btnArray[i, j].IsMined == false)
                        {
                            btnArray[i, j].BackgroundImage = null;
                            btnArray[i, j].Text = "";
                            btnArray[i, j].Enabled = false;
                        }
                        j++;
                    }
                    i++;
                }
                timer1.Stop();
            }
            else if (gameOver == true)
            {
                int i = 0;
                int j = 0;
                while (i < columnNumber)
                {
                    j = 0;
                    while (j < rowNumber)
                    {
                        if (btnArray[i, j].IsMined == true)
                        {
                            //btnArray[i, j].Text = "M";
                            btnArray[i, j].BackgroundImage = Image.FromFile("mine.png");
                            btnArray[i, j].Enabled = false;
                        }
                        if (btnArray[i, j].IsMined == false)
                        {
                            btnArray[i, j].Text = "";
                            btnArray[i, j].BackgroundImage = null;
                            btnArray[i, j].Enabled = false;
                        }
                        j++;
                    }
                    i++;
                }
                timer1.Stop();
            }
        }
    }
}
