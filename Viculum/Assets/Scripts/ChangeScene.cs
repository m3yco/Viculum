using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Oracle.ManagedDataAccess.Client;
using System;
using TMPro;

public class ChangeScene : MonoBehaviour {
    private string result;
    public void JumpForward()
    {
        TextMeshProUGUI txt;
        String select;
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                select = "select fakultaetid from fakultaet where bezeichnung = '"+ txt.text + "'";
                CrossSceneInformation.faculty = GetId(select);
                Debug.Log(CrossSceneInformation.faculty);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                select = "select studiengangid from studiengang where bezeichnung = '" + txt.text + "'";
                CrossSceneInformation.direction = GetId(select);
                Debug.Log(CrossSceneInformation.direction);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 2:
                txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                String semesterid = txt.text.Substring(0, 1);
                CrossSceneInformation.semester = semesterid;
                Debug.Log(CrossSceneInformation.semester);
                if (CrossSceneInformation.semester == "5" || CrossSceneInformation.semester == "7")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
                break;
            case 3:
                txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                select = "select wahlrichtungid from wahlrichtung where bezeichnung = '" + txt.text + "'";
                CrossSceneInformation.extension = GetId(select);
                Debug.Log(CrossSceneInformation.extension);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 4:
                txt = GameObject.Find(EventSystem.current.currentSelectedGameObject.name + "/Text").GetComponent<TextMeshProUGUI>();
                select = "select modulid from modul where bezeichnung='" + txt.text + "'";
                CrossSceneInformation.modul = GetId(select);
                Debug.Log(CrossSceneInformation.modul);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }

    public void JumpBackward()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && CrossSceneInformation.semester != "5" && CrossSceneInformation.semester != "7")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.B))
            {
                Cursor.visible = true;
                SceneManager.LoadScene("Modul", LoadSceneMode.Single);
            }
        }
    }

    public string GetId(String select)
    {
        String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";
  
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
}
