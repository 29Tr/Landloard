using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lean.Pool;
public class CharacterBase:MonoBehaviour
{
    public CharacterType characterType;
    Transform createPoint;
    List<Card> cards = new List<Card>();
   public GameObject prefab;
    
    /// <summary>
    /// 手牌
    /// </summary>
    public List<Card> Cards
    {
        get
        {
            return cards;
        }
    }
    /// <summary>
    /// 是否有牌
    /// </summary>
    public bool HasCard { get { return cards.Count != 0; } }
    //当前牌数
    public int CardCount { get { return cards.Count; } }

    public Transform CreatePoint
    {
        get
        {
            if (createPoint == null) createPoint = transform.Find("CreatePoint");
            return createPoint;
        }
    }
    /// <summary>
    /// 添加牌
    /// </summary>
    /// <param name="card">添加地主牌</param>
    /// <param name="selected">是否增高</param>
    public virtual void AddCard(Card card,bool selected)
    {
        Cards.Add(card);
        //***//
        card.BelongTo = characterType;
        CreateCardUI(card, cards.Count - 1, selected);

    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="asc"></param>
    public void  Sort(bool asc)
    {
        //数据
        Tools.Sort(cards, asc);
        //UI
        SortCardUI(cards);
    }
    /// <summary>
    /// 排序cardUI
    /// </summary>
    /// <param name="cards">传入有序list</param>
    public void SortCardUI(List<Card> cards)
    {
        CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cardUIs.Length; j++)
            {
                if(cardUIs[j].Card==cards[i])
                cardUIs[j].SetPosition(CreatePoint, i);
            }
        }
    }
    /// <summary>
    /// 根据数据创建CardUI
    /// </summary>
    /// <param name="card">数据</param>
    /// <param name="index">索引</param>
    /// <param name="selected">上升？</param>
    private void CreateCardUI(Card card, int index, bool selected)
    {
        //对象池生成
        GameObject g = LeanPool.Spawn(prefab);
        g.name = characterType.ToString() + index.ToString();
        //设置位置和是否选中
        CardUI cardUI = g.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelected = selected;
        cardUI.SetPosition(CreatePoint, index);
    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <returns></returns>
    public virtual Card DealCard()
    {
        Card card = cards[CardCount - 1];
        cards.Remove(card);
        return card;
    }
}