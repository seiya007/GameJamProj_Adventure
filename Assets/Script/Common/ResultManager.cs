using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private CursorList cursorList;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private bool isFinishAnimation = false;

    private int score;
    private void OnEnable()
    {
        VirtualInputManager.Instance.InputInteractionAction.AddListener(InputSubmitAction);
    }

    private void OnDisable()
    {
        VirtualInputManager.Instance.InputInteractionAction.RemoveListener(InputSubmitAction);
    }

    private void InputSubmitAction()
    {
        switch(cursorList.CursorNum)
        {
            case 0:
                if(isFinishAnimation) return;

                Time.timeScale = 1.0f;
                hide();
                SceneManager.Instance.mLoadingManager.OnFadeIn(() => {        
                    SceneManager.Instance.loadSceneGame(SceneManager.SceneType.Scene_MainGame);
                });
                break;
            case 1:
                QuitGame();
                break;
            case 2:
                break;
            case 3:

                break;
        }
    }
    public void show(int _s)
    {
        root.SetActive(true);
        scoreText.text = _s.ToString();
        StartCoroutine(_ShowResult());
    }

    public void hide()
    {
        root.SetActive(false);
    }
    private IEnumerator _ShowResult()
    {
        var time = 0.0f;
        while(time < 1.0f)
        {
            time += 0.0016f;
            root.transform.position = Vector3.Lerp(root.transform.position,new Vector3(0.0f,0.0f,0.0f),time);

            yield return null;
        }
        Debug.Log($"{time} 秒経過");


        StopCoroutine(_ShowResult());
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
