///Disables and enables some fields on register UI based on selection of account type

using UnityEngine;
using TMPro;

public class AccountFields : MonoBehaviour
{
    public TMP_Dropdown accountTypeData;
    public TMP_InputField ageRegisterField;    
    public TMP_Dropdown cpTypeField;
    public TMP_Dropdown gmfcField;

    private int acc_val;

    void Start()
    {
        //Gets the value of the option selected e.g. 0, 1...
        acc_val = accountTypeData.value;
    }

    void Update()
    {
        accountTypeData.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        //Gets the value of the option selected e.g. 0, 1...
        acc_val = accountTypeData.value;

        if (acc_val == 1) 
        {
            //If faculty account, disable age, CPtype and GMFC fields
            ageRegisterField.text = "";
            ageRegisterField.enabled = false;
            cpTypeField.value = 0;
            cpTypeField.enabled = false;
            gmfcField.value = 0;
            gmfcField.enabled = false;

            //Change color of disabled fields to grey as improvement         
            
        }
        else 
        {
            //If student account, disable age, CPtype and GMFC fields
            ageRegisterField.enabled = true;
            cpTypeField.enabled = true;
            gmfcField.enabled = true;
            }
    }
}
