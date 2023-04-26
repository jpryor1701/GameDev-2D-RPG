using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeraEnter : MonoBehaviour
{
    // [SerializeField] private string currentTransitionName;
    enum Directions {north, east, south, west};
    [SerializeField] Directions currentMapLocation;

    private void Start()
    {
        // if (currentTransitionName == SceneManagement.Instance.SceneTransitionName)
        if (currentMapLocation.ToString() == SceneManagement.Instance.SceneTransitionName)
        {
            UI_Fade.Instance.FadeToClear();
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
        }  
    }
}