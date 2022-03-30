using System;
using System.Collections.Generic;
using System.Linq;
using Hl.Core.Application.Interfaces.Contracts;
using Hl.Core.Domain.Common;
using MediatR;

namespace Hl.Presentation.WebApi.Extensions.Services
{
    public class ActiveObjectsService : IActiveObjectsService
    {
        private readonly object synchronize = new();
        /// <summary>
        /// ონლაინში დარჩენის დრო
        /// </summary>
        private readonly long _expirationInterval;
        /// <summary>
        /// ობიექტები მათი სიცოცხლის ხანგრძლივობით
        /// </summary>
        private IList<Supervaiser> _activeObjects;
        /// <summary>
        /// ვადის გასვლის დრო
        /// </summary>
        private DateTime ExpirationTime() => DateTime.Now.AddSeconds(_expirationInterval);

        public ActiveObjectsService(long expirationInterval = 3600)
        {
            this._expirationInterval = expirationInterval;
            this._activeObjects = new List<Supervaiser>();
        }

        //action and middleware
        public void AddOrProlong(string name, DateTime dayOfEntry) // DateTime departureDay
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                lock (synchronize)
                {
                    var activeObject = _activeObjects.FirstOrDefault(x => x.Name == name);

                    if (activeObject != null)
                    {
                        activeObject.Count = dayOfEntry.Date == DateTime.Now.Date ? activeObject.Count : 0;
                        activeObject.Expiration = ExpirationTime();
                        activeObject.IsActive = true;   
                    }
                    else
                    {
                        _activeObjects.Add(new Supervaiser
                        {
                            Name = name,
                            Count = 0,
                            Expiration = ExpirationTime(),
                            IsActive = true
                        });
                    }
                }
            }
        }

        //action
        public void Remove(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var activeObject = _activeObjects.FirstOrDefault(x => x.Name == name);
                //lock (synchronize) _activeObjects.Remove(activeObject);
                lock (synchronize)
                {
                    activeObject.Expiration = null;
                    activeObject.IsActive = false;
                }
            }
        }

        //action
        public IList<Supervaiser> GetActiveRecords()
        {
            //return _activeObjects.Where(x => x.Expiration > DateTime.Now).ToList();
            return _activeObjects.ToList();
        }

        //action
        public string GetCandidate()
        {
            try
            {
                var res = _activeObjects.Where(x => x.IsActive == true).OrderBy(x => x.Count).First();
                return res?.Name;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //action
        public void IncrementTask(string name)
        {
            var activeObject = _activeObjects.FirstOrDefault(x => x.Name == name);
            activeObject.Count = activeObject.Count + 1;
        }

    }


}
