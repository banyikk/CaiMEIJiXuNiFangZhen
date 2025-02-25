using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectOutline : MonoBehaviour
{
    
    public GameObject[] model;
    public Button restarButton;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    
    private GameObject selectGamobject;
    private GameObject currentGamobject;

    // Start is called before the first frame update
    void Start()
    {
        initialPositions=new Vector3[model.Length];
        initialRotations=new Quaternion[model.Length];
        for (int i = 0; i < model.Length; i++)
        {
            initialPositions[i] = model[i].transform.position;
            initialRotations[i] = model[i].transform.rotation;
        }
        restarButton.onClick.AddListener(ReTurnPosition);
    }
    
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Physics.Raycast(ray,out hit))
            {
                
                selectGamobject = hit.transform.gameObject;
                if (selectGamobject!=currentGamobject)
                {
                    if (currentGamobject!=null)
                    {
                       
                        Outline lastOutline = currentGamobject.GetComponent<Outline>();
                        if (lastOutline!=null)
                        {
                            
                        Destroy(lastOutline);
                        }
                    }
                }
                currentGamobject=selectGamobject;
                OutLine();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (selectGamobject==null)
            {
                return;
            }
            Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 mouseWorldPosition=Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            selectGamobject.transform.position = mouseWorldPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectGamobject!=null)
            {
                Outline outline = selectGamobject.GetComponent<Outline>();
                Destroy(outline);
            }
            selectGamobject =null;
            
        }

    }
    void OutLine()
    {
        if (selectGamobject!=null)
        {
            Outline outLine=selectGamobject.GetComponent<Outline>();
            if (outLine == null)
            {
                outLine = selectGamobject.AddComponent<Outline>();
                
            }
            outLine.OutlineMode= Outline.Mode.OutlineAll;
            outLine.OutlineWidth = 10;
            outLine.OutlineColor = new Color(0, 0, 0, 0);
            DOTween.To(() => outLine.OutlineColor, x => outLine.OutlineColor = x, new Color(1, 1, 1, 1),1f).SetLoops(-1, LoopType.Yoyo);
        }
    }
    void ReTurnPosition()
    {
        for (int i = 0; i < model.Length; i++)
        {
            model[i].transform.position=initialPositions[i];
            model[i].transform.rotation=initialRotations[i];
        }
    }
}
