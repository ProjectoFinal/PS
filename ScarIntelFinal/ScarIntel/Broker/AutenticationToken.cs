using System;
using Domain;

namespace Broker
{
    internal class AutenticationToken
    {
        internal DateTime creation;
        internal Credential credential;
        internal DateTime expire;

        internal AutenticationToken(Credential credential)
        {
            this.credential = credential;
            creation = DateTime.Now;
            expire = creation.AddHours(1);
        }
    }
}