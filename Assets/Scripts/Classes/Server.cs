using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Server {

    public string serverName;
    public string serverIp;

    public Server()
    {
        serverName = "New Server";
        serverIp = "0.0.0.0";
    }

    public Server(string _serverName, string _serverIp)
    {
        serverName = _serverName;
        serverIp = _serverIp;
    }
}

[System.Serializable]
public class ServerList
{

    public List<Server> servers;

    public void Add(Server _server)
    {
        servers = LoadServerList().servers;

        servers.Add(_server);

        SaveServerList();
    }

    public void Remove(Server _server)
    {
        servers = LoadServerList().servers;

        servers.Remove(_server);

        SaveServerList();
    }

    void SaveServerList()
    {
        string jsonString = JsonUtility.ToJson(this);
        try
        {
            File.WriteAllText(Application.dataPath + "/Servers.json", jsonString.ToString());
            Debug.Log("Saving servers file.");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Cannot find servers file. Creating a new one.");
            Directory.CreateDirectory(Application.dataPath + "/Servers.json");
        }
    }

    public ServerList LoadServerList()
    {
        ServerList _serverList = new ServerList();
        try
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Servers.json");
            _serverList = JsonUtility.FromJson<ServerList>(jsonString);
        }
        catch (System.Exception)
        {
            SaveServerList();
        }
        Debug.Log("Loading servers file.");
        return _serverList;
    }
}
