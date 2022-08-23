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
    bool isDaughterWalking = true;
    bool isKagawaWalking = true;
    bool isAdvisorWalking = true;
    bool isAdvisorGesteringLeft = true;
    bool isAdvisorGesteringRight = true;
    //Considering trying to use Tags to GameObject.FindObjectsWithTag("") to get all of similar/the same items to be highlighted when just one is selected without clogging the god damn array

    void Start()
    {
        //arrays are added in Unity Editor, script is under PlayerBody in the XR rig, lists are automatic
        for (int i = 0; i < importantObjectsArray.Length; i++)
        {
            print("poop");
            isLookingAtObjectList.Add(false);
            isInTriggerList.Add(false);
            originalMaterialList.Add(importantObjectsArray[i].GetComponent<MeshRenderer>().material);
            //originalMaterialsArray[i] = importantThingos[i].GetComponent<MeshRenderer>().material;
        }
    }

    void Update()
    {
        JeffAnimator.SetBool("IsCrouching", isJeffCrouching);
        //JeffAnimator.SetBool("IsTurningRight", isJeffTurningRight); i'm not dealing with turning anymore
        DaughterAnimator.SetBool("IsWalking", isDaughterWalking);
        KagawaAnimator.SetBool("IsWalking", isKagawaWalking);
        AdvisorAnimator.SetBool("IsWalking", isAdvisorWalking);
        AdvisorAnimator.SetBool("IsPointingLeft", isAdvisorGesteringLeft);
        AdvisorAnimator.SetBool("IsPointingRight", isAdvisorGesteringRight);
        if (isInTriggerList[0] && isLookingAtObjectList[0])
        {
            //textPanel.SetActive(true);
            //importantObjectsArray[0].GetComponent<MeshRenderer>().material = highlightMaterial;
            //ObjDescriptionText.text = "This Cube is an Ancient Handball before they invented rounded corners.";
            //ObjDescriptionText.fontSize = 150;
            HighlightWithText(0, "This cube is an ancient handball before corners were invented.", 150);
            print("buts");
        }
        else if (isInTriggerList[1] && isLookingAtObjectList[1])
        {
            HighlightWithText(1, "This Cube is just a fancy rock that someone made.", 150);
            print("wow");
        }
        else if (isInTriggerList[2] && isLookingAtObjectList[2])
        {//first object that is made of parts, works perfectly as of 7/21/2022.
            HighlightWithText(2, "Nerds use these microscopes.", 150);
            importantObjectsArray[2].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            //unsure if i need this top one, but it's working and i don't wanna mess with it 
            print("labs");
        }
        else if (isInTriggerList[3] && isLookingAtObjectList[3])
        {
            HighlightWithText(3, "This lettuce is delicious, don't mind the bugs.", 150);
            importantObjectsArray[3].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
            print("labs");
        }

        else if (isInTriggerList[4] && isLookingAtObjectList[4])
        {
            HighlightWithText(4, "Try some Food, it's there AND overpriced!", 150);
            importantObjectsArray[4].GetComponentInChildren<MeshRenderer>().material = highlightMaterial;
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
            }//clear List and Array for parts, iffy on if array should be cleared, but nothing's broke so far
            originalMaterialsForPartsList.Clear();
            importantObjPartsArray = new GameObject[] { };//FLAG FOR POTENTIALLY BEING BAD.
        }
    }

    //This allows for things to be cleaning swapped in Update instead of having to change a lot of stuff manaually through multiple lines of code.
    private void HighlightWithText(int ObjNum, string ObjText, int TextSize)
    {
        print(ObjNum + " is curr ObjNum");
        textPanelArray[ObjNum].SetActive(true);
        if (ObjNum.Equals(2) || ObjNum.Equals(3))
        {//checking if the thing has a renderer with the NoMeshRenderer i made, meaning it's composed of parts
         //at least, that's what I TRIED to do, and it worked until i tried it with ONE other thing, which, yeah sure that's cool, whatever. Now it's hard coded, so whatever
            importantObjPartsArray = GameObject.FindGameObjectsWithTag("PartOfObj" + ObjNum);

            for (int i = 0; i < importantObjPartsArray.Length; i++)
            {//then go through that array and add their original materials to the parts' material list, and then highlight the part
                if (originalMaterialsForPartsList.Count < importantObjPartsArray.Length)
                {
                    print(importantObjPartsArray[i].ToString());
                    originalMaterialsForPartsList.Add(importantObjPartsArray[i].GetComponent<MeshRenderer>().material);
                }
                importantObjPartsArray[i].GetComponent<MeshRenderer>().material = highlightMaterial;
            }
        }
        else
        {//since it's made of parts, fill the parts array with every part that has a specific tag
            importantObjectsArray[ObjNum].GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        //change text when calling function
        ObjDescriptionTextArray[ObjNum].text = ObjText;
        ObjDescriptionTextArray[ObjNum].fontSize = TextSize;
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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
    public void LookedAtObj0()
    {

        isLookingAtObjectList[0] = true;
    }
    public void LookedAtObj1()
    {

        isLookingAtObjectList[1] = true;
    }
    public void LookedAtObj2Micro()//added specific flare to this because it's a special one
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
    public void HiDawson()
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
    //public void TurnAround()
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
