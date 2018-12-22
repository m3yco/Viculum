using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oracle.ManagedDataAccess.Client;
using TMPro;
using System;


public class RoomRequire : MonoBehaviour
{
    private TextMeshProUGUI txt;
    private string result;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";

    OracleConnection con = new OracleConnection();
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        try
        {
            con.ConnectionString = connectionString;

            con.Open();
            result = "Voraussetzungs Veranstaltungen:\n\n";

            String select = "select bezeichnung from modul where modulid in"
                                + "(select x.untermodulid from modul v, empfehlung x "
                                + "where v.modulid = x.obermodulid and x.obermodulid = 21000)";

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Bezeichnung = (string)dr.GetValue(0);
                result += "+ " + Bezeichnung + "\n";
            }

            result += "\nAufbau Veranstaltungen:\n\n";
            select = "select bezeichnung from modul where modulid in" +
                "(select x.obermodulid from modul v, empfehlung x " +
                "where v.modulid = x.untermodulid and x.untermodulid = " + CrossSceneInformation.modul + ")";

            cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Bezeichnung = (string)dr.GetValue(0);
                result += "+ " + Bezeichnung + "\n";
            }

            txt.text = result;
            Debug.Log(result);
            dr.Dispose();
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
