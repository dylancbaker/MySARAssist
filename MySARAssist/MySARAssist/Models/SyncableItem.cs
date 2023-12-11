﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Models
{
    [ProtoContract]
    [Serializable]
    [ProtoInclude(217, typeof(IncidentResource))]


    public class SyncableItem
    {
        [ProtoMember(1)] private Guid _ID;
        [ProtoMember(2)] private bool _Active;
        [ProtoMember(3)] private int _OpPeriod;
        [ProtoMember(4)] private DateTime _LastUpdatedUTC;

        public Guid ID { get { return _ID; } set => _ID = value; }
        public bool Active { get => _Active; set => _Active = value; }
        public int OpPeriod { get => _OpPeriod; set => _OpPeriod = value; }
        public DateTime LastUpdatedUTC { get => _LastUpdatedUTC; set => _LastUpdatedUTC = value; }


        public SyncableItem() { ID = Guid.NewGuid(); LastUpdatedUTC = DateTime.UtcNow; Active = true; }
    }
}
