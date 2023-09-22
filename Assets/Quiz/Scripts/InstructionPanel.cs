using UnityEngine;
using TMPro;

public class InstructionsPanel : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public GameObject instructionsPanel;

    void Start()
    {
        // Initialize with instructions hidden.
        //HideInstructions();

        // Populate with formatted text.
        SetFormattedText();
    }

    void SetFormattedText()
    {
        // This is a simple example. Adjust this to fit your desired formatting and content!
        string content =
    "<color=#FFD700><size=60><b><u>INSTRUCTIONS:</u></b></size></color>\n\n" +
    "<size=40><i>1. Start the quiz by selecting a category.</i></size>\n\n" +
    "<size=40><i>2. Answer questions correctly to earn points (+5 points for each correct answer).</i></size>\n\n" +
    "<size=40><i>3. Answering questions incorrectly will deduct points (-5 points for each incorrect answer).</i></size>\n\n" +
    "<size=40><i>4. Use your points wisely:</i></size>\n" +
    "<size=30><color=#00FF00><b>- To regain a life, click on the heart icon when you have enough points.</b></color></size>\n" +
    "<size=30><color=#00FFFF><b>- To play the Battleship game on the left, you'll need 5 points per attempt.</b></color></size>\n" +
    "<size=30><color=#FFA400><b>- Win the Battleship game to unlock special rewards and power-ups!</b></color></size>\n\n" +
    "<size=40><i>5. To win the game, you must conquer the Battleship!</i></size>\n\n" +
    "<size=40><i>6. Enjoy and have fun!</i></size>";

        instructionsText.text = content;
        instructionsText.color = Color.red;

    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}
