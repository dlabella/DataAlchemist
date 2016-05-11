using Data;
using System;

namespace Biz.Data
{
    [TableName("GEMMA_OWN", "WORKITEM")]
    public class Workitem : BizObject
    {
        Decimal? _id;
        String _reference;
        Decimal? _statusId;
        DateTime? _initialDate;
        DateTime? _finishDate;
        Decimal? _typeId;
        Decimal? _emergencyLevelId;
        Decimal? _locationId;
        Decimal? _classificationId;
        String _closed;
        Decimal? _asignedUserId;
        Decimal? _workAdminLevelId;
        Decimal? _managementWalId;
        String _description;
        Decimal? _wiWfCauseId;
        Decimal? _priority;
        DateTime? _editableDate;
        Decimal? _trace;
        Decimal? _company;
        String _coordinated;
        Decimal? _altLocationId;
        Decimal? _idOperationalArea;
        Decimal? _agencyWalId;
        String _callId;
        String _endUserIdentifier;
        Decimal? _lotId;

        public Workitem() { }

        [Column("ID")]
        public Decimal? Id
        {
            get { return _id; }
            set { SetPropertyValue("Id", ref _id, value); }
        }

        [Column("REFERENCE")]
        public String Reference
        {
            get { return _reference; }
            set { SetPropertyValue("Reference", ref _reference, value); }
        }

        [Column("STATUS_ID")]
        public Decimal? StatusId
        {
            get { return _statusId; }
            set { SetPropertyValue("StatusId", ref _statusId, value); }
        }

        [Column("INITIAL_DATE")]
        public DateTime? InitialDate
        {
            get { return _initialDate; }
            set { SetPropertyValue("InitialDate", ref _initialDate, value); }
        }

        [Column("FINISH_DATE")]
        public DateTime? FinishDate
        {
            get { return _finishDate; }
            set { SetPropertyValue("FinishDate", ref _finishDate, value); }
        }

        [Column("TYPE_ID")]
        public Decimal? TypeId
        {
            get { return _typeId; }
            set { SetPropertyValue("TypeId", ref _typeId, value); }
        }

        [Column("EMERGENCY_LEVEL_ID")]
        public Decimal? EmergencyLevelId
        {
            get { return _emergencyLevelId; }
            set { SetPropertyValue("EmergencyLevelId", ref _emergencyLevelId, value); }
        }

        [Column("LOCATION_ID")]
        public Decimal? LocationId
        {
            get { return _locationId; }
            set { SetPropertyValue("LocationId", ref _locationId, value); }
        }

        [Column("CLASSIFICATION_ID")]
        public Decimal? ClassificationId
        {
            get { return _classificationId; }
            set { SetPropertyValue("ClassificationId", ref _classificationId, value); }
        }

        [Column("CLOSED")]
        public String Closed
        {
            get { return _closed; }
            set { SetPropertyValue("Closed", ref _closed, value); }
        }

        [Column("ASIGNED_USER_ID")]
        public Decimal? AsignedUserId
        {
            get { return _asignedUserId; }
            set { SetPropertyValue("AsignedUserId", ref _asignedUserId, value); }
        }

        [Column("WORK_ADMIN_LEVEL_ID")]
        public Decimal? WorkAdminLevelId
        {
            get { return _workAdminLevelId; }
            set { SetPropertyValue("WorkAdminLevelId", ref _workAdminLevelId, value); }
        }

        [Column("MANAGEMENT_WAL_ID")]
        public Decimal? ManagementWalId
        {
            get { return _managementWalId; }
            set { SetPropertyValue("ManagementWalId", ref _managementWalId, value); }
        }

        [Column("DESCRIPTION")]
        public String Description
        {
            get { return _description; }
            set { SetPropertyValue("Description", ref _description, value); }
        }

        [Column("WI_WF_CAUSE_ID")]
        public Decimal? WiWfCauseId
        {
            get { return _wiWfCauseId; }
            set { SetPropertyValue("WiWfCauseId", ref _wiWfCauseId, value); }
        }

        [Column("PRIORITY")]
        public Decimal? Priority
        {
            get { return _priority; }
            set { SetPropertyValue("Priority", ref _priority, value); }
        }

        [Column("EDITABLE_DATE")]
        public DateTime? EditableDate
        {
            get { return _editableDate; }
            set { SetPropertyValue("EditableDate", ref _editableDate, value); }
        }

        [Column("TRACE")]
        public Decimal? Trace
        {
            get { return _trace; }
            set { SetPropertyValue("Trace", ref _trace, value); }
        }

        [Column("COMPANY")]
        public Decimal? Company
        {
            get { return _company; }
            set { SetPropertyValue("Company", ref _company, value); }
        }

        [Column("COORDINATED")]
        public String Coordinated
        {
            get { return _coordinated; }
            set { SetPropertyValue("Coordinated", ref _coordinated, value); }
        }

        [Column("ALT_LOCATION_ID")]
        public Decimal? AltLocationId
        {
            get { return _altLocationId; }
            set { SetPropertyValue("AltLocationId", ref _altLocationId, value); }
        }

        [Column("ID_OPERATIONAL_AREA")]
        public Decimal? IdOperationalArea
        {
            get { return _idOperationalArea; }
            set { SetPropertyValue("IdOperationalArea", ref _idOperationalArea, value); }
        }

        [Column("AGENCY_WAL_ID")]
        public Decimal? AgencyWalId
        {
            get { return _agencyWalId; }
            set { SetPropertyValue("AgencyWalId", ref _agencyWalId, value); }
        }

        [Column("CALL_ID")]
        public String CallId
        {
            get { return _callId; }
            set { SetPropertyValue("CallId", ref _callId, value); }
        }

        [Column("END_USER_IDENTIFIER")]
        public String EndUserIdentifier
        {
            get { return _endUserIdentifier; }
            set { SetPropertyValue("EndUserIdentifier", ref _endUserIdentifier, value); }
        }

        [Column("LOT_ID")]
        public Decimal? LotId
        {
            get { return _lotId; }
            set { SetPropertyValue("LotId", ref _lotId, value); }
        }

    }
}