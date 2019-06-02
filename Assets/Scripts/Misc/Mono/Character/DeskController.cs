using Lean.Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeskController:CharacterBase
{
    public DeskUI deskUI;
    /// <summary>
    /// player computer手牌
    /// </summary>
    List<Card> playerCardList = new List<Card>();
    public List<Card> PlayerCardList
    {
        get
        {
            return playerCardList;
        }
    }
    List<Card> computerLeftCardList = new List<Card>();
    public List<Card> ComputerLeftCardList
    {
        get
        {
            return computerLeftCardList;
        }
    }
    List<Card> computerRightCardList = new List<Card>();
    public List<Card> ComputerRightCardList
    {
        get
        {
            return computerRightCardList;
        }
    }
    
    /// <summary>
    /// player computer 生成位置
    /// </summary>
    Transform playerPoint;
    public Transform PlayerPoint
    {
        get
        {
            if (playerPoint == null) playerPoint = transform.Find("PlayerPoint"); return playerPoint;
        }
    }

    Transform computerLeftPoint;
    public Transform ComputerLeftPoint
    {
        get
        {
            if (computerLeftPoint == null)
                computerLeftPoint = transform.Find("ComputerLeftPoint");
            return computerLeftPoint;
        }
    }
    Transform computerRightPoint;
    public Transform ComputerRightPoint
    {
        get
        {
            if (computerRightPoint == null)
                computerRightPoint = transform.Find("ComputerRightPoint");
            return computerRightPoint;
        }
    }
    public void SetShowCard(Card card,int index)
    {
        deskUI.SetShowCard(card, index);
    }
    private void CreateCardUI(Card card, int index, bool selected,ShowPoint pos)
    {
        //对象池生成
        GameObject g = LeanPool.Spawn(prefab);
        g.name = characterType.ToString() + index.ToString();
        //设置位置和是否选中
        CardUI cardUI = g.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelected = selected;
        switch (pos)
        {
            case ShowPoint.Desk:
                cardUI.SetPosition(CreatePoint, index);
                break;
            case ShowPoint.Player:
                cardUI.SetPosition(PlayerPoint, index);
                break;
            case ShowPoint.ComputerRight:
                cardUI.SetPosition(ComputerRightPoint, index);
                break;
            case ShowPoint.ComputerLeft:
                cardUI.SetPosition(ComputerLeftPoint, index);
                break;
            default:
                break;
        }
    }


    public virtual void AddCard(Card card, bool selected,ShowPoint pos)
    {
        switch (pos)
        {
            case ShowPoint.Desk:
                Cards.Add(card);
                //***//
                card.BelongTo = characterType;
                CreateCardUI(card, Cards.Count - 1, selected, pos);
                break;
            case ShowPoint.Player:
                PlayerCardList.Add(card);
                //***//
                card.BelongTo = characterType;
                CreateCardUI(card, PlayerCardList.Count - 1, selected, pos);
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCardList.Add(card);
                //***//
                card.BelongTo = characterType;
                CreateCardUI(card, ComputerRightCardList.Count - 1, selected, pos);
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCardList.Add(card);
                //***//
                card.BelongTo = characterType;
                CreateCardUI(card, ComputerLeftCardList.Count - 1, selected, pos);
                break;
            default:
                break;
        }        
    }
    /// <summary>
    /// 桌面清空
    /// </summary>
    /// <param name="pos"></param>
    public void Clear(ShowPoint pos)
    {
        switch (pos)
        {
            case ShowPoint.Desk:
                Cards.Clear();
                CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIs.Length; i++)
                    cardUIs[i].Destroy();
                break;
            case ShowPoint.Player:
                PlayerCardList.Clear();
                CardUI[] cardUIPlayer = PlayerPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIPlayer.Length; i++)
                    cardUIPlayer[i].Destroy();
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCardList.Clear();
                CardUI[] cardUIRight= ComputerRightPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIRight.Length; i++)
                    cardUIRight[i].Destroy();
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCardList.Clear();
                CardUI[] cardUILeft = ComputerLeftPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUILeft.Length; i++)
                    cardUILeft[i].Destroy();
                break;
            default:
                break;
        }
    }

}