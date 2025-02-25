using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ThirdScenenSelect : MonoBehaviour
{
    public Slider slider;
    public Button thirdScenenButton;
    public TextMeshProUGUI loadText;
    public Animator thirdScenenAnim;
    // Start is called before the first frame update
    void Start()
    {

        thirdScenenButton.onClick.AddListener(LoadThirdButton);
    }

    private void LoadThirdButton()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        slider.gameObject.SetActive(true);
        loadText.gameObject.SetActive(true);
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ThirdScene");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            slider.value = asyncOperation.progress;
            loadText.text = "等待加载" + (asyncOperation.progress * 100).ToString("F1") + "%";
            if (asyncOperation.progress >= 0.9f)
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
    public void AnimPlayer(bool ison)
    {
        if (ison)
        {

            thirdScenenAnim.SetBool("isThirdScene", true);
        }
        else
        {
            thirdScenenAnim.SetBool("isThirdScene", false);

        }
    }

}
