using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    //public GameValues gameValues;
    public bool stopSpawning = true;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDayOver;



    public static DeliveryManager Instance { get; private set; }


    public RecipeListSO recipeListSO;


    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 2f;
    private int waitingRecipesMax = 1;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start()
    {
        stopSpawning = true;
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsWaitingToStart() || GameValues.Instance.IsCountdownToStartActive() || GameValues.Instance.IsDayOver() || GameValues.Instance.IsGameOver() || GameValues.Instance.IsIntro())
        {
            stopSpawning = true;
        }
        else {
            stopSpawning = false;
        }

        if (GameValues.Instance.IsDayOver()) {
            waitingRecipeSOList.Clear();
            spawnRecipeTimer = spawnRecipeTimerMax;
            OnDayOver?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax && !stopSpawning) { 
                RecipeSO waitingRecipeSo = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSo);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    //im following a tutorial but if im honest this thing confuses me
    public void DeliverRecipe(PlateKitchenObj plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (ItemScriptableObj recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (ItemScriptableObj plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredient in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient Matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //This recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    //Player delivered the correct recipe!
                    
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    spawnRecipeTimer = spawnRecipeTimerMax;
                    return;
                }
            }
        }
        //No matches found
        //Player did not deliver correct recipe
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

}
