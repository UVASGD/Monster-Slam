using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI text, rematch;

    public List<string> lose_phrases = new List<string>() { "Sorry, folks!" };
    public List<string> win_phrases = new List<string>() { "GG" };

    List<MonsterBody> monsters = new List<MonsterBody>();
    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
        rematch.gameObject.SetActive(false);
        monsters = new List<MonsterBody>(FindObjectsOfType<MonsterBody>());
        foreach (MonsterBody m in monsters)
        {
            m.DeathEvent += DeathUpdate;
        }
    }

    public void DeathUpdate(MonsterBody mb)
    {
        text.gameObject.SetActive(true);
        monsters.Remove(mb);
        text.text = mb.monster_name + " has been eliminated! " + lose_phrases[Random.Range(0, lose_phrases.Count)];
        if (monsters.Count == 1)
        {
            Win(mb);
        }
        else if (monsters.Count == 0)
        {
            Draw();
        }
    }

    public void Win(MonsterBody mb)
    {
        rematch.gameObject.SetActive(true);
        text.text = mb.monster_name + " wins!" + win_phrases[Random.Range(0, win_phrases.Count)];
    }

    public void Draw()
    {
        rematch.gameObject.SetActive(true);
        text.text = "It's a draw! " + lose_phrases[Random.Range(0, lose_phrases.Count)];
    }

    public void Rematch() 
    {
        text.text = "";
        text.gameObject.SetActive(false);
        rematch.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
