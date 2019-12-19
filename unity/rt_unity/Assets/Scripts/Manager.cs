using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(INetworkManager))]
    // Singleton mgr pattern from Unity in Action (Chp8 - https://www.manning.com/books/unity-in-action-second-edition)
    public class Manager : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static INetworkManager NetworkManager { get; private set; }
        private readonly List<IManager> _startupSequence = new List<IManager>();

        private void Awake()
        {
            NetworkManager = GetComponent<INetworkManager>();

            _startupSequence.Add(NetworkManager);

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