#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Scene_MainMenu : MonoBehaviour
{
    [SerializeField,Header("カーソル")]
    private CursorList cursorList;

    [SerializeField,Header("メインシーン")]
    private Scene_Main sceneMain;

    [SerializeField,Header("キャンバス")]
    private Canvas canvas;

    private bool isLoadGame = false;

    private void OnEnable()
    {
        VirtualInputManager.Instance.InputInteractionAction.AddListener(InputSubmitAction);
        canvas.worldCamera = Camera.main;
    }
    private void OnDisable()
    {
        VirtualInputManager.Instance.InputInteractionAction.RemoveListener(InputSubmitAction);
    }

    private void InputSubmitAction()
    {
        if(Option.Instance.IsActive()) return;
        if(cursorList.IsStop) return;
        
        switch(cursorList.CursorNum)
        {
            case 0:
                if (sceneMain == null) return;
                if (isLoadGame) return;
                sceneMain.MainGameOpen();
                isLoadGame = true;
                break;
            case 1:
                cursorList.IsStop = true;
                Option.Instance.Show(() => { cursorList.IsStop = false; },_isTitleOptionOpen:true);
                Debug.Log("option open");
                break;
            case 2:
                QuitGame();
                break;
            case 3:
                break;
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
