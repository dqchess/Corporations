﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStartingNichesListView : ListView
{
    public GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var niche = entity as GameEntity;
        var preview = t.GetComponent<NichePreview>();
        preview.SetNiche(niche, true);

        var link = preview.GetComponentInChildren<LinkToNiche>();
        link.gameObject.AddComponent<SetInitialNiche>()
            .SetNiche(niche.niche.NicheType, TypeCorporationNameContainer, ChooseInitialNicheContainer);

        Destroy(link);
    }

    void Start()
    {
        var niches = new GameEntity[2];

        niches[0] = NicheUtils.GetNicheEntity(GameContext, NicheType.Com_Forums);
        niches[1] = NicheUtils.GetNicheEntity(GameContext, NicheType.Com_SocialNetwork);

        SetItems(niches);
    }
}
