using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{

    private DynamicCharacterAvatar characterAvatar;
    private Dictionary<string, DnaSetter> DNA;

    [SerializeField]
    private Slider heightSlider, muscleSlider, weightSlider;

    [SerializeField]
    private UMAWardrobeRecipe[] maleHair, femaleHair;

    [SerializeField]
    private Color[] skinColors;

    // Start is called before the first frame update
    void Start()
    {
        characterAvatar = GetComponent<DynamicCharacterAvatar>();
        characterAvatar.CharacterUpdated.AddListener(OnCharacterUpdated);
        characterAvatar.CharacterCreated.AddListener(OnCharacterCreated);

        heightSlider.onValueChanged.AddListener(OnHeightChange);    
        weightSlider.onValueChanged.AddListener(OnWeightChange);
        muscleSlider.onValueChanged.AddListener(OnMuscleChange);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCharacterCreated(UMAData data){
        DNA = characterAvatar.GetDNA();
    }

    void OnCharacterUpdated(UMAData data){
        DNA = characterAvatar.GetDNA();
        UpdateSliders();
    }

    void OnHeightChange(float height){
        DNA["height"].Set(height);
        characterAvatar.BuildCharacter();
    }

    void OnWeightChange(float weight){
        DNA["lowerWeight"].Set(weight);
        DNA["upperWeight"].Set(weight);
        characterAvatar.BuildCharacter();
    }

    void OnMuscleChange(float muscle){
        DNA["lowerMuscle"].Set(muscle);
        DNA["upperMuscle"].Set(muscle);
        characterAvatar.BuildCharacter();
    }

    public void ChangeHair(int hair){
        if (characterAvatar.activeRace.name == "HumanMaleDCS"){
            characterAvatar.SetSlot(maleHair[hair]);
        } else if (characterAvatar.activeRace.name == "HumanFemaleDCS"){
            characterAvatar.SetSlot(femaleHair[hair]);
        }

        characterAvatar.BuildCharacter();
    }

    private void OnDisable(){
        characterAvatar.CharacterUpdated.RemoveListener(OnCharacterUpdated);
        characterAvatar.CharacterCreated.RemoveListener(OnCharacterCreated);
        heightSlider.onValueChanged.RemoveListener(OnHeightChange);
        weightSlider.onValueChanged.RemoveListener(OnWeightChange);
        muscleSlider.onValueChanged.RemoveListener(OnMuscleChange);
    }

    public void ChangeSkinColor(int skinColor){
        characterAvatar.SetColor("Skin", skinColors[skinColor]);
        characterAvatar.UpdateColors(true);
    }

    public void ChangeSex(int sex){
        if (sex == 0){
            characterAvatar.ChangeRace("HumanFemaleDCS");
        } else if (sex == 1){
            characterAvatar.ChangeRace("HumanMaleDCS");
        }
        characterAvatar.BuildCharacter();
    }

    void UpdateSliders(){
        heightSlider.value = DNA["height"].Get();
        weightSlider.value = DNA["upperWeight"].Get();
        muscleSlider.value = DNA["upperMuscle"].Get();
    }

    public void SaveCharacter(){
        PlayerPrefs.SetString("CharacterData", characterAvatar.GetCurrentRecipe());
        print("saved");
    }
}
