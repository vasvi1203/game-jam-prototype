using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    [SerializeField] private QuizGameUI quizGameUI;
    [SerializeField] private QuizManager quizManager;

    //[SerializeField] private GameObject gameOverPanel; // Drag your Game Over panel in the Unity inspector.
    ////[SerializeField] private GameOverPanel gameOverTextUpdater; // This assumes you have attached the GameOverTextUpdater script to your Game Over panel or its child.

    //[SerializeField] private GameObject GameOver;
    //GameOver.
    //private

    public bool hasShip = false;
    private bool isRevealed = false;
    private Image tileImage;

    private void Awake()
    {
        tileImage = GetComponent<Image>();
        //gameOverTextUpdater = gameOverPanel.GetComponent<GameOverPanel>();
    }

    public void OnMouseDown()
    {
        if (!isRevealed && quizManager.gameScore >= 5)
        {
            if (hasShip)
            {
                tileImage.color = Color.green;
                quizGameUI.correct_count++;
                if (quizGameUI.correct_count == 3)
                {
                    //EndGame(true); // True means the player has won.
                    quizManager.GameEnd();
                    return;
                }
            }
            else
            {
                tileImage.color = Color.red;
            }
            isRevealed = true;
            quizManager.gameScore -= 5;
            quizGameUI.ScoreText.text = quizManager.gameScore.ToString();
        }
    }
}
