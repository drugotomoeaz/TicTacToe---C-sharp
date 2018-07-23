using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PlayTicTacToe();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadButtons(TicTacToe chess)
        {
            this.Controls.AddRange(chess.ButtonList);
        }

        public void PlayTicTacToe()
        {
            var chess = new TicTacToe();
            LoadButtons(chess);
            Click_Buttons(chess);
             
        }

        private void MenuBar()
        {
            var result = MessageBox.Show("Do you want to play again?", "End", MessageBoxButtons.YesNo) ;
            if (result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                RemoveControls();
                InitializeComponent();
                PlayTicTacToe();
            }
        }


        public void RemoveControls()
        {
            this.Controls.Clear();
        }

        
        //Gameplay
        public void WhoWon(int player)
        {
            if (player < 50)
            {
                label2.Visible = false;
                label1.Visible = true;
                if (player == 0) label1.Text = "Player X won!\n Congratulations!";
                else if (player == 1) label1.Text = "Player O won!\n Congratulations!";
            }
        }

        public void Click_Buttons(TicTacToe chess)
        {
            foreach (Button item in chess.ButtonList)
            {
                item.Click += new System.EventHandler(button_click);
                void button_click(object sender, EventArgs e)
                {
                    item.Enabled = false;
                    item.Text = String.Format($"{(chess.values[chess.turn % 2])}");
                    chess.Choices[Int32.Parse(item.Name)] = chess.values[chess.turn % 2];
                    chess.turn += 1;
                    label2.Text = String.Format($"{(chess.values[(chess.turn % 2) + 2])}");
                    if (chess.turn > 0) label1.Visible = false;
                    chess.Check();
                    WhoWon(chess.result);
                    if (chess.Check())
                    {
                        EndGame(chess);
                        RemoveControls();
                        this.Controls.Add(this.label1);
                        this.Controls.Add(this.pictureBox1);
                        pictureBox1.Visible = true;

                    }
                    if (chess.Check() || chess.turn == 9) MenuBar();
                }
            }
        }

        public void EndGame(TicTacToe chess)
        {
            foreach (Button item in chess.ButtonList)
            {
                item.Enabled = false;
            }
        }




    public class TicTacToe
    {
        public Button[] ButtonList { set; get; }
        public string[] Choices { set; get; }
        private int[][] Positions { set; get; }
        public int turn = 0;
        public string[] values = new String[4] { "X", "O", "Player X:", "Player O:" };
        public int result = 100; 

        //Constructor
        public TicTacToe()
        {
            ButtonList = new Button[9];
            Choices = new string[9] { "A", "A", "A", "A", "A", "A", "A", "A", "A"};
            GenButtons();
            Positions = new int[8][] {
                    //rows
                    new int[] { 0,1,2},
                    new int[] { 3,4,5},
                    new int[] { 6,7,8},
                    //columns
                    new int[] {0,3,6 },
                    new int[] {1,4,7 },
                    new int[] {2,5,8 },
                    //diagonals
                    new int[] { 0,4,8},
                    new int[] {2,4,6 } };
        }

        //Generate the buttons for ButtonList. Invoked when an instance is created.
        public void GenButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                    ButtonList[i] = new Button
                    {
                        Location = new System.Drawing.Point(25 + (100 * (i % 3)), 100 + (100 * (i / 3))),
                        Name = i.ToString(),
                        //Text = i.ToString(),
                        Font =  new System.Drawing.Font("Arial",20,FontStyle.Bold),
                        
                        Size = new System.Drawing.Size(100, 100),
                        ForeColor = System.Drawing.Color.FromArgb(100,33,56,206),
                        BackColor = System.Drawing.Color.FromArgb(100,248,170,237),
                        //BackColor = System.Drawing.Color.FromArgb(100, 33, 56, 206),

                    };
            }
        }

            public bool Check()
            //Return a value lt 2 if someone wins
            {
                foreach (int[] item in Positions)
                {
                    //Indices
                    int x = item[0];
                    int y = item[1];
                    int z = item[2];
                    int player1 = 0;
                    int player2 = 1;

                    if (Choices[x] == "X" & Choices[y] == "X" & Choices[z] == "X")
                    {
                        result = player1;
                        return true;
                    }
                    else if (Choices[x] == "O" & Choices[y] == "O" & Choices[z] == "O")
                    {
                        result = player2;
                        return true;
                    }
                    else result = 99;
                }
                return false;
            }
            
        }
    }    
    }

