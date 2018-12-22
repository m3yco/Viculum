using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Oracle.ManagedDataAccess.Client;
using System;

public class GetExtensionInfo : MonoBehaviour
{
    public TextMeshProUGUI ext1;
    public TextMeshProUGUI ext2;
    public TextMeshProUGUI ext3;
    public TextMeshProUGUI ext4;

    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select bezeichnung from wahlrichtung";

    OracleConnection con = new OracleConnection();

    // Use this for initialization
    void Start()
    {
        List<TextMeshProUGUI> extensions = new List<TextMeshProUGUI>();
        List<String> result = new List<String>();
        extensions.Add(ext1);
        extensions.Add(ext2);
        extensions.Add(ext3);
        extensions.Add(ext4);

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

            for (int i = 0; i < 4; i++)
            {
                TextMeshProUGUI txt = extensions[i].GetComponent<TextMeshProUGUI>();
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
