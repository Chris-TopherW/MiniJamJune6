using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update () {
    	RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.4f);
	}
}
