using System;
using System.Collections.Generic;
using Hl.Core.Domain.Common;

namespace Hl.Core.Application.Interfaces.Contracts
{
    public interface IActiveObjectsService
    {
        void AddOrProlong(string name, DateTime dayOfEntry);
        void Remove(string name);
        IList<Supervaiser> GetActiveRecords();
        string GetCandidate();
        void IncrementTask(string name);
    }
}
