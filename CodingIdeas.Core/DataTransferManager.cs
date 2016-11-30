namespace CodingIdeas.Core
{
    public partial class DataTransferManager : IDataTransferManager
    {
        private readonly int PostsPerPage;
        private readonly int CommentsPerPage;
        private readonly int SavedPostsPerPage;

        public DataTransferManager(int postsPerPage, int commentsPerPage, int savedPostsPerPage)
        {
            PostsPerPage = postsPerPage;
            CommentsPerPage = commentsPerPage;
            SavedPostsPerPage = savedPostsPerPage;
        }
    }
}
