﻿using System;
using Assets;
using Assets.Classes;

public class Application
{
    public World world;
    public ViewManager ViewManager;
    AudioManager audioManager;
    Notifier Notifier;

    public int projectId = 0;

    public Application(World world, ViewManager ViewManager, AudioManager audioManager)
    {
        this.world = world;
        this.ViewManager = ViewManager;
        this.audioManager = audioManager;

        Notifier = new Notifier();
    }



    public bool PeriodTick(int count)
    {
        bool isMonthTick = world.PeriodTick(count);

        if (isMonthTick)
        {
            audioManager.PlayCoinSound();
            ViewManager.HighlightMonthTick();
        }

        return isMonthTick;
    }

    public void PrepareAd(int projectId, int channelId, int duration)
    {
        audioManager.PlayPrepareAdSound();
        world.PrepareAd(projectId, channelId, duration);
        RedrawResources();
        RedrawAds();
    }

    public void StartAdCampaign(int projectId, int channelId)
    {
        audioManager.PlayStartAdSound();
        world.StartAdCampaign(projectId, channelId);
        RedrawResources();
        RedrawAds();
    }

    internal void Notify(string message)
    {
        Notifier.Notify(message);
        audioManager.PlayNotificationSound();
    }

    public void ExploreFeature(int projectId, int featureId)
    {
        audioManager.PlayClickSound();
        world.ExploreFeature(projectId, featureId);
        RedrawFeatures();
    }

    public void UpgradeFeature(int projectId, int featureId)
    {
        audioManager.PlayClickSound();
        world.UpgradeFeature(projectId, featureId);
        RedrawFeatures();
    }


    // rendering
    public void RedrawResources()
    {
        TeamResource teamResource = world.GetProjectById(projectId).resources;
        TeamResource resourceMonthChanges = world.GetProjectById(projectId).resourceMonthChanges;

        Audience audience = world.GetProjectById(projectId).audience;

        string formattedDate = world.GetFormattedDate();

        ViewManager.RedrawResources(teamResource, resourceMonthChanges, audience, formattedDate);
    }

    public void RedrawTeam()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawTeam(p);
    }

    internal void RedrawCompanies()
    {
        ViewManager.RedrawCompanies(world.projects);
    }

    public void RedrawAds()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawAds(p.GetAds());
    }

    public void RedrawFeatures()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawFeatures(p.Features);
    }
}
