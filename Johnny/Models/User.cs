using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
//using EFUtil.Core.Indexes;
//using EFUtil.Core.Defaults;
//using EFUtil.Core.Conventions.Common;

namespace Johnny.Models
{
    //[Flags]
    //public enum UserRole : byte
    //{
    //    Undefined       = 0x0,
    //    Admin           = 0x1,
    //    Client          = 0x2,
    //    TrustedClient   = 0x4,
    //}

    public class User : Model
    {
        private static HashAlgorithm hasher = SHA1.Create();
        private static Encoding encoding = Encoding.ASCII;

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public virtual string Login { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public virtual string FirstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public virtual string LastName { get; set; }

        [NotMapped]
        public virtual string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        //[StringLength(20, MinimumLength=4)]
        //[DataType(DataType.Password)]
        //[NotMapped]
        //public virtual string Password
        //{
        //    get { return string.Empty; }

        //    set
        //    {
        //        PasswordHash = hash(value);
        //    }
        //}

        [Column("Password", TypeName="char")]
        [MaxLength(32)]
        public virtual string PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength=5)]
        [Column(TypeName="char")]
        public virtual string Phone { get; set; }

        public virtual bool IsAdmin { get; set; }

        public virtual bool IsMale { get; set; }

        //Властивості безпеки

        public virtual bool IsEmailVisible { get; set; }

        public virtual bool IsPhoneVisible { get; set; }

        public virtual bool IsEmailVerified { get; set; }

        public virtual bool IsOnline { get; set; }

        public virtual bool IsLockedOut { get; set; }

        public virtual byte FailedPasswordAttemptCount { get; set; }

        public User()
        {
            IsVisible = true;
            IsEmailVisible = true;
            IsPhoneVisible = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                var user = obj as User;
                return
                    user.Login == Login &&
                    user.PasswordHash == user.PasswordHash &&
                    user.Email == Email &&
                    user.Phone == Phone;
            }
            return false;
        }

        public void SetPassword(string password)
        {
            PasswordHash = hash(password);
        }

        public bool HasPassword(string password)
        {
            return PasswordHash.Equals(hash(password));
        }

        private string hash(string password)
        {
            return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(password)));
        }

        public byte[] GetPasswordHash()
        {
            return Convert.FromBase64String(PasswordHash);
        }

        public Secret GetSecret()
        {
            return new Secret
            {
                Id = Id,
                Password = GetPasswordHash()
            };
        }

        public struct Secret
        {
            public int Id;
            public byte[] Password;

        }
    }
}