using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(INetworkManager))]
    [RequireComponent(typeof(IItemManager))]
    // Singleton mgr pattern from Unity in Action (Chp8 - https://www.manning.com/books/unity-in-action-second-edition)
    public class Manager : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static INetworkManager NetworkManager { get; private set; }
        public static IItemManager ItemManager { get; private set; }

        private readonly List<IManager> _startupSequence = new List<IManager>();

        private void Awake()
        {
            NetworkManager = GetComponent<INetworkManager>();
            ItemManager = GetComponent<IItemManager>();

            _startupSequence.Add(NetworkManager);
            _startupSequence.Add(ItemManager);

            StartCoroutine(StartupManagers());
        }

        private IEnumerator StartupManagers()
        {
            foreach (var manager in _startupSequence)
            {
                manager.Startup();
            }

            yield return null;
        }
    }
}