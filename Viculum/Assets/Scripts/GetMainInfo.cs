using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oracle.ManagedDataAccess.Client;
using System;

public class GetMainInfo : MonoBehaviour {
    public TextMeshProUGUI fac1;
    public TextMeshProUGUI fac2;
    public TextMeshProUGUI fac3;
    public TextMeshProUGUI fac4;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select bezeichnung from fakultaet";

    OracleConnection con = new OracleConnection();

    // Use this for initialization
    void Start () {
        List<TextMeshProUGUI> faculty = new List<TextMeshProUGUI>();
        List<String> result = new List<String>();
        faculty.Add(fac1);
        faculty.Add(fac2);
        faculty.Add(fac3);
        faculty.Add(fac4);

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

            for (int i = 0; i < faculty.Count; i++)
            {
                TextMeshProUGUI txt = faculty[i].GetComponent<TextMeshProUGUI>();
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
