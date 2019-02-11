using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oracle.ManagedDataAccess.Client;
using System;

public class GetModulInfo : MonoBehaviour {
    // Initialisierung der Variablen die dem GameObject zugewiesen wurden.
    public TextMeshProUGUI mod1;
    public TextMeshProUGUI mod2;
    public TextMeshProUGUI mod3;
    public TextMeshProUGUI mod4;
    public TextMeshProUGUI mod5;
    public TextMeshProUGUI mod6;
    public TextMeshProUGUI mod7;
    public TextMeshProUGUI mod8;
    public TextMeshProUGUI mod9;

    // Use this for initialization
    void Start()
    {
        // Alle Variablen in eine Liste eingefügt.
        List<TextMeshProUGUI> modules = new List<TextMeshProUGUI>();
        List<String> result = new List<String>();
        modules.Add(mod1);
        modules.Add(mod2);
        modules.Add(mod3);
        modules.Add(mod4);
        modules.Add(mod5);
        modules.Add(mod6);
        modules.Add(mod7);
        modules.Add(mod8);
        modules.Add(mod9);

        // JDBC Connection String zur Oracle Datenbank.
        String connectionString = "Data Source=(DESCRIPTION=" +
               "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
               "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
               "User Id = projektstudium; Password = projektstudium; ";

        String select = "select m.bezeichnung from modul m, semesterveranstaltung s where s.semesterid= " + CrossSceneInformation.semester + " and m.modulid = s.modulid group by m.bezeichnung";

        OracleConnection con = new OracleConnection();
        // Es werden verschiedene selects durchgeführt weil je nach den welches Semester man wählt muss
        // die Vertiefung gewählt werden.
        if (CrossSceneInformation.semester == "5" || CrossSceneInformation.semester == "7")
        {
            select = "select m.bezeichnung from wahlrichtung w, semesterveranstaltung s, modul m  where w.wahlrichtungid = " + CrossSceneInformation.extension + " and s.wahlrichtungid = w.wahlrichtungid and m.modulid = s.modulid and s.semesterid = " + CrossSceneInformation.semester + "";
        }

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

            for (int i = 0; i < result.Count; i++)
            {
                TextMeshProUGUI txt = modules[i].GetComponent<TextMeshProUGUI>();
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

        if(result.Count < modules.Count)
        {
            for(int i= result.Count; i<modules.Count; i++)
            {
                GameObject btn = modules[i].transform.parent.gameObject;
                btn.SetActive(false);
            }
        }
    }
    void Awake()
    {
        Start();
    }
}
