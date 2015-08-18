using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class Regist
    {
        private List<Participant> _list;

        public Regist()
        {
            CrimeType = new CrimeType();
            _list = new List<Participant>();
        }

        public Regist(int id, CrimeType crimeType, String description, DateTime date) : this()
        {
            Id = id;
            CrimeType = crimeType;
            Description = description;
            Date = date;
            _list = new List<Participant>();
        }

        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public CrimeType CrimeType { set; get; }

        [DataMember]
        public String Description { set; get; }

        [DataMember]
        public DateTime Date { set; get; }

        [DataMember]
        public Participant[] Participants { get; set; }


        public void SetParticipants()
        {
            if (Participants != null && Participants.Length != 0)
            {
                foreach (Participant participant in Participants)
                {
                    _list.Add(participant);
                }
                Participants = _list.ToArray();
                _list = new List<Participant>();
            }
        }

        public void AddParticipant(Person person)
        {
            var p = new Participant();
            p.Person = person;
            p.Regist = this;
            _list.Add(p);
        }

        public void AddParticipant(Participant participant)
        {
            participant.Regist = this;
            _list.Add(participant);
        }
    }
}