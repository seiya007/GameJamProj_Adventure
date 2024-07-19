#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Option : SingletonCustom<Option>
{
    [SerializeField]
    private SliderList sliderList;

    [SerializeField]
    private GameObject Root;

    private bool isTitleOptionOpen = false;

    public bool IsActive() { return Root.activeSelf; }

    private UnityAction callback;
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
        if(sliderList.IsStop) return;

        switch(sliderList.CursorNum)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                Hide();
                isTitleOptionOpen = false;
                break;
            case 3:
                Hide();

                if(!isTitleOptionOpen)
                {

                    SceneManager.Instance.mLoadingManager.OnFadeIn(() => {        
                        SceneManager.Instance.loadSceneGame(SceneManager.SceneType.Scene_Main);
                    });
                    isTitleOptionOpen = false;
                }
                break;
        }
    }

    public void Show(UnityAction _callback = null,bool _isTitleOptionOpen = false)
    {
        Root.SetActive(true);

        sliderList.IsStop = false;
        isTitleOptionOpen = _isTitleOptionOpen;

        callback = _callback;
        Time.timeScale = 0.0f;
    }

    public void Hide()
    {
        StartCoroutine(_Hide());
    }

    private IEnumerator _Hide()
    {
        Root.SetActive(false);
        sliderList.IsStop = true;

        yield return null;

        if(Time.timeScale <= 0.1f)
        {
            yield return null;
            Time.timeScale = 1.0f;
        }

        if(callback != null) 
        {
            callback.Invoke();
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
