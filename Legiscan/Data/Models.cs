using System.Net;

namespace Legiscan.Data
{
    public class Models
    {
        public class Bill
        {
            public string? bill_id { get; set; }
            public string? session_id { get; set; }
            public string? bill_number { get; set; }
            public string? status { get; set; }
            public string? status_desc { get; set; }
            public string? status_date { get; set; }
            public string? title { get; set; }
            public string? description { get; set; }
            public string? committee_id { get; set; }
            public string? last_action_date { get; set; }
            public string? last_action { get; set; }
            public string? url { get; set; }
            public string? state_link { get; set; }
            public List<PersonSponser> sponsors { get; set; }
        }
        public class Document
        {
            public int bill_id { get; set; }
            public int document_id { get; set; }
            public string document_type { get; set; }
            public int document_size { get; set; }
            public string document_mime { get; set; }
            public string document_desc { get; set; }
            public string url { get; set; }
            public string state_link { get; set; }
        }
        public class History
        {
            public int bill_id { get; set; }
            public DateTime date { get; set; }
            public string chamber { get; set; }
            public int sequence { get; set; }
            public string action { get; set; }
        }
        public class Person
        {
            public Nullable<int> people_id { get; set; }
            public string? name { get; set; }
            public string? first_name { get; set; }
            public string? last_name { get; set; }
            public string? middle_name { get; set; }
            public string? suffix { get; set; }
            public string? nickname { get; set; }
            public Nullable<int> party_id { get; set; }
            public char party { get; set; }
            public Nullable<int> role_id { get; set; }
            public string? role { get; set; }
            public string? district { get; set; }
            public Nullable<int> followthemoney_eid { get; set; }
            public Nullable<int> votesmart_id { get; set; }
            public Nullable<int> opensecrets_id { get; set; }
            public string? ballotpedia { get; set; }
            public Nullable<int> knowwho_pid { get; set; }
            public Nullable<int> committee_id { get; set; }
        }
        public class RollCall
        {
            public int bill_id { get; set; }
            public int roll_call_id { get; set; }
            public DateTime date { get; set; }
            public string chamber { get; set; }
            public string description { get; set; }
            public int yea { get; set; }
            public int nay { get; set; }
            public int nv { get; set; }
            public int absent { get; set; }
            public int total { get; set; }
        }
        public class Sponsor
        {
            public int bill_id { get; set; }
            public int people_id { get; set; }
            public int position { get; set; }
        }
        public class Session
        {
            public int session_id { get; set; }
            public int state_id { get; set; }
            public int year_start { get; set; }
            public int year_end { get; set; }
            public int prefile { get; set; }
            public int sine_die { get; set; }
            public int prior { get; set; }
            public int special { get; set; }
            public string session_tag { get; set; }
            public string session_title { get; set; }
            public string session_name { get; set; }
        }
        public class ApiResultPeople
        {
            public string status { get; set; }
            public SessionPeople sessionpeople { get; set; }
        }
        public class SessionPeople
        {
            public Session session { get; set; }
            public List<Person> people { get; set; }
        }
        public class MasterList
        {
            public string status { get; set; }
            public List<BillList> masterlist { get; set; }
        }
        public class BillList
        {
            public int bill_id { get; set; }
        }

        public class ApiResultBill
        {
            public string status { get; set; }
            public Bill bill { get; set; }
        }
        public class PersonSponser
        {
            public string? people_id { get; set; }
            public string? name { get; set; }
            public string? first_name { get; set; }
            public string? last_name { get; set; }
            public string? middle_name { get; set; }
            public string? suffix { get; set; }
            public string? nickname { get; set; }
            public string? party_id { get; set; }
            public string? party { get; set; }
            public string? role_id { get; set; }
            public string? role { get; set; }
            public string? district { get; set; }
            public string? followthemoney_eid { get; set; }
            public string? votesmart_id { get; set; }
            public string? opensecrets_id { get; set; }
            public string? ballotpedia { get; set; }
            public string? knowwho_pid { get; set; }
            public string? committee_id { get; set; }
            public string? ftm_eid { get; set; }
            public string? person_hash { get; set; }
            public string? state_id { get; set; }
            public string? bioguide_id { get; set; }
            public string? sponsor_type_id { get; set; }
            public string? sponsor_order { get; set; }
            public string? committee_sponsor { get; set; }
            public string? state_federal { get; set; }
        }
    }
}
