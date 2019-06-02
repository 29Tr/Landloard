using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.command.impl;
public class GameOverCommand:EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    [Inject]
    public CardModel CardModel { get; set; }
    public override void Execute()
    {
        int temp = IntegrationModel.Result;

        GameOverArgs e = (GameOverArgs) evt.data;
        if (e.PlayWin)
            IntegrationModel.PlayIntegration += temp;
        else
        {
            IntegrationModel.PlayIntegration -= temp;
        }

        if (e.ComputerRightWin)
            IntegrationModel.ComputerRightIntegration += temp;
        else
        {
            IntegrationModel.ComputerRightIntegration -= temp;
        }

        if (e.ComputerLeftWin)
            IntegrationModel.ComputerLeftIntegration += temp;
        else
        {
            IntegrationModel.ComputerLeftIntegration -= temp;
        }





    }
}