using Data;
using System;

namespace Biz.Data
{
    [TableName("SITREM", "CALLS")]
    public class PhoneCall : BizObject
    {
        Decimal? _id;
        String _sid;
        String _agent;
        String _extension;
        String _callid;
        Decimal? _action;
        String _zone;
        String _phone;
        String _callingDevice;
        String _calledDevice;
        String _callData;
        Decimal? _callState;
        String _userData;
        String _userCreated;
        DateTime? _dateCreated;
        String _userUpdated;
        DateTime? _dateUpdated;
        Decimal? _line;
        DateTime? _callEnd;
        DateTime? _callStart;
        String _role;

        public PhoneCall() { }

        [Column("ID")]
        public Decimal? Id
        {
            get { return _id; }
            set { SetPropertyValue("Id", ref _id, value); }
        }

        [Column("SID")]
        public String Sid
        {
            get { return _sid; }
            set { SetPropertyValue("Sid", ref _sid, value); }
        }

        [Column("AGENT")]
        public String Agent
        {
            get { return _agent; }
            set { SetPropertyValue("Agent", ref _agent, value); }
        }

        [Column("EXTENSION")]
        public String Extension
        {
            get { return _extension; }
            set { SetPropertyValue("Extension", ref _extension, value); }
        }

        [Column("CALLID")]
        public String Callid
        {
            get { return _callid; }
            set { SetPropertyValue("Callid", ref _callid, value); }
        }

        [Column("ACTION")]
        public Decimal? Action
        {
            get { return _action; }
            set { SetPropertyValue("Action", ref _action, value); }
        }

        [Column("ZONE")]
        public String Zone
        {
            get { return _zone; }
            set { SetPropertyValue("Zone", ref _zone, value); }
        }

        [Column("PHONE")]
        public String Phone
        {
            get { return _phone; }
            set { SetPropertyValue("Phone", ref _phone, value); }
        }

        [Column("CALLING_DEVICE")]
        public String CallingDevice
        {
            get { return _callingDevice; }
            set { SetPropertyValue("CallingDevice", ref _callingDevice, value); }
        }

        [Column("CALLED_DEVICE")]
        public String CalledDevice
        {
            get { return _calledDevice; }
            set { SetPropertyValue("CalledDevice", ref _calledDevice, value); }
        }

        [Column("CALL_DATA")]
        public String CallData
        {
            get { return _callData; }
            set { SetPropertyValue("CallData", ref _callData, value); }
        }

        [Column("CALL_STATE")]
        public Decimal? CallState
        {
            get { return _callState; }
            set { SetPropertyValue("CallState", ref _callState, value); }
        }

        [Column("USER_DATA")]
        public String UserData
        {
            get { return _userData; }
            set { SetPropertyValue("UserData", ref _userData, value); }
        }

        [Column("USER_CREATED")]
        public String UserCreated
        {
            get { return _userCreated; }
            set { SetPropertyValue("UserCreated", ref _userCreated, value); }
        }

        [Column("DATE_CREATED")]
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set { SetPropertyValue("DateCreated", ref _dateCreated, value); }
        }

        [Column("USER_UPDATED")]
        public String UserUpdated
        {
            get { return _userUpdated; }
            set { SetPropertyValue("UserUpdated", ref _userUpdated, value); }
        }

        [Column("DATE_UPDATED")]
        public DateTime? DateUpdated
        {
            get { return _dateUpdated; }
            set { SetPropertyValue("DateUpdated", ref _dateUpdated, value); }
        }

        [Column("LINE")]
        public Decimal? Line
        {
            get { return _line; }
            set { SetPropertyValue("Line", ref _line, value); }
        }

        [Column("CALL_END")]
        public DateTime? CallEnd
        {
            get { return _callEnd; }
            set { SetPropertyValue("CallEnd", ref _callEnd, value); }
        }

        [Column("CALL_START")]
        public DateTime? CallStart
        {
            get { return _callStart; }
            set { SetPropertyValue("CallStart", ref _callStart, value); }
        }

        [Column("ROLE")]
        public String Role
        {
            get { return _role; }
            set { SetPropertyValue("Role", ref _role, value); }
        }

    }
}