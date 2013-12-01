namespace ABE.LDraw.File
{
    public static class OtherFile
    {
        public static LdrFileElementBuilder IncludeFile(string file)
        {
            return new Constant("1 0 0 0 0 1 0 0 0 1 0 0 0 1 " + file);
        }
    }
}
