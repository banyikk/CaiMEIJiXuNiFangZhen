using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ��ú��״̬
{
    ֹͣ,
    ��������,
    ��������,
    �ȶ��ٶ�,

}

public class CaiMeiJi : MonoBehaviour
{
    private ��ú��״̬ ״̬;
    public GameObject _��ú��;

    [Header("����ǣ��ǣͣ")]
    [SerializeField]
    private bool �Ƿ���ǣ = false;
    [SerializeField]
    private bool �Ƿ���ǣ = false;
    public Button zuoQian;
    public Button youQian;
    public Button qianTing;
    public Color selectColor;
    [SerializeField]
    private bool isMove = false;

    public Button jiaSu;
    public Button jianSu;
    public Button yunSu;
    public float ��ú�����ٶ� = 0.001f;
    public float ��ú������ٶ� = 0.1f;
    public float ���� = 0.1f;
    public float ��ǰ�ٶ�;
    Vector3 FangXiang;
    [Header("Ԥ�裺��ǣ")]
    public Button caimeijiTurnleft;
    public Button caimeijiTurnright;
    public Button allBuffFuck;

    void Start()
    {
        ״̬ = ��ú��״̬.ֹͣ;
        zuoBiTargetRotation = ���.transform.localRotation;
        youBiTargetRotation = �ұ�.transform.localRotation;
        caimeijiTurnleft.onClick.AddListener(() => {
            ֹͣ���ж���();
            Invoke("��ǣԤ��", 3f);
             });
        caimeijiTurnright.onClick.AddListener(() =>
        {
            ֹͣ���ж���();
            Invoke("��ǣԤ��", 3f);
            
        });
        allBuffFuck.onClick.AddListener(ֹͣ���ж���);
    }

    void Update()
    {
        ��ú���ƶ�();
        ��ú�����();
        ��ú���ұ�();
        ��Ͳ����();
    }
    #region 
    public void ��ú���ƶ�()
    {

        if (isMove)
        {
            if (״̬ == ��ú��״̬.��������)
            {
                SetButtonColor(yunSu, Color.white);
                SetButtonColor(jianSu, Color.white);
                SetButtonColor(jiaSu, selectColor);
                // �𲽼��٣�ȷ���ٶȲ���������ٶ�
                ��ú�����ٶ� += ���� * Time.deltaTime;
                ��ú�����ٶ� = Mathf.Min(��ú�����ٶ�, ��ú������ٶ�);
                _��ú��.transform.Translate(FangXiang * ��ú�����ٶ�);
                ��ǰ�ٶ� = ��ú�����ٶ�;
            }
            if (״̬ == ��ú��״̬.��������)
            {
                SetButtonColor(jiaSu, Color.white);
                SetButtonColor(yunSu, Color.white);
                SetButtonColor(jianSu, selectColor);
                ��ú�����ٶ� -= ���� * Time.deltaTime;
                ��ú�����ٶ� = Mathf.Max(��ú�����ٶ�, 0);
                _��ú��.transform.Translate(FangXiang * ��ú�����ٶ�);
                ��ǰ�ٶ� = ��ú�����ٶ�;
            }
            if (״̬ == ��ú��״̬.�ȶ��ٶ�)
            {
                SetButtonColor(jiaSu, Color.white);
                SetButtonColor(jianSu, Color.white);
                SetButtonColor(yunSu, selectColor);
                _��ú��.transform.Translate(FangXiang * ��ǰ�ٶ�);

            }
        }

        if (��ú�����ٶ� == 0)
        {
            isMove = false; // ֹͣ�ƶ�
            ״̬ = ��ú��״̬.ֹͣ;
        }
        // ���Ʋ�ú�����ƶ���Χ
        if (_��ú��.transform.position.z <= -33f || _��ú��.transform.position.z >= 30f || ״̬ == ��ú��״̬.ֹͣ)
        {
            ��ú�����ٶ� = 0;
            ��ǰ�ٶ� = 0;

        }
        if (״̬ == ��ú��״̬.ֹͣ)
        {
            SetButtonColor(jiaSu, Color.white);
            SetButtonColor(jianSu, Color.white);
            SetButtonColor(yunSu, Color.white);
            ��ú�����ٶ� = 0;
            ��ǰ�ٶ� = 0;
        }
    }

