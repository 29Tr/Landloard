using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CardUI:MonoBehaviour
{
    Card card;
    Image image;
    bool isSelected;
    LearnButton btn;

    public Card Card
    {
        get
        {
            return card;
        }
        set
        {
            card = value;
            SetImage();
        }
    }
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            if (card.BelongTo != CharacterType.Player || isSelected == value)
                return;
            if (value)
                transform.localPosition += Vector3.up * 10;
            else
                transform.localPosition -= Vector3.up * 10;
            isSelected = value;
        }
    }
    //设置图片
    void SetImage()
    {
        if (card.BelongTo == CharacterType.Player || card.BelongTo == CharacterType.Desk)
        {
            image.sprite = Resources.Load<Sprite>("Pokers/" + card.CardName);
        }
        else { image.sprite = Resources.Load<Sprite>("Pokers/FixedBack"); }//computer 显示背面
    }
    //第一次地主牌
    public void SetImageAgain()
    {
        image.sprite = Resources.Load<Sprite>("Pokers/CardBack");
    }
    ///设置位置以及偏移
    ///
    public void SetPosition(Transform parent,int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);
        if (card.BelongTo == CharacterType.Player || card.BelongTo == CharacterType.Desk)
        {
            transform.localPosition = Vector3.right * index * 25f;

            //防止还原
            if (IsSelected)
                transform.localPosition += Vector3.up * 10;
        }
        else if (card.BelongTo == CharacterType.ComputerLeft || card.BelongTo == CharacterType.ComputerRight)
            transform.localPosition = -Vector3.up * 8 * index + Vector3.left * 8 * index;
    }

    //初始化数据
    public void OnSpawn()
    {
        image = GetComponent<Image>();
        btn = GetComponent<LearnButton>();
        btn.Highlight_btn += Btn_HighlightedBtn;
        btn.Pressed_btn += Btn_PessedBtn;
    }
    /// <summary>
    /// 回收数据
    /// </summary>
    public void OnDespawn()
    {
        btn.Highlight_btn -= Btn_HighlightedBtn;
        btn.Pressed_btn -= Btn_PessedBtn;
        isSelected = false;
        image.sprite = null;
        card = null;
    }
    public void Btn_HighlightedBtn()
    {
        if (Input.GetMouseButton(1))
        {
            IsSelected = !IsSelected;
        }
    }
    public void Btn_PessedBtn()
    {
        if (card.BelongTo == CharacterType.Player)
        {
            IsSelected = !IsSelected;
        }
    }
    /// <summary>
    /// 回收
    /// </summary>
    public void Destroy()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}