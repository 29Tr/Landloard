using strange.extensions.context.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MVCSContext {

    public GameContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
    {
    }
    protected override void mapBindings()
    {
        //model
        injectionBinder.Bind<IntegrationModel>().To<IntegrationModel>().ToSingleton();//绑定为单例
        injectionBinder.Bind<RoundModel>().To<RoundModel>().ToSingleton();//绑定为单例
        injectionBinder.Bind<CardModel>().To<CardModel>().ToSingleton();//绑定为单例


        //view
        mediationBinder.Bind<StartView>().To<StartMediator>();
        mediationBinder.Bind<InteractionView>().To<InteractionMediator>();
        mediationBinder.Bind<CharacterView>().To<CharacterMediator>();


        //command
        commandBinder.Bind(CommandEvent.ChangeMultiple).To<ChangeMultipleCommand>();
        commandBinder.Bind(CommandEvent.RequestDeal).To<RequestDealCommand>();
        commandBinder.Bind(CommandEvent.GrabLanlord).To<GrabLandlordCommand>();
        commandBinder.Bind(CommandEvent.PlayCard).To<PlayCardCommand>();
        commandBinder.Bind(CommandEvent.PassCard).To<PassCardCommand>();
        commandBinder.Bind(CommandEvent.GameOver).To<GameOverCommand>();


        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
    }
}