    public void ��ǣ()
    {
        if (isMove == false)
        {
            SetButtonColor(zuoQian, selectColor);
            SetButtonColor(youQian, Color.white);
            SetButtonColor(qianTing, Color.white);

            �Ƿ���ǣ = true;
            �Ƿ���ǣ = false;
            ��ú�����ٶ� = 0;
            ��ǰ�ٶ� = 0;
            FangXiang = _��ú��.transform.forward;
        }
    }
    public void ǣͣ()
    {

        DOTween.To(() => ��ǰ�ٶ�, x =>
        {
            ��ǰ�ٶ� = x;
            ��ú�����ٶ� = x; // ͬ�����²�ú�����ٶ�
            _��ú��.transform.Translate(FangXiang * ��ǰ�ٶ� * Time.deltaTime); // ʹ��Time.deltaTimeȷ���ƶ�ƽ��
            if (��ǰ�ٶ� <= 0.001f)
            {
                ��ǰ�ٶ� = 0;
                ��ú�����ٶ� = 0;
                isMove = false;
                ״̬ = ��ú��״̬.ֹͣ;
            }
        }, 0, 3f);
        SetButtonColor(qianTing, selectColor);
        SetButtonColor(zuoQian, Color.white);
        SetButtonColor(youQian, Color.white);
        �Ƿ���ǣ = false;
        �Ƿ���ǣ = false;

    }
    public void ��ǣ()
    {
        if (isMove == false)
        {
            SetButtonColor(youQian, selectColor);
            SetButtonColor(qianTing, Color.white);
            SetButtonColor(zuoQian, Color.white);
            �Ƿ���ǣ = true;
            �Ƿ���ǣ = false;
            ��ú�����ٶ� = 0;
            ��ǰ�ٶ� = 0;
            FangXiang = -_��ú��.transform.forward;
        }
    }

    public void ���ٳ���()
    {
        if (�Ƿ���ǣ || �Ƿ���ǣ)
        {
            isMove = true;
            ״̬ = ��ú��״̬.��������;
        }

    }

    public void ����̧��()
    {

        ״̬ = ��ú��״̬.�ȶ��ٶ�; // ���Ϊֹͣ����
    }

    public void ���ٳ���()
    {
        ״̬ = ��ú��״̬.��������; // ���Ϊ��������
    }

    public void ����̧��()
    {
        ״̬ = ��ú��״̬.�ȶ��ٶ�; // ���Ϊֹͣ����
    }
    #endregion
    #region ���
    [Header("���")]
    public GameObject ���;
    float ��ú��������̧���߶� = 24;
    float ��ú�������С̧���߶� = 0;
    private Quaternion zuoBiTargetRotation;
    public float zuoBispeed = 1f;
    public Button zuoSheng;
    public Button zuoTing;
    public Button zuoJiang;
    [SerializeField]
    private bool isZuoBiUP;
    [SerializeField]
    private bool isZuoBiDown;

    public void ��ú�����()
    {
        if (isZuoBiUP)
        {
            SetButtonColor(zuoSheng, selectColor);
            SetButtonColor(zuoTing, Color.white);
            SetButtonColor(zuoJiang, Color.white);
            // ��X��������ת����������ÿ����תһ���Ƕȣ�ʾ��Ϊ1��ÿ�룬���Ը���ʵ�����������
            zuoBiTargetRotation *= Quaternion.AngleAxis(zuoBispeed * Time.deltaTime, Vector3.right);
            ���.transform.localRotation = zuoBiTargetRotation;
            // ��ȡ��ǰ��ת��Euler�Ƕ����ж��Ƿ񳬹����ƽǶȣ�������24�ȣ�
            float currentAngleX = ���.transform.localEulerAngles.x;
            currentAngleX = (currentAngleX > 180) ? currentAngleX - 360 : currentAngleX;
            if (currentAngleX >= ��ú��������̧���߶�)
            {
                // ������������ƽǶȣ�����ת�ָ����պ�24�ȶ�Ӧ��Quaternion
                zuoBiTargetRotation = Quaternion.Euler(��ú��������̧���߶�, ���.transform.localEulerAngles.y, ���.transform.localEulerAngles.z);
            }
        }

        if (isZuoBiDown)
        {
            SetButtonColor(zuoJiang, selectColor);
            SetButtonColor(zuoTing, Color.white);
            SetButtonColor(zuoSheng, Color.white);
            // ��X�ᷴ����ת
            zuoBiTargetRotation *= Quaternion.AngleAxis(-zuoBispeed * Time.deltaTime, Vector3.right);
            ���.transform.localRotation = zuoBiTargetRotation;
            float currentAngleX = ���.transform.localEulerAngles.x;
            currentAngleX = (currentAngleX > 180) ? currentAngleX - 360 : currentAngleX;
            if (currentAngleX <= ��ú�������С̧���߶�)
            {
                // ���С�������޽Ƕȣ�������0�ȣ�������ת�ָ����պ�0�ȶ�Ӧ��Quaternion
                zuoBiTargetRotation = Quaternion.Euler(��ú�������С̧���߶�, ���.transform.localEulerAngles.y, ���.transform.localEulerAngles.z);
            }
        }
        if (!isZuoBiUP && !isZuoBiDown)
        {
            SetButtonColor(zuoTing, selectColor);
            SetButtonColor(zuoJiang, Color.white);
            SetButtonColor(zuoSheng, Color.white);

        }
    }
    public void ��ú�����̧��()
    {
        isZuoBiUP = true;


    }
    public void ֹͣ��ú�����̧��()
    {
        isZuoBiUP = false;

    }
    public void ��ú������½�()
    {
        isZuoBiDown = true;
    }
    public void ֹͣ��ú������½�()
    {
        isZuoBiDown = false;
    }
    #endregion
    #region �ұ�̧��
    [Header("�ұ�")]

