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
    public class Personnel_ManagerService : IDataStore<Personnel>
    {
        private List<Personnel> _AllTeamMembers;

        public Personnel_ManagerService()
        {
            _AllTeamMembers = new List<Personnel>();
            RefreshMembersListFromDB();
        }

        private async void RefreshMembersListFromDB()
        {
            IEnumerable<Personnel> members = GetItems();
            _AllTeamMembers.AddRange(members);
        }

        public async Task<bool> AddItemAsync(Personnel item)
        {
            _AllTeamMembers.Add(item);
            saveToSQLite(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var oldItem = _AllTeamMembers.Where((Personnel arg) => arg.PersonID == id).FirstOrDefault();
            _AllTeamMembers.Remove(oldItem);
            deleteFromSQLite(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAllItemsAsync()
        {
            List<Personnel> oldItems = new List<Personnel>(_AllTeamMembers);
            foreach(Personnel p in oldItems)
            {
                _AllTeamMembers.Remove(p);
                deleteFromSQLite(p);
            }
            

            return await Task.FromResult(true);
        }


        public async Task<Personnel> GetItemAsync(Guid id)
        {
            return await Task.FromResult(_AllTeamMembers.FirstOrDefault(s => s.PersonID == id));
        }

        public IEnumerable<Personnel> GetItems(bool forceRefresh = false)
        {
            /*
            if(forceRefresh || !_AllTeamMembers.Any())
            {
                _AllTeamMembers.Clear();
                _AllTeamMembers = GetTeamMembers();
            }*/
            List<Personnel> members = GetTeamMembers();
            List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);
            foreach (Personnel member in members)
            {
                if (member.OrganizationID != Guid.Empty && allOrgs.Any(o => o.OrganizationID == member.OrganizationID)) { member.MemberOrganization = allOrgs.Where(o => o.OrganizationID == member.OrganizationID).First(); }
            }
            return members;
        }


        public async Task<bool> UpdateItemAsync(Personnel item)
        {
            var oldItem = _AllTeamMembers.Where((Personnel arg) => arg.PersonID == item.PersonID).FirstOrDefault();
            _AllTeamMembers.Remove(oldItem);
            _AllTeamMembers.Add(item);
            saveToSQLite(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpsertItemAsync(Personnel item)
        {
            if (_AllTeamMembers.Where(o => o.PersonID == item.PersonID).Any())
            {
                return await UpdateItemAsync(item);
            }
            else
            {
                return await AddItemAsync(item);
            }
        }



        private static bool saveToSQLite(Personnel member)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Personnel>();
                int check = 0;
                if (isSavedToDB(member)) { check = conn.Update(member); }
                else { check = conn.Insert(member); }
                if (check > 0) { return true; }
                else { return false; }
            }
        }

        private static bool deleteFromSQLite(Personnel member)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Personnel>();
                int check = 0;

                check = conn.Delete(member);

                if (check > 0) { return true; }
                else { return false; }
            }
        }

        private static bool isSavedToDB(Personnel member)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Personnel>();
                List<Personnel> members = conn.Table<Personnel>().Where(o => o.PersonID == member.PersonID).ToList();
                return members.Any();
            }
        }

        public Personnel GetCurrentTeamMember()
        {
            if (_AllTeamMembers != null && _AllTeamMembers.Where(o => o.CurrentlySelected).Any())
            {
                return _AllTeamMembers.Where(o => o.CurrentlySelected).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async void setCurrentTeamMember(Guid selected_memberID)
        {
            foreach (Personnel m in _AllTeamMembers)
            {
                m.CurrentlySelected = false;
                saveToSQLite(m);
            }
            if (_AllTeamMembers.Where(o => o.PersonID == selected_memberID).Any())
            {
                _AllTeamMembers.Where(o => o.PersonID == selected_memberID).First().CurrentlySelected = true;
                //member.CurrentlySelected = true;
                await UpsertItemAsync(_AllTeamMembers.Where(o => o.PersonID == selected_memberID).First());
            }
        }

        private List<Personnel> GetTeamMembers()
        {
            List<Personnel> members = new List<Personnel>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Personnel>();
                members = conn.Table<Personnel>().OrderBy(o => o.Name).ToList();
                //assessments = assessments.Where(o => user.thisUserCanView(o)).ToList();


            }

            return members;
        }

        public Organization GetMostCommonOrganization()
        {
            Organization mostPopularOrg = null;

            List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);
            foreach (Organization org in allOrgs)
            {
                org.UserCount = _AllTeamMembers.Count(o => o.OrganizationID == org.OrganizationID);
            }
            if (allOrgs.Any(o => o.UserCount > 0))
            {
                mostPopularOrg = allOrgs.OrderByDescending(o => o.UserCount).First();
            }

            return mostPopularOrg;
        }

        Task<IEnumerable<Personnel>> IDataStore<Personnel>.GetItems(bool forceRefresh)
        {
            return (Task<IEnumerable<Personnel>>)GetItems(forceRefresh);
        }
    }

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
            IEnumerable<TeamMember> members = await GetItems();
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

        public async Task<IEnumerable<TeamMember>> GetItems(bool forceRefresh = false)
        {
            /*
            if(forceRefresh || !_AllTeamMembers.Any())
            {
                _AllTeamMembers.Clear();
                _AllTeamMembers = GetTeamMembers();
            }*/
            List<TeamMember> members = GetTeamMembers();
            List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);
            foreach(TeamMember member in members)
            {
                if(member.OrganizationID != Guid.Empty && allOrgs.Where(o=>o.OrganizationID == member.OrganizationID).Any()) { member.MemberOrganization = allOrgs.Where(o => o.OrganizationID == member.OrganizationID).First(); }
            }
            return members;
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

        public async void setCurrentTeamMember(Guid selected_memberID)
        {
            foreach(TeamMember m in _AllTeamMembers)
            {
                m.CurrentlySelected = false;
                saveToSQLite(m);
            }
            if (_AllTeamMembers.Where(o => o.PersonID == selected_memberID).Any())
            {
                _AllTeamMembers.Where(o => o.PersonID == selected_memberID).First().CurrentlySelected = true;
                //member.CurrentlySelected = true;
                await UpsertItemAsync(_AllTeamMembers.Where(o => o.PersonID == selected_memberID).First());
            }
        }

        private List<TeamMember> GetTeamMembers()
        {
            List<TeamMember> members = new List<TeamMember>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<TeamMember>();
                members = conn.Table<TeamMember>().OrderBy(o => o.Name).ToList();
                //assessments = assessments.Where(o => user.thisUserCanView(o)).ToList();

               
            }

            return members;
        }

        public Organization GetMostCommonOrganization()
        {
            Organization mostPopularOrg = null;

            List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);
            foreach(Organization org in allOrgs)
            {
                org.UserCount = _AllTeamMembers.Where(o => o.OrganizationID == org.OrganizationID).Count();
            }
            if(allOrgs.Where(o=>o.UserCount > 0).Any())
            {
                mostPopularOrg = allOrgs.OrderByDescending(o => o.UserCount).First();
            }

            return mostPopularOrg;
        }
    }
}
