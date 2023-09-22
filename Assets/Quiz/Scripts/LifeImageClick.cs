using UnityEngine;
using UnityEngine.EventSystems;

public class LifeImageClick : MonoBehaviour, IPointerClickHandler
{
    // Reference to QuizGameUI script
    [SerializeField] private QuizGameUI quizGameUI;
    // Reference to QuizManager script
    [SerializeField] private QuizManager quizManager;

    // Method called when the image is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug logs to help you trace the logic
        Debug.Log("Life Image Clicked!");

        // Check if the game score is >= 5
        if (quizManager.gameScore >= 5)
        {
            Debug.Log("Score is greater than or equal to 5");

            // Check if remaining lives is less than 3
            if (quizManager.lifesRemaining < 3)
            {
                Debug.Log("Remaining lives are less than 3");

                // Increment the remaining lives by 1
                quizManager.lifesRemaining++;

                // Debug log to check the value
                Debug.Log("lifesRemaining after increment: " + quizManager.lifesRemaining);

                // Reset the color of the life image back to its original state
                quizGameUI.lifeImageList[quizManager.lifesRemaining - 1].color = Color.green;

                // Decrement the game score by 5
                quizManager.gameScore -= 5;

                // Update the game score UI
                quizGameUI.ScoreText.text = quizManager.gameScore.ToString();
            }
        }
    }
}
