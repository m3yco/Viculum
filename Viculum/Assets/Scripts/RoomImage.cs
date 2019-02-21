using UnityEngine;
using UnityEngine.UI;
using Oracle.ManagedDataAccess.Client;
using System;


public class RoomImage : MonoBehaviour
{
    // Hier wird der Name des Bildes aus der Datenbank geholt.   
    public Image pic;
    String connectionString = "Data Source=(DESCRIPTION=" +
           "(ADDRESS=(PROTOCOL=TCP)(HOST=orahost)(PORT=1521))" +
           "(CONNECT_DATA=(SERVICE_NAME=infdb.inf.hs-albsig.de)));" +
           "User Id = projektstudium; Password = projektstudium; ";

    // Hier der Select für den blob des Bildes.
    String select = "select BEISPIEL from MODULMEDIUM where dateiformat in ('.png','.jpg', '.JPG') and MODULMEDIUMID in " +
                    "(select MODULMEDIUMID from VERANSTALTUNGSMATERIAL where SEMESTERVERANSTALTUNGID in "+
                    "(select SEMESTERVERANSTALTUNGID from SEMESTERVERANSTALTUNG where SEMESTERVERANSTALTUNG.MODULID=" + CrossSceneInformation.modul + "))";
    OracleConnection con = new OracleConnection();

    void Start()
    {
        // Leeres Bild Object wird mit GameObject initialisiert.
        pic = GetComponent<Image>();
        // Textur des Bildes festgelegt.
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
                // Beispiel ist die Spalte vom blob in der Datenbank.
                byte[] img = (byte[])dr["Beispiel"];
                // Ladet Bild aus Byte String
                result.LoadImage(img);
                //Setzt Größe des Bildes fest hier -> 800x600
                pic.sprite = Sprite.Create(result, new Rect(0, 0, 800, 600), new Vector2(0.5f, 0.5f));
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
    }
}
