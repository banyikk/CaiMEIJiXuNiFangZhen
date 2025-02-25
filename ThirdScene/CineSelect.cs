using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CineSelect : MonoBehaviour
{
    public GameObject[] cinemachine;
    public Button[] button;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < button.Length; i++)
        {
            int index = i;
            button[index].onClick.AddListener(() => CineCameraSelect(cinemachine[index]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CineCameraSelect(GameObject gameObject)
    {
        foreach (var item in cinemachine)
        {
             gameObject.SetActive(true);
            if (item.name!=gameObject.name)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
