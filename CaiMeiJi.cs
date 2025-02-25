using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum 采煤机状态
{
    停止,
    持续加速,
    持续减速,
    稳定速度,

}

public class CaiMeiJi : MonoBehaviour
{
    private 采煤机状态 状态;
    public GameObject _采煤机;

    [Header("左右牵启牵停")]
    [SerializeField]
    private bool 是否左牵 = false;
    [SerializeField]
    private bool 是否右牵 = false;
    public Button zuoQian;
    public Button youQian;
    public Button qianTing;
    public Color selectColor;
    [SerializeField]
    private bool isMove = false;

    public Button jiaSu;
    public Button jianSu;
    public Button yunSu;
    public float 采煤机起步速度 = 0.001f;
    public float 采煤机最大速度 = 0.1f;
    public float 速率 = 0.1f;
    public float 当前速度;
    Vector3 FangXiang;
    [Header("预设：左牵")]
    public Button caimeijiTurnleft;
    public Button caimeijiTurnright;
    public Button allBuffFuck;

    void Start()
    {
        状态 = 采煤机状态.停止;
        zuoBiTargetRotation = 左臂.transform.localRotation;
        youBiTargetRotation = 右臂.transform.localRotation;
        caimeijiTurnleft.onClick.AddListener(() => {
            停止所有动作();
            Invoke("左牵预设", 3f);
             });
        caimeijiTurnright.onClick.AddListener(() =>
        {
            停止所有动作();
            Invoke("右牵预设", 3f);
            
        });
        allBuffFuck.onClick.AddListener(停止所有动作);
    }

    void Update()
    {
        采煤机移动();
        采煤机左臂();
        采煤机右臂();
        滚筒启动();
    }
    #region 
    public void 采煤机移动()
    {

        if (isMove)
        {
            if (状态 == 采煤机状态.持续加速)
            {
                SetButtonColor(yunSu, Color.white);
                SetButtonColor(jianSu, Color.white);
                SetButtonColor(jiaSu, selectColor);
                // 逐步加速，确保速度不超过最大速度
                采煤机起步速度 += 速率 * Time.deltaTime;
                采煤机起步速度 = Mathf.Min(采煤机起步速度, 采煤机最大速度);
                _采煤机.transform.Translate(FangXiang * 采煤机起步速度);
                当前速度 = 采煤机起步速度;
            }
            if (状态 == 采煤机状态.持续减速)
            {
                SetButtonColor(jiaSu, Color.white);
                SetButtonColor(yunSu, Color.white);
                SetButtonColor(jianSu, selectColor);
                采煤机起步速度 -= 速率 * Time.deltaTime;
                采煤机起步速度 = Mathf.Max(采煤机起步速度, 0);
                _采煤机.transform.Translate(FangXiang * 采煤机起步速度);
                当前速度 = 采煤机起步速度;
            }
            if (状态 == 采煤机状态.稳定速度)
            {
                SetButtonColor(jiaSu, Color.white);
                SetButtonColor(jianSu, Color.white);
                SetButtonColor(yunSu, selectColor);
                _采煤机.transform.Translate(FangXiang * 当前速度);

            }
        }

        if (采煤机起步速度 == 0)
        {
            isMove = false; // 停止移动
            状态 = 采煤机状态.停止;
        }
        // 限制采煤机的移动范围
        if (_采煤机.transform.position.z <= -33f || _采煤机.transform.position.z >= 30f || 状态 == 采煤机状态.停止)
        {
            采煤机起步速度 = 0;
            当前速度 = 0;

        }
        if (状态 == 采煤机状态.停止)
        {
            SetButtonColor(jiaSu, Color.white);
            SetButtonColor(jianSu, Color.white);
            SetButtonColor(yunSu, Color.white);
            采煤机起步速度 = 0;
            当前速度 = 0;
        }
    }

