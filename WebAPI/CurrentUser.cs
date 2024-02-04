namespace WebAPI
{
    public static class CurrentUser
    {
        public static long? Get(HttpContext httpContext)
        {
            try
            {
                var ccl = httpContext.User.Claims.Where(k => k.Type.Contains("nameidentifier")).FirstOrDefault();
                if (ccl != null)
                {
                    if (long.TryParse(ccl.Value, out long result))
                    {
                        return result;
                    }
                    else
                    {
                        return null; // Dönüşüm başarısız olduysa null dönebilirsiniz.
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
