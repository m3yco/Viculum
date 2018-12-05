using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void GetLectures()
    {
        // File -> Build Settings -> Scenen sortieren!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