    public void 左牵()
    {
        if (isMove == false)
        {
            SetButtonColor(zuoQian, selectColor);
            SetButtonColor(youQian, Color.white);
            SetButtonColor(qianTing, Color.white);

            是否左牵 = true;
            是否右牵 = false;
            采煤机起步速度 = 0;
            当前速度 = 0;
            FangXiang = _采煤机.transform.forward;
        }
    }
    public void 牵停()
    {

        DOTween.To(() => 当前速度, x =>
        {
            当前速度 = x;
            采煤机起步速度 = x; // 同步更新采煤机起步速度
            _采煤机.transform.Translate(FangXiang * 当前速度 * Time.deltaTime); // 使用Time.deltaTime确保移动平滑
            if (当前速度 <= 0.001f)
            {
                当前速度 = 0;
                采煤机起步速度 = 0;
                isMove = false;
                状态 = 采煤机状态.停止;
            }
        }, 0, 3f);
        SetButtonColor(qianTing, selectColor);
        SetButtonColor(zuoQian, Color.white);
        SetButtonColor(youQian, Color.white);
        是否左牵 = false;
        是否右牵 = false;

    }
    public void 右牵()
    {
        if (isMove == false)
        {
            SetButtonColor(youQian, selectColor);
            SetButtonColor(qianTing, Color.white);
            SetButtonColor(zuoQian, Color.white);
            是否右牵 = true;
            是否左牵 = false;
            采煤机起步速度 = 0;
            当前速度 = 0;
            FangXiang = -_采煤机.transform.forward;
        }
    }

    public void 加速长按()
    {
        if (是否左牵 || 是否右牵)
        {
            isMove = true;
            状态 = 采煤机状态.持续加速;
        }

    }

    public void 加速抬起()
    {

        状态 = 采煤机状态.稳定速度; // 标记为停止加速
    }

    public void 减速长按()
    {
        状态 = 采煤机状态.持续减速; // 标记为持续减速
    }

    public void 减速抬起()
    {
        状态 = 采煤机状态.稳定速度; // 标记为停止减速
    }
    #endregion
    #region 左臂
    [Header("左臂")]
    public GameObject 左臂;
    float 采煤机左臂最大抬升高度 = 24;
    float 采煤机左臂最小抬升高度 = 0;
    private Quaternion zuoBiTargetRotation;
    public float zuoBispeed = 1f;
    public Button zuoSheng;
    public Button zuoTing;
    public Button zuoJiang;
    [SerializeField]
    private bool isZuoBiUP;
    [SerializeField]
    private bool isZuoBiDown;

    public void 采煤机左臂()
    {
        if (isZuoBiUP)
        {
            SetButtonColor(zuoSheng, selectColor);
            SetButtonColor(zuoTing, Color.white);
            SetButtonColor(zuoJiang, Color.white);
            // 绕X轴正向旋转，这里设置每秒旋转一定角度（示例为1度每秒，可以根据实际需求调整）
            zuoBiTargetRotation *= Quaternion.AngleAxis(zuoBispeed * Time.deltaTime, Vector3.right);
            左臂.transform.localRotation = zuoBiTargetRotation;
            // 获取当前旋转的Euler角度来判断是否超过限制角度（这里是24度）
            float currentAngleX = 左臂.transform.localEulerAngles.x;
            currentAngleX = (currentAngleX > 180) ? currentAngleX - 360 : currentAngleX;
            if (currentAngleX >= 采煤机左臂最大抬升高度)
            {
                // 如果超过了限制角度，将旋转恢复到刚好24度对应的Quaternion
                zuoBiTargetRotation = Quaternion.Euler(采煤机左臂最大抬升高度, 左臂.transform.localEulerAngles.y, 左臂.transform.localEulerAngles.z);
            }
        }

        if (isZuoBiDown)
        {
            SetButtonColor(zuoJiang, selectColor);
            SetButtonColor(zuoTing, Color.white);
            SetButtonColor(zuoSheng, Color.white);
            // 绕X轴反向旋转
            zuoBiTargetRotation *= Quaternion.AngleAxis(-zuoBispeed * Time.deltaTime, Vector3.right);
            左臂.transform.localRotation = zuoBiTargetRotation;
            float currentAngleX = 左臂.transform.localEulerAngles.x;
            currentAngleX = (currentAngleX > 180) ? currentAngleX - 360 : currentAngleX;
            if (currentAngleX <= 采煤机左臂最小抬升高度)
            {
                // 如果小于了下限角度（这里是0度），将旋转恢复到刚好0度对应的Quaternion
                zuoBiTargetRotation = Quaternion.Euler(采煤机左臂最小抬升高度, 左臂.transform.localEulerAngles.y, 左臂.transform.localEulerAngles.z);
            }
        }
        if (!isZuoBiUP && !isZuoBiDown)
        {
            SetButtonColor(zuoTing, selectColor);
            SetButtonColor(zuoJiang, Color.white);
            SetButtonColor(zuoSheng, Color.white);

        }
    }
    public void 采煤机左臂抬升()
    {
        isZuoBiUP = true;


    }
    public void 停止采煤机左臂抬升()
    {
        isZuoBiUP = false;

    }
    public void 采煤机左臂下降()
    {
        isZuoBiDown = true;
    }
    public void 停止采煤机左臂下降()
    {
        isZuoBiDown = false;
    }
    #endregion
    #region 右臂抬升
    [Header("右臂")]

