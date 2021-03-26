using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Consolidation_REST.Models
{
    public partial class BlazerAudits
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? QueryId { get; set; }
        public string Statement { get; set; }
        public string DataSource { get; set; }
    }
}
