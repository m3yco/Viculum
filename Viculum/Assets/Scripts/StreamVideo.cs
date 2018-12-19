using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayVideo());
	}
	
	IEnumerator PlayVideo()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        videoPlayer.url = Application.dataPath + "/Video/" + "zsmall.mp4";
        videoPlayer.Prepare();
        wait = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
