using UnityEngine;
using Oracle.ManagedDataAccess.Client;
using TMPro;
using System;


public class RoomKnowledge: MonoBehaviour
{
    private TextMeshProUGUI txt;
    private string result = "Kenntnisse:\n\n";

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select z.bezeichnung from ziel z, kenntnis k, zielvorstellung v " +
        "where z.zielid=k.zielid and k.zielid =v.zielid and v.modulid = " + CrossSceneInformation.modul + "";

    OracleConnection con = new OracleConnection();
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
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
