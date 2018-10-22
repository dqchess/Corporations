﻿using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertController : MonoBehaviour, CommandHandler {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartCampaign(Advert advert)
    {

    }

    public void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        Debug.LogFormat("Handle command! AdvertController {0} {1}", eventName, parameters["advert"].ToString());

        switch (eventName)
        {
            case Commands.AD_CAMPAIGN_START:
                Advert ad = (Advert) parameters["advert"];

                StartCampaign(ad);
                break;
        }
    }
}

public interface CommandHandler
{
    void HandleCommand(string eventName, Dictionary<string, object> parameters);
}
