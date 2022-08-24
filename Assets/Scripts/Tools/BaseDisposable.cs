using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    internal class BaseDisposable : IDisposable
    {
        private bool _isDisposed;
        private List<IDisposable> _subscriptions;
        private List<GameObject> _objects;
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (_subscriptions != null)
                {
                    foreach (IDisposable subscription in _subscriptions)
                    {
                        subscription?.Dispose();
                    }
                    _subscriptions.Clear();
                }
                if (_objects != null)
                {
                    foreach (GameObject usedObject in _objects)
                    {
                        GameObject.Destroy(usedObject);
                    }
                    _objects.Clear();
                }
                OnDispose();
            }
        }
        public void AddSubscription(IDisposable disposable)
        {
            if (_subscriptions == null)
                _subscriptions = new List<IDisposable>();
            _subscriptions.Add(disposable);
        }
        public void AddGameObject(GameObject addedGameObject)
        {
            if (_objects == null)
                _objects = new List<GameObject>();
            _objects.Add(addedGameObject);
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}
