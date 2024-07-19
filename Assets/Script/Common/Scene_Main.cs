using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Main : MonoBehaviour
{

    private void OnEnable()
    {
        // SceneManager.Instance.loadScene(SceneManager.SceneType.Scene_MainGame);
        // Hide();
        SoundManager.Instance.Playbgm(BGMSoundData.BGM.Title);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void MainGameOpen()
    {
        DestoryGameScene();
        SceneManager.Instance.mLoadingManager.OnFadeIn(() => {        
            SceneManager.Instance.loadSceneGame(SceneManager.SceneType.Scene_MainGame);
        });
    }

    private void DestoryGameScene()
    {
        Destroy(gameObject);
    }
}
