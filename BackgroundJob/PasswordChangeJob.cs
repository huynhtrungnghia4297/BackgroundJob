namespace BackgroundJob
{
    public class PasswordChangeJob
    {
        private readonly UserRepository _userRepository;
        private readonly EmailService _emailService;

        public PasswordChangeJob (UserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }       

        public void Execute ()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var users = _userRepository.GetUsers()
                .Where(u=>u.LastUpdatePwd <= sixMonthsAgo && u.Status != "REQUIRE_CHANGE_PWD")
                .ToList();

                    
            foreach (var user in users)
            {
                try
                {
                    user.Status = "REQUIRE_CHANGE_PWD"; 
                    _userRepository.UpdateUser(user);

                    _emailService.SendMail(
                   user.Email,
                   "Password Change Required",
                   "Your password has not been changed for over 6 months. Please update your password.");



                    Console.WriteLine($"Email sent to {user.Email}: Your password has not been changed for over 6 months. Please update your password.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email to {user.Email}. Error: {ex.Message}");
                }
            }
        }
    }
}
