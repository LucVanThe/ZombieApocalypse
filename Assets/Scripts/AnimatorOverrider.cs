using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorOverrideController AnimatorOverrideController;
    public AnimationClip ArmoAnimationClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //animator = GameObject.Find("AnimatorController").GetComponent<Animator>();
        ////create a new animator overrider controler
        //AnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        ////applies the new aoc to the player
        //animator.runtimeAnimatorController = AnimatorOverrideController;
    }
    //public void UnEquipAnimation()
    //{
    //        AnimatorOverrideController["Iidel"] = ArmoAnimationClip;
        
    //}
    //public void EquipAnimation(AnimationClip Idiel )
    //{
       
    //        AnimatorOverrideController["Idiel"] = ArmoAnimationClip;
        
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
