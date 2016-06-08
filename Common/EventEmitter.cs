using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EventEmitter
    {
        public static ConcurrentDictionary<Type, Dictionary<object, List<Action<object, object>>>> _subscriptions = new ConcurrentDictionary<Type, Dictionary<object, List<Action<object,object>>>>();
        public static void ListenEvent(this object subscriber ,Type subscription, Action<object, object> action)
        {
            if (!_subscriptions.ContainsKey(subscription))
            {
                _subscriptions.TryAdd(subscription, new Dictionary<object, List<Action<object, object >>>());
            }
            if (!_subscriptions[subscription].ContainsKey(subscriber))
            {
                _subscriptions[subscription].Add(subscriber, new List<Action<object,object>>());
            }
            if (!_subscriptions[subscription][subscriber].Contains(action))
            {
                _subscriptions[subscription][subscriber].Add(action);
            }
        }
        public static void ListenEventAsync(this object subscriber, Type subscription, Action<object, object> action)
        {
            Task.Run(() =>
            {
                if (!_subscriptions.ContainsKey(subscription))
                {
                    _subscriptions.TryAdd(subscription, new Dictionary<object, List<Action<object, object>>>());
                }
                if (!_subscriptions[subscription].ContainsKey(subscriber))
                {
                    _subscriptions[subscription].Add(subscriber, new List<Action<object, object>>());
                }
                if (!_subscriptions[subscription][subscriber].Contains(action))
                {
                    _subscriptions[subscription][subscriber].Add(action);
                }
            });
        }

        public static void RemoveEventListening(this object subscriber,Type subscription)
        {
            if (_subscriptions.ContainsKey(subscription))
            {
                if (_subscriptions[subscription].ContainsKey(subscriber))
                {
                    if (_subscriptions[subscription].ContainsKey(subscriber))
                    {
                        _subscriptions[subscription][subscriber].Clear();
                        _subscriptions[subscription].Remove(subscriber);
                    }

                }
            }
        }

        public static void RemoveEventListeningAsync(this object subscriber, Type subscription)
        {
            Task.Run(() =>
            {
                if (_subscriptions.ContainsKey(subscription))
                {
                    if (_subscriptions[subscription].ContainsKey(subscriber))
                    {
                        if (_subscriptions[subscription].ContainsKey(subscriber))
                        {
                            _subscriptions[subscription][subscriber].Clear();
                            _subscriptions[subscription].Remove(subscriber);
                        }

                    }
                }
            });
        }

        public static void EmitEvent(this object source, Type subscription, object payload)
        {
            if (!_subscriptions.ContainsKey(subscription)) return;
            foreach (var item in _subscriptions[subscription])
            {
                if (item.Value != source)
                {
                    var tasks = new Task[item.Value.Count];
                    int i = 0;
                    foreach (var actions in item.Value)
                    {
                        tasks[i] = Task.Run(() => actions.Invoke(source, payload));
                        i++;
                    }
                    Task.WaitAll(tasks);
                }
            }
        }

        public static void EmitEventAsync(object source, Type subscription, object payload)
        {
            if (!_subscriptions.ContainsKey(subscription)) return;
            foreach (var item in _subscriptions[subscription])
            {
                if (item.Value != source)
                {
                    foreach (var actions in item.Value)
                    {
                        actions.Invoke(source, payload);
                    }
                }
            }
        }
    }
}
