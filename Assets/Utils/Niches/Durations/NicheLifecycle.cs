﻿using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        // update
        public static void UpdateNicheDuration(GameEntity niche)
        {
            var phase = GetMarketState(niche);
            var newDuration = GetNicheDuration(niche);

            niche.ReplaceNicheState(phase, newDuration);
        }

        // get
        public static int GetNicheDuration(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            var duration = GetMinimumPhaseDurationInPeriods(phase) * GetNichePeriodDurationInMonths(niche);

            return duration;
        }

        public static int GetNichePeriodDurationInMonths(GameEntity niche)
        {
            NicheDuration nicheDuration = niche.nicheLifecycle.Period;

            var state = GetMarketState(niche);

            return GetNichePeriodDurationInMonths(nicheDuration, state);
        }

        public static int GetNichePeriodDurationInMonths(NicheDuration nicheDuration, NicheLifecyclePhase phase)
        {
            // innovation 5
            // trending 10
            // mass use 55
            // decay 20
            // death 10

            if (nicheDuration == NicheDuration.EntireGame)
            {
                switch (phase)
                {
                    case NicheLifecyclePhase.Innovation: return 12;
                    case NicheLifecyclePhase.Trending: return 24;
                    case NicheLifecyclePhase.MassUse: return 1000;
                    case NicheLifecyclePhase.Decay: return 1000;
                    default: return 0;
                }
            }

            var durationInMonths = (int)nicheDuration;

            var sumOfPeriods = NICHE_PHASE_DURATION_INNOVATION + NICHE_PHASE_DURATION_TRENDING + NICHE_PHASE_DURATION_MASS + NICHE_PHASE_DURATION_DECAY;
            
            var X = durationInMonths / sumOfPeriods;

            return Math.Max(X, 1);
        }

        public static int GetMinimumPhaseDurationInPeriods(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return NICHE_PHASE_DURATION_INNOVATION;

                case NicheLifecyclePhase.Trending:
                    return NICHE_PHASE_DURATION_TRENDING;

                case NicheLifecyclePhase.MassUse:
                    return NICHE_PHASE_DURATION_MASS;

                case NicheLifecyclePhase.Decay:
                    return NICHE_PHASE_DURATION_DECAY;

                default:
                    return 0;
            }
        }

        public static NicheLifecyclePhase GetNextPhase(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Idle:
                    return NicheLifecyclePhase.Innovation;

                case NicheLifecyclePhase.Innovation:
                    return NicheLifecyclePhase.Trending;

                case NicheLifecyclePhase.Trending:
                    return NicheLifecyclePhase.MassUse;

                case NicheLifecyclePhase.MassUse:
                    return NicheLifecyclePhase.Decay;

                case NicheLifecyclePhase.Decay:
                default:
                    return NicheLifecyclePhase.Death;
            }
        }

        public const int NICHE_PHASE_DURATION_INNOVATION = 5;
        public const int NICHE_PHASE_DURATION_TRENDING = 10;
        public const int NICHE_PHASE_DURATION_MASS = 55;
        public const int NICHE_PHASE_DURATION_DECAY = 20;
        public const int NICHE_PHASE_DURATION_DEATH = 10;
    }
}