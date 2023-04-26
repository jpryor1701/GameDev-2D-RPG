using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    // [SerializeField] private string sceneTransitionToName;
    // enum Directions {north, east, south, west};
    [SerializeField] EnumDirections nextMapEnterLocation;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // SceneManagement.Instance.SetTransitionName(sceneTransitionToName);
            SceneManagement.Instance.SetTransitionName(nextMapEnterLocation.ToString());
            UI_Fade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }   
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        
        SceneManager.LoadScene(sceneToLoad);        
    }
}
