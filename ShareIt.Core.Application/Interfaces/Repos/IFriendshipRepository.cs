﻿using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public interface IFriendshipRepository : IGenericRepository<Friendship>
    {
        Task DeleteAsync(string appProfileId, string friendId);
    }
}
