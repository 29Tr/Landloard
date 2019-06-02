using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 回合数据
/// </summary>
public class RoundModel
{
    public static event Action<bool> PlayerHandle;
    public static event Action<ComputerSmartArgs> ComputerHandle;



    public bool isLandlord = false;
    public bool isWin = false;


    int currentWeight;
    int currentLength;
    CardType currentType;
    CharacterType biggestCharacter;
    CharacterType currentCharater;
    /// <summary>
    /// 现在该谁出牌
    /// </summary>
    public CharacterType CurrentCharater
    {
        get
        {
            return currentCharater;
        }

        set
        {
            currentCharater = value;
        }
    }
    /// <summary>
    /// 最大出牌者
    /// </summary>
    public CharacterType BiggestCharacter
    {
        get
        {
            return biggestCharacter;
        }

        set
        {
            biggestCharacter = value;
        }
    }
    /// <summary>
    /// 出牌类型
    /// </summary>
    public CardType CurrentType
    {
        get
        {
            return currentType;
        }

        set
        {
            currentType = value;
        }
    }
    /// <summary>
    /// 最大出牌人的出牌大小
    /// </summary>
    public int CurrentWeight
    {
        get
        {
            return currentWeight;
        }
        set
        {
            currentWeight = value;
        }
    }
    /// <summary>
    /// 出牌长度
    /// </summary>
    public int CurrentLength
    {
        get
        {
            return currentLength;
        }

        set
        {
            currentLength = value;
        }
    }
    public void InitRound()
    {
        this.currentType = CardType.None;
        this.currentLength = -1;
        this.currentWeight = -1;
        this.biggestCharacter = CharacterType.Desk;
        this.currentCharater = CharacterType.Desk;
    }

    /// <summary>
    /// 抢地主牌
    /// </summary>
    /// <param name="ctype"></param>
    public void Start(CharacterType ctype)
    {
        this.biggestCharacter = ctype;
        this.currentCharater = ctype;
        BeginWith(ctype);
    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="ctype"></param>
    private void BeginWith(CharacterType ctype)
    {
        if (ctype == CharacterType.Player)
        {
            //玩家出牌
            if (PlayerHandle != null)
                PlayerHandle(BiggestCharacter!=CharacterType.Player);
        }
        else//电脑出牌
        {
            if (ComputerHandle != null)
            {
                ComputerSmartArgs e=new ComputerSmartArgs()
                {
                    CardType=this.CurrentType,
                    Weight = this.CurrentWeight,
                    Length=this.CurrentLength,
                    IsBiggest=this.BiggestCharacter,
                    CharacterType=this.CurrentCharater
                };
                ComputerHandle(e);
            }
        }
    }

    /// <summary>
    /// 转换出牌
    /// </summary>
    public void Turn()
    {
        currentCharater++;
        if (currentCharater == CharacterType.Desk || currentCharater == CharacterType.Library)
            currentCharater = CharacterType.Player;
        BeginWith(currentCharater);
    }
}
   