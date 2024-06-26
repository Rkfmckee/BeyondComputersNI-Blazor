﻿using BeyondComputersNi.Dal.Entities;

namespace BeyondComputersNi.Services.DataTransferObjects;

public class UserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Name { get; set; }

    public List<Computer>? Computers { get; set; }
}
