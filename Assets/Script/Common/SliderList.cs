using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderList : MonoBehaviour
{
    [SerializeField]
    private Slider[] sliders;

    [SerializeField,Header("選択カーソルオブジェクト")]
    private GameObject[] cursorObj;

    [SerializeField,Header("カーソル")]
    private GameObject CursorObj;

    private delegate void DelegateType();
    private DelegateType del;

    private int cursorNum = 0;
    public int CursorNum => cursorNum;

    private int prev_cursorNum = 0;

    private bool isStop = true;
    public bool IsStop
    {
        get => isStop;
        set => isStop = value;
    }

    private void OnEnable() 
    { 
        // cursorNum = 0;
        // prev_cursorNum = 0;
        
        
        del = SelectObj;
        // del.Invoke();
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
        if(isStop) return;
        if(cursorNum > 1) return;

        sliders[cursorNum].value -= 0.1f;

        switch(cursorNum)
        {
            case 0:
                SoundManager.Instance.bgmMasterVolume = sliders[cursorNum].value;
                break;
            case 1:
                SoundManager.Instance.seMasterVolume = sliders[cursorNum].value;
                break;
        }
    }

    private void InputRightProcess()
    {
        if(isStop) return;
        if(cursorNum > 1) return;

        cursorNum = Mathf.Clamp(cursorNum,0,1);

        sliders[cursorNum].value += 0.1f;

        switch(cursorNum)
        {
            case 0:
                SoundManager.Instance.bgmMasterVolume = sliders[cursorNum].value;
                break;
            case 1:
                SoundManager.Instance.seMasterVolume = sliders[cursorNum].value;
                break;
        }
    }

    public void SelectObj()
    {
        var tmp = new Vector3(-0.3f,0.0f,0.0f);
        if(cursorObj.Length <= 0) return;
        if(isStop) return;

        // cursorObj[prev_cursorNum].transform.position -= tmp;
        // cursorObj[cursorNum].transform.position += tmp;


        CursorObj.transform.parent = cursorObj[cursorNum].transform;
        CursorObj.transform.position = cursorObj[cursorNum].transform.position;
        CursorObj.transform.position += new Vector3(300.0f,0.0f,0.0f);
    }
}

