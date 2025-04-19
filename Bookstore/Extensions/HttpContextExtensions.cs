namespace Bookstore.API.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        ///  获取 用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string? GetUserId(this HttpContext context)
        {
            return context.Items["UserId"] as string;
        }
    }
}
