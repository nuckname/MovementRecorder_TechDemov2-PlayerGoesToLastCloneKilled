using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostCollision : MonoBehaviour
{
    public TextMeshProUGUI debugTextPrefab;
    public Canvas uiCanvas;
    [SerializeField] private bool hasGhostCollectedKey;

    private void Start()
    {
        hasGhostCollectedKey = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GhostKey"))
        {
            hasGhostCollectedKey = true; 
            Destroy(other.gameObject);
        }
        
        if (gameObject.CompareTag("GhostToKill") && other.gameObject.CompareTag("FinishLine") && hasGhostCollectedKey)
        {
            // Debug message
            print("The red ghost got to the finish line: You die :( Reset Scene");

            // Instantiate debug text and start coroutine to remove it after 3 seconds
            /*
            TextMeshProUGUI debugTextInstance = Instantiate(debugTextPrefab, uiCanvas.transform);
0            
            debugTextInstance.transform.localPosition = Vector3.zero; // Adjust position as needed
            StartCoroutine(RemoveTextAfterDelay(debugTextInstance, 3f));
            */  
            StartCoroutine(RestartSceneAfterDelay(3f));
            
            if (GameManager.DebugRestartScene)
            {
                // Restart the scene after a delay
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GhostToKill"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RemoveTextAfterDelay(TextMeshProUGUI text, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(text.gameObject);
    }

    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}