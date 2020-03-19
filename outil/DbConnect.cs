using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace banque
{
    public class DbConnect
    {
        private string constring = "Server=localhost;Port=5432;User Id=postgres;Password=m1234;Database=prjespace;";
        public NpgsqlConnection connect()
        {
            NpgsqlConnection con;
            try
            {
                con = new NpgsqlConnection(this.Constring);
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return con;
        }
        public string Constring
        {
            get
            {
                return constring;
            }

            set
            {
                constring = value;
            }
        }
    }
}