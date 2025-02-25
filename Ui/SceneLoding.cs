using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoding : MonoBehaviour
{
    public TextMeshProUGUI loadText;
    
    public Button quitButton;
    
    public Image image;
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        


        quitButton.onClick.AddListener(LoadButton);
    }

    private void LoadButton()
    {
        

        StartCoroutine(LoadSceneMode());
    }

    IEnumerator LoadSceneMode()
    {
        image.DOFade(1, 1f).SetEase(Ease.InOutQuad);
        loadText.gameObject.SetActive(true);
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
        asyncOperation.allowSceneActivation=false;
        while (!asyncOperation.isDone)
        {
            loadText.text = "等待加载：" + (asyncOperation.progress * 100).ToString("F1") + "%";
            if (asyncOperation.progress>=0.9f)
            {
                loadText.text = "按下任意键继续";
                if (Input.anyKey)
                {
                    asyncOperation.allowSceneActivation = true;
                    image.DOFade(0, 1f).SetEase(Ease.InOutQuad);
                    loadText.text = "";

                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
