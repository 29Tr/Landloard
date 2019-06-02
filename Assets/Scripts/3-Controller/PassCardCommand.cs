using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using strange.extensions.command.impl;

public class PassCardCommand:Command
{
    [Inject]
    public RoundModel RoundModel { get; set; }

    public override void Execute()
    {
        RoundModel.Turn();
    }
}