using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oracle.ManagedDataAccess.Client;
using System;

public class GetDirectionInfo : MonoBehaviour
{
    // Initialisierung der Variablen die dem GameObject zugewiesen wurden.
    public TextMeshProUGUI dir1;
    public TextMeshProUGUI dir2;
    public TextMeshProUGUI dir3;
    public TextMeshProUGUI dir4;
    public TextMeshProUGUI dir5;
    public TextMeshProUGUI dir6;
    public TextMeshProUGUI dir7;

    // JDBC Connection String zur Oracle Datenbank.
    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select bezeichnung from studiengang where fakultaetid = "+ CrossSceneInformation.faculty  + "";

    OracleConnection con = new OracleConnection();

    // Use this for initialization
    void Start()
    {
        // Alle Variablen in eine Liste eingefügt.
        List<TextMeshProUGUI> directions = new List<TextMeshProUGUI>();
        List<String> result = new List<String>();
        directions.Add(dir1);
        directions.Add(dir2);
        directions.Add(dir3);
        directions.Add(dir4);
        directions.Add(dir5);
        directions.Add(dir6);
        directions.Add(dir7);

        try
        {
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Bezeichnung = (string)dr.GetValue(0);
                result.Add(Bezeichnung);
            }
            dr.Dispose();

            for (int i = 0; i < directions.Count; i++)
            {
                TextMeshProUGUI txt = directions[i].GetComponent<TextMeshProUGUI>();
                // Die Ergebnisse des Select Statement werden den GameObject zugewiesen
                txt.text = result[i];
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Debug.Log(ex.StackTrace);
        }
        finally
        {
            con.Close();
            con.Dispose();
            con = null;
        }
    }
}

