using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.EssentialKit;

public class MailComposerInvite : MonoBehaviour
{
    [SerializeField] string subject;
    [SerializeField] string body;

    bool canSendMail = MailComposer.CanSendMail();

    void Start()
    {
        AddressBookService.instance.ReadContacts();
    }

    void Update()
    {

    }

    public void SendTextMail()
    {
        IAddressBookContact[] addressContacts = AddressBookService.instance.addressContacts;

        if (addressContacts != null && addressContacts.Length > 0)
        {
            // Create an instance of the mail composer
            MailComposer composer = MailComposer.CreateInstance();

            // Set the recipients from the address book
            List<string> toRecipients = new List<string>();
            foreach (var contact in addressContacts)
            {
                // Check if the contact has an email address
                if (contact.EmailAddresses != null && contact.EmailAddresses.Length > 0)
                {
                    toRecipients.Add(contact.EmailAddresses[0]);
                }
            }

            // Set other mail composer properties
            composer.SetToRecipients(toRecipients.ToArray());
            composer.SetSubject(subject);
            composer.SetBody(body, false);
            composer.SetCompletionCallback((result, error) =>
            {
                Debug.Log("Mail composer was closed. Result code: " + result.ResultCode);
            });

            // Show the mail composer
            composer.Show();
        }
        else
        {
            Debug.Log("No contacts available to send mail to.");
        }

    }
}
