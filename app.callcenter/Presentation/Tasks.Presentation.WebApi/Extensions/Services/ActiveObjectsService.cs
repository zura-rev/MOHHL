using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Core.Application.Interfaces.Contracts;
using Tasks.Core.Domain.Common;

namespace Tasks.Presentation.WebApi.Extensions.Services
{
    public class ActiveObjectsService: IActiveObjectsService
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
        public void AddOrProlong(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                lock (synchronize)
                {
                    var activeObject = _activeObjects.FirstOrDefault(x => x.Name == name);

                    if (activeObject != null)
                    {
                        activeObject.Expiration = ExpirationTime();
                    }
                    else
                    {
                        _activeObjects.Add(new Supervaiser
                        {
                            Name = name,
                            Count = 0,
                            Expiration = ExpirationTime()
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
                lock (synchronize) _activeObjects.Remove(activeObject);
            }
               
        }

        //action
        public IList<Supervaiser> GetActiveRecords()
        {
            return _activeObjects.Where(x => x.Expiration > DateTime.Now).ToList();
        }

        //action
        public string GetCandidate()
        {
            var res =  _activeObjects.OrderBy(x => x.Count).First();
            return res.Name;
        }

        //action
        public void IncrementTask(string name)
        {
            var activeObject = _activeObjects.FirstOrDefault(x => x.Name == name);
            activeObject.Count = activeObject.Count + 1;
        }

    }

    //public class ActiveObjectsService
    //{
    //    private readonly object synchronize = new();
    //    /// <summary>
    //    /// ონლაინში დარჩენის დრო
    //    /// </summary>
    //    private readonly long _expirationInterval;
    //    /// <summary>
    //    /// ობიექტები მათი სიცოცხლის ხანგრძლივობით
    //    /// </summary>
    //    private readonly IDictionary<string, DateTime> _activeObjects;
    //    /// <summary>
    //    /// ვადის გასვლის დრო
    //    /// </summary>
    //    private DateTime ExpirationTime() => DateTime.Now.AddSeconds(_expirationInterval);

    //    public ActiveObjectsService(long expirationInterval = 60)
    //    {
    //        this._expirationInterval = expirationInterval;
    //        this._activeObjects = new Dictionary<string, DateTime>();
    //    }

    //    // action and middleware
    //    public void AddOrProlong(string name)
    //    {
    //        if (!string.IsNullOrWhiteSpace(name))
    //        {
    //            lock (synchronize)
    //            {
    //                if (_activeObjects.ContainsKey(name))
    //                { 
    //                    _activeObjects[name] = ExpirationTime(); 
    //                }
    //                else _activeObjects.TryAdd(name, ExpirationTime());
    //            }
    //        }
    //    }

    //    // action
    //    public void Remove(string name)
    //    {
    //        if (!string.IsNullOrWhiteSpace(name))
    //            lock (synchronize) _activeObjects.Remove(name);
    //    }

    //    // action
    //    public IList<string> GetActiveRecords()
    //    {
    //        return _activeObjects.Where(x => x.Value > DateTime.Now).Select(x => x.Key).ToList();
    //    }

    //}

}
