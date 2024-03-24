﻿using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Services.Services;

public class UserService(IRepository<User> userRepo, IMapper mapper) : IUserService
{
    public Task<bool> UserExistsAsync(string email)
    {
        return userRepo.Get().Where(u => u.Email == email).AnyAsync();
    }

    public Task<List<UserDto>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddUserAsync(UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        await userRepo.AddAsync(user);

        var success = await userRepo.SaveChangesAsync() > 0;

        return success;
    }

    public Task DeleteUserAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
