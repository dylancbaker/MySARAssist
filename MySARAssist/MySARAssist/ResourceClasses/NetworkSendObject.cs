using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MySARAssist.Models;
using NetworkCommsDotNet.DPSBase;
using NetworkCommsDotNet.Tools;
using ProtoBuf;

namespace MySARAssist.ResourceClasses
{
    [ProtoContract]
    [Serializable]
    public class NetworkSendObject : IExplicitlySerialize
    {
        [ProtoMember(1)]
        public Guid RequestID { get; set; }

        //for network reasons
        public ShortGuid SourceIdentifier { get { return new ShortGuid(_sourceIdentifier); } set { _sourceIdentifier = value; } }
        [ProtoMember(2)]
        string _sourceIdentifier;
        [ProtoMember(3)]
        public int RelayCount { get; set; }
        [ProtoMember(4)]
        public string SourceName { get; set; }
        [ProtoMember(5)]
        public string objectType { get; set; }
        [ProtoMember(6)]
        private Guid _guidValue = Guid.Empty;
        public Guid GuidValue { get { return _guidValue; } set { _guidValue = value; } }
        [ProtoMember(7)]
        public string comment { get; set; }

        [ProtoMember(20)]
        private TeamMember _teamMember = new TeamMember();
        public TeamMember teamMember { get => _teamMember; set => _teamMember = value; }

        [ProtoMember(24)]
        private List<TeamMember> _memberList;
        public List<TeamMember> memberList { get => _memberList; set => _memberList = value; }

        public void Serialize(Stream outputStream)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, this);
            }
        }

        public void Deserialize(Stream inputStream)
        {
            var obj = ProtoBuf.Serializer.Deserialize<NetworkSendObject>(inputStream);
            
        }




        /*
        [ProtoMember(8)]
        private Assignment _assignment = new Assignment();
        public Assignment assignment { get { return _assignment; } set { _assignment = value; } }
        [ProtoMember(9)]
        private Briefing _briefing = new Briefing(false);
        public Briefing briefing { get { return _briefing; } set { _briefing = value; } }
        [ProtoMember(10)]
        private IncidentObjective _objective = new IncidentObjective();
        public IncidentObjective objective { get { return _objective; } set { _objective = value; } }
        [ProtoMember(11)]
        private SafetyPlan _safetyPlan = new SafetyPlan();
        public SafetyPlan safetyPlan { get { return _safetyPlan; } set { _safetyPlan = value; } }
        [ProtoMember(12)]
        private SubjectProfile _subjectProfile = new SubjectProfile();
        public SubjectProfile subjectProfile { get { return _subjectProfile; } set { _subjectProfile = value; } }
        [ProtoMember(13)]
        private CommsPlanItem _commsPlanItem = new CommsPlanItem();
        public CommsPlanItem commsPlanItem { get { return _commsPlanItem; } set { _commsPlanItem = value; } }
        [ProtoMember(14)]
        private OrganizationChart _organizationChart = new OrganizationChart();
        public OrganizationChart organizationChart { get { return _organizationChart; } set { _organizationChart = value; } }
        [ProtoMember(15)]
        private UrgencyCalculation _urgencyCalculation = new UrgencyCalculation();
        public UrgencyCalculation urgencyCalculation { get { return _urgencyCalculation; } set { _urgencyCalculation = value; } }
        [ProtoMember(16)]
        private MedicalPlan _medicalPlan = new MedicalPlan();
        public MedicalPlan medicalPlan { get { return _medicalPlan; } set { _medicalPlan = value; } }
        [ProtoMember(17)]
        private CommsPlan _commsPlan = new CommsPlan();
        public CommsPlan commsPlan { get { return _commsPlan; } set { _commsPlan = value; } }
        [ProtoMember(18)]
        private WhiteboardItem _whiteboardItem = new WhiteboardItem();
        public WhiteboardItem whiteboardItem { get { return _whiteboardItem; } set { _whiteboardItem = value; } }
        [ProtoMember(19)]
        private SignInRecord _signInRecord = new SignInRecord();
        public SignInRecord signInRecord { get => _signInRecord; set => _signInRecord = value; }
        [ProtoMember(20)]
        private TeamMember _teamMember = new TeamMember();
        public TeamMember teamMember { get => _teamMember; set => _teamMember = value; }
        [ProtoMember(21)]
        private CommsLogEntry _commsLogEntry = new CommsLogEntry();
        public CommsLogEntry commsLogEntry { get => _commsLogEntry; set => _commsLogEntry = value; }
        [ProtoMember(22)]
        private Clue _clue;
        public Clue clue { get => _clue; set => _clue = value; }
        [ProtoMember(23)]
        private AssignmentDebrief _assignmentDebrief;
        public AssignmentDebrief assignmentDebrief { get => _assignmentDebrief; set => _assignmentDebrief = value; }
        [ProtoMember(24)]
        private List<TeamMember> _memberList;
        public List<TeamMember> memberList { get => _memberList; set => _memberList = value; }
        [ProtoMember(25)]
        private MattsonEvaluation _mattsonEvaluation;
        public MattsonEvaluation mattsonEvaluation { get => _mattsonEvaluation; set => _mattsonEvaluation = value; }
        [ProtoMember(26)]
        private MapSegment _mapSegment;
        public MapSegment mapSegment { get => _mapSegment; set => _mapSegment = value; }
        */
    }



}
