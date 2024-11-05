using my_XO_Game.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace my_XO_Game
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // to reserve the pos that played on 




        PlayerTurn turn = PlayerTurn.player1;
        GameState state ;
   
        enum PlayerTurn
        { player1, player2 }
        enum GameState
        { player1, player2, Draw, inproggres }

        void change_player_turn()
        {
            if (turn == PlayerTurn.player1)
            {
                lblPLayer.Text = "player2";
                turn = PlayerTurn.player2;
            }
            else
            {
                lblPLayer.Text = "player1";
                turn = PlayerTurn.player1;
            }
        }


        void check_player_image(Button b)
        {
            if (turn == PlayerTurn.player1)
            {
                b.Tag = "X";
                b.Image = Resources.X;


            }
            else { b.Tag = "O";
                b.Image = Resources.O; }

        }

        bool is_box_empty(Button b)
        {
            return (b.Image == null);
        }
        bool is_draw()
        {
            return !is_box_empty(bn1) && !is_box_empty(bn2) && !is_box_empty(bn3) &&
                   !is_box_empty(bn4) && !is_box_empty(bn5) && !is_box_empty(bn6) &&
                   !is_box_empty(bn7) && !is_box_empty(bn8) && !is_box_empty(bn9);
        }
        void show_wrong_poss_message()
        {
            MessageBox.Show("cant choose this box ", "choosing warning ", MessageBoxButtons.OK);
        }
        void end_game_message()
        {
            MessageBox.Show("Game over ", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }




        bool Ditermin_the_win(Button bn1, Button bn2, Button bn3)
        {
            

            if (bn1.Tag != null && bn1.Tag==bn2.Tag&&bn2.Tag==bn3.Tag)
            {
             bn1.BackColor = Color.Green;
                bn2.BackColor = Color.Green;
                bn3.BackColor = Color.Green;
                return true;
            }
            else
            {
             
                return false;
            }




        }

        GameState Detirmin_Winner()
        {
            if (turn == PlayerTurn.player1)
            {
 
                return GameState.player1;
            }
            else
            {

                return GameState.player2;
            }


        }
        GameState Check_Game_state()
        {
            if (Ditermin_the_win(bn1, bn2, bn3) ||
                Ditermin_the_win(bn4, bn5, bn6) ||
                Ditermin_the_win(bn7, bn8, bn9) ||
                Ditermin_the_win(bn1, bn4, bn7) ||
                Ditermin_the_win(bn2, bn5, bn8) ||
                Ditermin_the_win(bn3, bn6, bn9) ||
                Ditermin_the_win(bn1, bn5, bn9) ||
                Ditermin_the_win(bn3, bn5, bn7))
            {
                return Detirmin_Winner();

            }
            else
            {
                if (is_draw())
                {
                    return GameState.Draw;
                }
                else
                {
                    
                    return GameState.inproggres; }
            }



        }


        void PlayRound(Button bn)
        {
            if (!is_box_empty(bn))
            {
                show_wrong_poss_message();
            }
            else
            { 
                check_player_image(bn);

                GameState newState = Check_Game_state(); // Get the new game state  
                if (newState == GameState.player1)
                {
                    lblwinner.Text = "player1";
                    end_game_message();
                    state = newState; // Update the state variable  
                }
                else if (newState == GameState.player2)
                {
                    lblwinner.Text = "player2";
                    end_game_message();
                    state = newState; // Update the state variable  
                }
                else if (newState == GameState.Draw)
                {
                    lblwinner.Text = "Draw";
                    end_game_message();
                    state = newState; // Update the state variable  
                }
                else
                {
                    change_player_turn();
                    state = newState; // Update the state variable  
                }
            }
        }





        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.Azure;
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;
            //whitePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            //draw Vertical lines
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);



        }
      
      

        void reset_button(Button b)
        {
            b.BackColor = Color.Transparent;
            b.Tag = null;
            b.Image= null;
            
        }
        void reset_Game()
        {
            reset_button(bn1);
            reset_button(bn2);
            reset_button(bn3);
            reset_button(bn4);
            reset_button(bn5);  
            reset_button(bn6);
            reset_button(bn7);
            reset_button(bn8);
            reset_button(bn9);
            lblPLayer.Text = "Player1";
            lblwinner.Text = "in Proggres";
            state = GameState.inproggres;
            turn = PlayerTurn.player1;
        }

        private void button_click(object sender, EventArgs e)
        {

            PlayRound((Button)sender);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            reset_Game();
        }
    }
}
