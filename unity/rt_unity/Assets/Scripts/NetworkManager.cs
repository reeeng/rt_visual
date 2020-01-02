using System;
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

    private bool _isPolling;
    private readonly float pollingInterval = 5f;
    private string _baseUrl = "http://localhost:8085/api/";

    public ManagerState State { get; private set; }

    public void Startup()
    {
        State = ManagerState.Active;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl),
            DefaultRequestHeaders =
            {
                Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
            },
            Timeout = TimeSpan.FromSeconds(pollingInterval - 1f)
        };

        _isPolling = true;
        PollAsync();
    }

    async void PollAsync()
    {
        while (_isPolling)
        {
            List<Item> itemList;

            try
            {
                itemList = (await GetItems()).items;
            }
            catch
            {
                itemList = null;
            }

            if (itemList != null)
            {
                var items = new Dictionary<int, Item>();

                foreach (var item in itemList)
                {
                    items.Add(item.id , item);
                }

                Manager.ItemManager.OnFetchItems(items);
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
        _isPolling = false;
    }

    public void StartPolling()
    {
        _isPolling = true;
    }
}