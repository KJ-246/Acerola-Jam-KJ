using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : MonoBehaviour, IKitchenObjectParent, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    private enum State { 
        Idle,
        Frying,
        Fried,
        Burnt,
    }

    // public ItemScriptableObj kitchenObjectSO;
    public FryingRecipeSO[] fryingRecipeSOArray;
    public BurningRecipeSO[] burningRecipeSOArray;


    public Transform counterPoint;
    private KitchenObj kitchenObject;


    private State state;
    private float fryingTimer;
    private float burntTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObj())
        {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObj().DestroySelf();

                        KitchenObj.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        burntTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObj().GetKitchenObjectSO());
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    burntTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burntTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burntTimer > burningRecipeSO.burningTimerMax)
                    {
                        //fried
                        GetKitchenObj().DestroySelf();

                        KitchenObj.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burnt;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                    break;
                case State.Burnt:
                    break;
            }
        }
    }

    public void Interact(CustomCursor customCursor)
    {
        if (!HasKitchenObj())
        {
            //customCursor.GetKitchenObj().SetKitchenObjectParent(customCursor);
            //There is no kitchen object here
            if (customCursor.HasKitchenObj())
            {
                if (hasRecipeWithInput(customCursor.GetKitchenObj().GetKitchenObjectSO()))
                {
                    //Carrying something that can be FRIED!!
                    customCursor.GetKitchenObj().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                }
                //Player is carrying something
            }
        }
        else
        {
            //There is a kitchen object here
            if (customCursor.HasKitchenObj())
            {
                //GetKitchenObj();
            }
            else
            {
                //player isnt carrying anythign
                GetKitchenObj().SetKitchenObjectParent(customCursor);

                state = State.Idle;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    
    private bool hasRecipeWithInput(ItemScriptableObj inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private ItemScriptableObj GetOutputForInput(ItemScriptableObj inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(ItemScriptableObj inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(ItemScriptableObj inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterPoint;
    }

    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        this.kitchenObject = kitchenObj;
    }

    public KitchenObj GetKitchenObj()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObj()
    {
        return kitchenObject != null;
    }
}
