﻿using virtual_receptionist.Repository.MySQLConnection;

namespace virtual_receptionist.Repository
{
    /// <summary>
    /// Az alkalmazáshoz szükséges adatbázis adatokat perzisztensen tároló adattár osztály
    /// </summary>
    public class Repository
    {
        #region Adattagok

        /// <summary>
        /// Adatbázis kapcsolódást, adatlekérdezés és adatmanipulációs műveleteket megvalósító egyke osztály egy példánya
        /// </summary>
        protected Database database;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Adattár konstruktora
        /// </summary>
        public Repository()
        {
            database = Database.GetDatabaseInstance();
        }

        #endregion
    }
}
