﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oracle.ManagedDataAccess.Client;
using System;
using TMPro;
using System.IO;

public class RoomVideo : MonoBehaviour
{
    private TextMeshProUGUI txt;
    private string result;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String selectData = "SELECT beispiel FROM Modulbeispiel WHERE modulbeispielid = 13";
    String selectName = "SELECT beispielname FROM Modulbeispiel WHERE modulbeispielid = 13";

    String dataName = "";

    OracleConnection con = new OracleConnection();

    void Start()
    {
        try
        {
            txt = GetComponent<TextMeshProUGUI>();

            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmdName = new OracleCommand(selectName, con);
            cmdName.CommandType = System.Data.CommandType.Text;
            OracleDataReader drName = cmdName.ExecuteReader();
            OracleCommand cmdData = new OracleCommand(selectData, con);
            cmdData.CommandType = System.Data.CommandType.Text;
            OracleDataReader drData = cmdData.ExecuteReader();
            while (drName.Read())
            {
                dataName = (string)drName.GetValue(0);
            }
            drName.Dispose();
            while (drData.Read())
            {
                byte[] videoByte = (byte[])drData["Beispiel"];
                File.WriteAllBytes((@Application.dataPath + "/Video/" + dataName), videoByte);
                // File.WriteAllBytes(@"C:\Users\dizep\Desktop" + dataName, videoByte);
            }
            result = Application.dataPath;
            txt.text = result;
            drData.Dispose();
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