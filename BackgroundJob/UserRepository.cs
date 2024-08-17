namespace BackgroundJob
{
    public class UserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User { Id = "1", Email = "bihuynh4297@gmail.com", LastUpdatePwd = DateTime.Now.AddMonths(-7), Status = "ACTIVE" },
                new User { Id = "2", Email = "huynhtrungnghia4297@gmail.com", LastUpdatePwd = DateTime.Now.AddMonths(-5), Status = "ACTIVE" },
                new User { Id = "3", Email = "ABC@gmail.com", LastUpdatePwd = DateTime.Now.AddMonths(-4), Status = "ACTIVE" },
                new User { Id = "4", Email = "XYZ@gmail.com", LastUpdatePwd = DateTime.Now.AddMonths(-8), Status = "ACTIVE" },
                new User { Id = "5", Email = "MMDDYYY@gmail.com", LastUpdatePwd = DateTime.Now.AddMonths(-10), Status = "ACTIVE" },

            };
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public void UpdateUser(User user)
        {
            var exitstingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (exitstingUser != null)
            {
                exitstingUser.Status = user.Status;
            }

        }
    }
}
