using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Scrool_Goal : MonoBehaviour
{
    [SerializeField,Header("ゴール表示")] GameObject Goal_image;

    void Start()
    {
        Goal_image.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Goal();
        }
    }

    async void Goal()
    {
        Goal_image.SetActive(true);
        await Wait();
        SceneManager.LoadScene("ScrollGame");
    }

    async Task Wait()
    {
        await Task.Delay(5000);
    }
}
