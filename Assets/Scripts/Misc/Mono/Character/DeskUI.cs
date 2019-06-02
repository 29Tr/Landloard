using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskUI : MonoBehaviour {

    Transform showPoint;

    public Transform ShowPoint
    {
        get
        {
            if (showPoint == null)
                showPoint = transform.Find("ShowPoint");
            return showPoint;
        }
    }
    public CanvasGroup ShowGroup { get { return showPoint.GetComponent<CanvasGroup>(); } }
    /// <summary>
    /// 显示地主牌
    /// </summary>
    /// <param name="card">显示卡牌信息</param>
    /// <param name="index">索引</param>
    public void SetShowCard(Card card,int index)
    {
        Image[] showcards = ShowPoint.GetComponentsInChildren<Image>();
        showcards[index].sprite = Resources.Load<Sprite>("Pokers/" + card.CardName);
        SetAlpha(1);
    }
    void SetAlpha(int i)
    {
        ShowGroup.alpha = i;
    }
}
