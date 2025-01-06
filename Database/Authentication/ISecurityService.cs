﻿namespace Database.Authentication
{
    public interface ISecurityService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashPassword);
    }
}