    public GameObject 右臂;
    float 采煤机右臂最大抬升距离 = -24;
    float 采煤机右臂最小抬升距离 = 0;
    private Quaternion youBiTargetRotation;
    public float youBiSpeed = 2f;
    [SerializeField]
    private bool isYouBiUp;
    [SerializeField]
    private bool isYouBiDown;
    public Button youSheng;
    public Button youTing;
    public Button youJiang;


    public void 采煤机右臂()
    {
        if (isYouBiUp)
        {
            SetButtonColor(youSheng, selectColor);
            SetButtonColor(youTing, Color.white);
            SetButtonColor(youJiang, Color.white);
            youBiTargetRotation *= Quaternion.AngleAxis(-youBiSpeed * Time.deltaTime, Vector3.right);
            右臂.transform.localRotation = youBiTargetRotation;
            float LimitRotation = 右臂.transform.localEulerAngles.x;
            LimitRotation = LimitRotation > 180 ? LimitRotation - 360 : LimitRotation;
            if (LimitRotation <= 采煤机右臂最大抬升距离)
            {
                youBiTargetRotation = Quaternion.Euler(采煤机右臂最大抬升距离, 右臂.transform.localEulerAngles.y, 右臂.transform.localEulerAngles.z);
            }
        }
        if (isYouBiDown)
        {
            SetButtonColor(youJiang, selectColor);
            SetButtonColor(youTing, Color.white);
            SetButtonColor(youSheng, Color.white);
            youBiTargetRotation *= Quaternion.AngleAxis(youBiSpeed * Time.deltaTime, Vector3.right);
            右臂.transform.localRotation = youBiTargetRotation;
            float LimitRotation = 右臂.transform.localEulerAngles.x;
            LimitRotation = LimitRotation > 180 ? LimitRotation - 360 : LimitRotation;
            if (LimitRotation >= 采煤机右臂最小抬升距离)
            {
                youBiTargetRotation = Quaternion.Euler(采煤机右臂最小抬升距离, 右臂.transform.localEulerAngles.y, 右臂.transform.localEulerAngles.z);
            }
        }
        if (!isYouBiUp && !isYouBiDown)
        {
            SetButtonColor(youTing, selectColor);
            SetButtonColor(youJiang, Color.white);
            SetButtonColor(youSheng, Color.white);
        }
    }
    public void 采煤机右臂抬升()
    {
        isYouBiUp = true;
    }
    public void 停止采煤机右臂抬升()
    {
        isYouBiUp = false;
    }
    public void 采煤机右臂下降()
    {
        isYouBiDown = true;
    }
    public void 停止采煤机右臂下降()
    {
        isYouBiDown = false;
    }
    public void 停止采煤机双臂运动()
    {
        isYouBiUp = false;
        isZuoBiUP = false;
        isYouBiDown = false;
        isZuoBiDown = false;

    }
    #endregion
    #region
    [Header("滚筒")]
    public GameObject zuoGunTong;
    public GameObject youGunTong;
    public float GunTongSpeed;
    public Button zuoGunTongButton;
    public Button youGunTongButton;
    public Button gunTongTingZhuan;
    public bool isZuoGunTong;
    public bool isYouGunTong;
    private float currentZuoGunTongSpeed;
    private float currentYouGunTongSpeed;
    [SerializeField]
    private bool isZuoDecelerating;//旋转位
    [SerializeField]
    private bool isYouDecelerating;

