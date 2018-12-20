using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Oracle.ManagedDataAccess.Client;
using System;
using UnityEngine.UI;
using TMPro;

public class ChangeScene : MonoBehaviour {
    private string result;
    public void JumpForward()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                TextMeshProUGUI txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                CrossSceneInformation.faculty = txt.text; //gleich Id speichern!?
                Debug.Log(CrossSceneInformation.faculty);
                break;
            case 1:
                CrossSceneInformation.direction = EventSystem.current.currentSelectedGameObject.name;
                break;
            case 2:
                CrossSceneInformation.semester = EventSystem.current.currentSelectedGameObject.name;
                break;
            case 3:
                CrossSceneInformation.modul = EventSystem.current.currentSelectedGameObject.name;
                getId();
                break;
            case 4:
                break;
            default:
                break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JumpBackward()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void getId()
    {
        String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";
  
        String select = "select modulid from modul where bezeichnung='"+ CrossSceneInformation.modul +"'";
        OracleConnection con = new OracleConnection();

        try
        {
            con.ConnectionString = connectionString;
            con.Open();

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                long Bezeichnung = dr.GetInt32(0);
                result += Bezeichnung.ToString();
            }
            CrossSceneInformation.modulId = result;
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