    public GameObject �ұ�;
    float ��ú���ұ����̧������ = -24;
    float ��ú���ұ���С̧������ = 0;
    private Quaternion youBiTargetRotation;
    public float youBiSpeed = 2f;
    [SerializeField]
    private bool isYouBiUp;
    [SerializeField]
    private bool isYouBiDown;
    public Button youSheng;
    public Button youTing;
    public Button youJiang;


    public void ��ú���ұ�()
    {
        if (isYouBiUp)
        {
            SetButtonColor(youSheng, selectColor);
            SetButtonColor(youTing, Color.white);
            SetButtonColor(youJiang, Color.white);
            youBiTargetRotation *= Quaternion.AngleAxis(-youBiSpeed * Time.deltaTime, Vector3.right);
            �ұ�.transform.localRotation = youBiTargetRotation;
            float LimitRotation = �ұ�.transform.localEulerAngles.x;
            LimitRotation = LimitRotation > 180 ? LimitRotation - 360 : LimitRotation;
            if (LimitRotation <= ��ú���ұ����̧������)
            {
                youBiTargetRotation = Quaternion.Euler(��ú���ұ����̧������, �ұ�.transform.localEulerAngles.y, �ұ�.transform.localEulerAngles.z);
            }
        }
        if (isYouBiDown)
        {
            SetButtonColor(youJiang, selectColor);
            SetButtonColor(youTing, Color.white);
            SetButtonColor(youSheng, Color.white);
            youBiTargetRotation *= Quaternion.AngleAxis(youBiSpeed * Time.deltaTime, Vector3.right);
            �ұ�.transform.localRotation = youBiTargetRotation;
            float LimitRotation = �ұ�.transform.localEulerAngles.x;
            LimitRotation = LimitRotation > 180 ? LimitRotation - 360 : LimitRotation;
            if (LimitRotation >= ��ú���ұ���С̧������)
            {
                youBiTargetRotation = Quaternion.Euler(��ú���ұ���С̧������, �ұ�.transform.localEulerAngles.y, �ұ�.transform.localEulerAngles.z);
            }
        }
        if (!isYouBiUp && !isYouBiDown)
        {
            SetButtonColor(youTing, selectColor);
            SetButtonColor(youJiang, Color.white);
            SetButtonColor(youSheng, Color.white);
        }
    }
    public void ��ú���ұ�̧��()
    {
        isYouBiUp = true;
    }
    public void ֹͣ��ú���ұ�̧��()
    {
        isYouBiUp = false;
    }
    public void ��ú���ұ��½�()
    {
        isYouBiDown = true;
    }
    public void ֹͣ��ú���ұ��½�()
    {
        isYouBiDown = false;
    }
    public void ֹͣ��ú��˫���˶�()
    {
        isYouBiUp = false;
        isZuoBiUP = false;
        isYouBiDown = false;
        isZuoBiDown = false;

    }
    #endregion
    #region
    [Header("��Ͳ")]
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
    private bool isZuoDecelerating;//��תλ
    [SerializeField]
    private bool isYouDecelerating;

    public Animator youMeiKuai;
    public GameObject youMeiKuaiParent;
    public void ��Ͳ����()
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
    public void ���Ͳ()
    {
        isZuoGunTong = true;
        isZuoDecelerating = false;
        currentZuoGunTongSpeed = GunTongSpeed;
    }
    public void �ҹ�Ͳ()
    {
        isYouGunTong = true;
        isYouDecelerating = false;
        currentYouGunTongSpeed = GunTongSpeed;
    }
    public void ��Ͳ�ر�()
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

    public void ��ǣԤ��()
    {

        ��ǣ();
        ��ǰ�ٶ� = 0.005f;
        ��ú�����ٶ� = 0.005f;
        isMove = true;
        // ���ò�ú��״̬Ϊ��ǣ
        ״̬ = ��ú��״̬.�ȶ��ٶ�;

        // ����������̧�����ұ���С̧��
        ��ú�����̧��();
        ��ú���ұ��½�();


        // �������ҹ�Ͳ
        ���Ͳ();
        �ҹ�Ͳ();
    }
    public void ��ǣԤ��()
    {
        ��ǣ();
        ��ǰ�ٶ� = 0.005f;
        ��ú�����ٶ� = 0.005f;
        isMove = true;
        ״̬ = ��ú��״̬.�ȶ��ٶ�;
        ��ú���ұ�̧��();
        ��ú������½�();
        ���Ͳ();
        �ҹ�Ͳ();

    }
    public void ֹͣ���ж���()
    {
        ǣͣ();
        ֹͣ��ú��˫���˶�();
        ��Ͳ�ر�();
    }
    #endregion
    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}
