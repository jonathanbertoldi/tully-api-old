using Microsoft.AspNetCore.Identity;

namespace Tully.Api.Identity
{
    public class AppIdentityErrorDescriber : IdentityErrorDescriber
    {
        public AppIdentityErrorDescriber() { }

        public override IdentityError ConcurrencyFailure()
        {
            return base.ConcurrencyFailure();
        }

        public override IdentityError DefaultError()
        {
            return base.DefaultError();
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return base.DuplicateRoleName(role);
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Já existe um usuário com o login informado."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return base.InvalidEmail(email);
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return base.InvalidRoleName(role);
        }

        public override IdentityError InvalidToken()
        {
            return base.InvalidToken();
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return base.InvalidUserName(userName);
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return base.LoginAlreadyAssociated();
        }

        public override IdentityError PasswordMismatch()
        {
            return base.PasswordMismatch();
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Senhas devem ter ao menos um dígito ('0'-'9')."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Senhas devem ter ao menos um caracter minúsculo ('a'-'z')."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Senhas devem ter ao menos um caracter não alfa-numérico."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Senhas devem ter ao menos um caracter maiúsculo ('A'-'Z')."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = "Senhas devem ter ao menos 6 caractéres."
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return base.UserAlreadyHasPassword();
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return base.UserAlreadyInRole(role);
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return base.UserLockoutNotEnabled();
        }

        public override IdentityError UserNotInRole(string role)
        {
            return base.UserNotInRole(role);
        }

    }
}
