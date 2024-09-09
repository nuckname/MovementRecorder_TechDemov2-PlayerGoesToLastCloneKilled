using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);

    }
    
    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
}
