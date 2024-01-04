﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Models
{
    [Serializable]


    public class SyncableItem
    {
       private Guid _ID;
        private bool _Active;
        private int _OpPeriod;
      private DateTime _LastUpdatedUTC;

        public Guid ID { get { return _ID; } set => _ID = value; }
        public bool Active { get => _Active; set => _Active = value; }
        public int OpPeriod { get => _OpPeriod; set => _OpPeriod = value; }
        public DateTime LastUpdatedUTC { get => _LastUpdatedUTC; set => _LastUpdatedUTC = value; }


        public SyncableItem() { ID = Guid.NewGuid(); LastUpdatedUTC = DateTime.UtcNow; Active = true; }
    }
}
