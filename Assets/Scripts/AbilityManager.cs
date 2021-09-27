using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] GameObject playerOBJ;
    static public AbilityManager _instance { get; private set; }
    public bool allowAbility;
    public bool abilityReady;
    bool allowSecondSentece;

    [InlineEditor]
    AbilityTrigger currentTriggerData;

    Sentence sentence1;
    Sentence sentence2;

    AbilityWords target;
    AbilityWords action;
    AbilityWords time;

    bool hasTarget;
    bool hasAction;
    bool hasTime;





    private void Awake()
    {
        if (_instance == null) _instance = this;

    }
    private void Update()
    {

        if (allowAbility)
        {
            if (!hasTarget)
            {
                //Target
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (currentTriggerData.PlataformActive != null)
                    {
                        target = AbilityWords.Plataform;
                        hasTarget = true;
                        Debug.Log("PLATAFORM");
                    }
                    else//não tem plataforma conectada
                    {
                        Debug.Log("Não tem plataforma");
                    }

                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (currentTriggerData.BreakableWallActive != null)
                    {
                        target = AbilityWords.Wall;
                        hasTarget = true;
                        Debug.Log("WALL");
                    }
                    else//não tem plataforma conectada
                    {
                        Debug.Log("Não tem parede");
                    }

                }
                //
            }

            else if (hasTarget && !hasAction)
            {
                //Action
                if (Input.GetKeyDown(KeyCode.I))
                {
                    action = AbilityWords.Move;
                    hasAction = true;
                    Debug.Log("MOVE");

                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    hasAction = true;
                    action = AbilityWords.Damage;
                    Debug.Log("DAMAGE");
                }
                //
            }

            else if (hasAction&&!hasTime)
            {
                //Time
                if (Input.GetKeyDown(KeyCode.I ))
                {
                    time = AbilityWords.Now;
                    hasTime = true;
                    Debug.Log("NOW");
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    time = AbilityWords.Later;
                    hasTime = true;
                    Debug.Log("LATER");
                }
                //
            }


            //setup
            if (Input.GetKeyDown(KeyCode.E))
            {

                AbilityManager._instance.BuildSentence(target, action, time);

            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AbilityManager._instance.ActivateSentences();
            }
            if (Input.GetKeyDown(KeyCode.T)) CleanChoices();
        }
    }
    private void CleanChoices()
    {
        target = AbilityWords.NONE;
        action = AbilityWords.NONE;
        time = AbilityWords.NONE;
        hasTarget = false;
        hasAction = false;
        hasTime = false;
        sentence1 = null;
        sentence2 = null;
    }
    public void BuildSentence(AbilityWords target, AbilityWords actionword, AbilityWords timeword)
    {
        Sentence aux = new Sentence();
        //escolher qual sentença será montada
        ChooseTarget(target, aux);
        ChooseAction(actionword, aux);
        ChooseTime(timeword, aux);
        if (sentence1 == null)
        {
            sentence1 = aux;
            Debug.Log("Primeira sentença criada");
        }
        else if (sentence2 == null && allowSecondSentece)
        {
            sentence2 = aux;
            Debug.Log("Segunda sentença criada");
        }
        else
        {
            Debug.Log("Sentenças já estão ocupadas");
        }
        //*************************************




    }
    public void ActivateSentences()
    {
        if (sentence1 == null && sentence2 == null)
        {
            Debug.Log("Não existe sentenças construidas");
        }
        else
        {
            if (sentence1 != null)
            {
                StartCoroutine(ActivateSentence1());
            }
            if (sentence2 != null)
            {
                // StartCoroutine(ActivateSentence2());
            }
        }
    }
    IEnumerator ActivateSentence1()
    {
        yield return new WaitForSeconds(sentence1.timeDelay);
        sentence1.target.GetComponentInChildren<IInteractableObjects>().CallWord(sentence1.actionWord);
        if (sentence2 != null)
        {
            sentence1.target.GetComponentInChildren<IInteractableObjects>().actionEnded += ActionSentence1EndedTrigger;
        }
        else
        {
            CleanChoices();
        }
    }
    IEnumerator ActivateSentence2()
    {
        yield return new WaitForSeconds(sentence2.timeDelay);


        sentence2.target.GetComponent<IInteractableObjects>().CallWord(sentence2.actionWord);
        sentence1.target.GetComponent<IInteractableObjects>().actionEnded -= ActionSentence1EndedTrigger;

        CleanChoices();

    }
    private void ActionSentence1EndedTrigger(object sender, System.EventArgs e)
    {
        StartCoroutine(ActivateSentence2());

    }
    void ChooseTarget(AbilityWords abilitywords, Sentence sentence)
    {
        switch (abilitywords)
        {
            case AbilityWords.Plataform: sentence.target = GetCurrentPlataform(); break;
            case AbilityWords.Wall: sentence.target = GetCurrentWall(); break;
        }


    }
    void ChooseAction(AbilityWords abilitywords, Sentence sentence)
    {
        sentence.actionWord = abilitywords;

    }
    void ChooseTime(AbilityWords abilitywords, Sentence sentence)
    {


        if (abilitywords == AbilityWords.Now)
        {
            sentence.timeDelay = 0f;

        }
        else if (abilitywords == AbilityWords.Later)
        {
            const float DELAY_TIME = 3f;
            sentence.timeDelay = DELAY_TIME;
        }
        else
        {
            Debug.LogError("Erro na escolha do delay da sentença");
        }

    }
    GameObject GetCurrentPlataform()
    {
        return currentTriggerData.PlataformActive;
    }
    GameObject GetCurrentWall()
    {
        return currentTriggerData.BreakableWallActive;

    }
    /*  GameObject GetClosestGameObject(List<GameObject> list)
      {

          GameObject closestGO = new GameObject();
          float closestDistance = Mathf.Infinity;
          foreach (GameObject GOmoveTarget in list)
          {
              if (Vector3.Distance(playerOBJ.transform.position, GOmoveTarget.transform.position) < closestDistance)
              {
                  closestDistance = Vector3.Distance(playerOBJ.transform.position, GOmoveTarget.transform.position);
                  closestGO = GOmoveTarget;
              }
          }
          return closestGO;
      }*/
    public void SetAbilityTriggerData(AbilityTrigger trigger)
    {
        currentTriggerData = trigger;
    }
    public class Sentence
    {
        public AbilityWords actionWord;
        public float timeDelay;

        public GameObject target;
    }
}

