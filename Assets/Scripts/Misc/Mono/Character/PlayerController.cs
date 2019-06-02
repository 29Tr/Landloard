using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerController:CharacterBase
{
    public CharacterUI characterUI;
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
    public override void AddCard(Card card, bool selected)
    {
        base.AddCard(card, selected);
        characterUI.SetRemain(CardCount);
    }

    public override Card DealCard()
    {
        Card card = base.DealCard();
        characterUI.SetRemain(CardCount);
        return card;
    }
    List<Card> tempCard = null;
    List<CardUI> tempUI = null;
    /// <summary>
    /// 找到选中的牌
    /// </summary>
    /// <returns>选中的牌</returns>
    public List<Card > FindSelected()
    {
        CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
        tempCard = new List<Card>();
        tempUI = new List<CardUI>();
        for (int i = 0; i < cardUIs.Length; i++)
        {
            if (cardUIs[i].IsSelected)
            {
                tempUI.Add(cardUIs[i]);
                tempCard.Add(cardUIs[i].Card);
            }
        }
        //***//
        Tools.Sort(tempCard, true);
        return tempCard;
    }
    /// <summary>
    /// 删除手牌/成功出牌
    /// </summary>
    public  void DestroySelectCard()
    {
        if (tempCard == null || tempUI == null) return;
        else
        {
            for (int i = 0; i < tempCard.Count; i++)
            {
                tempUI[i].Destroy();
                Cards.Remove(tempCard[i]);
            }
            SortCardUI(Cards);
            characterUI.SetRemain(CardCount);
        }
    }
}