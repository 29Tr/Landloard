using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 一副牌
/// </summary>
public class CardModel
{
    CharacterType ctype = CharacterType.Library;
    Queue<Card> CardLibrary = new Queue<Card>();

    //剩余牌数
    public int CardCount { get { return CardLibrary.Count; } }




    /// <summary>
    /// 54
    /// </summary>
    public void InitCardLibrary()
    {
        //52张
        for (int color = 1; color < 5; color++)
        {
            for (int wegiht = 0; wegiht < 13; wegiht++)
            {
                Colors c = (Colors)color;
                Weight w = (Weight)wegiht;
                string name = c.ToString() + w.ToString();
                Card card = new Card(name, c, w, ctype);
                CardLibrary.Enqueue(card);
            }
        }
        Card sJoker = new Card("SJoker", Colors.None, Weight.SJoker, ctype);
        Card lJoker = new Card("LJoker", Colors.None, Weight.LJoker, ctype);
        CardLibrary.Enqueue(sJoker);
        CardLibrary.Enqueue(lJoker);
    }

    //洗牌
    public void Shuffle()
    {
        List<Card> newList = new List<Card>();
        foreach(var card in CardLibrary)
        {
            int index = Random.Range(0, newList.Count + 1);
            newList.Insert(index, card);
        }
        CardLibrary.Clear();
        foreach (var card in newList)
        {
            CardLibrary.Enqueue(card);
        }
        newList.Clear();
    }

    //最开始发牌
    //sendTo
    public Card DealCard(CharacterType sendTo)
    {
        Card card = CardLibrary.Dequeue();//移除并返回头对象
        card.BelongTo = sendTo;
        return card;
    }

}
