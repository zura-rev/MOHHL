using System;
using System.Collections.Generic;
using System.Linq;

namespace HL.Presentation.WebApi.Extensions.Services
{

    public class ActiveObjectsService
    {
        private readonly object synchronize = new();
        /// <summary>
        /// ონლაინში დარჩენის დრო
        /// </summary>
        private readonly long _expirationInterval;
        /// <summary>
        /// ობიექტები მათი სიცოცხლის ხანგრძლივობით
        /// </summary>
        private readonly IDictionary<string, DateTime> _activeObjects;
        /// <summary>
        /// ვადის გასვლის დრო
        /// </summary>
        private DateTime ExpirationTime() => DateTime.Now.AddSeconds(_expirationInterval);

        public ActiveObjectsService(long expirationInterval = 60)
        {
            this._expirationInterval = expirationInterval;
            this._activeObjects = new Dictionary<string, DateTime>();
        }

        // action and middleware
        public void AddOrProlong(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                lock (synchronize)
                {
                    if (_activeObjects.ContainsKey(name)) _activeObjects[name] = ExpirationTime();
                    else _activeObjects.TryAdd(name, ExpirationTime());
                }
            }
        }
        // action
        public void Remove(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                lock (synchronize) _activeObjects.Remove(name);
        }
        // action
        public IList<string> GetActiveRecords()
        {
            return _activeObjects.Where(x => x.Value > DateTime.Now).Select(x => x.Key).ToList();
        }
    }


    //public class ActiveObjectsService
    //{
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

    //    // action
    //    public void Add(string name)
    //    {
    //        if (!string.IsNullOrWhiteSpace(name))
    //        {
    //            if (_activeObjects.ContainsKey(name)) _activeObjects[name] = ExpirationTime();
    //            else _activeObjects.TryAdd(name, ExpirationTime());
    //        }
    //    }

    //    // action
    //    public void Remove(string name)
    //    {
    //        if (!string.IsNullOrWhiteSpace(name))
    //            _activeObjects.Remove(name);
    //    }

    //    // action
    //    public IList<string> GetActiveRecords()
    //    {
    //        return _activeObjects.Where(x => x.Value > DateTime.Now).Select(x => x.Key).ToList();
    //    }

    //    // middleware
    //    public void Prolong(string name)
    //    {
    //        if (!string.IsNullOrWhiteSpace(name) && _activeObjects.ContainsKey(name))
    //            _activeObjects[name] = ExpirationTime();
    //    }
    //}
}
