using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class Tools  {
    static Transform uiPanel;

    public static Transform UiPanel
    {
        get
        {
            if (uiPanel == null)
                uiPanel = GameObject.Find("Canvas").transform;
            return uiPanel;
        }
    }

    ///
    ///生成panel
    public static GameObject CreatPanel(PanelType type)
    {
        GameObject g = Resources.Load<GameObject>(type.ToString());
        if (g == null)
        {
            return null;
            Debug.Log("不存在" + type.ToString());
        }
        else
        {
            GameObject panel = GameObject.Instantiate(g);
            panel.transform.name = type.ToString();
            panel.transform.SetParent(UiPanel, false);
        }
        return g;
    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="asc">升序？</param>
    public static void Sort(List<Card> cards,bool asc)
    {
        cards.Sort(
            (Card a, Card x) =>
        {
            if (asc)
            {
                return a.CardWeight.CompareTo(x.CardWeight);//升序
            }
            else
                return -a.CardWeight.CompareTo(x.CardWeight);//降序
        }
        );
    }

    ///
    ///获取牌的大小
    ///cards 出的牌  type出牌类型
    ///
    public static int GetWeight(List<Card> cards,CardType cardType)
    {
        int totalWeight = 0;
        if (cardType == CardType.ThreeAndOne || cardType == CardType.ThreeAndTwo)
        {
            for(int i = 0; i < cards.Count; i++)
            {
                if(cards[i].CardWeight==cards[i+1].CardWeight&&cards[i].CardWeight==cards[i+2].CardWeight)
                {
                    totalWeight += (int)cards[i].CardWeight;
                    totalWeight *= 3;
                    break;
                }
            }
        }else if (cardType == CardType.Straight || cardType == CardType.DoubleStraight)
        {
            totalWeight = (int)cards[0].CardWeight;
        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                totalWeight += (int)cards[i].CardWeight;
            }
        }


        return totalWeight;
    }



}



