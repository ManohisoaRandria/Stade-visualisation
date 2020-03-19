using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace rallye.outil
{
    public class Util<T> {
        public Point[] getPoints(String point) {
            List<Point> liste = new List<Point>();
            int x = 0;
            int y = 0;
            String[] coord = point.Split(';');
            String[] temp;
            for (int i = 0; i < coord.Length; i++) {
                temp = coord[i].Split(',');
                x = int.Parse(temp[0]);
                y = int.Parse(temp[1]);
                liste.Add(new Point(x, y));
            }
            return liste.ToArray();
        }


        public String setPoints(Point[] points) {
            String pointConstituant = "";
            for (int i = 0; i < points.Length; i++) {
                pointConstituant += "" + points[i].X + "," + points[i].Y;
                if (i != (points.Length - 1)) {
                    pointConstituant += ";";
                }
            }
            return pointConstituant;
        }
        public bool toIntArray(string s, int sep)
        {
            bool res = false;
            string[] spl = s.Split(',');
            int[] result = new int[spl.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = int.Parse(spl[i]);
            }
            if (result.Contains(sep)) res = true;
            return res;
        }
        public TimeSpan stringToTime(string s)
        {
            TimeSpan t = new TimeSpan(0,0,0);
            //string[] sep = new string[] {" ","."};
            s = s.Trim();
            if(s.Contains(" ") && !s.Contains(":"))
            {

            }
            if (s.Contains(":"))
            {
                string[]tab=s.Split(':');
            }
            
            return t;
        }  
        public TimeSpan[] triCroissant(TimeSpan[] tab)
        {
            int taille = tab.Length;
            TimeSpan[] reponse = new TimeSpan[taille];
            TimeSpan temporaire = new TimeSpan(1,24,30);
            for (int i = 0; i < taille; i++)
            {
                for (int j = i; j < taille; j++)
                {
                    if (tab[i] > tab[j])
                    {
                        temporaire = tab[i];
                        tab[i] = tab[j];
                        tab[j] = temporaire;
                    }
                }
                reponse[i] = tab[i];
            }
            return reponse;
        }    
        public T[] multiFind(T o, string nomTable, string apsWhere, NpgsqlConnection  con)
        {
            try
            {
                string requete = "select * from " + nomTable;
                if (apsWhere != null)
                {
                    requete += " where " + apsWhere;
                }
                NpgsqlCommand cmd = new NpgsqlCommand(requete, con);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                Type objectType = o.GetType();
                PropertyInfo[] properties = objectType.GetProperties();
                List<T> result = new List<T>();
                //int compteurBoucle = 0;
                while (reader.Read())
                {
                    T temporaire = (T)Activator.CreateInstance(objectType);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        temporaire.GetType().GetProperty(properties[i].Name).SetValue(temporaire, reader[properties[i].Name]);
                    }
                    //compteurBoucle++;
                    result.Add(temporaire);
                }
                reader.Close();
                //Console.Write(compteurBoucle);
                return result.ToArray();
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }
        public string getNextval(string sequence, NpgsqlConnection con) {
            try {
                string nextval = "";
                NpgsqlCommand cmd = new NpgsqlCommand("select nextval('"+sequence+"') as next", con);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    nextval=""+reader.GetInt16(0);
                }
                reader.Close();
                return nextval;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public void insertObjectTran(object obj, string nomTable, NpgsqlTransaction tran )
        {
            
            try
            {
                Type type = obj.GetType();
                //FieldInfo[] tabFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                PropertyInfo[] properties = type.GetProperties();
                ///Insertion dans la base de donnée

                String request = "INSERT INTO " + nomTable + "(";
                for (int i = 0; i < properties.Length; i++)
                {
                    if (i != properties.Length - 1) request += "" + properties[i].Name + ",";
                    else request += "" + properties[i].Name + ")";
                }
                request += " VALUES (";


                for (int i = 0; i < properties.Length; i++)
                {
                    if (!(string.Equals("id" + nomTable, properties[i].Name, StringComparison.OrdinalIgnoreCase)) && !properties[i].GetValue(obj).ToString().Contains("nextval"))
                    {

                        if (string.Equals("string", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("timespan", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            if (i == properties.Length - 1) request += "'" + properties[i].GetValue(obj) + "')";
                            else request += "'" + properties[i].GetValue(obj) + "',";
                        }
                        else if (string.Equals("DateTime", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime dt = (DateTime)properties[i].GetValue(obj);
                            if (i == properties.Length - 1) request += "'" + dt.ToString("yyyy-MM-dd") + "')";
                            else request += "'" + dt.ToString("yyyy-MM-dd") + "',";
                        }
                        else
                        {
                            string temporaire = properties[i].GetValue(obj).ToString();
                            string val="";
                            if (temporaire.Contains(",")) val = temporaire.Replace(",", ".");
                            else val = temporaire;
                          
                            if (i == properties.Length - 1) request += "" + val + ")";
                            else request += "" + val + ",";
                        }
                    }
                    else
                    {
                        if (i == properties.Length - 1) request += "" + properties[i].GetValue(obj) + ")";
                        else request += "" + properties[i].GetValue(obj) + ",";
                    }

                }

                NpgsqlCommand command = new NpgsqlCommand(request, tran.Connection);
                
                Console.WriteLine(request);
                command.ExecuteNonQuery();
                
            }
            catch (Exception exp)
            {
               
                throw (exp);

            }

        }
        
        public void insertObject(object obj, string nomTable, NpgsqlConnection  connexion)
        {

            // Start a local transaction.
            NpgsqlTransaction tran = connexion.BeginTransaction();

            try
            {
                Type type = obj.GetType();
                //FieldInfo[] tabFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                PropertyInfo[] properties = type.GetProperties();
                ///Insertion dans la base de donnée

                String request = "INSERT INTO " + nomTable + "(";
                for (int i = 0; i < properties.Length; i++)
                {
                    if (i != properties.Length - 1) request += "" + properties[i].Name + ",";
                    else request += "" + properties[i].Name + ")";
                }
                request += " VALUES (";


                for (int i = 0; i < properties.Length; i++)
                {
                    if (!(string.Equals("id" + nomTable, properties[i].Name, StringComparison.OrdinalIgnoreCase)) && !properties[i].GetValue(obj).ToString().Contains("nextval"))
                    {
                      
                        if (string.Equals("string", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("timespan", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            if (i == properties.Length - 1) request += "'" + properties[i].GetValue(obj) + "')";
                            else request += "'" + properties[i].GetValue(obj) + "',";
                        }
                        else if (string.Equals("DateTime", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime dt = (DateTime)properties[i].GetValue(obj);
                            if (i == properties.Length - 1) request += "'" + dt.ToString("yyyy-MM-dd") + "')";
                            else request += "'" + dt.ToString("yyyy-MM-dd") + "',";
                        }
                        else
                        {
                            string temporaire = properties[i].GetValue(obj).ToString();
                            string val = "";
                            if (temporaire.Contains(",")) val = temporaire.Replace(",", ".");
                            else val = temporaire;

                            if (i == properties.Length - 1) request += "" + val + ")";
                            else request += "" + val + ",";
                        }
                    }
                    else
                    {
                        if (i == properties.Length - 1) request += "" + properties[i].GetValue(obj) + ")";
                        else request += "" + properties[i].GetValue(obj) + ",";
                    }

                }

                NpgsqlCommand command = new NpgsqlCommand(request, connexion);
                command.Transaction = tran;
                Console.WriteLine(request);
                command.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                throw (exp);

            }

        }
        public void updateTable(object objetModifier, string nomTable,string afterWhere, NpgsqlConnection  con)
        {
            NpgsqlTransaction tran = con.BeginTransaction();
            try
            {
                Type typeObj = objetModifier.GetType();
                PropertyInfo[] properties = typeObj.GetProperties();
                string requete = "update " + nomTable + " set ";

              
                for (int i = 0; i < properties.Length; i++)
                {
                    
                    if (string.Equals("string", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("dateTime", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("timespan", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        if (i == properties.Length - 1) requete += properties[i].Name + "='" + properties[i].GetValue(objetModifier) + "'";
                        else requete += properties[i].Name + "='" + properties[i].GetValue(objetModifier) + "',";
                    }
                    else
                    {
                        if (i == properties.Length - 1) requete += properties[i].Name + "=" + properties[i].GetValue(objetModifier) + "";
                        else requete += properties[i].Name + "=" + properties[i].GetValue(objetModifier) + ",";
                    }

                }
                requete += " where " + afterWhere;

                NpgsqlCommand cmd = new NpgsqlCommand(requete, con);
                cmd.Transaction = tran;

                Console.WriteLine(requete);

                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception exp)
            {
                tran.Rollback();
                throw exp;
            }
        }
        //public void insertTran(object obj, string nomTable, NpgsqlTransaction tran) {
        //    try {
        //        Type type = obj.GetType();
        //        //FieldInfo[] tabFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

        //        PropertyInfo[] properties = type.GetProperties();
        //        ///Insertion dans la base de donnée

        //        String request = "INSERT INTO " + nomTable + "(";
        //        for (int i = 0; i < properties.Length; i++) {
        //            if (i != properties.Length - 1) request += "" + properties[i].Name + ",";
        //            else request += "" + properties[i].Name + ")";
        //        }
        //        request += " VALUES (";


        //        for (int i = 0; i < properties.Length; i++) {
        //            if (!(string.Equals("id" + nomTable, properties[i].Name, StringComparison.OrdinalIgnoreCase)) && !properties[i].GetValue(obj).ToString().Contains("nextval")) {

        //                if (string.Equals("string", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("timespan", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase)) {
        //                    if (i == properties.Length - 1) request += "'" + properties[i].GetValue(obj) + "')";
        //                    else request += "'" + properties[i].GetValue(obj) + "',";
        //                }
        //                else if (string.Equals("DateTime", properties[i].PropertyType.Name, StringComparison.OrdinalIgnoreCase)) {
        //                    DateTime dt = (DateTime)properties[i].GetValue(obj);
        //                    if (i == properties.Length - 1) request += "'" + dt.ToString("yyyy-MM-dd") + "')";
        //                    else request += "'" + dt.ToString("yyyy-MM-dd") + "',";
        //                }
        //                else {
        //                    string temporaire = properties[i].GetValue(obj).ToString();
        //                    string val = "";
        //                    if (temporaire.Contains(",")) val = temporaire.Replace(",", ".");
        //                    else val = temporaire;

        //                    if (i == properties.Length - 1) request += "" + val + ")";
        //                    else request += "" + val + ",";
        //                }
        //            }
        //            else {
        //                if (i == properties.Length - 1) request += "" + properties[i].GetValue(obj) + ")";
        //                else request += "" + properties[i].GetValue(obj) + ",";
        //            }

        //        }

        //        NpgsqlCommand command = new NpgsqlCommand(request, tran.Connection);
        //        command.Transaction = tran;
        //        Console.WriteLine(request);
        //        command.ExecuteNonQuery();

        //    }
        //    catch (Exception exp) {

        //        throw (exp);

        //    }
        //}
        //public void insertConnex(object obj, string nomTable, NpgsqlConnection connexion) {
        //    // Start a local transaction.
        //    NpgsqlTransaction tran = connexion.BeginTransaction();
        //    try {
        //        insertTran(obj, nomTable, tran);
        //        tran.Commit();
        //    }
        //    catch (Exception ex) {
        //        tran.Rollback();
        //        throw ex;
        //    }
        //}
    }
}