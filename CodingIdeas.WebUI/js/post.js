$(document).ready(function ()
{
    /*if(window.location.hash == "")
        window.location.hash = "#1";
    $('.navigationButtons').click(function(e){e.preventDefault(); return false;});
    $('.navigationButtons').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('disabled'))
            return false; // Do something else in here if required
    });*/
    loadTopPosters();
    loadPost(window.location.hash.substr(1));
    loadComments(window.location.hash.substr(1), 1);
    //loadPosts();

});

function buildComment(comment)
{
    var usernamePart = '<a href="user.html#'+comment.AuthorId+'">' + comment.AuthorUsername + "</a>";
    var publishDatePart = '<small>' + new Date(comment.PublishDate).toDateString() + '</small>';
    var deleteButtonPart = '';
    if(comment.AuthorId == localStorage.getItem("UserId"))
        deleteButtonPart = '<button class="btn btn-danger btn-xs" onclick="removeCommentClick(\''+comment.Id+'\')" ><span class="glyphicon glyphicon-trash"></span></button>'
    var headingPart = '<h4 class="media-heading">' +
                        usernamePart + 
                        publishDatePart +
                        deleteButtonPart + 
                        '</h4>';
    var full = '<div class="media">' + 
                    '<div class="media-body">' + 
                        headingPart + 
                        comment.Content + 
                    '</div>' + 
                '</div>' + 
                '<hr />';
    return full;
}

function removeCommentClick(id)
{
    $.ajax({
        url: URL + 'Comment/'+id,
        type: 'DELETE',
        success: function(response){
            loadComments(window.location.hash.substr(1), 1);
        }
    });
}

function submitComment()
{
    var postId = window.location.hash.substr(1);
    addComment(postId, localStorage.getItem("UserId"), document.getElementById('commentInput').value);
}

function buildPost(post)
{
    var titlePart = '<h1>'+post.Title+'</h1>';

    var authorPart = '<p class="lead">'+
                     'by <a href="user.html#'+post.AuthorId+'">'+post.AuthorUsername+'</a>'+
                     '</p>';
    var publishDatePart = '<p><span class="glyphicon glyphicon-time"></span> Posted on '+new Date(post.PublishDate).toDateString()+'</p>';
    var line = '<hr />';
    var contentPart = '<p class="lead">'+post.Content+'</p>';
    var post = '<div class="post">' + 
                titlePart + 
                authorPart + 
                publishDatePart + 
                line + 
                contentPart + 
                line + 
                '</div>';
    
    if(localStorage.getItem('UserId') !== null){
        var commentFormPart = '<div class="well">'+
                        '<h4>Leave a Comment:</h4>'+
                        '<form role="form">'+
                            '<div class="form-group">'+
                                '<textarea class="form-control" id="commentInput" rows="3"></textarea>'+
                            '</div>'+
                            '<button type="submit" onclick="submitComment()" class="btn btn-primary">Submit</button>'+
                        '</form>'+
                    '</div>';
        post += commentFormPart;
        post += line;
    }
    
    post += '<div id="comments"></div>';
    return post;
}