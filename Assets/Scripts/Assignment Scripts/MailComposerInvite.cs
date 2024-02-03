using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.EssentialKit;

public class MailComposerInvite : MonoBehaviour
{
    // [SerializeField] string toRecipient;
    // [SerializeField] string ccRecipient;
    // [SerializeField] string bccRecipient;

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
            MailComposer composer = MailComposer.CreateInstance();

            List<string> toRecipients = new List<string>();
            foreach (var contact in addressContacts)
            {
                if (contact.EmailAddresses != null && contact.EmailAddresses.Length > 0)
                {
                    toRecipients.Add(contact.EmailAddresses[0]);
                }
            }
            composer.SetToRecipients(toRecipients.ToArray());

            composer.SetSubject(subject);
            composer.SetBody(body, false);
            composer.SetCompletionCallback((result, error) =>
            {
                Debug.Log("Mail composer was closed. Result code: " + result.ResultCode);
            });
            composer.Show();
        }
        else
        {
            Debug.Log("No Contacts Available");
        }

    }
}
