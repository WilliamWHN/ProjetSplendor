/**
 * \file      frmAddVideoGames.cs
 * \author    F. Andolfatto
 * \version   1.0
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game
        private int nbRubis;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;
        private string NewPlayerName;
        private int NbTotalCoins;

        //used to store the number of coins there is on table
        private int NbDiamandAvailable = 7;
        private int NbEmeraudeAvailable = 7;
        private int NbOnyxAvailable = 7;
        private int NbRubisAvailable = 7;
        private int NbSaphirAvailable = 7;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel = false;
        private bool enableClicOnRubis = false;
        private bool enableClicOnOnyx = false;
        private bool enableClicOnEmeraude = false;
        private bool enableClicOnDiamand = false;
        private bool enableClicOnSaphir = false;
        //connection to the database
        private ConnectionDB conn;

        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {
            

            lblGoldCoin.Text = "5";

            lblDiamandCoin.Text = NbDiamandAvailable + "";
            lblEmeraudeCoin.Text = NbEmeraudeAvailable + "" ;
            lblOnyxCoin.Text = NbOnyxAvailable + "";
            lblRubisCoin.Text = NbRubisAvailable + "";
            lblSaphirCoin.Text = NbSaphirAvailable + "";

            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            Card card11 = new Card();
            card11.Level = 1;
            card11.PrestigePt = 1;
            card11.Cout = new int[] { 1, 0, 2, 0, 2 };
            card11.Ress = Ressources.Rubis;

            Card card12 = new Card();
            card12.Level = 1;
            card12.PrestigePt = 0;
            card12.Cout = new int[] { 0, 1, 2, 1, 0 };
            card12.Ress = Ressources.Saphir;

            txtLevel11.Text = card11.ToString();
            txtLevel12.Text = card12.ToString();

            //load cards from the database
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            //Go through the results
            //Don't forget to check when you are at the end of the stack
            
            //fin TO DO

            this.Width = 680;
            this.Height = 540;

            lblDiamandCoin.Visible = false;
            lblEmeraudeCoin.Visible = false;
            lblOnyxCoin.Visible = false;
            lblSaphirCoin.Visible = false;
            lblRubisCoin.Visible = false;
            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel11.Click += ClickOnCard;
        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;      
            int id = 0;
           
            LoadPlayer(id);

        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            //Put visible the coins
            lblDiamandCoin.Visible = true;
            lblEmeraudeCoin.Visible = true;
            lblOnyxCoin.Visible = true;
            lblSaphirCoin.Visible = true;
            lblRubisCoin.Visible = true;

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[1].ToString();
            lblPlayerRubisCoin.Text = player.Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[4].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
            cmdInsertPlayer.Enabled = false;
        }

        /// <summary>
        /// Check how much coins the user have and compare it to rules
        /// </summary>
        private void ControlCoinOnHand()
        {
            NbTotalCoins = nbRubis + nbDiamand + nbEmeraude + nbOnyx + nbSaphir;
            if (NbTotalCoins == 0)
            {
                enableClicLabel = true;
                enableClicOnRubis = true;
                enableClicOnSaphir = true;
                enableClicOnEmeraude = true;
                enableClicOnDiamand = true;
                enableClicOnOnyx = true;
            }
            else if (NbTotalCoins == 2)
            {
                if (nbRubis == 2 || nbSaphir == 2 || nbEmeraude == 2 || nbOnyx == 2 || nbDiamand == 2)
                {
                    enableClicLabel = false;                  
                }
                ///On test ici les combinaisons  disponibles avec Rubis
                else if(nbRubis == 1 && nbSaphir == 1)
                {
                    enableClicOnRubis = false;
                    enableClicOnSaphir = false;            
                }
                else if(nbRubis == 1 && nbDiamand == 1)
                {
                    enableClicOnRubis = false;
                    enableClicOnDiamand = false;
                }
                else if (nbRubis == 1 && nbEmeraude == 1)
                {
                    enableClicOnRubis = false;
                    enableClicOnEmeraude = false;
                }
                else if (nbRubis == 1 && nbOnyx == 1)
                {
                    enableClicOnRubis = false;
                    enableClicOnOnyx = false;
                }
                //On test ici les combinaisons disponibles avec Saphir
                else if (nbSaphir == 1 && nbDiamand == 1)
                {
                    enableClicOnSaphir = false;
                    enableClicOnDiamand = false;
                }
                else if (nbSaphir == 1 && nbEmeraude == 1)
                {
                    enableClicOnSaphir = false;
                    enableClicOnEmeraude = false;
                }
                else if (nbSaphir == 1 && nbOnyx == 1)
                {
                    enableClicOnSaphir = false;
                    enableClicOnOnyx = false;
                }
                //On test ici les combinaisons  disponibles avec Diamand
                else if (nbDiamand == 1 && nbEmeraude == 1)
                {
                    enableClicOnDiamand = false;
                    enableClicOnEmeraude = false;
                }
                else if (nbDiamand == 1 && nbOnyx == 1)
                {
                    enableClicOnRubis = false;
                    enableClicOnOnyx = false;
                }
                //On test ici les combinaisons  disponibles avec Emeraude
                else if (nbEmeraude == 1 && nbOnyx == 1)
                {
                    enableClicOnEmeraude = false;
                    enableClicOnOnyx = false;
                }
            }
            else if(NbTotalCoins > 2)
            {
                enableClicLabel = false;
            }
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            ControlCoinOnHand();
            if (enableClicLabel == false || enableClicOnRubis == false)
            {
                MessageBox.Show("Vous ne pouvez plus prendre ce jeton");
            }
            else
            {              
                cmdValidateChoice.Visible = true;
                lblChoiceRubis.Visible = true;
                NbRubisAvailable = NbRubisAvailable - 1;
                lblRubisCoin.Text = NbRubisAvailable + "" ;
                nbRubis++;
                lblChoiceRubis.Text = nbRubis + "\r\n";
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            ControlCoinOnHand();
            if (enableClicLabel == false || enableClicOnSaphir == false)
            {
                MessageBox.Show("Vous ne pouvez plus prendre ce jeton");
            }
            else
            {
                cmdValidateChoice.Visible = true;
                lblChoiceSaphir.Visible = true;
                NbSaphirAvailable = NbSaphirAvailable - 1;
                lblSaphirCoin.Text = NbSaphirAvailable + "";
                nbSaphir++;
                lblChoiceSaphir.Text = nbSaphir + "\r\n";
            }

        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {
            ControlCoinOnHand();
            if (enableClicLabel == false || enableClicOnOnyx == false)
            {
                MessageBox.Show("Vous ne pouvez plus prendre ce jeton");
            }
            else
            {
                cmdValidateChoice.Visible = true;
                lblChoiceOnyx.Visible = true;
                NbOnyxAvailable = NbOnyxAvailable - 1;
                lblOnyxCoin.Text = NbOnyxAvailable + "";
                nbOnyx++;
                lblChoiceOnyx.Text = nbOnyx + "\r\n";
            }

        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {
            ControlCoinOnHand();
            if (enableClicLabel == false || enableClicOnEmeraude == false)
            {
                MessageBox.Show("Vous ne pouvez plus prendre ce jeton");
            }
            else
            {
                cmdValidateChoice.Visible = true;
                lblChoiceEmeraude.Visible = true;
                NbEmeraudeAvailable = NbEmeraudeAvailable - 1;
                lblEmeraudeCoin.Text = NbEmeraudeAvailable + "";
                nbEmeraude++;
                lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
            }


        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            ControlCoinOnHand();
            if (enableClicLabel == false || enableClicOnDiamand == false)
            {
                MessageBox.Show("Vous ne pouvez plus prendre ce jeton");
            }
            if (enableClicLabel == true)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceDiamand.Visible = true;
                NbDiamandAvailable = NbDiamandAvailable - 1;
                lblDiamandCoin.Text = NbDiamandAvailable + "";
                nbDiamand++;
                lblChoiceDiamand.Text = nbDiamand + "\r\n";
            }
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time
            
            cmdNextPlayer.Enabled = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            NewPlayerName = Interaction.InputBox("Veuiller entrer le nom du joueur", "Entrer un joueur", "prénom", 500, 500);     
        }
        
        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button
            
        }

        private void lblChoiceRubis_Click(object sender, EventArgs e)
        {
            if (nbRubis == 1)
            {
                lblChoiceRubis.Visible = false;
            }
            NbRubisAvailable = NbRubisAvailable + 1;
            lblRubisCoin.Text = NbRubisAvailable + "";
            nbRubis--;
            lblChoiceRubis.Text = nbRubis + "\r\n";           
        }

        private void lblChoiceSaphir_Click(object sender, EventArgs e)
        {
            if (nbSaphir == 1)
            {
                lblChoiceSaphir.Visible = false;
            }
            NbSaphirAvailable = NbSaphirAvailable + 1;
            lblSaphirCoin.Text = NbSaphirAvailable + "";
            nbSaphir--;
            lblChoiceSaphir.Text = nbSaphir + "\r\n";
        }

        private void lblChoiceOnyx_Click(object sender, EventArgs e)
        {
            if (nbOnyx == 1)
            {
                lblChoiceOnyx.Visible = false;
            }
            NbOnyxAvailable = NbOnyxAvailable + 1;
            lblOnyxCoin.Text = NbOnyxAvailable + "";
            nbOnyx--;
            lblChoiceOnyx.Text = nbOnyx + "\r\n";
        }

        private void lblChoiceEmeraude_Click(object sender, EventArgs e)
        {
            if (nbEmeraude == 1)
            {
                lblChoiceEmeraude.Visible = false;
            }
            NbEmeraudeAvailable = NbEmeraudeAvailable + 1;
            lblEmeraudeCoin.Text = NbEmeraudeAvailable + "";
            nbEmeraude--;
            lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
        }

        private void lblChoiceDiamand_Click(object sender, EventArgs e)
        {
            if (nbDiamand == 1)
            {
                lblChoiceDiamand.Visible = false;
            }
            NbDiamandAvailable = NbDiamandAvailable + 1;
            lblDiamandCoin.Text = NbDiamandAvailable + "";
            nbDiamand--;
            lblChoiceDiamand.Text = nbDiamand + "\r\n";
        }
    }
}
