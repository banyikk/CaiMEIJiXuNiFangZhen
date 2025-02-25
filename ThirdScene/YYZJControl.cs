using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class YYZJControl : MonoBehaviour
{
    [Header("»¤°ï°å")]
    public GameObject huBangBan;
    public float huBangBanRotateSpeed;
    public float minHuBangBanAngle = -60f;
    public float maxHuBangBanAngle = 180f;
    [SerializeField]
    private bool isHuBangBanLeftRotate;
    [SerializeField]
    private bool isHuBangBanRightRotate;
    [Header("ÉìËõÁº")]
    public GameObject shenSuoLiang;
    public float shenSuoLiangSpeed;
    public float minShenSuoLiang;
    public float maxShenSuoLiang;
    private float shengSuoLiangDirection = 0f;
    [Header("¶¥²¿")]
    public GameObject dingBu;
    public float dingBuSpeed;
    public float minDingBu;
    public float maxDingBu;
    private float dingBuDirection = 0f;
    [Header("QianLiuZi")]
    public GameObject qianLiuZi;
    public float qianLiuZiSpeed;
    public float minQianLiuZi;
    public float maxQianLiuZi;
    private float qianLiuZiDirection=0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HuBangBanRotate();
        Move(shengSuoLiangDirection, shenSuoLiang, shenSuoLiangSpeed,minShenSuoLiang, maxShenSuoLiang);
        Move(dingBuDirection,dingBu,dingBuSpeed,minDingBu,maxDingBu);
        Move(qianLiuZiDirection, qianLiuZi, qianLiuZiSpeed, minQianLiuZi, maxQianLiuZi);
    } 
    void HuBangBanRotate()
    {
        if (isHuBangBanLeftRotate)
        {
            huBangBan.transform.Rotate(Vector3.up*huBangBanRotateSpeed*Time.deltaTime);
            float y=huBangBan.transform.localEulerAngles.y;
            y = y > 180 ? y - 360 : y;
            y = Mathf.Clamp(y, minHuBangBanAngle, maxHuBangBanAngle);
            huBangBan.transform.localEulerAngles = new Vector3(huBangBan.transform.localEulerAngles.x, y, huBangBan.transform.localEulerAngles.z);
        }
        if (isHuBangBanRightRotate)
        {
            huBangBan.transform.Rotate(Vector3.down * huBangBanRotateSpeed * Time.deltaTime);
            float y = huBangBan.transform.localEulerAngles.y;
            y = y > 180 ? y - 360 : y;
            y = Mathf.Clamp(y, minHuBangBanAngle, maxHuBangBanAngle);
            huBangBan.transform.localEulerAngles = new Vector3(huBangBan.transform.localEulerAngles.x, y, huBangBan.transform.localEulerAngles.z);
        }
    }
    
    void Move(float direction, GameObject gameObject, float speed, float min, float max)
    {
        if (direction!=0)
        {
            gameObject.transform.Translate(direction * Vector3.left * speed / 100);
            gameObject.transform.localPosition = new Vector3(Mathf.Clamp(gameObject.transform.localPosition.x, min, max), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
    }

    public void HuBangBan(bool ison)
    {
        if (ison)
        {
            isHuBangBanLeftRotate=true;
        }
        else
        {
            isHuBangBanLeftRotate = false;
        }
    }
    public void HuBangBanRight(bool ison)
    {
        if (ison)
        {
            isHuBangBanRightRotate = true;
        }
        else
        {
            isHuBangBanRightRotate = false;
        }
    }
    public void ShenSuoLiagForward(bool ison)
    {
        if (ison)
        {
            shengSuoLiangDirection = 1f;
            
        }
        else
        {
            shengSuoLiangDirection = 0f;
            
        }
    }
    public void ShenSuoLiagBack(bool ison)
    {
        if (ison)
        {
            shengSuoLiangDirection = -1f;

            
        }
        else
        {
            shengSuoLiangDirection = 0f;

            
        }
    }
    public void DingBuUP(bool ison)
    {
        if (ison)
        {
            dingBuDirection = 1f;


        }
        else
        {
            dingBuDirection = 0f;


        }
    }
    public void DingBuDown(bool ison)
    {
        if (ison)
        {
            dingBuDirection = -1f;


        }
        else
        {
            dingBuDirection = 0f;


        }
    }
    public void QianLiuZiRight(bool ison)
    {
        if (ison)
        {
            qianLiuZiDirection = -1f;


        }
        else
        {
            qianLiuZiDirection = 0f;


        }
    }
    public void QianLiuZiLeft(bool ison)
    {
        if (ison)
        {
            qianLiuZiDirection = 1f;


        }
        else
        {
            qianLiuZiDirection = 0f;


        }
    }
}
