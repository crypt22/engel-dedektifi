using EEE.Utils;

namespace EEE.Models
{
    public class SignupModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public bool IsValid(SignupModel model)
        {
            return !string.IsNullOrEmpty(model.Name)
                   && !string.IsNullOrEmpty(model.Surname)
                   && !string.IsNullOrEmpty(model.Email)
                   && !string.IsNullOrEmpty(model.Password)
                   && !string.IsNullOrEmpty(model.Email)
                   && model.Email.IsEmail();
        }
    }
}