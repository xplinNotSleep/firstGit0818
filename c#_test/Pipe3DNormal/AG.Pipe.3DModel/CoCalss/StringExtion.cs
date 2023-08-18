namespace AG.Pipe.Analyst3DModel
{
    public static class StringExtion
    {
        public static bool IsNull(this string v)
        {
            return string.IsNullOrWhiteSpace(v);
        }
    }
}
