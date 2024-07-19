using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneManager : SingletonCustom<SceneManager>
{

    [SerializeField]
    public LoadingManager mLoadingManager;
    
    private string[] SceneName = 
    {
        "Scene_Main",
        "Scene_MainGame",
    };

    public enum SceneType
    {
        Scene_Main,
        Scene_MainGame,
    };

    public SceneType sceneType = SceneType.Scene_Main;

    private GameObject gameScene;
    private GameObject prevGameScene;

    // Start is called before the first frame update
    private void Start()
    {
        loadSceneMain(sceneType);
        Application.targetFrameRate = 60;
        // mLoadingManager.Canvas.gameObject.SetActive(false);
        // mLoadingManager.Canvas.gameObject.SetActive(true);
    }

    public async void loadSceneMain(SceneType _sceneType)
    {
        string str = SceneName[(int)_sceneType];
        var scene = await Addressables.LoadAssetAsync<GameObject>(str).Task;
        Instantiate(scene,this.transform);
    }

    public void loadSceneGame(SceneType _sceneType)
    {
        if(gameScene == null)
        {
            loadscene(_sceneType);
            Debug.Log("ロードしてスポーン");
        }
        else
        {
            // StartCoroutine(deleteGameScene());
            prevGameScene.SetActive(false);
            Destroy(prevGameScene);
            StartCoroutine(SpawnGameScene(_sceneType));
        }
    }
    
    private async void loadscene(SceneType _sceneType)
    {
        string str = SceneName[(int)_sceneType];
        var scene = await Addressables.LoadAssetAsync<GameObject>(str).Task;
        gameScene = Instantiate(scene,this.transform);
        prevGameScene = gameScene;
    }

    private IEnumerator SpawnGameScene(SceneType _sceneType)
    {
        yield return new WaitForSeconds(0.5f);
        loadscene(_sceneType);
        // prevGameScene = gameScene;
    }
}
