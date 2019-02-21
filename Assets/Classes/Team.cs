﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Classes
{
    public class Team
    {
        public List<Human> Workers;

        public Team (List<Human> workers)
        {
            Workers = workers;
        }

        bool IsProgrammer(Human human)
        {
            return human.IsProgrammer();
        }

        bool IsMarketer(Human human)
        {
            return human.IsMarketer();
        }

        bool IsManager(Human human)
        {
            return human.IsManager();
        }

        public List<Human> Programmers
        {
            get { return Workers.FindAll(IsProgrammer); }
        }

        public List<Human> Marketers
        {
            get { return Workers.FindAll(IsMarketer); }
        }

        public List<Human> Managers
        {
            get { return Workers.FindAll(IsManager); }
        }

        int SumPointProduction(List<Human> workers)
        {
            return workers.Sum(p => p.IsCompletelyDemoralised ? 0 : p.BaseProduction);
        }

        public int GetProgrammingPointsProduction()
        {
            return SumPointProduction(Programmers);
        }
        public int GetManagerPointsProduction()
        {
            return SumPointProduction(Managers);
        }
        public int GetSalesPointsProduction()
        {
            return SumPointProduction(Marketers);
        }
        public int GetIdeaPointsProduction()
        {
            return 100;
        }

        internal TeamResource GetMonthlyResources()
        {
            return new TeamResource()
                .SetProgrammingPoints(GetProgrammingPointsProduction())
                .SetManagerPoints(GetManagerPointsProduction())
                .SetSalesPoints(GetSalesPointsProduction());
        }

        internal long GetExpenses()
        {
            //Workers.Sum(h => h.salary)
            return (Workers.Count + 1) * 1000;
        }

        internal int GetProgrammerAverageLevel()
        {
            int val = Programmers.Sum(p => p.Skills.Programming.Level);
            int count = Programmers.Count;

            return Ceil(val, count);
        }

        internal int GetManagerAverageLevel()
        {
            int val = Managers.Sum(m => m.Skills.Management.Level);
            int count = Managers.Count;

            return Ceil(val, count);
        }

        internal int GetMarketerAverageLevel()
        {
            int val = Marketers.Sum(m => m.Skills.Marketing.Level);
            int count = Marketers.Count;

            return Ceil(val, count);
        }

        int Ceil(int sum, int count)
        {
            if (count == 0)
                return 1;

            return (int)Math.Ceiling((decimal)sum / count);
        }

        internal void Join(Human employee)
        {
            Workers.Add(employee);
        }

        internal void Fire(int workerId)
        {
            Workers.RemoveAt(workerId);
        }



        internal void UpgradeMarketers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Marketers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }

        internal void UpgradeProgrammers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Programmers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }

        internal void UpgradeManagers(float xpRatio)
        {
            int experience = (int)(xpRatio * 100);
            Managers.ForEach(m => m.Skills.GainXP(experience, m.Specialisation));
        }

        internal void UpdateMorale(TeamMoraleData moraleData)
        {
            for (var i = 0; i < Workers.Count; i++)
                Workers[i].UpdateMorale(moraleData.Morale);
            //Workers.ForEach(w => w.UpdateMorale(moraleData.Morale));
        }
    }
}
