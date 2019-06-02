using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ComputerController:CharacterBase
{
    public CharacterUI characterUI;

   public  CanvasGroup group;


    public ComputerAI computerAI;
    Identity identity;
    /// <summary>
    /// 角色身份
    /// </summary>
    public Identity Identity
    {
        get
        {
            return identity;
        }

        set
        {
            identity = value;
            characterUI.SetIdentity(value);
        }
    }
    /// <summary>
    /// 当前要出的牌
    /// </summary>
    public List<Card> SelectCards { get { return computerAI.selectCards; } }
    /// <summary>
    /// 当前出牌类型
    /// </summary>
    public CardType CurrentType { get { return computerAI.currType; } }
    public override void AddCard(Card card, bool selected)
    {
        base.AddCard(card, selected);
        characterUI.SetRemain(CardCount);
    }


    public override Card DealCard()
    {
        Card card= base.DealCard();
        characterUI.SetRemain(CardCount);
        return card;
    }

    /// <summary>
    /// 电脑出牌
    /// </summary>
    /// <param name="cardType">手牌</param>
    /// <param name="weigth">出牌大小</param>
    /// <param name="length">出牌长度</param>
    /// <param name="isBiggest">是否最大</param>
    /// <returns></returns>
    public bool SmartSelectCards(CardType cardType,int weigth,int length,bool isBiggest)
    {
        computerAI.SmartSelectCards(Cards, cardType, weigth, length, isBiggest);
        if (SelectCards.Count != 0)
        {
            DestroyCards();
            return true;
        }
        else
        {
            ComputerPass();
            return false;
        }
    }

    private void DestroyCards()
    {
        //删数据
        //删UI
        CardUI[] cardsUi = CreatePoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cardsUi.Length; i++)
        {
            for (int j = 0; j < SelectCards.Count; j++)
            {
                //是否出牌与UI一样
                if (SelectCards[j] == cardsUi[i].Card)
                {
                    cardsUi[i].Destroy();
                    Cards.Remove(SelectCards[j]);
                }
            }
        }
        //Ui排序
        SortCardUI(Cards);
        characterUI.SetRemain(CardCount);
    }

    /// <summary>
    /// 显示UIPass
    /// </summary>
    public void ComputerPass()
    {
        group.alpha = 1;
        StartCoroutine(Pass());
    }
    IEnumerator Pass()
    {
        yield return new WaitForSeconds(1.5f);
        group.alpha = 0;
    }
}