using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMediator:EventMediator
{
    [Inject]
    public StartView StartView { get; set; }

    public override void OnRegister()
    {
        StartView.Add.onClick.AddListener(OnAddClick);
        StartView.DonAdd.onClick.AddListener(OnDonAddClick);
    }

    public override void OnRemove()
    {
        StartView.Add.onClick.RemoveListener(OnAddClick);
        StartView.DonAdd.onClick.RemoveListener(OnDonAddClick);
    }


    void OnAddClick()
    {
        dispatcher.Dispatch(CommandEvent.ChangeMultiple, 1);
        Destroy(StartView.gameObject);

    }
    void OnDonAddClick()
    {
        dispatcher.Dispatch(CommandEvent.ChangeMultiple, 2);


        Destroy(StartView.gameObject);

    }
}
