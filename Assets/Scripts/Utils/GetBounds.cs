using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetBounds : MonoBehaviour
{
    public GameObject bounds;

    [SerializeField] private CinemachineConfiner2D confiner;

    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    public void SetBounds()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++) 
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if(scene.isLoaded)
            {
                bounds = GameObject.FindGameObjectWithTag("Bounds");
            }
        }

        if(bounds != null)
            confiner .m_BoundingShape2D = bounds.GetComponent<Collider2D>();
        else
        {
            //Debug.Log("No Bounds found in the scene.");
        }
    }
}
