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
    [SerializeField] string toRecipientEmail;

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
        MailComposer composer = MailComposer.CreateInstance();
        composer.SetToRecipients(toRecipientEmail);

        composer.SetSubject(subject);
        composer.SetBody(body, false);
        composer.SetCompletionCallback((result, error) =>
        {
            Debug.Log("Mail composer was closed. Result code: " + result.ResultCode);
        });
        composer.Show();

    }
}
