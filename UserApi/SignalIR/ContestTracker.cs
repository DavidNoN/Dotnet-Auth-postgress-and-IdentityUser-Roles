using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.SignalIR
{
    public class ContestTracker
    {
        private static readonly Dictionary<Guid, DateTime> NewContest = new();

        public Task NewContestCreated(Guid contest, DateTime dateContest)
        {
            lock (NewContest)
            {
                if (NewContest.ContainsKey(contest))
                {
                    NewContest[contest].Add(dateContest.TimeOfDay);
                }
                else
                {
                    NewContest.Add(contest, dateContest);
                }
            }
            
            return  Task.CompletedTask;
        }

        public Task<Guid> GetNewContests()
        {
            Guid newContestsArray;
            lock (NewContest)
            {
                newContestsArray = NewContest.Select(k => k.Key).AsQueryable().First();
            }

            return Task.FromResult(newContestsArray);
        }
    }
}