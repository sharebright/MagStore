using System.Linq;

namespace RavenDbMembership.UserStrings
{
    public enum Sr
    {
        [SrValue("A sample user friendly string with values 1 and 2.")]
        sample_string_resource_2,

        [SrValue("{0} cannot be empty.")]
        Parameter_can_not_be_empty_1,

        [SrValue("{0} is longer than maximum {1}.")]
        Parameter_too_long_2,

        [SrValue("{0} cannot include a comma.")]
        Parameter_can_not_contain_comma_1,

        [SrValue("Password needs at least {0} nonalphanumeric characters.")]
        Password_need_more_non_alpha_numeric_chars_1,

        [SrValue("Password does not meet minimum complexity requirements.")]
        Password_does_not_match_regular_expression,

        [SrValue("Password was not allowed.")]
        Membership_Custom_Password_Validation_Failure
    }

    public static class SRExtensions
    {
        public static string WithParameters(this Sr resource, params object[] parameters)
        {
            return string.Format(resource.GetUnformattedUserString(), parameters);
        }

        public static string GetUnformattedUserString(this Sr resource)
        {
            var type = typeof(Sr);
            var memInfo = type.GetMember(resource.ToString()).Single();
            var attribute = memInfo.GetCustomAttributes(typeof (SrValueAttribute), false).Single();
            return ((SrValueAttribute)attribute).Value;
        }
    }
}
