using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;

public class Player : MonoBehaviour
{
    public DynamicCharacterAvatar characterAvatar;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterAvatar = GetComponent<DynamicCharacterAvatar>();
        characterAvatar.CharacterCreated.AddListener(OnCharacterCreated);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
    }

    void OnCharacterCreated(UMAData data){
        characterAvatar.LoadFromRecipeString(PlayerPrefs.GetString("CharacterData"));
        characterAvatar.BuildCharacter();
    }
}
