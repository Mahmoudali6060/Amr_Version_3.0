using System;
using System.Collections.Generic;

namespace AMR_Server.Domain.Entities
{
    public partial class MeterReading
    {
        public decimal ReadingId { get; set; }
        public string MeterId { get; set; }
        public string ComModuleId { get; set; }
        public bool? ComModulePort { get; set; }
        public decimal? GatewayId { get; set; }
        public decimal? ObisId { get; set; }
        public DateTime? MeterConsumptionDatetime { get; set; }
        public decimal? MeterConsumptionValue { get; set; }
        public decimal? MeterConsumptionUnitId { get; set; }
        public decimal? ConsumptionFactor { get; set; }
        public decimal? ConsumptionUnitId { get; set; }
        public DateTime? ServerReceiveDatetime { get; set; }
        public bool? DeleteStatus { get; set; }
        public short? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public short? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsBackflow { get; set; }
        public bool? IsInvalidRead { get; set; }

        public virtual Unit ConsumptionUnit { get; set; }
        public virtual UserBasicData CreatedUser { get; set; }
        public virtual Gateway Gateway { get; set; }
        public virtual Meter Meter { get; set; }
        public virtual ObisData Obis { get; set; }
        public virtual UserBasicData UpdatedUser { get; set; }
    }
}
