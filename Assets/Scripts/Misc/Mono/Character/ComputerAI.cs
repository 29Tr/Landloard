using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 电脑出牌
/// </summary>
public class ComputerAI:MonoBehaviour
{
    /// <summary>
    /// 要出的牌
    /// </summary>
    public List<Card> selectCards = new List<Card>();
    
    public CardType currType = CardType.None;//出牌类型

    public void SmartSelectCards(List<Card> cards,CardType cardType,int weight,int length,bool isBiggest)
    {

        cardType = isBiggest ? CardType.None : cardType;
        currType = cardType;
        Debug.Log(cardType.ToString());
        selectCards.Clear();
        switch (cardType)
        {
            case CardType.None:
                //出合适牌
                selectCards = FindSmallestCards(cards);
                Debug.Log("随机");
                break;
            case CardType.Single:
                selectCards = FindSingle(cards, weight);
                break;
            case CardType.Double:
                selectCards = FindDouble(cards, weight);
                break;
            case CardType.Straight:
                selectCards = FindStraight(cards, weight, length);
                if (selectCards.Count == 0)
                {
                    selectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (selectCards.Count == 0)
                    {
                        selectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.DoubleStraight:
                selectCards = FindDoubleStraight(cards, weight, length);
                if (selectCards.Count == 0)
                {
                    selectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (selectCards.Count == 0)
                    {
                        selectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.TripleStraight:
                selectCards = FindBoom(cards, -1);
                currType = CardType.Boom;
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                    currType = CardType.JokerBoom;
                }
                break;
            case CardType.Three:
                selectCards = FindThree(cards, weight);
                break;
            case CardType.ThreeAndOne:
                selectCards = FindThreeAndOne(cards, weight);
                break;
            case CardType.ThreeAndTwo:
                selectCards = FindThreeAndDouble(cards, weight);
                break;
            case CardType.Boom:
                selectCards = FindBoom(cards, weight);
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                    currType = CardType.JokerBoom;
                }
                break;
            case CardType.JokerBoom:
                break;
            default:
                break;
        }
        
    }

    public List<Card> FindSmallestCards(List<Card> cards)
    {
        List<Card> select = new List<Card>();
        //先出顺
        for (int i = 12; i >=5; i--)
        {
            select = FindStraight(cards, -1, i);
            if (select.Count != 0)
            {
                currType = CardType.Straight;
                break;
            }
        }

        //三代二
        if (select.Count == 0)
        {
            //3*12
            for(int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndDouble(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.ThreeAndTwo;
                    break;
                }
            }
        }
        //三带一
        if (select.Count == 0)
        {
            //3*12
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndOne(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.ThreeAndOne;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            //3*12
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThree(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.Three;
                    break;
                }
            }
        }

        //对
        if (select.Count == 0)
        {
            for(int i = 0; i < 24; i += 2)
            {
                select = FindDouble(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.Double;
                    break;
                }
            }
        }

        //单
        if (select.Count == 0)
        {
          select=FindSingle(cards, -1);
            currType = CardType.Single;
        }
        return select;
    }


    /// <summary>
    ///  单牌
    /// </summary>
    /// <param name="cards">排好序的牌</param>
    /// <param name="weight">上家出牌大小</param>
    /// <returns></returns>
    public List<Card> FindSingle(List<Card> cards,int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            if ((int)cards[i].CardWeight > weight)
            {
                select.Add(cards[i]);
                break;
            }
        }
        return select;
    }
    /// <summary>
    /// 对儿
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindDouble(List<Card> cards,int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count-1; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i + 1].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight;
                if (totalWeight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i+1]);
                    break;

                }
            }
        }
        return select;
    }
    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="minweight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public List<Card> FindStraight(List<Card> cards,int minweight,int length)
    {
        List<Card> select=new List<Card>();
        int counter = 1;
        //Card的索引
        List<int> indexList = new List<int>();
        for (int i = 0; i < cards.Count-4; i++)
        {
            int weight = (int)cards[i].CardWeight;
            if (weight > minweight)
            {
                counter = 1;
                indexList.Clear();
                for (int j = i+1; j < cards.Count; j++)//cards[i]cardweight-weight==counter,牌与第一张牌的插值counter
                {
                    if (cards[j].CardWeight > Weight.One)
                        break;
                    if ((int)cards[j].CardWeight - weight == counter)
                    {
                        indexList.Add(j);
                        counter++;
                    }
                    if (counter == length)
                        break;
                }

            }
            if (counter == length)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        //加元素
        if (counter == length)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }
    /// <summary>
    /// 双顺
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="minweight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public List<Card> FindDoubleStraight(List<Card> cards,int minweight,int length)
    {
        List<Card> select = new List<Card>();
        int counter = 0;
        //Card的索引
        List<int> indexList = new List<int>();

        for (int i = 0; i < cards.Count-4 ; i++)
        {
            int weight = (int)cards[i].CardWeight;
            if (weight > minweight)
            {
                counter = 0;
                indexList.Clear();
                //游标 控制counter++
                int temp = 0;
                for (int j = i + 1; j < cards.Count; j++)//cards[i]cardweight-weight==counter,牌与第一张牌的插值counter
                {
                    if (cards[j].CardWeight > Weight.One)
                        break;
                    if ((int)cards[j].CardWeight - weight == counter)
                    {
                        temp++;
                        if(temp%2==1)
                            counter++;
                        indexList.Add(j);
                       
                    }
                    if (counter == length/2)
                        break;
                }

            }
            if (counter == length / 2)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        //加元素
        if (counter == length/2)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }
    /// <summary>
    /// 三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindThree(List<Card> cards,int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 3; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i + 1].CardWeight && (int)cards[i + 1].CardWeight == (int)cards[i + 2].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight + (int)cards[i + 2].CardWeight;
                if (totalWeight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    break;

                }
            }
        }
        return select;
    }

    /// <summary>
    /// 三代一
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindThreeAndOne(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> Three = FindThree(cards, weight);
        if (Three.Count > 0)
        {
            foreach (var card in Three)
            {
                cards.Remove(card);
            }

            List<Card> one = FindSingle(cards, -1);
            if (one.Count != 0)
            {
                select.AddRange(Three);
                select.AddRange(one);
            }
        }
        return select;
    }
    /// <summary>
    /// 三代二
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindThreeAndDouble(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> Three = FindThree(cards, weight);
        if (Three.Count > 0)
        {
            foreach (var card in Three)
            {
                cards.Remove(card);
            }

            List<Card> two = FindDouble(cards, -1);
            if (two.Count != 0)
            {
                select.AddRange(Three);
                select.AddRange(two);
            }
        }
        return select;
    }
    /// <summary>
    ///Boom 
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindBoom(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 4; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i + 1].CardWeight &&
                (int)cards[i + 1].CardWeight == (int)cards[i + 2].CardWeight&&
                (int)cards[i].CardWeight==(int)cards[i+3].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight + 
                    (int)cards[i + 2].CardWeight+(int)cards[i+3].CardWeight;
                if (totalWeight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    select.Add(cards[i + 3]);
                    break;
                }
            }
        }
        return select;
    }
    /// <summary>
    /// JokerBoom
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> FindJokerBoom(List<Card> cards)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].CardWeight ==Weight.SJoker&& cards[i + 1].CardWeight==Weight.LJoker)
            {
               
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    break;
            }
        }
        return select;
    }
}