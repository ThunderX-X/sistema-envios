﻿namespace Shipments.Service.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string? message) : base(message)
        {
        }
    }
}
