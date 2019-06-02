using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationModel
{
    //底分
    public int BasePoint;


    //倍数
    public int Multiple;

	//积分
   public int Result { get { return BasePoint * Multiple; } }

    public int PlayIntegration
    {
        get
        {
            return playIntegration;
        }

        set
        {
            playIntegration = value;
        }
    }

    public int ComputerLeftIntegration
    {
        get
        {
            return computerLeftIntegration;
        }

        set
        {
            computerLeftIntegration = value;
        }
    }

    public int ComputerRightIntegration
    {
        get
        {
            return computerRightIntegration;
        }

        set
        {
            computerRightIntegration = value;
        }
    }

    int playIntegration;
    int computerLeftIntegration;
    int computerRightIntegration;
    //初始化数据

    public void InitIntegration()
    {
        Multiple = 1;
        BasePoint = 100;
        PlayIntegration = 2000;
        ComputerLeftIntegration = 2000;
        computerRightIntegration = 2000;
    }
}
