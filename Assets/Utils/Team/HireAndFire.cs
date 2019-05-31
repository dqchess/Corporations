﻿namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static void HireWorker(GameEntity company, WorkerRole workerRole)
        {
            var worker = HumanUtils.GenerateHuman(Contexts.sharedInstance.game);

            AttachToTeam(company, worker.human.Id, workerRole);

            HumanUtils.AttachToCompany(worker, company.company.Id);

            if (workerRole == WorkerRole.Programmer)
                HumanUtils.SetSkill(worker, workerRole, HumanUtils.GetRandomProgrammingSkill());
        }

        public static void AttachToTeam(GameEntity company, int humanId, WorkerRole role)
        {
            var team = company.team;

            team.Workers[humanId] = role;

            ReplaceTeam(company, team);
        }

        public static void FireWorker(GameEntity company, int humanId)
        {
            var team = company.team;

            team.Workers.Remove(humanId);

            ReplaceTeam(company, team);
        }

        public static void HireManager(GameEntity company)
        {
            HireWorker(company, WorkerRole.Manager);
        }

        public static void HireProgrammer(GameEntity company)
        {
            HireWorker(company, WorkerRole.Programmer);
        }

        public static void HireMarketer(GameEntity company)
        {
            HireWorker(company, WorkerRole.Marketer);
        }
    }
}
