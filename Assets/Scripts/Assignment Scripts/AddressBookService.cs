using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;

public class AddressBookService : MonoBehaviour
{
    public static AddressBookService instance;
    AddressBookContactsAccessStatus status;
    public IAddressBookContact[] addressContacts;
    public TMP_Text contactDisplayText;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    public void Invite()
    {
        AddressBookService.instance.ReadContacts();
    }

    public void ReadContacts()
    {
        status = AddressBook.GetContactsAccessStatus();

        if (status == AddressBookContactsAccessStatus.NotDetermined)
        {
            AddressBook.RequestContactsAccess(callback: OnRequestContactsAccessFinish);
        }
        if (status == AddressBookContactsAccessStatus.Authorized)
        {
            AddressBook.ReadContacts(OnReadContactFinish);
        }
    }

    private void OnRequestContactsAccessFinish(AddressBookRequestContactsAccessResult result, Error error)
    {
        Debug.Log("Request for contacts access finished");
        Debug.Log("Address book contacts access status: " + result.AccessStatus);

        if (result.AccessStatus == AddressBookContactsAccessStatus.Authorized)
        {
            AddressBook.ReadContacts(OnReadContactFinish);
        }
    }

    private void OnReadContactFinish(AddressBookReadContactsResult result, Error error)
    {
        if (error == null)
        {
            addressContacts = result.Contacts;

            UpdateContactDisplay();
            Debug.Log("Request to read contacts finished successfully.");
            Debug.Log("Total contacts fetched: " + addressContacts.Length);
            Debug.Log("Below are the contact details (capped to first 10 results only):");
            for (int iter = 0; iter < addressContacts.Length && iter < 10; iter++)
            {
                Debug.Log(string.Format("[{0}]: {1}", iter, addressContacts[iter]));
            }
        }
        else
        {
            Debug.Log("Request to read contacts failed with error. Error: " + error);
        }
    }

    private void UpdateContactDisplay()
    {
        string displayText = "Sr.No. FirstName LastNames PhoneNumber Email\n\n";

        for (int iter = 0; iter < addressContacts.Length && iter < 10; iter++)
        {
            string emailAddresses = string.Join(", ", addressContacts[iter].EmailAddresses);
            string phoneNumbers = string.Join(", ", addressContacts[iter].PhoneNumbers);

            displayText += $"{iter + 1}. {addressContacts[iter].FirstName} {addressContacts[iter].LastName} {phoneNumbers} {emailAddresses}\n";
        }
        contactDisplayText.text = displayText;
    }
}
