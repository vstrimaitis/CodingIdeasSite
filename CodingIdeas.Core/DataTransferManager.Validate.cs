using CodingIdeas.Core.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodingIdeas.Core
{
    public partial class DataTransferManager
    {
        private const string _emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private const string _passwordPattern = "^[a-f0-9]{64}$";

        private void ValidateProgrammingLanguage(ProgrammingLanguage language, ProgrammingLanguageProperties props)
        {
            if (props.HasFlag(ProgrammingLanguageProperties.Name) && string.IsNullOrWhiteSpace(language.Name))
                throw new InvalidNameException();
        }

        private void ValidateComment(Comment comment, CommentProperties props)
        {
            if (comment.Content == null || props.HasFlag(CommentProperties.Content) && string.IsNullOrWhiteSpace(comment.Content))
                throw new InvalidContentException();
            if (props.HasFlag(CommentProperties.PublishDate) && comment.PublishDate > DateTime.Now)
                throw new InvalidPublishDateException("The publish date cannot be in the future.");
            using (var ctx = new DB.CodingIdeasEntities())
            {
                var post = ctx.Posts.Where(x => x.Id == comment.PostId).FirstOrDefault();
                if (props.HasFlag(CommentProperties.PostId) && post == null)
                    throw new PostNotFoundException();
                if (props.HasFlag(CommentProperties.PublishDate) && comment.PublishDate < post.RatableEntity.PublishDate)
                    throw new InvalidPublishDateException("The comment cannot be published before the post.");
                if (props.HasFlag(CommentProperties.AuthorId) && comment.AuthorId != null && ctx.Users.Where(x => x.Id == comment.AuthorId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
            }
        }

        private void ValidatePost(Post post, PostProperties props)
        {
            if (post.Content == null || props.HasFlag(PostProperties.Content) && string.IsNullOrWhiteSpace(post.Content))
                throw new InvalidContentException();
            if (post.Title == null || props.HasFlag(PostProperties.Title) && string.IsNullOrWhiteSpace(post.Title))
                throw new InvalidTitleException();
            if (props.HasFlag(PostProperties.PublishDate) && post.PublishDate > DateTime.Now)
                throw new InvalidPublishDateException("The publish date cannot be in the future.");
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (props.HasFlag(PostProperties.AuthorId) && post.AuthorId != null && ctx.Users.Where(x => x.Id == post.AuthorId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
            }
        }

        private void ValidateRating(Guid userId, Guid entityId, sbyte rating)
        {
            if (rating != -1 && rating != 1)
                throw new InvalidRatingException();
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Id == userId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
                if (ctx.RatableEntities.Where(x => x.Id == entityId).FirstOrDefault() == null)
                    throw new RatableEntityNotFoundException();
            }
        }

        private void ValidateSkill(Guid userId, Guid languageId, byte proficiency)
        {
            if (proficiency < 1 || proficiency > 5)
                throw new InvalidProficiencyException();
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Id == userId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
                if (ctx.ProgrammingLanguages.Where(x => x.Id == languageId).FirstOrDefault() == null)
                    throw new ProgrammingLanguageNotFoundException();
            }
        }

        private void ValidateUser(User user, UserProperties props)
        {
            if (user.Email == null || props.HasFlag(UserProperties.Email) && !Regex.IsMatch(user.Email, _emailPattern, RegexOptions.IgnoreCase))
                throw new InvalidEmailException();
            if (user.PasswordHash == null || props.HasFlag(UserProperties.PasswordHash) && !Regex.IsMatch(user.PasswordHash, _passwordPattern, RegexOptions.IgnoreCase))
                throw new InvalidPasswordHashException();
            if (user.DateOfBirth != null && props.HasFlag(UserProperties.DateOfBirth) && user.DateOfBirth >= DateTime.Now)
                throw new InvalidDateOfBirthException();
        }

        private void ValidateSavePost(Guid userId, Guid postToSaveId)
        {
            using (var ctx = new DB.CodingIdeasEntities())
            {
                if (ctx.Users.Where(x => x.Id == userId).FirstOrDefault() == null)
                    throw new UserNotFoundException();
                if (ctx.Posts.Where(x => x.Id == postToSaveId).FirstOrDefault() == null)
                    throw new PostNotFoundException();
            }
        }
    }
}
