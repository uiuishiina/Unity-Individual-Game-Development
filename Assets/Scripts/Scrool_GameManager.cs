using System;
using System.Threading.Tasks;
using UnityEngine;

public class Scrool_GameManager : MonoBehaviour
{
    [SerializeField, Header("ライトプール")] Scrool_Light_Pool pool;
    [SerializeField, Header("プレイヤー")] GameObject Player;
    [SerializeField, Header("床")] GameObject[] Floor;
    public bool is_Goal = false;
    
    void Start()
    {
        foreach (GameObject floor in Floor)
        {
            floor.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public async void PlayerMiss(Action action)
    {
        var g = pool.pool.Get();
        g.transform.position = Player.transform.position;

        foreach (GameObject floor in Floor)
        {
            floor.GetComponent<SpriteRenderer>().enabled = true;
        }

        g.GetComponent<Scroll_Light_Move>().BombLight();

        await Wait();
        action();

        foreach (GameObject floor in Floor)
        {
            floor.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void Goal() {
        is_Goal = true;
    }

    async Task Wait()
    {
        await Task.Delay(2000);
    }

}
