using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] RectTransform _scoresContainer;
    [SerializeField] ScoreItem _scorePrefab ;
    [SerializeField] GameObject _noScoresGo;
    
    private SaveDataController _saveDataController = new();
    
    private void Start()
    {
        var scores = _saveDataController.GetScores();
        
        _noScoresGo.SetActive(scores.scores == null || scores.scores.Count == 0);

        foreach (var score in scores.scores)
        {
            var item = Instantiate(_scorePrefab, _scoresContainer, worldPositionStays: false);
            
            item.SetData(score.n, score.s);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
