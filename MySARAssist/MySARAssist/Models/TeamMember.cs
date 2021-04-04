using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MySARAssist.Models
{
    public class TeamMember
    {
        public TeamMember()
        {
            PersonID = Guid.NewGuid();
            MemberActive = true;
        }

        private Guid _PersonID;
        private string _Name;
        private string _Group;
        private string _Callsign;
        private string _Phone;
        private bool _RopeRescue;
        private bool _Tracker;
        private bool _FirstAid;
        private bool _GSAR;
        private bool _Swiftwater;
        private bool _MountainRescue;

        private string _SpecialSkills;
        private bool _isAssignmentTeamLeader;
        private string _Reference;
        private bool _GSTL = false;
        private bool _SARM;
        private string _barcode;
        private bool _signedIn;
        private Guid _organizationID;
        private Guid _userID;
        private bool _memberActive;
        private DateTime _lastUpdatedUTC;
        private string _Email;
        private Guid _CreatedByOrgID;
        private string _Address;
        private string _NOKName;
        private string _NOKRelation;
        private string _NOKPhone;
        private string _D4HStatus;
        private bool _CurrentlySelected;

        [PrimaryKey]
        public Guid PersonID { get => _PersonID; set => _PersonID = value; }

        public string Name { get => _Name; set => _Name = value; }

        public string Group { get => _Group; set => _Group = value; } //Use OrganizationName for this value
        public string NameWithGroup
        {
            get
            {
                if (!string.IsNullOrEmpty(Group)) { return Name + " (" + Group + ")"; }
                else { return Name; }
            }
        }
        public string NameWithTL
        {
            get
            {
                if (GSTL) { return Name + " (GSTL)"; }
                else { return Name; }
            }
        }
        public string NameWithSARM
        {
            get
            {
                if (SARM) { return Name + " (SAR Mgr)"; }
                else { return Name; }
            }
        }


        public string Callsign { get => _Callsign; set => _Callsign = value; }
        public string Phone { get => _Phone; set => _Phone = value; }
        public bool RopeRescue { get => _RopeRescue; set => _RopeRescue = value; }
        public bool Tracker { get => _Tracker; set => _Tracker = value; }
        public bool FirstAid { get => _FirstAid; set => _FirstAid = value; }
        public bool GSAR { get => _GSAR; set => _GSAR = value; }
        public bool Swiftwater { get => _Swiftwater; set => _Swiftwater = value; }
        public bool MountainRescue { get => _MountainRescue; set => _MountainRescue = value; }
        public string SpecialSkills { get => _SpecialSkills; set => _SpecialSkills = value; }
        public bool isAssignmentTeamLeader { get => _isAssignmentTeamLeader; set => _isAssignmentTeamLeader = value; }
        public string Reference { get { return _Reference; } set { _Reference = value; } }
        public bool GSTL { get { return _GSTL; } set { _GSTL = value; } }
        public bool SARM { get { return _SARM; } set { _SARM = value; } }
        public string Barcode { get { return _barcode; } set { _barcode = value; } }
        public bool SignedInToTask { get { return _signedIn; } set { _signedIn = value; } }
        public Guid OrganizationID { get => _organizationID; set => _organizationID = value; }
        public Guid UserID { get => _userID; set => _userID = value; }
        public bool MemberActive { get => _memberActive; set => _memberActive = value; }
        public DateTime LastUpdatedUTC { get => _lastUpdatedUTC; set => _lastUpdatedUTC = value; }
        public string Email { get => _Email; set => _Email = value; }
        public Guid CreatedByOrgID { get => _CreatedByOrgID; set => _CreatedByOrgID = value; } //used when a team creates members from outside their group for a task.
        public string Address { get => _Address; set => _Address = value; }
        public string AddressWithPhone
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(Address))
                {
                    sb.Append(Address.Replace("\r\n", " "));
                    if (!string.IsNullOrEmpty(Phone)) { sb.Append("\n"); }
                }
                if (!string.IsNullOrEmpty(Phone)) { sb.Append(Phone); }

                return sb.ToString();
            }
        }
        public string NOKName { get => _NOKName; set => _NOKName = value; }
        public string NOKRelation { get => _NOKRelation; set => _NOKRelation = value; }
        public string NOKPhone { get => _NOKPhone; set => _NOKPhone = value; }
        public string NOKOneLine
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(NOKName)) { sb.Append(NOKName); sb.Append(" "); }
                if (!string.IsNullOrEmpty(NOKRelation)) { sb.Append("("); sb.Append(NOKRelation); sb.Append(") "); }
                if (!string.IsNullOrEmpty(NOKPhone)) { sb.Append(NOKPhone); }
                return sb.ToString();
            }
        }
        public string D4HStatus { get => _D4HStatus; set => _D4HStatus = value; }
        public bool CurrentlySelected { get => _CurrentlySelected; set => _CurrentlySelected = value; }



        public string StringForQR
        {
            get
            {
                StringBuilder qr = new StringBuilder();
                qr.Append(PersonID.ToString()); qr.Append(";");

                qr.Append(Name); qr.Append(";");
                qr.Append(OrganizationID); qr.Append(";");
                qr.Append(Address); qr.Append(";");
                qr.Append(Phone); qr.Append(";");
                qr.Append(Email); qr.Append(";");
                qr.Append(Callsign); qr.Append(";");
                qr.Append(Reference); qr.Append(";");
                //qualifications
                if (GSAR) { qr.Append("1"); } else { qr.Append("0"); }
                if (GSTL) { qr.Append("1"); } else { qr.Append("0"); }
                if (SARM) { qr.Append("1"); } else { qr.Append("0"); }
                if (FirstAid) { qr.Append("1"); } else { qr.Append("0"); }
                if (RopeRescue) { qr.Append("1"); } else { qr.Append("0"); }
                if (Tracker) { qr.Append("1"); } else { qr.Append("0"); }
                if (Swiftwater) { qr.Append("1"); } else { qr.Append("0"); }
                if (MountainRescue) { qr.Append("1"); } else { qr.Append("0"); }
                qr.Append(";");

                //nok
                qr.Append(NOKName); qr.Append(";");
                qr.Append(NOKRelation); qr.Append(";");
                qr.Append(NOKPhone); qr.Append(";");


                return qr.ToString();
            }
        }
    }
}
