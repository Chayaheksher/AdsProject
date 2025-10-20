using finalProject.Models;
using finalProject.Models1;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static finalProject.COMMON.Enums;

namespace finalProject;

public class ConnectionDAL
{
    private readonly AdCycContext _db;

    public ConnectionDAL()
    {
        _db = new AdCycContext();
    }

    public async Task<UserLogin> UserLogin(string userName, int userPassword)
    {
        // Retrieve user from the Users table based on userName and userPassword
        var user = _db.Users
            .Where(u => u.UserName == userName && u.Passwords == userPassword)
            .Select(u => new
            {
                u.UserId,
                u.UserName,
                u.CharactersId
            })
            .FirstOrDefault();

        if (user != null)
        {
            // Update LastEnterDate
            var userEntity = await _db.Users.FindAsync(user.UserId);
            userEntity.LastEnterDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            // Retrieve the character's name associated with the user
            var character = _db.Characters
                .Where(c => c.CharactersId == user.CharactersId)
                .Select(c => new
                {
                    c.CharactersName
                })
                .FirstOrDefault();

            if (character != null)
            {
                return new UserLogin
                {
                    userId = user.UserId,
                    userName = user.UserName,
                    charactersId = user.CharactersId,
                    charactersName = character.CharactersName
                };
            }
        }

        return new UserLogin
        {
            charactersId = -2,
            charactersName = "שם לא קיים",
            userName = "שם לא קיים",
            userId = -2
        };
    }
}


