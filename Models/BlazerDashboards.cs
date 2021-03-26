using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Consolidation_REST.Models
{
    public partial class BlazerDashboards
    {
        public long Id { get; set; }
        public long? CreatorId { get; set; }
        public string Name { get; set; }
    }
}
