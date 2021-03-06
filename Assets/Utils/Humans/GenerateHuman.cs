﻿using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Humans
    {
        public static GameEntity GenerateHuman(GameContext gameContext, WorkerRole workerRole)
        {
            var worker = GenerateHuman(gameContext);

            SetSkills(worker, workerRole);
            SetRole(worker, workerRole);

            return worker;
        }

        public static GameEntity GenerateHuman(GameContext gameContext)
        {
            var e = gameContext.CreateEntity();

            int id = GenerateHumanId(gameContext);

            e.AddHuman(id, "Dude", id + "");
            e.AddWorker(-1, WorkerRole.Universal);
            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());
            e.AddHumanSkills(
                GenerateRandomSkills(),
                GenerateRandomTraits(),
                new Dictionary<NicheType, int>()
                );

            return e;
        }


        // generate as secondary skill, but save genius chances
        static int GetRandomXP()
        {
            var geniusChance = UnityEngine.Random.Range(1, 100) < 10;

            return UnityEngine.Random.Range(40, 60 + (geniusChance ? 20 : 0));
        }

        // generate as secondary skill, but save genius chances
        static int GetRandomTrait()
        {
            var geniusChance = UnityEngine.Random.Range(1, 100) < 20;

            return UnityEngine.Random.Range(45, 65 + (geniusChance ? 20 : 0));
        }

        static void SetPrimarySkill(GameEntity worker, WorkerRole role)
        {
            int level = UnityEngine.Random.Range(65, 85);

            SetSkill(worker, role, level);
        }

        static void SetPrimaryTrait(GameEntity worker, TraitType traitType)
        {
            int level = UnityEngine.Random.Range(70, 90);

            SetTrait(worker, traitType, level);
        }

        public static Dictionary<TraitType, int> GenerateRandomTraits()
        {
            return new Dictionary<TraitType, int>
            {
                [TraitType.Ambitions] = GetRandomTrait(),
                [TraitType.Charisma] = GetRandomTrait(),
                [TraitType.Discipline] = GetRandomTrait(),
                [TraitType.Education] = GetRandomTrait(),
                [TraitType.Vision] = GetRandomTrait(),
                [TraitType.Will] = GetRandomTrait(),
            };
        }

        public static Dictionary<WorkerRole, int> GenerateRandomSkills()
        {
            return new Dictionary<WorkerRole, int>
            {
                [WorkerRole.CEO] = GetRandomXP(),
                [WorkerRole.Manager] = GetRandomXP(),
                [WorkerRole.Marketer] = GetRandomXP(),
                [WorkerRole.Programmer] = 0,
            };
        }
    }
}
