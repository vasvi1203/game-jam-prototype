using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Text gameOverText; // The text component to change.
    //ref to the QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;

    // This method will update the text based on the correct_count value.
    public void UpdateGameOverText()
    {
        if (quizGameUI.correct_count >= 3) // Adjust this condition as needed.
        {
            gameOverText.text = "You Won!";
        }
        else
        {
            gameOverText.text = "You Lost!";
        }
    }

    // If you want the text to be updated every time the panel is activated:
    private void OnEnable()
    {
        UpdateGameOverText();
    }
}
