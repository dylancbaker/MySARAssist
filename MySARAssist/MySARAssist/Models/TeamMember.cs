using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using SQLite;

namespace MySARAssist.Models
{
    [Serializable]
    [ProtoContract]
    public class TeamMember
    {
        public TeamMember()
        {
            PersonID = Guid.NewGuid();
            MemberActive = true;
            MemberOrganization = new Organization(new Guid("96BA69A4-436C-4DA1-85B1-992E84C36019"), "Unassigned");
        }

        [ProtoMember(1)]
        private Guid _PersonID;
        [ProtoMember(2)]
        private string _Name;
        [ProtoMember(3)]
        private string _Group;
        [ProtoMember(4)]
        private string _Callsign;
        [ProtoMember(5)]
        private string _Phone;
        [ProtoMember(6)]
        private bool _RopeRescue;
        [ProtoMember(7)]
        private bool _Tracker;
        [ProtoMember(8)]
        private bool _FirstAid;
        [ProtoMember(9)]
        private bool _GSAR;
        [ProtoMember(10)]
        private string _SpecialSkills;
        [ProtoMember(11)]
        private bool _isAssignmentTeamLeader;
        [ProtoMember(12)]
        private string _Reference;
        [ProtoMember(13)]
        private bool _GSTL = false;
        [ProtoMember(14)]
        private bool _SARM;
        [ProtoMember(15)]
        private string _barcode;
        [ProtoMember(16)]
        private bool _signedIn;
        [ProtoMember(17)]
        private Guid _organizationID;
        [ProtoMember(18)]
        private Guid _userID;
        [ProtoMember(19)]
        private bool _memberActive;
        [ProtoMember(20)]
        private DateTime _lastUpdatedUTC;
        [ProtoMember(21)]
        private string _Email;
        [ProtoMember(22)]
        private Guid _CreatedByOrgID;
        [ProtoMember(23)]
        private string _Address;
        [ProtoMember(24)]
        private string _NOKName;
        [ProtoMember(25)]
        private string _NOKRelation;
        [ProtoMember(26)]
        private string _NOKPhone;
        [ProtoMember(27)]
        private string _D4HStatus;
        
        [ProtoMember(29)]
        private bool _Swiftwater;
        [ProtoMember(30)]
        private bool _MountainRescue;
        [ProtoMember(31)]
        private int _D4HID;




        [ProtoIgnore]
        private bool _CurrentlySelected;
        [ProtoIgnore]
        private Organization _MemberOrganization;
        [ProtoIgnore]
        private double _PacesPer100;

        [PrimaryKey]
        public Guid PersonID { get => _PersonID; set => _PersonID = value; }
        public int D4HID { get => _D4HID; set => _D4HID = value; }
        public string Name { get => _Name; set => _Name = value; }

        public string Group { get => _MemberOrganization.OrganizationName; set => _MemberOrganization.OrganizationName = value; } //Use OrganizationName for this value
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
        public string NameWithGroupAndCurrent
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (CurrentlySelected) { sb.Append("*"); }
                sb.Append(Name);
                if (!string.IsNullOrEmpty(Group))
                {
                    sb.Append(" ("); sb.Append(Group); sb.Append(")");
                }
                return sb.ToString();
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
        public Guid OrganizationID { get => _MemberOrganization.OrganizationID; set => _MemberOrganization.OrganizationID = value; }
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
        
        [Ignore]
        public Organization MemberOrganization { get => _MemberOrganization; set => _MemberOrganization = value; }
        public double PacesPer100 { get => _PacesPer100; set => _PacesPer100 = value; }

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
