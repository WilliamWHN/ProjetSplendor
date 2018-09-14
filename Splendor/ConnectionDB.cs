using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {
        //connection to the database
        private SQLiteConnection m_dbConnection; 

        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {
            // Création de la BD (j'imagine ...)
            SQLiteConnection.CreateFile("Splendor.sqlite");
            
            // Instanciation de la connexion à la base de donnée
            m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
            // Ouverture de la BD (j'imagine ...)
            m_dbConnection.Open();

            //create and insert players VOIR EN DESSOUS
            CreateInsertPlayer();
            //Create and insert cards VOIR EN DESSOUS
            //TO DO
            CreateInsertCards();
            //Create and insert ressources VOIR EN DESSOUS
            //TO DO
            CreateInsertRessources();
        }


        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //while (....)
            //{
                //Get the ressourceid and the number of prestige points
                //Create a card object
                
                //select the cost of the card : look at the cost table (and other)
                
                //do while to go to every record of the card table
                //while (....)
                //{
                    //get the nbRessource of the cost
                //}
                //push card into the stack
                
            //}
            return listCard;
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            // Création de la table "player"
            string sql = "CREATE TABLE player (id INT PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données
            sql = "insert into player (id, pseudo) values (0, 'Fred')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (1, 'Harry')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (2, 'Sam')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        
        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {
            // Création de la table "ressource"
            string sql = "CREATE TABLE ressoure (id INT PRIMARY KEY)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données
            sql = "insert into ressource (id) values (0)"; // -> Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (1)"; // -> Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (2)"; // -> Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (3)"; // -> Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (4)"; // -> Diamand
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        { 
            // Création de la table "cards"
            string sql = "CREATE TABLE card (id INT PRIMARY KEY, level INT, nbPtPrestige INT, fkPlayer INT, FOREIGN KEY (fkPlayer) REFERENCES player (id))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Création de la table "cost"
            sql = "CREATE TABLE cost (fkCard INT, fkRessource INT, nbRessource INT, FOREIGN KEY (fkCard) REFERENCES card (id), FOREIGN KEY (fkRessource) REFERENCES ressource (id))";
            SQLiteCommand command2 = new SQLiteCommand(sql, m_dbConnection);
            command2.ExecuteNonQuery();

            // Insertion des données -> utiliser le fichier Excel
        }

    }
}
