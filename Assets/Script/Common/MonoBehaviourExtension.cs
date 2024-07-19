using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// MonoBehaviour拡張クラス
/// </summary>
public class MonoBehaviourExtension : MonoBehaviour {

	/// <summary>
    /// 指定時間後処理の汎用コルーチン呼び出し
    /// </summary> 
    /// <param name="_wait">待機秒数</param>
    /// <param name="_act">実行処理</param>
    public void WaitAfterExec( float _wait, Action _act, bool _isTimeIgnore = false ) {
        
        if(_isTimeIgnore)
        {
            StartCoroutine(_WaitAfterExecTimeIgnore(_wait, _act));
        }
        else
        {
            StartCoroutine(_WaitAfterExec(_wait, _act));
        }
    }
    
    /// <summary>
    /// 指定時間後処理の汎用コルーチン呼び出し
    /// </summary> 
    private static IEnumerator _WaitAfterExec( float _wait, Action _act ) {
        
        yield return new WaitForSeconds( _wait );
        
        if(_act != null)
            _act();
    }

    /// <summary>
    /// 指定時間後処理の汎用コルーチン呼び出し
    /// </summary> 
    private static IEnumerator _WaitAfterExecTimeIgnore(float _wait, Action _act)
    {
        yield return new WaitForSecondsRealtime(_wait);
        
        if(_act != null)
            _act();
    }

    public void StopExec()
    {
        StopAllCoroutines();
    }
}
