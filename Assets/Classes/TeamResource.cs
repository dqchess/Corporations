﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public struct TeamResource
    {
        private int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints;
        private long Money;

        public const int pointCap = 1000;

        public long money
        {
            get { return Money; }
            set { }
        }

        public int programmingPoints
        {
            get { return ProgrammingPoints; }
            set {
                ProgrammingPoints = value > pointCap ? pointCap : value;
            }
        }

        public int managerPoints
        {
            get { return ManagerPoints; }
            set {
                ManagerPoints = value > pointCap ? pointCap : value;
            }
        }

        public int salesPoints
        {
            get { return SalesPoints; }
            set {
                SalesPoints = value > pointCap ? pointCap : value;
            }
        }

        public int ideaPoints
        {
            get { return IdeaPoints; }
            set {
                IdeaPoints = value > pointCap ? pointCap : value;
            }
        }

        public TeamResource(int programmingPoints, int managerPoints, int salesPoints, int ideaPoints, long money)
        {
            ProgrammingPoints = programmingPoints;
            ManagerPoints = managerPoints;
            SalesPoints = salesPoints;
            IdeaPoints = ideaPoints;
            Money = money;
        }

        static bool IsEnoughResources(TeamResource Need)
        {
            Debug.LogError("IsEnoughResources not implemented");

            return false;
        }

        public void Spend(TeamResource spendable)
        {
            ProgrammingPoints -= spendable.ProgrammingPoints;
            ManagerPoints -= spendable.ManagerPoints;
            SalesPoints -= spendable.SalesPoints;
            IdeaPoints -= spendable.IdeaPoints;
            Money -= spendable.Money;
        }

        public void Print()
        {
            Debug.Log(String.Format("$$$: {4} PP: {0} MP: {1} SP: {2} i: {3}",
                ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money));
        }

        internal TeamResource AddMoney(long money)
        {
            Money += money;
            return this;
        }

        // Add all team resources except money
        internal void AddTeamPoints(TeamResource increase)
        {
            ProgrammingPoints += increase.ProgrammingPoints;
            ManagerPoints += increase.ManagerPoints;
            SalesPoints += increase.SalesPoints;
            IdeaPoints += increase.IdeaPoints;
        }

        internal TeamResource SetProgrammingPoints(int points)
        {
            ProgrammingPoints = points;
            return this;
        }
        internal TeamResource SetManagerPoints(int points)
        {
            ManagerPoints = points;
            return this;
        }
        internal TeamResource SetSalesPoints(int points)
        {
            SalesPoints = points;
            return this;
        }
        internal TeamResource SetIdeaPoints(int points)
        {
            IdeaPoints = points;
            return this;
        }
        internal TeamResource SetMoney(int points)
        {
            Money = points;
            return this;
        }

        internal void AddIdeas(int stealableIdeas)
        {
            IdeaPoints += stealableIdeas;
        }
    }
}
