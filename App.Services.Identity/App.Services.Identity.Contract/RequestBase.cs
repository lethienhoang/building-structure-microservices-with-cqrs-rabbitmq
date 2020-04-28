using System;

namespace App.Services.Identity.Contract
{
    public abstract class RequestBase
    {
        public Guid Id { get; set; }
    }
}
