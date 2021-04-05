using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MySARAssist.Interfaces;
using MySARAssist.Models;
using System.Threading.Tasks;
using SQLite;

namespace MySARAssist.Services
{
    public class TeamMember_ManagerService : IDataStore<TeamMember>
    {
        private List<TeamMember> _AllTeamMembers;

        public TeamMember_ManagerService()
        {
            _AllTeamMembers = new List<TeamMember>();
            RefreshMembersListFromDB();
        }

        private async void RefreshMembersListFromDB()
        {
            IEnumerable<TeamMember> members = await GetItemsAsync();
            _AllTeamMembers.AddRange(members);
        }

        public async Task<bool> AddItemAsync(TeamMember item)
        {
            _AllTeamMembers.Add(item);
            saveToSQLite(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var oldItem = _AllTeamMembers.Where((TeamMember arg) => arg.PersonID == id).FirstOrDefault();
            _AllTeamMembers.Remove(oldItem);
            deleteFromSQLite(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TeamMember> GetItemAsync(Guid id)
        {
            return await Task.FromResult(_AllTeamMembers.FirstOrDefault(s => s.PersonID == id));
        }

        public async Task<IEnumerable<TeamMember>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_AllTeamMembers);
        }

      
        public async Task<bool> UpdateItemAsync(TeamMember item)
        {
            var oldItem = _AllTeamMembers.Where((TeamMember arg) => arg.PersonID == item.PersonID).FirstOrDefault();
            _AllTeamMembers.Remove(oldItem);
            _AllTeamMembers.Add(item);
            saveToSQLite(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpsertItemAsync(TeamMember item)
        {
            if(_AllTeamMembers.Where(o=>o.PersonID == item.PersonID).Any())
            {
                return await UpdateItemAsync(item);
            } else
            {
                return await AddItemAsync(item);
            }
        }

        private static bool saveToSQLite(TeamMember member)
        {
           
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<TeamMember>();
                int check = 0;
                if (isSavedToDB(member)) { check = conn.Update(member); }
                else { check = conn.Insert(member); }
                if (check > 0) { return true; }
                else { return false; }
            }
        }

        private static bool deleteFromSQLite(TeamMember member)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<TeamMember>();
                int check = 0;

                check = conn.Delete(member);

                if (check > 0) { return true; }
                else { return false; }
            }
        }

        private static bool isSavedToDB(TeamMember member)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<TeamMember>();
                List<TeamMember> members = conn.Table<TeamMember>().Where(o => o.PersonID == member.PersonID).ToList();
                return members.Any();
            }
        }

        public TeamMember GetCurrentTeamMember()
        {
            if(_AllTeamMembers != null && _AllTeamMembers.Where(o => o.CurrentlySelected).Any())
            {
                return _AllTeamMembers.Where(o => o.CurrentlySelected).FirstOrDefault();
            } else
            {
                return null;
            }
        }
    }
}
