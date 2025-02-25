using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FirstSceneSelect : MonoBehaviour
{
    public Slider slider;
    public Button firstSceneButton;
    public TextMeshProUGUI loadText;
    public Animator firstScene;
    // Start is called before the first frame update
    void Start()
    {
        
        firstSceneButton.onClick.AddListener(LoadButton);
    }

    private void LoadButton()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        slider.gameObject.SetActive(true);
        loadText.gameObject.SetActive(true);
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("K2ShiCao");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            slider.value=asyncOperation.progress;
            loadText.text = "等待加载" + (asyncOperation.progress * 100).ToString("F1") + "%";
            if (asyncOperation.progress>=0.9f)
            {
                loadText.text = "按下任意键继续";
                if (Input.anyKey)
                {
                    asyncOperation.allowSceneActivation = true;
                    

                }
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public  void AnimPlayer(bool ison)
    {
        if (ison)
        {
            
        firstScene.SetBool("isFirstScene", true);
        }
        else
        {
        firstScene.SetBool("isFirstScene", false);

        }
    }

}
