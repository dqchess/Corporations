﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Channel
    {
        uint MaxClients;
        uint Clients;
        int Engagement; // 1 ... 5
        int marketId;

        //Dictionary<int, Advert> Adverts; // int - projectID

        public Channel(int engagement, uint maxClients, uint clients, int marketId)
        {
            MaxClients = maxClients;
            Clients = clients;
            Engagement = engagement;
            this.marketId = marketId;
        }

        public uint StartAdCampaign(int projectId, Advert advert)
        {
            float adEffeciency = advert.AdEffeciency / 100f;
            float dice = UnityEngine.Random.Range(Balance.advertClientsRangeMin, Balance.advertClientsRangeMax) / 100f;
            uint clients = (uint) (Engagement * adEffeciency * Clients * dice / 100);

            string info = String.Format("added {0} clients (possible: {1} . {2} dice {3} effeciency {4}) to {5} via last campaign",
                clients, Clients, Engagement, dice, adEffeciency, projectId);
            Debug.Log(info);

            Clients -= clients;

            return clients;
        }

        public void PrintProjectInfo(int projectId)
        {
            Debug.Log("Clients: PrintProjectInfo Useless");
        }
    }
}