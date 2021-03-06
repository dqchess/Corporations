﻿using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static void UpdateScreenWithoutAnyChanges(GameContext gameContext)
        {
            var m = GetMenu(gameContext);

            ReplaceMenu(gameContext, m.menu.ScreenMode, m.menu.Data);
        }

        public static void UpdateScreen(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            ReplaceMenu(context, screenMode, data);
        }

        public static GameEntity GetNavigationHistory(GameContext context)
        {
            return context.GetEntities(GameMatcher.NavigationHistory)[0];
        }

        public static void UpdateHistory(GameContext context, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;
            q.Add(new MenuComponent { Data = data, ScreenMode = screenMode });

            history.ReplaceNavigationHistory(q);
        }

        public static void Navigate(GameContext context, ScreenMode newScreen, Dictionary<string, object> newData)
        {
            var menu = GetMenu(context);

            UpdateHistory(context, menu.menu.ScreenMode, menu.menu.Data);
            UpdateScreen(context, newScreen, newData);

            //SoundManager.Play(Sound.Hover);
        }

        public static void Navigate(GameContext context, ScreenMode screenMode)
        {
            // only changes screen
            Navigate(context, screenMode, GetScreenData(context));
        }

        public static void Navigate(GameContext context, ScreenMode screenMode, string field, object data)
        {
            var screenData = GetScreenData(context);

            // https://stackoverflow.com/questions/139592/what-is-the-best-way-to-clone-deep-copy-a-net-generic-dictionarystring-t
            var newScreenData = new Dictionary<string, object>(screenData);

            newScreenData[field] = data;

            Navigate(context, screenMode, newScreenData);
        }

        //string names = String.Join(",", q.Select(e => e.ScreenMode).ToArray());

        public static bool IsCanNavigateBack(GameContext gameContext)
        {
            var history = GetNavigationHistory(gameContext);

            var q = history.navigationHistory.Queries;

            return q.Count > 1;
        }

        //Debug.Log("Rendering menues: " + names);
        public static void NavigateBack(GameContext context)
        {
            var history = GetNavigationHistory(context);

            var q = history.navigationHistory.Queries;

            if (q.Count == 0)
                return;


            var destination = q[q.Count - 1];

            q.RemoveAt(q.Count - 1);

            history.ReplaceNavigationHistory(q);

            UpdateScreen(context, destination.ScreenMode, destination.Data);
        }

    }
}
