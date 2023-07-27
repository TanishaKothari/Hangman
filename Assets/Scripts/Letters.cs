using UnityEngine;
using UnityEngine.UI;

public class Letters : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnLetterClick);
    }

    public void OnLetterClick()
    {
        char letter = gameObject.name[0];
        gameManager.CheckLetter(letter);
    }
}