using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Core.Domain.Common;

namespace Tasks.Core.Application.Interfaces.Contracts
{
    public interface IActiveObjectsService
    {
        void AddOrProlong(string name);
        void Remove(string name);
        IList<Supervaiser> GetActiveRecords();
        string GetCandidate();
        void IncrementTask(string name);
    }
}
