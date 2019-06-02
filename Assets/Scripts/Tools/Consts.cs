using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Consts
{
}
public enum PanelType
{
    StartPanel,
    CharacterPanel,
    InteractionPanel   
}


public enum CommandEvent
{
    ChangeMultiple,//加不加倍
    RequestDeal,//请求出牌
    GrabLanlord,
    PlayCard,
    PassCard,
    GameOver,
    RequestUpdate,//数据更新
    UpdateGameOver//更新结算界面
}
public enum Identity
{
    Former,
    Landlord
}

/// <summary>
/// 出牌类型
/// </summary>
public enum CardType
{
    None,
    Single,//单 1
    Double,//对儿 2
    Straight,//顺子 5 - 12
    DoubleStraight,//双顺 >= 6
    TripleStraight,//飞机 >= 6 
    Three,//三不带 3
    ThreeAndOne,//三带一 4
    ThreeAndTwo,//三代二 5
    Boom,//炸弹 4
    JokerBoom//王炸 2
}
/// <summary>
/// 牌的大小
/// </summary>
public enum Weight
{
    
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}



public enum ViewEvent
{
    DealCard,//给每个人发牌 characterView
    CompleteDeal,//发牌结束
    DealThreeCards,//发地主牌
    RequestPlay,//玩家请求出牌
    SuccessedPlay,//成功出牌
    UpdateIntegration,//更新分数
    UpdateGameOver,//更新结算界面
    RestartGame//重新开始

}

/// <summary>
/// 持有牌的角色
/// </summary>
public enum CharacterType
{
    Library,
    Player,
    ComputerRight,
    ComputerLeft,
    Desk
}

/// <summary>
/// 花色
/// </summary>
public enum Colors
{
    None,//小王 大王
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方片
}
public enum ShowPoint
{
    Desk,
    Player,
    ComputerRight,
    ComputerLeft
}