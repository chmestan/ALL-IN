// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;


// public class ChangeSceneWithDelay : MonoBehaviour
// {
//     private bool debug = false;

//     public void LoadSceneWithDelay(string scene)
//     {
//         scene = scene.Trim();
//         StartCoroutine(LoadSceneAfterDelayCoroutine(scene, 1f));
//     }

//     private IEnumerator LoadSceneAfterDelayCoroutine(string scene, float delay)
//     {
//         if (debug) Debug.Log("(ChangeScene) Delaying scene load by " + delay + " seconds.");
//         yield return new WaitForSeconds(delay);

//         if (debug) Debug.Log("(ChangeScene) Scene name to load: " + scene);
//         SceneManager.LoadScene(scene);
//     }
// }
