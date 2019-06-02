using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 判断是否可以出牌
/// </summary>
public static  class Ruler
{
    ///card 传入的牌  type 出牌类型
    ///
    public static bool CanPop(List<Card> cards,out CardType type)
    {
        type = CardType.None;
        bool can = false;//是否可以出牌
        switch (cards.Count)
        {
            case 1:
                if (IsSingle(cards))
                {
                    can = true;
                    type = CardType.Single;
                }     
                break;
            case 2:
                if (IsDouble(cards))
                {
                    can = true;
                    type = CardType.Double;
                }
                else if(IsJokerBoom(cards))
                {
                    can = true;
                    type = CardType.JokerBoom;
                }
                break;
            case 3:
                if (IsThree(cards))
                {
                    can = true;
                    type = CardType.Three;
                }
                break;
            case 4:
                if (IsBoom(cards))
                {
                    can = true;
                    type = CardType.Boom;
                }else if (ThreeAndOne(cards))
                {
                    can = true;
                    type = CardType.ThreeAndOne;
                }
                break;
            case 5:
                if (IsThreeAndTwo(cards))
                {
                    can = true;
                    type = CardType.ThreeAndTwo;
                }
                else if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                break;
            case 6:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardType.TripleStraight;
                }
                break;
            case 7:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                break;
            case 8:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                break;
            case 9:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardType.TripleStraight;
                }
                break;
            case 10:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                break;
            case 11:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                break;
            case 12:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardType.TripleStraight;
                }
                break;
            case 13:              
                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                break;
            case 15:
                if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardType.TripleStraight;
                }
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardType.TripleStraight;
                }
                break;
            case 19:
                break;
            case 20:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardType.DoubleStraight;
                }
                break;
            default:
                break;
        }
        return can;
    }
    //Single,//单 1
    public static  bool IsSingle(List<Card> cards)
    {
        return cards.Count ==1;
    }
    //Double,//对儿 2
    public static bool IsDouble(List<Card> cards)
    {
        if (cards.Count == 2)
            if (cards[0].CardWeight == cards[1].CardWeight)
                return true;
        return false;
    }
    //Straight,//顺子 5 - 12
    public static bool IsStraight(List<Card> cards)
    {
        if (cards.Count < 5 || cards.Count > 12) return false;
        for(int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i + 1].CardWeight - cards[i].CardWeight != 1)
                return false;
            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 1].CardWeight > Weight.One) return false;

        }
        return true;
    }
    //DoubleStraight,//双顺 >= 6
    public static bool IsDoubleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 2 != 0) return false;
        for(int i = 0; i < cards.Count - 2; i+=2)
        {
            if (cards[i + 1].CardWeight != cards[i].CardWeight) return false;
            if (cards[i + 2].CardWeight - cards[i].CardWeight != 1) return false;

            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One) return false;
        }
        return true;
    }
    //TripleStraight,//飞机 >= 6 
    public static bool IsTripleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0) return false;
        for (int i = 0; i < cards.Count - 3; i+=3)
        {
            if (cards[i + 1].CardWeight != cards[i].CardWeight) return false;
            if (cards[i + 2].CardWeight != cards[i].CardWeight) return false;
            if (cards[i + 1].CardWeight != cards[i + 2].CardWeight) return false;
            if (cards[i + 3].CardWeight - cards[i].CardWeight != 1) return false;

            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 3].CardWeight > Weight.One) return false;
        }
        return true;
    }

    //Three,//三不带 3
    public static bool IsThree(List<Card> cards)
    {
        if (cards.Count > 3) return false;
        if (cards[1].CardWeight != cards[0].CardWeight) return false;
        if (cards[1].CardWeight != cards[2].CardWeight) return false;
        return true;
    }

    //ThreeAndOne,//三带一 4
    public static bool ThreeAndOne(List<Card> cards)
    {
        if (cards.Count != 4) return false;
        if (cards[1].CardWeight == cards[2].CardWeight && cards[1].CardWeight == cards[0].CardWeight) return true;
        else if (cards[1].CardWeight == cards[2].CardWeight && cards[3].CardWeight == cards[2].CardWeight) return true;
        return false;
    }
    //ThreeAndTwo,//三代二 5
    public static bool IsThreeAndTwo(List<Card> cards)
    {
        if (cards.Count != 5) return false;
        if (cards[1].CardWeight == cards[2].CardWeight && cards[1].CardWeight == cards[0].CardWeight)
        {
            if (cards[3].CardWeight == cards[4].CardWeight) return true;
        }
        if (cards[3].CardWeight == cards[2].CardWeight && cards[3].CardWeight == cards[4].CardWeight)
        {
            if (cards[1].CardWeight == cards[0].CardWeight) return true;
        }
        return false;
    }
    //Boom,//炸弹 4
    public static bool IsBoom(List<Card> cards)
    {
        if (cards.Count != 4) return false;
        if (cards[1].CardWeight != cards[0].CardWeight) return false;
        if (cards[1].CardWeight != cards[2].CardWeight) return false;
        if (cards[3].CardWeight != cards[2].CardWeight) return false;
        return true;
    }
    //JokerBoom//王炸 2
    public static bool IsJokerBoom(List<Card> cards)
    {
        if (cards.Count != 2) return false;
        if (cards[0].CardWeight == Weight.SJoker && cards[1].CardWeight == Weight.LJoker) return true;
        return false;
    }
}