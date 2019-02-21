using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Oracle.ManagedDataAccess.Client;
using UnityEngine.UI;

public class RoomTable : MonoBehaviour {
    private TextMeshProUGUI txt;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";

    // Use this for initialization
    void Start () {
        txt = GetComponent<TextMeshProUGUI>();
        string showText = txt.text;
        CrossSceneInformation.anzeige.Clear();
        String select = "";
        select = "select bezeichnung from fakultaet where fakultaetid = '" + CrossSceneInformation.faculty + "'";
        String temp = GetIdName(select);
        CrossSceneInformation.anzeige.Add(GetIdName(select));
        select = "select bezeichnung from studiengang where studiengangid = '" + CrossSceneInformation.direction + "'";
        CrossSceneInformation.anzeige.Add(GetIdName(select));

        CrossSceneInformation.anzeige.Add(GetSemesterName(CrossSceneInformation.semester));

        select = "select w.wahlrichtungid from wahlrichtung w, semesterveranstaltung s where s.modulid = '" + CrossSceneInformation.modul + "'" +
                " and s.wahlrichtungid = w.wahlrichtungid";

        //Debug.Log(CrossSceneInformation.modul);
        Debug.Log(CrossSceneInformation.extension);
        if (GetExtensionName(select)=="1" || GetExtensionName(select) == "2" || GetExtensionName(select) == "3" || GetExtensionName(select) == "4")
        {
            //Debug.Log("Hallo");
            CrossSceneInformation.extension = GetExtensionName(select);
            select = "select bezeichnung from wahlrichtung where wahlrichtungid = '" + CrossSceneInformation.extension + "'";
            CrossSceneInformation.anzeige.Add(GetIdName(select));
        }
        //Debug.Log("Hier 1,2,3,4");
        //Debug.Log(CrossSceneInformation.extension);

        select = "select bezeichnung from modul where modulid = '" + CrossSceneInformation.modul + "'";
        CrossSceneInformation.anzeige.Add(GetIdName(select));

        for (int i = 0; i < CrossSceneInformation.anzeige.Count; i++)
        {
            showText += "\n" + CrossSceneInformation.anzeige[i];
        }

        txt.text = showText;
    }

    public string GetExtensionName(String select)
    {
        OracleConnection con = new OracleConnection();
        String result = "";
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
                result += id.ToString();
            }
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
        return result;
    }

    public string GetIdName(String select)
    {
        OracleConnection con = new OracleConnection();
        String result = "";
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
                result = Bezeichnung;
            }
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
        return result;
    }

    public string GetSemesterName(String semesterId)
    {
        return "Semester " + semesterId.ToString();
    }
}
