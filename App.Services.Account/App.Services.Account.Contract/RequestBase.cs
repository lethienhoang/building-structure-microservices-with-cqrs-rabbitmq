using System;

namespace App.Services.Account.Contract
{
    public abstract class RequestBase
    {
        public Guid Id { get; set; }
    }
}
