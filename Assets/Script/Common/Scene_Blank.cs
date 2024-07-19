using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Blank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Main");
    }
}
