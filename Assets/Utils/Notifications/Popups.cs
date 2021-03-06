﻿using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class NotificationUtils
    {
        internal static void AddPopup(GameContext gameContext, PopupMessage popup)
        {
            var container = GetPopupContainer(gameContext);
            var messages = container.popup.PopupMessages;

            messages.Add(popup);

            container.ReplacePopup(messages);
            ScreenUtils.TriggerScreenUpdate(gameContext);
        }

        internal static void ClosePopup(GameContext gameContext)
        {
            if (!IsHasActivePopups(gameContext))
                return;

            var container = GetPopupContainer(gameContext);
            var messages = container.popup.PopupMessages;

            messages.RemoveAt(0);

            container.ReplacePopup(messages);
            ScreenUtils.TriggerScreenUpdate(gameContext);
        }

        public static GameEntity GetPopupContainer(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext);
        }

        public static bool IsHasActivePopups(GameContext gameContext)
        {
            return GetNotificationsComponent(gameContext).popup.PopupMessages.Count > 0;
        }

        public static List<PopupMessage> GetPopups(GameContext gameContext)
        {
            var container = GetPopupContainer(gameContext);

            return container.popup.PopupMessages;
        }

        public static PopupMessage GetPopupMessage(GameContext gameContext)
        {
            var messages = GetPopups(gameContext);

            return messages[0];
        }
    }
}
