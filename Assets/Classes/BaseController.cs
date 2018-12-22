﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public static class BaseController
    {
        public static void SendCommand(string eventName, Dictionary<string, object> parameters)
        {
            GameObject core = GameObject.Find("Core");
            EventBus controller = core.GetComponent<EventBus>();

            controller.SendCommand(eventName, parameters);
        }

        internal static void ExploreFeature(int featureId, int projectId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["projectId"] = projectId;
            parameters["featureId"] = featureId;

            SendCommand(Commands.FEATURE_EXPLORE, parameters);
        }

        internal static void UpgradeFeature(int featureId, int projectId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["projectId"] = projectId;
            parameters["featureId"] = featureId;

            SendCommand(Commands.FEATURE_UPGRADE, parameters);
        }

        internal static void HireWorker(int workerId, int projectId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["projectId"] = projectId;
            parameters["workerId"] = workerId;

            SendCommand(Commands.TEAM_WORKERS_HIRE, parameters);
        }

        internal static void FireWorker(int workerId, int projectId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["projectId"] = projectId;
            parameters["workerId"] = workerId;

            SendCommand(Commands.TEAM_WORKERS_FIRE, parameters);
        }

        internal static void PrepareAdCampaign(Advert advert)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["advert"] = advert;

            SendCommand(Commands.AD_CAMPAIGN_PREPARE, parameters);
        }

        internal static void StartAdCampaign(Advert advert)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["advert"] = advert;

            SendCommand(Commands.AD_CAMPAIGN_START, parameters);
        }
    }

}
