using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomJump : MonoBehaviour {

    private TextMeshProUGUI txt;

    // Use this for initialization
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        string showText = txt.text + "<color=\"red\" > (" + CrossSceneInformation.jump + ")";
        txt.text = showText;
    }
}
