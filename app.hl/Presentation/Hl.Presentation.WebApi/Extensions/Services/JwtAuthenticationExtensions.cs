using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hl.Presentation.Extensions.Services
{
    public static class JwtAuthenticationExtensions
    {
        /// <summary>
        /// ავთენთიფიკაციის პარამეტრების დამატება
        /// </summary>
        public static void AddJwtAuthenticationConfigs(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ClockSkew = TimeSpan.Zero // ანულებს ტოკენის სიცოცხლის ხანგრძლივობას. დეფოლტად არის 5 წუთი
                };
            });
        }

        /// <summary>
        /// ავტორიზაციის პარამეტრების დამატება
        /// </summary>
        public static void AddJwtAuthorizationConfigs(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                // მისი გამოყენება მოხდება [Authorize(Policy = "EditUsersPolicy")] ატრიბუტით
                options.AddPolicy("EditUsersPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.edit"));
                });
                options.AddPolicy("DeleteUsersPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.delete"));
                });
                options.AddPolicy("ViewUsersPolicy", policy =>
                {
                    policy.RequireAssertion(con => con.User.HasClaim(x => x.Type == "resources" && x.Value == "user.view"));
                });
            });
        }

        /// <summary>
        /// ტოკენის გენერირება
        /// </summary>
        public static string GenerateJwtToken(
            IConfiguration configuration,
            string personId,
            string userName,
            string firstName,
            string lastName,
            string privateNumber,
            params string[] permissions)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, personId),
                new Claim("UserName", userName),
                new Claim("FirstName", firstName),
                new Claim("LastName", lastName),
                new Claim("PrivateNumber", privateNumber)
            };

            foreach (var permission in permissions)
                claims.Add(new Claim("resources", permission));


            // ქმნის JWT ხელმოწერას
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    issuer: configuration["Token:Issuer"],
                    audience: configuration["Token:Audience"],
                    signingCredentials: signinCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}
