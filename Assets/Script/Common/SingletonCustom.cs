using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCustom<T> : MonoBehaviourExtension where T : MonoBehaviourExtension
{
	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType(typeof(T));
				
				if (instance == null) {
					Debug.LogError (typeof(T) + "is nothing");
				}
			}
			return instance;
		}
	}
	
	protected bool CheckInstance()
	{
		if( this == Instance){ return true;}
		Destroy(gameObject);
		return false;
	}
	
	// 動作が有効になったときに呼び出される
	public void OnEnable(){
		CheckInstance();
		Resume();
	}
			
	public virtual void OnDestroy(){
		if( this == Instance){
			instance = null;
		}
	}

	public virtual void Resume(){}
}
