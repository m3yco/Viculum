using UnityEngine;
using UnityEngine.UI;
using Oracle.ManagedDataAccess.Client;
using System;


public class RoomImage : MonoBehaviour
{
    
    public Image pic;
    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";


    String select = "select BEISPIEL from MODULMEDIUM where dateiformat = '.png' and MODULMEDIUMID in "+
                    "(select MODULMEDIUMID from VERANSTALTUNGSMATERIAL where SEMESTERVERANSTALTUNGID in "+
                    "(select SEMESTERVERANSTALTUNGID from SEMESTERVERANSTALTUNG where SEMESTERVERANSTALTUNG.MODULID=" + CrossSceneInformation.modul + "))";
    OracleConnection con = new OracleConnection();

    void Start()
    {
        pic = GetComponent<Image>();
        Texture2D result = new Texture2D(64, 64);
        try
        {
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd = new OracleCommand(select, con);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                byte[] img = (byte[])dr["Beispiel"];
                result.LoadImage(img);
                //imgage 800x600
                pic.sprite = Sprite.Create(result, new Rect(0, 0, 800, 600), new Vector2(0.5f, 0.5f));
            }
            //GetComponent<Renderer>().material.mainTexture = result;
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
