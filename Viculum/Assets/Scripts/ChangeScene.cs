using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void JumpForward()
    {
        // File -> Build Settings -> Scenen sortieren!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JumpBackward()
    {
        // File -> Build Settings -> Scenen sortieren!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
