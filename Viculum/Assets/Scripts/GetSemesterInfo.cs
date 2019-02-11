using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oracle.ManagedDataAccess.Client;
using System;

public class GetSemesterInfo : MonoBehaviour {
    // Initialisierung der Variablen die dem GameObject zugewiesen wurden.
    public TextMeshProUGUI sem1;
    public TextMeshProUGUI sem2;
    public TextMeshProUGUI sem3;
    public TextMeshProUGUI sem4;
    public TextMeshProUGUI sem5;
    public TextMeshProUGUI sem6;
    public TextMeshProUGUI sem7;

    // JDBC Connection String zur Oracle Datenbank.
    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select semesterid from semesterveranstaltung where studiengangid = " + CrossSceneInformation.direction + " group by semesterid order by semesterid";

    OracleConnection con = new OracleConnection();

    // Use this for initialization
    void Start () {
        // Alle Variablen in eine Liste eingefügt.
        List<TextMeshProUGUI> semester = new List<TextMeshProUGUI>();
        List<String> result = new List<String>();
        semester.Add(sem1);
        semester.Add(sem2);
        semester.Add(sem3);
        semester.Add(sem4);
        semester.Add(sem5);
        semester.Add(sem6);
        semester.Add(sem7);

        try
        {
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                long id = dr.GetInt32(0);
                result.Add(id.ToString());
            }
            dr.Dispose();

            for (int i = 0; i < semester.Count; i++)
            {
                TextMeshProUGUI txt = semester[i].GetComponent<TextMeshProUGUI>();
                txt.text = result[i]+".Semester";
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
