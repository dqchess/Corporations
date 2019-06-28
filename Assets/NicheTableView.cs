﻿using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NicheTableView : View, IPointerEnterHandler
{
    [SerializeField] Text NicheName;
    [SerializeField] Text Competitors;
    [SerializeField] Image Panel;

    GameEntity entity;

    public void SetEntity(GameEntity niche)
    {
        entity = niche;

        NicheName.text = EnumUtils.GetFormattedNicheName(niche.niche.NicheType);
        Competitors.text = NicheUtils.GetCompetitorsAmount(niche.niche.NicheType, GameContext) + "\ncompanies";

        SetPanelColor();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedNiche(GameContext, entity.niche.NicheType);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity.niche.NicheType == ScreenUtils.GetSelectedNiche(GameContext));
    }
}