namespace JWTAPI.Authorization
{
    public class JwtCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, ITokenHandler tokenHandler)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var phone = tokenHandler.ValidateToken(token);
            if (phone != null)
            {
                var user = await userService.FindByPhone(phone);
                if (user != null)
                {
                    if(user.currentToken != null && user.currentToken == token)
                    {
                        // attach user to context on successful jwt validation

                        context.Items["User"] = user;
                    }
                }
                
            }

            await _next(context);
        }
    }
}
