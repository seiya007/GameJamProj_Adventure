using System.Collections.Generic;
using UnityEngine;

public class CursorList : MonoBehaviour
{
    [SerializeField,Header("選択カーソルオブジェクト")]
    private GameObject[] cursorObj;

    [SerializeField,Header("カーソル")]
    private GameObject CursorObj;

    [SerializeField,Header("縦型リストですか")]
    private bool isHorizontal = false;

    private delegate void DelegateType();
    private DelegateType del;

    private int cursorNum = -1;
    public int CursorNum => cursorNum;

    private int prev_cursorNum = -1;

    private bool isStop = true;
    public bool IsStop
    {
        get => isStop;
        set => isStop = value;
    }
    private void OnEnable() 
    { 
        cursorNum = 0;
        prev_cursorNum = 0;

        isStop = false;
        
        // SetPosition();
        
        del = SelectObj;
        del.Invoke();
        VirtualInputManager.Instance.InputDownAction.AddListener(InputDownProcess);
        VirtualInputManager.Instance.InputUpAction.AddListener(InputUpProcess);
        VirtualInputManager.Instance.InputLeftAction.AddListener(InputLeftProcess);
        VirtualInputManager.Instance.InputRightAction.AddListener(InputRightProcess);
    }

    private void OnDisable()
    {
        VirtualInputManager.Instance.InputDownAction.RemoveListener(InputDownProcess);
        VirtualInputManager.Instance.InputUpAction.RemoveListener(InputUpProcess);
        VirtualInputManager.Instance.InputLeftAction.RemoveListener(InputLeftProcess);
        VirtualInputManager.Instance.InputRightAction.RemoveListener(InputRightProcess);
    }

    private void InputDownProcess()
    {
        if(isStop) return;

        prev_cursorNum = cursorNum;

        cursorNum++;
        if(cursorNum > cursorObj.Length - 1)
            cursorNum = cursorObj.Length - 1;
            
        del.Invoke();
    }
    private void InputUpProcess()
    {
        if(isStop) return;

        prev_cursorNum = cursorNum;

        cursorNum--;
        if(cursorNum < 0)
            cursorNum = 0;
        
        del.Invoke();
    }

    public void InputLeftProcess()
    {
    }

    private void InputRightProcess()
    {
    }

    public void SelectObj()
    {
        var tmp = new Vector3(-0.3f,0.0f,0.0f);
        if(cursorObj.Length <= 0) return;
        if(isStop) return;

        cursorObj[prev_cursorNum].transform.position -= tmp;
        cursorObj[cursorNum].transform.position += tmp;


        CursorObj.transform.parent = cursorObj[cursorNum].transform;
        CursorObj.transform.position = cursorObj[cursorNum].transform.position;
        CursorObj.transform.position += new Vector3(2.5f,0.0f,0.0f);
    }
}
