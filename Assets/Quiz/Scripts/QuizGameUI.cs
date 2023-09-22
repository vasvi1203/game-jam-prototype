using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizManager quizManager;               //ref to the QuizManager script
    [SerializeField] private CategoryBtnScript categoryBtnPrefab;
    [SerializeField] private GameObject scrollHolder;
    [SerializeField] private TextMeshProUGUI scoreText, timerText;
    [SerializeField] public List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; //color of buttons
    [SerializeField] private Image questionImg;                     //image component to show image
    [SerializeField] private TextMeshProUGUI questionInfoText;                 //text to show question
    [SerializeField] private List<Button> options;                  //options button reference
#pragma warning restore 649

    public int correct_count = 0;
    private Question question;          //store current question data
    private bool answered = false;      //bool to keep track if answered or not

    public TextMeshProUGUI TimerText { get => timerText; }                     //getter
    public TextMeshProUGUI ScoreText { get => scoreText; }                     //getter
    public GameObject GameOverPanel { get => gameOverPanel; }                     //getter

    private void Start()
    {
        //add the listener to all the buttons
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        CreateCategoryButtons();

    }
    
    // Method which populates the questions on the screen
    public void SetQuestion(Question question)
    {
        //set the question
        this.question = question;

        //check for questionType
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    //activate image holder
                questionImg.transform.gameObject.SetActive(true);           //activate questionImg

                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
        }

        questionInfoText.text = question.questionInfo.ToString();                      //set the question text

        //shuffle the list of options
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //assign options to respective option buttons
        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = ansOptions[i].ToString();
            options[i].name = ansOptions[i].ToString();    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false;                       

    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    // Method assigned to the buttons
    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            // if answered is false
            if (!answered)
            {
                //set answered true
                answered = true;

                //get the bool value
                bool val = quizManager.Answer(btn.name);

                //if it is true
                if (val)
                {
                    //set color to correct
                    //btn.image.color = correctCol;
                    StartCoroutine(BlinkImg(btn.image));
                }
                else
                {
                    //else set it to wrong color
                    btn.image.color = wrongCol;
                }
            }
        }
    }

    // Method to create Category Buttons dynamically
    void CreateCategoryButtons()
    {
        // we loop through all the available categories in our QuizManager
        for (int i = 0; i < quizManager.QuizData.Count; i++)
        {
            // Create new CategoryBtn
            CategoryBtnScript categoryBtn = Instantiate(categoryBtnPrefab, scrollHolder.transform);
            // Set the button default values
            categoryBtn.SetButton(quizManager.QuizData[i].categoryName);
            int index = i;
            // Add listner to button which calls CategoryBtn method
            categoryBtn.Btn.onClick.AddListener(() => CategoryBtn(index, quizManager.QuizData[index].categoryName));
        }
    }

    // Method called by Category Button
    private void CategoryBtn(int index, string category)
    {
        quizManager.StartGame(index, category); //start the game
        mainMenu.SetActive(false);              //deactivate mainMenu
        gamePanel.SetActive(true);              //activate game panel
    }

    // this give blink effect
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            img.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RestryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
