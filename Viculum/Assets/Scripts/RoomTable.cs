using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomTable : MonoBehaviour {
    private TextMeshProUGUI txt;

    // Use this for initialization
    void Start () {
        txt = GetComponent<TextMeshProUGUI>();
        string showText = txt.text;

        for (int i = 0; i < CrossSceneInformation.anzeige.Count; i++)
        {
            showText += "\n" + CrossSceneInformation.anzeige[i];
        }

        txt.text = showText;
    }
}
