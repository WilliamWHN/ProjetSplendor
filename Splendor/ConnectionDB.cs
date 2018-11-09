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

            //Create Table player
            CreateTablePlayer();
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
            Stack<Card> listCard = new Stack<Card>();



            string sql = "select * from card where level =" + level;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader cardReader = command.ExecuteReader();

            Card card;

            int id = 0;
            while (cardReader.Read())
            {
                card = new Card();
                id = Convert.ToInt32(cardReader["id"]);
                card.PrestigePt = Convert.ToInt32(cardReader["nbPtPrestige"]);
                card.Level = Convert.ToInt32(cardReader["level"]);
                int Ressource = getRessource(id);
                switch (Ressource)
                {
                    case 1:
                        card.Ress = Ressources.Rubis;
                        break;

                    case 2:
                        card.Ress = Ressources.Emeraude;
                        break;
                    case 3:
                        card.Ress = Ressources.Onyx;
                        break;

                }

                int costRubis = requestCostRubis(1, id);
                int costEmeraude = requestCostEmeraude(2, id);
                int costOnyx = requestCostOnyx(3, id);
                int costSaphir = requestCostSaphir(4, id);
                int costDiamand = requestCostDiamand(5, id);
                card.Cout = new int[] { costRubis, costEmeraude, costOnyx, costSaphir, costDiamand};
                card.IdCard = id;

                listCard.Push(card);
            };
            
            Random rnd = new Random();
            var values = listCard.ToArray();
            listCard.Clear();
            foreach (var value in values.OrderBy(x => rnd.Next()))
                listCard.Push(value);
            return listCard;
        }
        
        /// <summary>
        /// create the "player" table
        /// </summary>
        public void CreateTablePlayer()
        {
            // Création de la table "player"
            string sql = "CREATE TABLE player (id INTEGER PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// insert data in payer table
        /// </summary>
        public string CreateInsertPlayer(string name)
        {
            // Insertion des données
            string sql = "insert into player (pseudo) values ('" + name + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            return "true";
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
            string sql = "CREATE TABLE card (id INT PRIMARY KEY, level INT, Ressource INT, nbPtPrestige INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Insertion des données -> utiliser le fichier Excel
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (2,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (3,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (4,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (5,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (6,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (7,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (8,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (9,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (10,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (11,0,4,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (12,4,3,5)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (13,3,3,5)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (14,2,3,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (15,5,3,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (16,1,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (17,2,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (18,5,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (19,5,3,5)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (20,1,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (21,4,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (22,2,3,5)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (23,3,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (24,1,3,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (25,4,3,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (26,2,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (27,3,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (28,4,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (29,1,3,5)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (30,5,3,4)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (31,3,3,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (32,5,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (33,1,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (34,5,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (35,5,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (36,5,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (37,2,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (38,4,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (39,4,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (40,2,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (41,2,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (42,3,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (43,1,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (44,5,2,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (45,4,2,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (46,2,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (47,3,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (48,1,2,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (49,4,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (50,3,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (51,2,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (52,4,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (53,1,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (54,1,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (55,3,2,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (56,4,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (57,3,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (58,1,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (59,5,2,2)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (60,2,2,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (61,3,2,3)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (62,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (63,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (64,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (65,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (66,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (67,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (68,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (69,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (70,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (71,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (72,5,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (73,5,1,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (74,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (75,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (76,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (77,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (78,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (79,1,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (80,1,1,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (81,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (82,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (83,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (84,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (85,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (86,3,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (87,3,1,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (88,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (89,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (90,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (91,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (92,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (93,4,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (94,4,1,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (95,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (96,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (97,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (98,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (99,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (100,2,1,0)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();
            sql = "insert into card(id, Ressource, level, nbPtPrestige) values (101,2,1,1)"; command = new SQLiteCommand(sql, m_dbConnection); command.ExecuteNonQuery();




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
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (2 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (3 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (4 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (5 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (6 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (7 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (8 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (9 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (10 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (11 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (12 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (13 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (14 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (15 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (16 , 2, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (17 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (18 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (19 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (20 , 2, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (21 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (22 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (23 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (24 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (25 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (26 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (27 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (28 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (29 , 2, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (30 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (31 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (32 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (33 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (34 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (35 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (36 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (37 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (38 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (39 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (40 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (41 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (42 , 2, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (43 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (44 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (45 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (46 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (47 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (48 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (49 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (50 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (51 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (52 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (53 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (54 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (55 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (56 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (57 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (58 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (59 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (60 , 2, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (61 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (62 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (63 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (64 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (65 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (66 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (67 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (68 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (69 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (70 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (71 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (72 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (73 , 2, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (74 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (75 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (76 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (77 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (78 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (79 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (80 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (81 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (82 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (83 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (84 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (85 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (86 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (87 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (88 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (89 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (90 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (91 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (92 , 2, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (93 , 2, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (94 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (95 , 2, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (96 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (97 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (98 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (99 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (100 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (101 , 2, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour l'Onyx
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (2, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (3, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (4, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (5, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (6, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (7, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (8, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (9, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (10, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (11, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (12, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (13, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (14, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (15, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (16, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (17, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (18, 3, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (19, 3, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (20, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (21, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (22, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (23, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (24, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (25, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (26, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (27, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (28, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (29, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (30, 3, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (31, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (32, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (33, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (34, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (35, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (36, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (37, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (38, 3, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (39, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (40, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (41, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (42, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (43, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (44, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (45, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (46, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (47, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (48, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (49, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (50, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (51, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (52, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (53, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (54, 3, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (55, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (56, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (57, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (58, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (59, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (60, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (61, 3, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (62, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (63, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (64, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (65, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (66, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (67, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (68, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (69, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (70, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (71, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (72, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (73, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (74, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (75, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (76, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (77, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (78, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (79, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (80, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (81, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (82, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (83, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (84, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (85, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (86, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (87, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (88, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (89, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (90, 3, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (91, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (92, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (93, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (94, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (95, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (96, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (97, 3, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (98, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (99, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (100, 3, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (101, 3, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour le Saphir
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (2, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (3, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (4, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (5, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (6, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (7, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (8, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (9, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (10, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (11, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (12, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (13, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (14, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (15, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (16, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (17, 4, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (18, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (19, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (20, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (21, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (22, 4, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (23, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (24, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (25, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (26, 4, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (27, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (28, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (29, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (30, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (31, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (32, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (33, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (34, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (35, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (36, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (37, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (38, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (39, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (40, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (41, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (42, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (43, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (44, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (45, 4, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (46, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (47, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (48, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (49, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (50, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (51, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (52, 4, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (53, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (54, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (55, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (56, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (57, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (58, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (59, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (60, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (61, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (62, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (63, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (64, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (65, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (66, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (67, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (68, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (69, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (70, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (71, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (72, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (73, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (74, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (75, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (76, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (77, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (78, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (79, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (80, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (81, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (82, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (83, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (84, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (85, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (86, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (87, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (88, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (89, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (90, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (91, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (92, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (93, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (94, 4, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (95, 4, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (96, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (97, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (98, 4, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (99, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (100, 4, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (101, 4, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();

            // Pour le Diamant
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (2, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (3, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (4, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (5, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (6, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (7, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (8, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (9, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (10, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (11, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (12, 5, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (13, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (14, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (15, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (16, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (17, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (18, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (19, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (20, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (21, 5, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (22, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (23, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (24, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (25, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (26, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (27, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (28, 5, 7)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (29, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (30, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (31, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (32, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (33, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (34, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (35, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (36, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (37, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (38, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (39, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (40, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (41, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (42, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (43, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (44, 5, 6)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (45, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (46, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (47, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (48, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (49, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (50, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (51, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (52, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (53, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (54, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (55, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (56, 5, 5)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (57, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (58, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (59, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (60, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (61, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (62, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (63, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (64, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (65, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (66, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (67, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (68, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (69, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (70, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (71, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (72, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (73, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (74, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (75, 5, 3)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (76, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (77, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (78, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (79, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (80, 5, 4)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (81, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (82, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (83, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (84, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (85, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (86, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (87, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (88, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (89, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (90, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (91, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (92, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (93, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (94, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (95, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (96, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (97, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (98, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (99, 5, 2)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (100, 5, 1)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
            sql = "insert into cost (fkCard, fkRessource, nbRessource) values (101, 5, 0)"; command2 = new SQLiteCommand(sql, m_dbConnection); command2.ExecuteNonQuery();
        }

        public int getRessource(int id)
        {
            string sql = "select Ressource from card where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int res = 0;
            while (reader.Read())
            {
                res = (int)(reader["Ressource"]);
            }


            return res;
        }

        /// <summary>
        /// Get the "Rubis" card's cost
        /// </summary>
        /// 
        public int requestCostRubis(int fkResc, int id)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = " + fkResc + " and  fkCard = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int cost = 0;
            while (reader.Read())
            {
                cost = (int)(reader["nbRessource"]);
            }


            return cost;
        }

        public int requestCostEmeraude(int fkResc, int id)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = " + fkResc + " and  fkCard = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int cost = 0;
            while (reader.Read())
            {
                cost = (int)(reader["nbRessource"]);
            }


            return cost;
        }

        public int requestCostOnyx(int fkResc, int id)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = " + fkResc + " and  fkCard = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int cost = 0;
            while (reader.Read())
            {
                cost = (int)(reader["nbRessource"]);
            }


            return cost;
        }

        public int requestCostSaphir(int fkResc, int id)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = " + fkResc + " and  fkCard = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int cost = 0;
            while (reader.Read())
            {
                cost = (int)(reader["nbRessource"]);
            }

            return cost;
        }

        public int requestCostDiamand(int fkResc, int id)
        {
            // Write Sql request
            string sql = "select nbRessource from cost where fkRessource = " + fkResc + " and  fkCard = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int cost = 0;
            while (reader.Read())
            {
                cost = (int)(reader["nbRessource"]);
            }


            return cost;
        }

        /// <summary>
        /// Get the "Emeraude" card's cost
        /// </summary>
        /*public int requestCostEmeraude(int levelCard)
        {
            // Write Sql request
            string sql = "select nbRessource from cost inner join card on cost.fkCard = card.id where fkRessource = 2  and level =" + levelCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costEmeraude = 0;
            while (reader.Read())
            {
                costEmeraude = Convert.ToInt32(reader["nbRessource"]);
            }

            return costEmeraude;
        }

        /// <summary>
        /// Get the "Onyx" card's cost
        /// </summary>
        public int requestCostOnyx(int levelCard)
        {
            // Write Sql request
            string sql = "select nbRessource from cost inner join card on cost.fkCard = card.id where fkRessource = 3  and level =" + levelCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costOnyx = 0;
            while (reader.Read())
            {
                costOnyx = Convert.ToInt32(reader["nbRessource"]);
            }

            return costOnyx;
        }

        /// <summary>
        /// Get the "Saphir" card's cost
        /// </summary>
        public int requestCostSaphir(int levelCard)
        {

            // Write Sql request
            string sql = "select nbRessource from cost inner join card on cost.fkCard = card.id where fkRessource = 4  and level =" + levelCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costSaphir = 0;
            while (reader.Read())
            {
                costSaphir = Convert.ToInt32(reader["nbRessource"]);
            }

            return costSaphir;
        }

        /// <summary>
        /// Get the "Diamand" card's cost
        /// </summary>
        public int requestCostDiamand(int levelCard)
        {

            // Write Sql request
            string sql = "select nbRessource from cost inner join card on cost.fkCard = card.id where fkRessource = 5 and level =" + levelCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int costDiamand = 0;
            while (reader.Read())
            {
                costDiamand = Convert.ToInt32(reader["nbRessource"]);
            }

            return costDiamand;
        }

        /*/// <summary>
        /// Get all the data of the Card table
        /// </summary>*/

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

        /// <summary>
        /// Get the number of prestige point for one card nbPtPrestige
        /// </summary>
        /// 
        /*public int nbPrestige(int levelCard)
        {
            // Write Sql request
            string sql = "select nbPtPrestige from card where level = " + levelCard;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int prestige = 0;
            while (reader.Read())
            {
                prestige = Convert.ToInt32(reader["nbPtPrestige"]);
            }

            return prestige;
        }*/

        /// <summary>
        /// /Get the level of the card
        /// </summary>       
    }
}
