using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Windows.Speech;
using Unity.VisualScripting;
using System;

public class 液压支架抬升 : MonoBehaviour
{
    
    
    public GameObject targetObject;
    private Vector3 targetRotation = new Vector3(0, -185, -90);
    private Vector3 originalRotation = new Vector3(0, -115, -90);
    public bool isRotating = false;
    
    private void Start()
    {
        
    }
    private void Awake()
    {
        
        string str1 = transform.parent.parent.name;
        string str = str1.Replace("FangDingMeiZhiJia_GongYi", "");
        int index;
        if (int.TryParse(str, out index))
        {
            //print(index);
            string targetName = "FangDingMeiZhiJia_GongYi" + index.ToString() + "/DingBu/ShenSuoLiang/HuBangBan/HuBangBan";
            targetObject = GameObject.Find(targetName);


        }
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CaiMeiJiCollider"))
        {

            if (targetObject != null && !isRotating)
            {
                //print(targetObject.transform.localEulerAngles);
                // 检查目标物体是否已处于目标旋转状态
                if (targetObject.transform.localEulerAngles != new Vector3(0, 175, 270))
                {
                    //print(1);
                    StartCoroutine(RotateToTarget());
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CaiMeiJiCollider"))
        {

            StartCoroutine(RestoreOriginalRotation());    
        }
    }
   

    private IEnumerator RotateToTarget()
    {
        isRotating = true;
        float duration = 0.5f;
        float time = 0;

        
        while (time < duration)
        {

            targetObject.transform.localEulerAngles = Vector3.Lerp(originalRotation, targetRotation, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        targetObject.transform.localEulerAngles = targetRotation; // 确保最终值是目标值
        isRotating = false;
    }

    private IEnumerator RestoreOriginalRotation()
    {
        //print(3);

        float duration = 1f;
        float time = 0;

        while (time < duration)
        {
            targetObject.transform.localEulerAngles = Vector3.Lerp(targetRotation, originalRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localEulerAngles = originalRotation; // 确保最终值是原始值  
    }
}

    

