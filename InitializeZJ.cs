using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class InitializeZJ : MonoBehaviour
{
    //我写
    private GameObject[] yyzjDingBu;
    private GameObject[] yyzjDiZuo;
    private GameObject[] yyzjQianLiuZi;
    private Animator[] poSuiMeiAnim;

    private void Awake()
    {
        MaterialFade.OnGameobjectShowOrHide += YYZJGameobjectShowOrHide;
        
    }
    private void OnDisable()
    {
        MaterialFade.OnGameobjectShowOrHide -= YYZJGameobjectShowOrHide;
        


    }
    //
    public GameObject _fangDingMeiYYZJ;
    public GameObject _guaBanJiTou;
    public GameObject _guaBanJiWei;
    /// <summary>
    /// 液压支架的父级别
    /// </summary>
    public Transform _YeYaZhiJiaParent;

    //煤壁破碎
    public GameObject _meiBiPoSui;

    /// <summary>
    /// 碎块的父级
    /// </summary>
    public Transform _poSuiParent;

    public int YeYaXuHao = 1;

    // Use this for initialization
    void Start()
    {
        //
        yyzjDingBu = new GameObject[47];
        yyzjDiZuo = new GameObject[47];
        yyzjQianLiuZi = new GameObject[47];
        poSuiMeiAnim = new Animator[94];
        //
        for (int i = 1; i < 48; i++)
        {
            GameObject yyzj = GameObject.Instantiate(_fangDingMeiYYZJ, new Vector3(_fangDingMeiYYZJ.transform.position.x, _fangDingMeiYYZJ.transform.position.y,
                _fangDingMeiYYZJ.transform.position.z - i * 2.0f),
                _fangDingMeiYYZJ.transform.rotation);
            yyzj.transform.parent = _YeYaZhiJiaParent;
            yyzj.name = "FangDingMeiZhiJia_GongYi" + i;

            //获取总览时需要隐藏的物体
            yyzjDingBu[i - 1] = yyzj.transform.Find("DingBu").gameObject;
            yyzjDiZuo[i - 1] = yyzj.transform.Find("DiZuo").gameObject;
            yyzjQianLiuZi[i - 1] = yyzj.transform.Find("QianLiuZi").Find("YeYaGang").gameObject;
            //给液压支架整理编号
            _YeYaZhiJiaParent.Find("FangDingMeiZhiJia_GongYi" + i.ToString() + "/DingBu/ShenSuoLiang/HuBangBan/HuBangBan/ZhiJiaBianHao").GetComponent<TextMesh>().text = (i + 1).ToString();

            //隐藏端头和端尾和刮板机端头刮板和端尾刮板重叠的刮板机
            if (i < 2 || i > 45)
            {
                GameObject GuaBan = GameObject.Find("FangDingMeiZhiJia_GongYi" +
                    i.ToString() + "/QianLiuZi/GuaBanJi");
                GuaBan.SetActive(false);
            }
            ////机尾斜切进刀
            //if (i > 13 && i <= 23)
            //{
            //    GameObject GuaBan = GameObject.Find("FangDingMeiZhiJia_GongYi" + i.ToString() + "/QianLiuZi/GuaBanJi");
            //    GameObject QianliuZi = GameObject.Find("FangDingMeiZhiJia_GongYi" + i.ToString() + "/QianLiuZi");
            //    QianliuZi.transform.position = new Vector3(QianliuZi.transform.position.x - (i - 13.0f) * 0.15f, QianliuZi.transform.position.y, QianliuZi.transform.position.z);
            //    GuaBan.transform.localEulerAngles = new Vector3(GuaBan.transform.localEulerAngles.x, GuaBan.transform.localEulerAngles.y, 3.0f);
            //}
            //if (i > 23)
            //{
            //    GameObject QianliuZi = GameObject.Find("FangDingMeiZhiJia_GongYi" + i.ToString() + "/QianLiuZi");
            //    QianliuZi.transform.position = new Vector3(GameObject.Find("FangDingMeiZhiJia_GongYi" + 23 + "/QianLiuZi").transform.position.x, QianliuZi.transform.position.y, QianliuZi.transform.position.z);
            //}
        }

        //隐藏第一个刮板
        GameObject QianliuZi0 = GameObject.Find("FangDingMeiZhiJia_GongYi0/QianLiuZi");
        QianliuZi0.transform.Find("GuaBanJi").gameObject.SetActive(false);

        #region 初始化端尾支架
        //获取端头支架的前溜
        GameObject DuanTouZhiJiaQianLiu = GameObject.Find("FangDingMeiZhiJia_GongYi0/QianLiuZi");
        _guaBanJiTou.transform.parent = DuanTouZhiJiaQianLiu.transform;
        #endregion

        #region 初始化端头支架
        //获取端尾支架的前溜
        GameObject DuanWeiZhiJiaQianLiu = GameObject.Find("FangDingMeiZhiJia_GongYi47/QianLiuZi");
        _guaBanJiWei.transform.parent = DuanWeiZhiJiaQianLiu.transform;
        #endregion

        //煤壁碎块动态生成
        for (int i = 1; i < 95; i++)
        {
            GameObject poSuiMeiBi = GameObject.Instantiate(_meiBiPoSui, new Vector3(_meiBiPoSui.transform.position.x, _meiBiPoSui.transform.position.y, _meiBiPoSui.transform.position.z - i * 1.006f), _meiBiPoSui.transform.rotation);
            poSuiMeiBi.transform.parent = _poSuiParent;
            poSuiMeiBi.name = "PoSuiMeiBi" + i;
            poSuiMeiAnim[i-1]=poSuiMeiBi.GetComponent<Animator>();

        }
        //初始化采煤机下放护帮板

        ChuShiHuaHuBang();
        YYZJGameobjectShowOrHide(false);

    }
    void ChuShiHuaHuBang()
    {
        for (int i = 0; i < 10; i++)
        {
            _YeYaZhiJiaParent.Find("FangDingMeiZhiJia_GongYi" + i.ToString() + "/DingBu/ShenSuoLiang/HuBangBan/HuBangBan").transform.localEulerAngles = new Vector3(0, -185, -90);
        }
    }
    public Animator[] GetPoSuiMeiAnim()
    {
        return poSuiMeiAnim;
    }
    public void YYZJGameobjectShowOrHide(bool ison)
    {
        if (ison)
        {
            foreach (var item in yyzjDingBu)
            {
                item.gameObject.SetActive(true);
            }
            foreach (var item in yyzjDiZuo)
            {
                item.gameObject.SetActive(true);
            }
            foreach (var item in yyzjQianLiuZi)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {

            foreach (var item in yyzjDingBu)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in yyzjDiZuo)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in yyzjQianLiuZi)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
