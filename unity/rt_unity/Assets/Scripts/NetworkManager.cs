using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityTemplateProjects;

public interface INetworkManager : IManager
{
    void StopPolling();
    void StartPolling();
}

public class NetworkManager : MonoBehaviour, INetworkManager
{
    private HttpClient _httpClient;

    public bool isPolling;
    public float pollingInterval = 5f;

    public ManagerState State { get; private set; }

    public void Startup()
    {
        State = ManagerState.Active;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8085/api/"),
            DefaultRequestHeaders =
            {
                Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
            },
            Timeout = TimeSpan.FromSeconds(pollingInterval - 1f)
        };

        isPolling = true;
        PollAsync();
    }

    async void PollAsync()
    {
        while (isPolling)
        {
            var itemList = (await GetItems()).items;
            var items = new Dictionary<int, Item>();

            foreach (var item in itemList)
            {
                items[item.Id ?? 0] = item;
            }

            await Task.Delay(TimeSpan.FromSeconds(pollingInterval));
        }
    }

    async Task<Item> GetItem(int id)
    {
        return JsonUtility.FromJson<Item>(await _httpClient.GetStringAsync($"item/{id}"));
    }

    async Task<ItemList> GetItems()
    {
        return JsonUtility.FromJson<ItemList>(await _httpClient.GetStringAsync($"item"));
    }

    public void StopPolling()
    {
        isPolling = false;
    }

    public void StartPolling()
    {
        isPolling = true;
    }
}