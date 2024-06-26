﻿using System.Text.RegularExpressions;

namespace HD.Core.DomainObjects;

public class Email
{
    public const int AddressMaxLength = 254;
    public const int AddressMinLength = 5;


    public string Address { get; private set; }


    protected Email() { }

    public Email(string address)
    {
        if (!Validate(address)) throw new DomainException("Invalid E-mail");

        Address = address;
    }

    public static bool Validate(string email)
    {
        var regex = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

        return regex.IsMatch(email);
    }
}