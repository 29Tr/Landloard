using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  {


    string cardName;
    Weight cardWeight;
    Colors cardColor;
    CharacterType belongTo;

    public string CardName
    {
        get
        {
            return cardName;
        }

      
    }

    public Weight CardWeight
    {
        get
        {
            return cardWeight;
        }
    }

    public Colors CardColor
    {
        get
        {
            return cardColor;
        }
    }

    public CharacterType BelongTo
    {
        get
        {
            return belongTo;
        }

        set
        {
            belongTo = value;
        }
    }
    ///
    ///初始化参数
    ///
    public  Card(string name,Colors color,Weight weight,CharacterType type)
    {
        this.cardName = name;
        this.cardColor = color;
        this.cardWeight = weight;
        this.belongTo = type;
    }
}
