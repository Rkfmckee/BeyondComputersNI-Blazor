﻿namespace BeyondComputersNi.Services.DataTransferObjects.Authentication;

public class AuthenticationDto
{
    public required string AuthToken { get; set; }
    public required DateTime AuthExpiration { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime RefreshExpiration { get; set; }
}
