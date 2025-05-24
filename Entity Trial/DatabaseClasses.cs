using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Trial
{
    public class Gage
    {
        public int GageId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Status
        public bool IsActive { get; set; }

        public string? CalibratedBy { get; set; }
        public DateTime? LastCalibration { get; set; }
        public DateTime? NextCalibrationDue { get; set; }

        // Info
        public string? InternalIdentNo { get; set; }
        public string? SerialNo { get; set; }
        public string? Type { get; set; }
        public string? ModelNo { get; set; }
        public string? Manufacturer { get; set; }
        public bool IsMetric { get; set; }
        public string? UnitOfMeasure { get; set; }
        public string? RangeOrSize { get; set; }
        public string? Accuracy { get; set; }
        public bool ReferenceStandard { get; set; }

        // Assignment
        public string? Owner { get; set; }
        public string? Area { get; set; }
        public string? Assignee { get; set; }

        // Calibration Info
        public string? Interval { get; set; }
        public string? Environment { get; set; }
        public string? ReferenceItem { get; set; }

        // Attachments: you may store file paths or blob references
        public ICollection<GageAttachment>? Attachments { get; set; }

        // History
        public ICollection<CalibrationHistory>? History { get; set; }
    }

    public class GageAttachment
    {
        public int GageAttachmentId { get; set; }
        public int GageId { get; set; }
        public string FilePath { get; set; } = null!;

        public Gage Gage { get; set; } = null!;
    }

    public class CalibrationHistory
    {
        public int CalibrationHistoryId { get; set; }
        public int GageId { get; set; }
        public DateTime Date { get; set; }
        public string? CalibratedBy { get; set; }
        public string? RangeOrSize { get; set; }
        public string? ReferenceItem { get; set; }
        public string? Area { get; set; }
        public string? Assignee { get; set; }

        public Gage Gage { get; set; } = null!;
    }
}
