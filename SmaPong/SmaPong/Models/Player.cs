using System;

namespace SmaPong.Models
{
    public class Player
    {
        private string _nickname = string.Empty;

        public bool Active { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public double Mu { get; set; }

        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        public bool Notifications { get; set; }

        public double Sigma { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
    }
}