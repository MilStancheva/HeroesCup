﻿using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionsService
    {
        Task<MissionListModel> GetMissionListModelAsync(Guid? ownerId);

        Task<MissionEditModel> CreateMissionEditModelAsync(Guid? ownerId);
    }
}
