using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class TextPopup : MonoBehaviour
{
    public List<bool> isLookingAtObjectList;
    public List<bool> isInTriggerList;
    public GameObject[] importantObjectsArray;
    public Material highlightMaterial;
    public List<Material> originalMaterialList;
    public GameObject[] importantObjPartsArray;
    public List<Material> originalMaterialsForPartsList;
    public GameObject[] textPanelArray;
    public Text[] ObjDescriptionTextArray;
    public Animator JeffAnimator;
    public Animator DaughterAnimator;
    public Animator KagawaAnimator;
    public Animator AdvisorAnimator;
    bool isJeffCrouching = true;
    bool isDaughterWalking = false;
    bool isKagawaWalking = false;
    bool isAdvisorWalking = false;
    bool isAdvisorGesteringLeft = false;
    bool isAdvisorGesteringRight = false;
    //Considering trying to use Tags to GameObject.FindObjectsWithTag("") to get all of similar/the same items to be highlighted when just one is selected without clogging the god damn array
    // Any print statements are for the sake of checking if something went through/is being detected, if you feel they unneeded, go ahead and erase those print statements, shouldn't be necessary 
    void Start()
    {
        //arrays are added in Unity Editor, script is under PlayerBody in the XR rig, lists are automatic
        for (int i = 0; i < importantObjectsArray.Length; i++)
        {
            isLookingAtObjectList.Add(false);
            isInTriggerList.Add(false);
            originalMaterialList.Add(importantObjectsArray[i].GetComponent<MeshRenderer>().material);
            //originalMaterialsArray[i] = importantThingos[i].GetComponent<MeshRenderer>().material;
        }
    }

    void Update()
    {
        //Below is set up for changing animation states for each NPC, if they need more animations, add another statement like this after adding the bool to trigger it in Unity
        JeffAnimator.SetBool("IsCrouching", isJeffCrouching);
        //JeffAnimator.SetBool("IsTurningRight", isJeffTurningRight); i'm not dealing with turning anymore
        DaughterAnimator.SetBool("IsWalking", isDaughterWalking);
        KagawaAnimator.SetBool("IsWalking", isKagawaWalking);
        AdvisorAnimator.SetBool("IsWalking", isAdvisorWalking);
        AdvisorAnimator.SetBool("IsPointingLeft", isAdvisorGesteringLeft);
        AdvisorAnimator.SetBool("IsPointingRight", isAdvisorGesteringRight);
        if (isInTriggerList[0] && isLookingAtObjectList[0])
        {
            //legacyed stuff to show what logic is behind function,
            //textPanel.SetActive(true);
            //importantObjectsArray[0].GetComponent<MeshRenderer>().material = highlightMaterial;
            //ObjDescriptionText.text = "This Cube is an Ancient Handball before they invented rounded corners.";
            //ObjDescriptionText.fontSize = 150;
            HighlightWithText(0, "This cube is an ancient handball before corners were invented.", 150);
            print("testCube");
        }
        else if (isInTriggerList[1] && isLookingAtObjectList[1])
        {
            HighlightWithText(1, "This Cube is just a fancy rock that someone made.", 150);
            print("otherTestCube");
        }
        else if (isInTriggerList[2] && isLookingAtObjectList[2])
        {// for any of the the text
            HighlightWithText(2, "Nerds use these microscopes.", 150);
            importantObjectsArray[2].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            //unsure if i need this top one, but it's working and i don't wanna mess with it 
            print("labs");
        }
        else if (isInTriggerList[3] && isLookingAtObjectList[3])
        {
            HighlightWithText(3, "This lettuce is delicious, don't mind the bugs.", 150);
            importantObjectsArray[3].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            print("vegatables");
        }

        else if (isInTriggerList[4] && isLookingAtObjectList[4])
        {
            HighlightWithText(4, "Try some Food, it's there AND overpriced!", 150);
            importantObjectsArray[4].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            print("menu");
        }
        else if (isInTriggerList[5] && isLookingAtObjectList[5])
        {
            HighlightWithText(5, "This is a plaque for something that is probably important.", 150);
            importantObjectsArray[5].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            print("menu");
        }

        else
        {
            for (int i = 0; i < importantObjectsArray.Length; i++)
            {
                print("back to normal");
                textPanelArray[i].SetActive(false);
                importantObjectsArray[i].GetComponent<MeshRenderer>().material = originalMaterialList[i];
            }
            //below is for objects that are made of parts with seperate renderers and don't have a material/mesh renderer for the object itself
            for (int x = 0; x < importantObjPartsArray.Length; x++)
            {
                importantObjPartsArray[x].GetComponent<MeshRenderer>().material = originalMaterialsForPartsList[x];
            }//clear List and Array for parts, iffy on if array should be cleared, but nothing's broke so far. should be 100% ok
            originalMaterialsForPartsList.Clear();
            importantObjPartsArray = new GameObject[] { };//FLAG FOR POTENTIALLY BEING BAD. No problems so far
        }
    }

    //This allows for things to be cleaning swapped in Update instead of having to change a lot of stuff manaually through multiple lines of code.
    private void HighlightWithText(int ObjNum, string ObjText, int TextSize)
    {
        print(ObjNum + " is curr ObjNum");
        textPanelArray[ObjNum].SetActive(true);
        //i thought about having text popup in front of player, and have it stay in a certain spot in their line of sight, but elias said that might not be a good idea, plus, it's a lot harder to do
        // however, this would mean that you could have all menus have the text popup be visible to the player, 
        //currently highlighting menus are disabled, but you can easily turn them on and the non highlighting ones off easily
        importantObjPartsArray = GameObject.FindGameObjectsWithTag("PartOfObj" + ObjNum);

        //if it's made of parts, fill the parts array with every part that has a specific tag, if not, then the array is just one thing and works anyways
        // writing it like this eans you can apply the tag to anything and it'll light up as well, helps a lot when there are multiples or objects and you want them all to highlight
        for (int i = 0; i < importantObjPartsArray.Length; i++)
        {//then go through that array and add their original materials to the parts' material list, and then highlight the part
            if (originalMaterialsForPartsList.Count < importantObjPartsArray.Length)
            {
                print(importantObjPartsArray[i].ToString() + " is now Highlighted");
                originalMaterialsForPartsList.Add(importantObjPartsArray[i].GetComponent<MeshRenderer>().material);
            }
            importantObjPartsArray[i].GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        //change text when calling function
        ObjDescriptionTextArray[ObjNum].text = ObjText;
        ObjDescriptionTextArray[ObjNum].fontSize = TextSize;
    }

    private void OnTriggerEnter(Collider other)// trigger in question is a giant cube childed on each trigger zone, just turned off except for the ones labels otherwise, turn that on and make sure the tags are correct
    {
        //Remember to set AND MAKE the TriggerZones' SPECIFIC tags.
        for (int i = 0; i < isInTriggerList.Count; i++)
        {
            if (other.gameObject.tag == "TriggerForObj" + i)
            {
                isInTriggerList[i] = true;
                print("entered trigger zone " + i);
            }
        }
        //legacy stuff i'm not deleting yet, just in case it can be used/for reference
        //if (other.gameObject.tag == "TriggerForObj0")
        //{
        //    isInTriggerList[0] = true;
        //    print("entered");
        //}
        //else if (other.gameObject.tag == "TriggerForObj1")
        //{
        //    isInTriggerList[1] = true;
        //    print("entered");
        //}
        //else if (other.gameObject.tag == "TriggerForObj2")
        //{
        //    isInTriggerList[2] = true;
        //    print("entered");
        //}
        //if (other.gameObject.tag == "InteractableTrigger")
        //{
        //    isInTrigger = true;
        //    print("entered");
        //}
    }

    private void OnTriggerExit(Collider other)// shouldn't need to mess with this, just make sure everything else is set up correctly
    {
        for (int i = 0; i < isInTriggerList.Count; i++)
        {
            isInTriggerList[i] = false;
            //isInTrigger = false;
            print("exit");
        }

    }

    // Make sure this is applied to the object's "Hover Exited" part of the XR interable script in the Unity Inspector for any new objects. Should wipe all "LookedAtObj"s that are true.
    // Don't forget that this List's length (or count) is based on the GameObject array
    public void ObjectNotLookedAt()
    {
        for (int i = 0; i < isLookingAtObjectList.Count; i++)
        {
            isLookingAtObjectList[i] = false;
        }

    }

    //To add more objects to look at, duplicate a method below and add a number in sequence.
    //Make sure to add the method in the Unity Inspector, under the object's "Hover Entered" section in its XR interactable script.
    public void LookedAtObj0()// this was for testing to make sure it works before applying to any object, same applies to bottom function, can repurpose if wanted, is attached to cubes on pedastals.
    {

        isLookingAtObjectList[0] = true;
    }
    public void LookedAtObj1()
    {

        isLookingAtObjectList[1] = true;
    }
    public void LookedAtObj2Micro()
    //added specific flare to this because it's a special one, except it's not anymore, but changing the name means you need to reapply it to every single thing that used this function,
    //safer to not touch it, but if you want, make sure you look all throughout the microscope and find where the function is used and replace it, or else something is going to be broken
    {

        isLookingAtObjectList[2] = true;
    }
    public void LookedAtObj3()
    {

        isLookingAtObjectList[3] = true;
    }
    public void LookedAtObj4()
    {

        isLookingAtObjectList[4] = true;
    }
    public void LookedAtObj5()
    {

        isLookingAtObjectList[5] = true;
    }
    public void HiDawson()//trigged currently via button in scene, can be called to change that state anywhere in code
    {
        if (isJeffCrouching)
        {
            isJeffCrouching = false;
        }
        else
        {
            isJeffCrouching = true;
        }
    }
    //public void TurnAround()// was used to trigger turn animation, was too complicated to make Dawson turn, so i moved him so we wouldn't have to
    //{
    //    if (isJeffTurningRight)
    //    {
    //        isJeffTurningRight = false;
    //    }
    //    else
    //    {
    //        isJeffTurningRight = true;
    //    }
    //}
}
