using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Play : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneLoaderManager.instance.LoadFirstScene();
    }
}
