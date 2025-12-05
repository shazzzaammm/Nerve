using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    public void LoadScene(int selection){
        SceneManager.LoadScene(selection);
    }

    public void Quit(){
        Application.Quit();
    }
}
