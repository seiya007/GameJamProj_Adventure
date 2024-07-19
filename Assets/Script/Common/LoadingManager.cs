using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LoadingManager : MonoBehaviour
{

    [SerializeField]
    private RawImage fadeImage;

    [SerializeField]
    private TextMeshProUGUI fadeLoadingText;
    [SerializeField]
    private TextMeshProUGUI fadeLoadingTextPoint;
    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;

    public bool IsLoading = false;
    private void OnEnable()
    {
        //なぜかコルーチンがうまく作動しないので、ダミーで作動させる
        canvas.gameObject.SetActive(true);
        StartCoroutine(_OnFadeIn(0.1f,0.1f));
        StopCoroutine(_OnFadeIn());
        canvas.gameObject.SetActive(false);
    }

    public void OnFadeIn(UnityAction Action = null)
    {
        // var sceneCamera = GameObject.Find("SceneCamera").GetComponent<Camera>();
        // if(sceneCamera != null)
        //     canvas.worldCamera = sceneCamera;
        // else
        //     canvas.worldCamera = Camera.main;
        // // canvas.worldCamera = Camera.current;

        StartCoroutine(_OnFadeIn(2.0f,2.0f,Action));
        StartCoroutine(_PointReading());

    }

    private IEnumerator _OnFadeIn(float fadeTime = 1.0f,float loadingTime = 1.0f, UnityAction action = null)
    {
        float time = 0;

        bool isLoadingTime = false;

        canvas.gameObject.SetActive(true);


        while (time <= fadeTime)
        {
            if(!isLoadingTime)
            {
                fadeImage.color = new Color(0, 0, 0, 1.0f);
                fadeLoadingText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                fadeLoadingTextPoint.color = new Color(1.0f, 1.0f, 1.0f,1.0f);

                yield return null;

                // シーンのロード
                if(action != null)
                    action.Invoke();
                
                yield return new WaitForSeconds(loadingTime);
                isLoadingTime = true;
            }
            else
            {
                fadeLoadingText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                fadeLoadingTextPoint.color = new Color(1.0f, 1.0f, 1.0f,0.0f);
                fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1.0f, 0.0f, time / fadeTime));
                IsLoading = true;

            // Debug.LogError($"シーンのロードTime = {time/fadeTime}");
            }
            time += Time.deltaTime;
            yield return null;
        }
        IsLoading = false;
        StopCoroutine(_PointReading());

        // fadeLoadingText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // fadeLoadingTextPoint.color = new Color(1.0f, 1.0f, 1.0f,0.0f);
        canvas.gameObject.SetActive(false);
        yield return null;
        // StopCoroutine(_OnFadeIn());
        // yield break;
    }

    private IEnumerator _PointReading()
    {
        while(true)
        {
            fadeLoadingTextPoint.text = ".";
            yield return new WaitForSeconds(0.5f);
            fadeLoadingTextPoint.text = "..";
            yield return new WaitForSeconds(0.5f);
            fadeLoadingTextPoint.text = "...";
            yield return new WaitForSeconds(0.5f);
            fadeLoadingTextPoint.text = ".";
            yield return new WaitForSeconds(0.5f);
            yield break;
        }
    }
}
