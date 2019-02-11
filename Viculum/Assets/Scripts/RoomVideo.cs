using UnityEngine;
using Oracle.ManagedDataAccess.Client;
using System;
using TMPro;

public class RoomVideo : MonoBehaviour
{
    // Hier wird die Videodatei aus der Datenbank runtergeladen und auf ein lokales Verzeichnis abgelegt.
    // Der Name wird in der statischen Klasse CrossSceneInformation gespeichert.
    private TextMeshProUGUI txt;
    private string result;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String selectData = "select BEISPIEL from MODULMEDIUM where dateiformat = '.mp4' and MODULMEDIUMID in " +
                    "(select MODULMEDIUMID from VERANSTALTUNGSMATERIAL where SEMESTERVERANSTALTUNGID in " +
                    "(select SEMESTERVERANSTALTUNGID from SEMESTERVERANSTALTUNG where SEMESTERVERANSTALTUNG.MODULID=" + CrossSceneInformation.modul + "))";
    String selectName = "select BEISPIELNAME from MODULMEDIUM where dateiformat = '.mp4' and MODULMEDIUMID in " +
                    "(select MODULMEDIUMID from VERANSTALTUNGSMATERIAL where SEMESTERVERANSTALTUNGID in " +
                    "(select SEMESTERVERANSTALTUNGID from SEMESTERVERANSTALTUNG where SEMESTERVERANSTALTUNG.MODULID=" + CrossSceneInformation.modul + "))";

    String dataName = "";

    OracleConnection con = new OracleConnection();

    void Start()
    {
        try
        {
            // Für Präsentations Zwecke wurde das runterladen deaktiviert,
            // durch das wieder auskommentieren der unteren Command, CommandType,
            // Reader und der while-Schleife kann die Funktion wieder eingeschaltet werden.
            txt = GetComponent<TextMeshProUGUI>();

            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmdName = new OracleCommand(selectName, con);
            cmdName.CommandType = System.Data.CommandType.Text;
            OracleDataReader drName = cmdName.ExecuteReader();
            //OracleCommand cmdData = new OracleCommand(selectData, con);
            //cmdData.CommandType = System.Data.CommandType.Text;
            //OracleDataReader drData = cmdData.ExecuteReader();
            while (drName.Read())
            {
                dataName = (string)drName.GetValue(0);
                CrossSceneInformation.video = dataName;
            }
            drName.Dispose();
            //while (drData.Read())
            //{
            //    byte[] videoByte = (byte[])drData["Beispiel"];
            //    File.WriteAllBytes((@Application.dataPath + "/Video/" + dataName), videoByte);
            //}
            //result = Application.dataPath;
            //txt.text = result;
            //drData.Dispose();
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