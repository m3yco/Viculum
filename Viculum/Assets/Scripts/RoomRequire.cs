using UnityEngine;
using Oracle.ManagedDataAccess.Client;
using TMPro;
using System;


public class RoomRequire : MonoBehaviour
{
    // Hier wird die Baumstruktur geladen und dadurch angezeigt welche Veranstaltungen besucht
    // werden müssen, um die Vorrausetzungen für die ausgewählte Veranstaltung zu erfüllen.
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
                                + "where v.modulid = x.obermodulid and x.obermodulid = " + CrossSceneInformation.modul + ")";

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Bezeichnung = (string)dr.GetValue(0);
                result += "+ " + Bezeichnung + "\n";
            }

            // Hier werden alle Namen der Veranstaltung geladen die, diese Veranstaltung als
            // voraussetzung haben.
            result += "\nAufbau Veranstaltungen:\n\n";
            select = "select bezeichnung from modul where modulid in" +
                "(select x.obermodulid from modul v, empfehlung x " +
                "where v.modulid = x.untermodulid and x.untermodulid = " + CrossSceneInformation.modul + ")";

            cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            dr = cmd.ExecuteReader();
            int jump = 0;
            while (dr.Read())
            {
                string Bezeichnung = (string)dr.GetValue(0);
                result += "+ " + Bezeichnung + "\n";
                if (jump == 0)
                {
                    CrossSceneInformation.jump = Bezeichnung;
                    jump++;
                }
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
