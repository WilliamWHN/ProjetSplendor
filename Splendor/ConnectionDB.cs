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
            // TO DO 50%
            CreateInsertCards();
            //Create and insert ressources VOIR EN DESSOUS
            CreateInsertRessources();
            // Create and insert nbCoin VOIR EN DESSOUS
            CreateNbCoin();
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

            int i = 2;
            do
            {
                int idCard = i;

                // Get the number of prestige points and the level of the card
                int nbPrestigepoints = nbPrestige(idCard);
                int cardLevel = getLevel(idCard);

                /*// Get the data of the card table
                string allCards = getCard();*/

                // Get the cost of the ressource
                int costRubis = requestCostRubis(idCard);
                int costEmeraude = requestCostEmeraude(idCard);
                int costOnyx = requestCostOnyx(idCard);
                int costSaphir = requestCostSaphir(idCard);
                int costDiamand = requestCostDiamand(idCard);

                // Create a card object and fills it
                Card card = new Card();
                card.PrestigePt = nbPrestigepoints;
                card.Level = cardLevel;

                int carteTest = 2;

                listCard.Push();

                idCard++;
            } while (i < 102);
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
            string sql = "CREATE TABLE ressource (id INT PRIMARY KEY)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données
            sql = "insert into ressource (id) values (1)"; // -> Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (2)"; // -> Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (3)"; // -> Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (4)"; // -> Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into ressource (id) values (5)"; // -> Diamand
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// get the ressource according to is id
        /// <param ressource="id">id of the ressource</param>
        /// </summary>
        /*public string GetRessourceId(int id)
        {
            string sql = "select name from ressource where id =" + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string ressource = "";
            while(reader.Read())
            {
                ressource = reader["name"].ToString();
            }
            return ressource;
        }*/

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        { 
            // Création de la table "cards"
            string sql = "CREATE TABLE card (id INT PRIMARY KEY, level INT, nbPtPrestige INT, fkPlayer INT, FOREIGN KEY (fkPlayer) REFERENCES player (id))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données -> utiliser le fichier Excel
            sql = "insert into card(id, level, nbPtPrestige) values(2, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(3, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(4, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(5, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(6, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(7, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(8, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(9, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(10, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(11, 4, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(12, 3, 5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(13, 3, 5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(14, 3, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(15, 3, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(16, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(17, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(18, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(19, 3, 5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(20, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(21, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(22, 3, 5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(23, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(24, 3, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(25, 3, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(26, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(27, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(28, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(29, 3, 5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(30, 3, 4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(31, 3, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(32, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(33, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(34, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(35, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(36, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(37, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(38, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(39, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(40, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(41, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(42, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(43, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(44, 2, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(45, 2, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(46, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(47, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(48, 2, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(49, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(50, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(51, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(52, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(53, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(54, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(55, 2, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(56, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(57, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(58, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(59, 2, 2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(60, 2, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(61, 2, 3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(62, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(63, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(64, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(65, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(66, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(67, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(68, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(69, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(70, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(71, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(72, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(73, 1, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(74, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(75, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(76, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(77, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(78, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(79, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(80, 1, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(81, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(82, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(83, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(84, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(85, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(86, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(87, 1, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(88, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(89, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(90, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(91, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(92, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(93, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(94, 1, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(95, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(96, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(97, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(98, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(99, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(100, 1, 0)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into card(id, level, nbPtPrestige) values(101, 1, 1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();


            // Création de la table "cost"
            sql = "CREATE TABLE cost (fkCard INT, fkRessource INT, nbRessource INT, FOREIGN KEY (fkCard) REFERENCES card (id), FOREIGN KEY (fkRessource) REFERENCES ressource (id))";
            SQLiteCommand command2 = new SQLiteCommand(sql, m_dbConnection);
            command2.ExecuteNonQuery();

            // Insertion des données
            // Pour les Rubis
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (2, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (3, 1, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (4, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (5, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (6, 1, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (7, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (8, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (9, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (10, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (11, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (12, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (13, 1, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (14, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (15, 1, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (16, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (17, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (18, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (19, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (20, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (21, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (22, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (23, 1, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (24, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (25, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (26, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (27, 1, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (28, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (29, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (30, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (31, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (32, 1, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (33, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (34, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (35, 1, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (36, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (37, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (38, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (39, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (40, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (41, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (42, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (43, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (44, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (45, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (46, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (47, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (48, 1, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (49, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (50, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (51, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (52, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (53, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (54, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (55, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (56, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (57, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (58, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (59, 1, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (60, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (61, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (62, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (63, 1, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (64, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (65, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (66, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (67, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (68, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (69, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (70, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (71, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (72, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (73, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (74, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (75, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (76, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (77, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (78, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (79, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (80, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (81, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (82, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (83, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (84, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (85, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (86, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (87, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (88, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (89, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (90, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (91, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (92, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (93, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (94, 1, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (95, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (96, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (97, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (98, 1, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (99, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (100, 1, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (101, 1, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour les Emeraudes
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (102 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (103 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (104 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (105 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (106 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (107 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (108 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (109 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (110 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (111 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (112 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (113 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (114 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (115 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (116 , 2, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (117 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (118 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (119 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (120 , 2, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (121 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (122 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (123 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (124 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (125 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (126 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (127 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (128 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (129 , 2, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (130 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (131 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (132 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (133 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (134 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (135 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (136 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (137 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (138 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (139 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (140 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (141 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (142 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (143 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (144 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (145 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (146 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (147 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (148 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (149 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (150 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (151 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (152 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (153 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (154 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (155 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (156 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (157 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (158 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (159 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (160 , 2, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (161 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (162 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (163 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (164 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (165 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (166 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (167 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (168 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (169 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (170 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (171 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (172 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (173 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (174 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (175 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (176 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (177 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (178 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (179 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (180 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (181 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (182 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (183 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (184 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (185 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (186 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (187 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (188 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (189 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (190 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (191 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (192 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (193 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (194 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (195 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (196 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (197 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (198 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (199 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (200 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (201 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour l'Onyx
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (202, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (203, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (204, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (205, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (206, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (207, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (208, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (209, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (210, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (211, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (212, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (213, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (214, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (215, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (216, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (217, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (218, 3, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (219, 3, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (220, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (221, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (222, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (223, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (224, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (225, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (226, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (227, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (228, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (229, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (230, 3, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (231, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (232, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (233, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (234, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (235, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (236, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (237, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (238, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (239, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (240, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (241, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (242, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (243, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (244, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (245, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (246, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (247, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (248, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (249, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (250, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (251, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (252, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (253, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (254, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (255, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (256, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (257, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (258, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (259, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (260, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (261, 3, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (262, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (263, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (264, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (265, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (266, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (267, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (268, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (269, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (270, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (271, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (272, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (273, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (274, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (275, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (276, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (277, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (278, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (279, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (280, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (281, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (282, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (283, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (284, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (285, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (286, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (287, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (288, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (289, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (290, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (291, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (292, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (293, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (294, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (295, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (296, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (297, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (298, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (299, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (300, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (301, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour le Saphir
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (302, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (303, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (304, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (305, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (306, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (307, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (308, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (309, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (310, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (311, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (312, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (313, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (314, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (315, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (316, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (317, 4, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (318, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (319, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (320, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (321, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (322, 4, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (323, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (324, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (325, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (326, 4, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (327, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (328, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (329, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (330, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (331, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (332, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (333, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (334, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (335, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (336, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (337, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (338, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (339, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (340, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (341, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (342, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (343, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (344, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (345, 4, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (346, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (347, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (348, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (349, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (350, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (351, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (352, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (353, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (354, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (355, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (356, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (357, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (358, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (359, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (360, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (361, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (362, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (363, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (364, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (365, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (366, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (367, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (368, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (369, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (370, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (371, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (372, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (373, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (374, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (375, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (376, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (377, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (378, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (379, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (380, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (381, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (382, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (383, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (384, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (385, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (386, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (387, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (388, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (389, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (390, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (391, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (392, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (393, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (394, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (395, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (396, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (397, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (398, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (399, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (400, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (401, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour le Diamant
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (402, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (403, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (404, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (405, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (406, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (407, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (408, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (409, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (410, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (411, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (412, 5, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (413, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (414, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (415, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (416, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (417, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (418, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (419, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (420, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (421, 5, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (422, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (423, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (424, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (425, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (426, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (427, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (428, 5, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (429, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (430, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (431, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (432, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (433, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (434, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (435, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (436, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (437, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (438, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (439, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (440, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (441, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (442, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (443, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (444, 5, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (445, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (446, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (447, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (448, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (449, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (450, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (451, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (452, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (453, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (454, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (455, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (456, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (457, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (458, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (459, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (460, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (461, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (462, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (463, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (464, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (465, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (466, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (467, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (468, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (469, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (470, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (471, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (472, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (473, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (474, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (475, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (476, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (477, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (478, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (479, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (480, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (481, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (482, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (483, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (484, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (485, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (486, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (487, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (488, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (489, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (490, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (491, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (492, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (493, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (494, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (495, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (496, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (497, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (498, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (499, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (500, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (501, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
        }

        /// <summary>
        /// Get the "Rubis" card's cost
        /// </summary>
        public int requestCostRubis(int idCard)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = 1 and fkCard =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costRubis = Convert.ToInt32(reader["nbRessource"]);
            return costRubis;
        }

        /// <summary>
        /// Get the "Emeraude" card's cost
        /// </summary>
        public int requestCostEmeraude(int idCard)
        {
            idCard = idCard + 100;
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = 2 and fkCard =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costEmeraude = Convert.ToInt32(reader["nbRessource"]);
            return costEmeraude;
        }

        /// <summary>
        /// Get the "Onyx" card's cost
        /// </summary>
        public int requestCostOnyx(int idCard)
        {
            idCard = idCard + 200;
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = 3 and fkCard =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costOnyx = Convert.ToInt32(reader["nbRessource"]);
            return costOnyx;
        }

        /// <summary>
        /// Get the "Saphir" card's cost
        /// </summary>
        public int requestCostSaphir(int idCard)
        {
            idCard = idCard + 300;

            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = 4 and fkCard =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costSaphir = Convert.ToInt32(reader["nbRessource"]);
            return costSaphir;
        }

        /// <summary>
        /// Get the "Diamand" card's cost
        /// </summary>
        public int requestCostDiamand(int idCard)
        {
            idCard = idCard + 400;

            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = 5 and fkCard =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costDiamand = Convert.ToInt32(reader["nbRessource"]);
            return costDiamand;
        }

        /// <summary>
        /// Get the number of prestige point for one card
        /// </summary>
        public int nbPrestige(int idCard)
        {
            // Write Sql request
            string sql = "select nbPtPrestige from card where id =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            // Write the result in nbPoints   pas au point
            int nbPoints;
            nbPoints = Convert.ToInt32(reader["nbPtPrestige"]);
            return nbPoints;
        }

        /// <summary>
        /// /Get the level of the card
        /// </summary>
        public int getLevel(int idCard)
        {
            // Write sql request
            string sql = "select level from card where id =" + idCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            // Write the result in cardLevel
            int cardLevel;
            cardLevel = Convert.ToInt32(reader["level"]);
            return cardLevel;
        }

        /*/// <summary>
        /// Get all the data of the Card table
        /// </summary>
        public string getCard()
        {
            // Write Sql request
            string sql = "select * from card";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            string allCard = Convert.ToString(reader);
            return allCard;
        }*/

        /// <summary>
        ///  create table "nbCoin" and insert data             
        /// </summary>
        private void CreateNbCoin()
        {
            // Création de la table "nbCoin
            string sql = "CREATE TABLE nbCoin (id INT PRIMARY KEY, nbCoin INT, fkPlayer INT, fkRessource INT, FOREIGN KEY (fkPlayer) REFERENCES player (id), FOREIGN KEY (fkRessource) REFERENCES ressource (id))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (1, 0, 0, 1)"; // -> Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (2, 0, 0, 2)"; // -> Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (3, 0, 0, 3)"; // -> Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (4, 0, 0, 4)"; // -> Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (5, 0, 0, 5)"; // -> Diamant
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (6, 0, 2, 1)"; // -> Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (7, 0, 2, 2)"; // -> Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (8, 0, 2, 3)"; // -> Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (9, 0, 2, 4)"; // -> Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (10, 0, 2, 5)"; // -> Diamant
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (11, 0, 3, 1)"; // -> Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (12, 0, 3, 2)"; // -> Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (13, 0, 3, 3)"; // -> Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (14, 0, 3, 4)"; // -> Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into nbCoin (id, nbCoin, fkPlayer, fkRessource) values (15, 0, 3, 5)"; // -> Diamant
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

    }
}
