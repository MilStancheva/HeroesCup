using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class MissionContentsService : IMissionContentsService
    {
        private readonly HeroesCupDbContext dbContext;

        public MissionContentsService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MissionContent> GetMissionContentByMissionId(Guid missionId)
        {
            return await this.dbContext.MissionContents.FirstOrDefaultAsync(mc => mc.MissionId == missionId);
        }

        public async Task SaveOrUpdateMissionContent(MissionContent missionContent, Mission mission, bool commit)
        {
            if (mission.Content == null)
            {
                missionContent.Mission = mission;
                await this.dbContext.MissionContents.AddAsync(missionContent);
            }
            else
            {
                mission.Content.What = missionContent.What;
                mission.Content.When = missionContent.When;
                mission.Content.Where = missionContent.Where;
                mission.Content.Equipment = missionContent.Equipment;
                mission.Content.Why = missionContent.Why;
                mission.Content.Contact = missionContent.Contact;
            }

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
