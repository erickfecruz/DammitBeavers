using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;

public class PlayersManager : MonoBehaviour
{
    private List<int> ids = new List<int>();
    private readonly List<Beaver> players = new List<Beaver>();

    public Beaver[] PlayersPrefab;

    public UnityAction<int> OnControllerConnected;
    public UnityAction<int> OnControllerDisconnected;

    void Start()
    {
        StartCoroutine(CoCheck());
        OnControllerConnected += CreatePlayer;
        OnControllerDisconnected += DestroyPlayer;
    }

    private void CreatePlayer(int id)
    {
        var player = Instantiate(PlayersPrefab[players.Count % PlayersPrefab.Length]);
        player.Id = id;
        var input = ScriptableObject.CreateInstance<GamepadInput>();
        input.id = id;
        player.input = input;
        players.Add(player);
    }

    private void DestroyPlayer(int id)
    {
        var pl = players.Find(p => p.Id == id);
        players.Remove(pl);
        Destroy(pl.gameObject);
    }

    IEnumerator CoCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Check();
        }
    }

    private void Check()
    {
        var list = Gamepad.all.Select(c => c.id).ToList();
        if (ids.EquivalentTo(list))
            return;
        foreach (var i in ids.Except(list))
            OnControllerDisconnected?.Invoke(i);
        foreach (var i in list.Except(ids))
            OnControllerConnected?.Invoke(i);
        ids = list;
    }

}
