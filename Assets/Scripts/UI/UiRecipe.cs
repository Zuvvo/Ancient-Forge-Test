using UnityEngine;
using UnityEngine.UI;

public class UiRecipe : MonoBehaviour
{
    [SerializeField] private Image _firstItem;
    [SerializeField] private GameObject _plus;
    [SerializeField] private Image _secondItem;
    [SerializeField] private Image _outputItem;
    [SerializeField] private Button _startButton;

    private UiMachine _uiMachine;
    private RecipeViewModel _recipeViewModel;
    public void Setup(UiMachine uiMachine, RecipeViewModel recipeViewModel)
    {
        _firstItem.sprite = recipeViewModel.Ingredients[0].Sprite;
        if(recipeViewModel.Ingredients.Length > 1)
        {
            _secondItem.sprite = recipeViewModel.Ingredients[1].Sprite;
        }
        else
        {
            _plus.SetActive(false);
            _secondItem.gameObject.SetActive(false);
        }
        _outputItem.sprite = recipeViewModel.Output.Sprite;

        _uiMachine = uiMachine;
        _recipeViewModel = recipeViewModel;
    }

    public void OnStartButtonClicked()
    {
        _uiMachine.OnRecipeStartClicked(_recipeViewModel.Id);
    }

    public void SetButtonInteractableState(bool state)
    {
        _startButton.interactable = state;
    }
}
