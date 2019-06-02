using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : ContextView {
    void Start()
    {       
            context = new GameContext(this, true);
            context.Start();        
    }
}
