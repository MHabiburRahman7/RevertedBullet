using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotoScene : MonoBehaviour
{
    public string scene_name;

    public void goToScene()
    {
        Application.LoadLevel(scene_name);
    }

}