    public Animator youMeiKuai;
    public GameObject youMeiKuaiParent;
    public void 滚筒启动()
    {
        if (isZuoGunTong && !isZuoDecelerating)
        {
            SetButtonColor(zuoGunTongButton, selectColor);
            zuoGunTong.transform.Rotate(new Vector3(-GunTongSpeed * Time.deltaTime, 0, 0));
        }
        if (isYouGunTong && !isYouDecelerating)
        {
            SetButtonColor(youGunTongButton, selectColor);
            youMeiKuai.SetBool("YouMeiKuai", true);
            youMeiKuaiParent.SetActive(true);
            youGunTong.transform.Rotate(new Vector3(GunTongSpeed * Time.deltaTime, 0, 0));
        }

    }
    public void 左滚筒()
    {
        isZuoGunTong = true;
        isZuoDecelerating = false;
        currentZuoGunTongSpeed = GunTongSpeed;
    }
    public void 右滚筒()
    {
        isYouGunTong = true;
        isYouDecelerating = false;
        currentYouGunTongSpeed = GunTongSpeed;
    }
    public void 滚筒关闭()
    {
        if (isZuoGunTong)
        {
            isZuoDecelerating = true;
            DOTween.To(() => currentZuoGunTongSpeed, x => currentZuoGunTongSpeed = x, 0, 2f)
              .SetEase(Ease.InOutElastic)
              .OnUpdate(() =>
              {
                  zuoGunTong.transform.Rotate(new Vector3(-currentZuoGunTongSpeed * Time.deltaTime, 0, 0));
              })
              .OnComplete(() =>
              {
                  isZuoGunTong = false;
                  currentZuoGunTongSpeed = 0;
              });
        }
        if (isYouGunTong)
        {
            youMeiKuai.SetBool("YouMeiKuai", false);
            youMeiKuaiParent.SetActive(false);

            isYouDecelerating = true;
            DOTween.To(() => currentYouGunTongSpeed, x => currentYouGunTongSpeed = x, 0, 2f)
              .SetEase(Ease.InOutElastic)
              .OnUpdate(() =>
              {
                  youGunTong.transform.Rotate(new Vector3(currentYouGunTongSpeed * Time.deltaTime, 0, 0));
              })
              .OnComplete(() =>
              {
                  isYouGunTong = false;
                  currentYouGunTongSpeed = 0;
              });
        }
        SetButtonColor(youGunTongButton, Color.white);
        SetButtonColor(zuoGunTongButton, Color.white);

    }

    public void 左牵预设()
    {

        左牵();
        当前速度 = 0.005f;
        采煤机起步速度 = 0.005f;
        isMove = true;
        // 设置采煤机状态为左牵
        状态 = 采煤机状态.稳定速度;

        // 设置左臂最大抬升，右臂最小抬升
        采煤机左臂抬升();
        采煤机右臂下降();


        // 启动左右滚筒
        左滚筒();
        右滚筒();
    }
    public void 右牵预设()
    {
        右牵();
        当前速度 = 0.005f;
        采煤机起步速度 = 0.005f;
        isMove = true;
        状态 = 采煤机状态.稳定速度;
        采煤机右臂抬升();
        采煤机左臂下降();
        左滚筒();
        右滚筒();

    }
    public void 停止所有动作()
    {
        牵停();
        停止采煤机双臂运动();
        滚筒关闭();
    }
    #endregion
    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}
