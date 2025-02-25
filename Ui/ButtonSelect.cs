using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class ButtonSelect : MonoBehaviour
{

    public Button[] buttons;
    public GameObject[] cineCamera;
    [Header("�����˳ƽ�ɫ")]
    public GameObject playerArmature;
    //
    [Header("����ui����")]
    public RectTransform cMJOtherPanel;
    public RectTransform caiMeiJiLeftPanel;
    public RectTransform initLeftPanel;
    public RectTransform initRightPanel;
    //
    
    private Vector2 panelLeftPos;
    private Vector2 panelRightPos;
    private Vector2 panelUpPos;
    //
    [Header("��ú���۲����������ť")]
    public Button caiMeiJiFKButton;
    public Button caiMeiJiFKReturnButton;
    public Button zuoQianButton;
    public Button youQianButton;

    void Start()
    {
        panelUpPos = new Vector2(7, 350);
        panelLeftPos = new Vector2(-760, -40);//����������λ��
        panelRightPos = new Vector2(760, -40);//����������λ��
        //��ʼ�����λ��
        InitPanel(caiMeiJiLeftPanel, true, false);
        InitPanel(initLeftPanel, true, false);
        InitPanel(initRightPanel, false, false);
        InitPanel(cMJOtherPanel, false, true);
        //Ĭ���ǿ�ɽ������ť�ѵ��
        PanelOpenAndClose(initLeftPanel, true, true);
        PanelOpenAndClose(initRightPanel, true, false);
        OnClickButton_Action(buttons[0]);
        CameraControl(cineCamera[0]);

        //
        caiMeiJiFKButton.onClick.AddListener(() => { CameraControl(cineCamera[2]);  });
        caiMeiJiFKReturnButton.onClick.AddListener(() => { CameraControl(cineCamera[1]);PanelOpenAndClose(caiMeiJiLeftPanel, true, true); });
        zuoQianButton.onClick.AddListener(() => { CameraControl(cineCamera[2]);  });
        youQianButton.onClick.AddListener(() => { CameraControl(cineCamera[2]); });
        //
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener
            (() =>
            {
                OnClickButton_Action(buttons[index]);

            });
        }
        //������ť���ƵĽ��棬�ر���������
        foreach (var item in buttons)
        {
            if (item.name == buttons[0].name)
            {
                item.onClick.AddListener(() => PanelOpenAndClose(initLeftPanel, true, true));
                item.onClick.AddListener(() => CameraControl(cineCamera[0]));
                item.onClick.AddListener(() => PanelOpenAndClose(initRightPanel, true, false));
            }
            else
            {
                item.onClick.AddListener(() => PanelOpenAndClose(initLeftPanel, false, true));
                item.onClick.AddListener(() => PanelOpenAndClose(initRightPanel, false, false));

            }
            if (item.name == buttons[1].name)
            {
                item.onClick.AddListener(() => PanelOpenAndClose(caiMeiJiLeftPanel, true, true));
                item.onClick.AddListener(() => CameraControl(cineCamera[1]));
                item.onClick.AddListener(() => UpPanelOpenAndClose(cMJOtherPanel,true));

            }
            else
            {
                item.onClick.AddListener(() => PanelOpenAndClose(caiMeiJiLeftPanel, false,true));
                item.onClick.AddListener(() => UpPanelOpenAndClose(cMJOtherPanel, false));

            }
            if (item.name == buttons[2].name)
            {
                item.onClick.AddListener(() => CameraControl(cineCamera[3]));
                item.onClick.AddListener(() => { playerArmature.gameObject.SetActive(true); });
            }
            else
            {
                item.onClick.AddListener(() => { playerArmature.gameObject.SetActive(false); });

            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClickButton_Action(Button clickedButton)//button���ʱ�Ķ���������button��Ӧ
    {
        //buttonȡ��ѡ�ж���
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.5f);

        }
        //
        clickedButton.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.5f);
        clickedButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(clickedButton.transform.localPosition.x, 50), 1f).SetEase(Ease.OutCirc);
        foreach (var item in buttons)
        {
            if (item.name != clickedButton.name)
            {
                item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(item.transform.localPosition.x, -7), 1f).SetEase(Ease.OutCirc);

            }

        }


    }
    void InitPanel(RectTransform panel,bool isleft,bool isUp)
    {
        if (isleft)
        {
            
        panel.transform.localPosition = new Vector3(-1190, -40, 0);
        }
        else 
        {
            panel.transform.localPosition = new Vector3(1190, -40, 0);

        }
        if (isUp)
        {
            panel.transform.localPosition = new Vector3(-50, 800, 0);

        }
        
    }
    public void PanelOpenAndClose(RectTransform panel, bool ison, bool isleft)//�����͹ر�panel
    {
        if (isleft)
        {

            if (ison)
            {
                panel.DOAnchorPos(panelLeftPos, 1f).SetEase(Ease.InOutCirc);

            }
            else
            {
                panel.DOAnchorPos(new Vector2(panelLeftPos.x - 430f, panelLeftPos.y), 1f).SetEase(Ease.InOutCirc);
            }
        }
        else
        {
            if (ison)
            {
                panel.DOAnchorPos(panelRightPos,1f).SetEase(Ease.InOutCirc);
            }
            else
            {
                panel.DOAnchorPos(new Vector2(panelRightPos.x + 430f, panelLeftPos.y), 1f).SetEase(Ease.InOutCirc);

            }
        }
    }
    public void UpPanelOpenAndClose(RectTransform panel, bool ison)
    {
        if (ison)
        {
            panel.DOAnchorPos(panelUpPos, 1f).SetEase(Ease.InOutCirc);
        }
        else
        {
            panel.DOAnchorPos(new Vector2(panelUpPos.x , panelUpPos.y+600), 1f).SetEase(Ease.InOutCirc);
        }
    }
    public void CameraControl(GameObject camera)
    {
        foreach (var item in cineCamera)
        {

            camera.gameObject.SetActive(true);
            if (item != camera)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}

